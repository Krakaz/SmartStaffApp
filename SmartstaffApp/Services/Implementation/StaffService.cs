using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Repo.Models;
using SmartstaffApp.Models;
using SmartstaffApp.Pages;

namespace SmartstaffApp.Services.Implementation
{
    internal class StaffService : IStaffService
    {
        private readonly Repo.Services.IStaffService repoStaffService;
        private readonly Repo.Services.IInterviewService interviewService;
        private readonly Repo.Services.IPositionService positionService;

        public StaffService(
            Repo.Services.IStaffService repoStaffService,
            Repo.Services.IInterviewService interviewService,
            Repo.Services.IPositionService positionService)
        {
            this.repoStaffService = repoStaffService;
            this.interviewService = interviewService;
            this.positionService = positionService;
        }

        public async Task<CurrentDataViewModel> GetCurrentDataAsync(CancellationToken cancellationToken)
        {
            var result = new CurrentDataViewModel();

            var staffs = await this.repoStaffService.GetActiveAsync(cancellationToken);
            result.CurrentCount = staffs.Count();
            result.FirstTargetCount = 101;
            result.SecondTargetCount = 120;
            result.YearTargetCount = 120;

            return result;
        }

        public async Task<IList<InformationByMonth>> GetInformationByMonthAsync(int year, CancellationToken cancellationToken)
        {
            var result = new List<InformationByMonth>();

            var interviews = await this.interviewService.GetByYear(year, cancellationToken);
            var staffs = await this.repoStaffService.GetAllAsync(cancellationToken);

            for (int month = 1; month <= 12; month++)
            {
                var info = new InformationByMonth() { Month = month };
                info.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                info.InterviewCnt = interviews.Where(el => el.Month == (Repo.Models.Month)info.Month).Sum(el => el.InterviewCount);
                info.IncomingCnt = staffs.Where(el => el.FirstWorkingDate.Month == month && el.FirstWorkingDate.Year == year && !el.IsArived).Count();
                info.FiredCnt = staffs.Where(el => el.NotActiveDate?.Month == month && el.NotActiveDate?.Year == year).Count();
                info.ArivedCnt = staffs.Where(el => el.ArivedDate?.Month == month && el.ArivedDate?.Year == year).Count();

                if (info.InterviewCnt != 0 || info.IncomingCnt != 0 || info.FiredCnt != 0 || info.ArivedCnt != 0)
                {
                    result.Add(info);
                }
            }

            var resultInfo = new InformationByMonth()
            {
                Month = 13,
                MonthName = "Итого",
                IncomingCnt = result.Sum(el => el.IncomingCnt),
                ArivedCnt = result.Sum(el => el.ArivedCnt),
                FiredCnt = result.Sum(el => el.FiredCnt),
                InterviewCnt = result.Sum(el => el.InterviewCnt),
            };

            result.Add(resultInfo);

            return result.OrderBy(el => el.Month).ToList();
        }

        public async Task<IList<DetailInformationByMonth>> GetDetailInformationByMonthAsync(InterviewFilter filter, CancellationToken cancellationToken)
        {
            var result = new List<DetailInformationByMonth>();

            var interviews = await this.interviewService.GetByYear(filter.Year, cancellationToken);
            var staffs = await this.repoStaffService.GetAllAsync(cancellationToken);
            var positions = await this.positionService.GetAllAsync(cancellationToken);

            for (int month = 1; month <= 12; month++)
            {

                var resultMonthInfo = new DetailInformationByMonth()
                {
                    Month = month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                };                

                foreach (var pposition in positions.Where(el=> el.Childs.Count != 0).OrderBy(el => el.Name))
                {
                    if(filter.DirectionId != 0 && pposition.Id != filter.DirectionId)
                    {
                        continue;
                    }

                    if (!filter.IsShort)
                    {
                        foreach (var position in pposition.Childs.OrderBy(el => el.Name))
                        {
                            var info = new DetailInformationByMonth() { Month = month, ParentPosition = pposition.Name, Position = position.Name };
                            info.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                            info.InterviewCnt = interviews.Where(el => el.Month == (Repo.Models.Month)info.Month && el.PositionName == position.Name).Sum(el => el.InterviewCount);
                            info.IncomingCnt = staffs.Where(el => el.FirstWorkingDate.Month == month && el.FirstWorkingDate.Year == filter.Year && el.Positions.Any(pos => pos.Id == position.Id) && !el.IsArived).Count();
                            info.FiredCnt = staffs.Where(el => el.NotActiveDate?.Month == month && el.NotActiveDate?.Year == filter.Year && el.Positions.Any(pos => pos.Id == position.Id)).Count();
                            info.ArivedCnt = staffs.Where(el => el.ArivedDate?.Month == month && el.ArivedDate?.Year == filter.Year && el.Positions.Any(pos => pos.Id == position.Id)).Count();
                            if (filter.IsSignificant && (info.IncomingCnt == 0 && info.InterviewCnt == 0 && info.ArivedCnt == 0 && info.FiredCnt == 0))
                            {
                                continue;
                            }
                            resultMonthInfo.Childs.Add(info);
                        }
                    }
                    else
                    {
                        var info = new DetailInformationByMonth() { Month = month, ParentPosition = pposition.Name, Position = "" };
                        info.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                        info.InterviewCnt = interviews.Where(el => el.Month == (Repo.Models.Month)info.Month && pposition.Childs.Any(pos => pos.Name == el.PositionName)).Sum(el => el.InterviewCount);
                        info.IncomingCnt = staffs.Where(el => el.FirstWorkingDate.Month == month && el.FirstWorkingDate.Year == filter.Year && el.Positions.Any(pos => pposition.Childs.Any(cpos => cpos.Id == pos.Id)) && !el.IsArived).Count();
                        info.FiredCnt = staffs.Where(el => el.NotActiveDate?.Month == month && el.NotActiveDate?.Year == filter.Year && el.Positions.Any(pos => pposition.Childs.Any(cpos => cpos.Id == pos.Id))).Count();
                        info.ArivedCnt = staffs.Where(el => el.ArivedDate?.Month == month && el.ArivedDate?.Year == filter.Year && el.Positions.Any(pos => pposition.Childs.Any(cpos => cpos.Id == pos.Id))).Count();
                        if (filter.IsSignificant && (info.IncomingCnt == 0 && info.InterviewCnt == 0 && info.ArivedCnt == 0 && info.FiredCnt == 0))
                        {
                            continue;
                        }
                        resultMonthInfo.Childs.Add(info);
                    }
                }

                if (filter.IsSignificant && (resultMonthInfo.IncomingCnt == 0 && resultMonthInfo.InterviewCnt == 0 && resultMonthInfo.ArivedCnt == 0 && resultMonthInfo.FiredCnt == 0))
                {
                    continue;
                }

                result.Add(resultMonthInfo);
            }

            var resultInfo = new DetailInformationByMonth()
            {
                Month = 13,
                MonthName = "Итого",
                IncomingCnt = result.Where(el => string.IsNullOrEmpty(el.Position)).Sum(el => el.IncomingCnt),
                ArivedCnt = result.Where(el => string.IsNullOrEmpty(el.Position)).Sum(el => el.ArivedCnt),
                FiredCnt = result.Where(el => string.IsNullOrEmpty(el.Position)).Sum(el => el.FiredCnt),
                InterviewCnt = result.Where(el => string.IsNullOrEmpty(el.Position)).Sum(el => el.InterviewCnt),
            };

            result.Add(resultInfo);

            return result;
        }

        public async Task<IList<StaffVM>> GetStaffAsync(CancellationToken cancellationToken)
        {
            var result = new List<StaffVM>();
            var staffs = await this.repoStaffService.GetAllAsync(cancellationToken);
            var positions = await this.positionService.GetAllAsync(cancellationToken);
            result = staffs.Select(el => new StaffVM
            {
                Id = el.Id,
                ArivedDate = el.ArivedDate,
                Birthday = el.Birthday,
                FirstName = el.FirstName,
                LastName = el.LastName,
                MiddleName = el.MiddleName,
                FullName = el.FullName,
                Email = el.Email,
                Skype = el.Skype,
                Phones = el.Phones,
                Female = el.Female,
                FirstWorkingDate = el.FirstWorkingDate,
                IsActive = el.IsActive,
                IsArived = el.IsArived,
                NotActiveDate = el.NotActiveDate,
                Groups = string.Join(", ", el.Groups),
                PositionId = (int)el.Positions.FirstOrDefault()?.Id,
                Position = el.Positions.FirstOrDefault()?.Name,
                Direction = positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Name,
                DirectionId = (int)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Id,
                IsTargetDirection = (bool)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.IsTarget,
                DirectionHasRO = (bool)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.HasRO,
            }).ToList();

            return result;
        }

        public Task<IList<Repo.Models.Position>> GetPositionsAsync(CancellationToken cancellationToken)
        {
            return this.positionService.GetAllAsync(cancellationToken);
        }

        public async Task<TotalGrowByMonthAndDirection> GetTotalGrowByMonthAndDirectionAsync(int year, CancellationToken cancellationToken)
        {
            var result = new TotalGrowByMonthAndDirection();

            var positions = await this.positionService.GetAllAsync(cancellationToken);
            foreach(var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
            {
                result.Header.Add(new DirectionVM { Id = direction.Id, Name = direction.Name });
            }

            var staffs = await this.repoStaffService.GetAllAsync(cancellationToken);

            var lastMonth = (DateTime.Now.Year == year) ? DateTime.Now.Month : 12;

            for (int month = 1; month <= lastMonth; month++)
            {
                var monthInfo = new TotalGrowByMonthAndDirectionValues()
                {
                    Month = month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                };

                foreach (var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
                {
                    var totalGrowByDirection = new TotalGrowByDirection();
                    totalGrowByDirection.DirectionId = direction.Id;
                    var shortList = staffs.Where(el => el.Positions.Any(pos => direction.Childs.Any(cpos => cpos.Id == pos.Id))).Distinct();

                    totalGrowByDirection.TotalGrowCount = 
                        shortList.Where(el => el.FirstWorkingDate.Month == month && el.FirstWorkingDate.Year == year && !el.IsArived).Count() -
                        shortList.Where(el => el.NotActiveDate?.Month == month && el.NotActiveDate?.Year == year).Count() +
                        shortList.Where(el => el.ArivedDate?.Month == month && el.ArivedDate?.Year == year).Count();
                    monthInfo.TotalGrowByDirection.Add(totalGrowByDirection);
                }

                result.Values.Add(monthInfo);
            }
            var totalMonthInfo = new TotalGrowByMonthAndDirectionValues()
            {
                Month = 13,
                MonthName = "Итого",
            };

            foreach (var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
            {
                var totalGrowByDirection = new TotalGrowByDirection();
                totalGrowByDirection.DirectionId = direction.Id;
                totalGrowByDirection.TotalGrowCount = result.Values.Sum(el => el.TotalGrowByDirection.Where(x => x.DirectionId == direction.Id).Sum(y => y.TotalGrowCount));
                totalMonthInfo.TotalGrowByDirection.Add(totalGrowByDirection);
            }

            result.Values.Add(totalMonthInfo);


            return result;
        }

        public async Task<IList<StaffVM>> GetStaffByBranchIdAsync(int branchId, CancellationToken cancellationToken)
        {
            var result = new List<StaffVM>();
            var staffs = await this.repoStaffService.GetAllByBranchIdAsync(branchId, cancellationToken);
            var positions = await this.positionService.GetAllAsync(cancellationToken);
            result = staffs.Select(el => new StaffVM
            {
                Id = el.Id,
                ArivedDate = el.ArivedDate,
                Birthday = el.Birthday,
                FirstName = el.FirstName,
                LastName = el.LastName,
                MiddleName = el.MiddleName,
                FullName = el.FullName,
                Email = el.Email,
                Skype = el.Skype,
                Phones = el.Phones,
                Female = el.Female,
                FirstWorkingDate = el.FirstWorkingDate,
                IsActive = el.IsActive,
                IsArived = el.IsArived,
                NotActiveDate = el.NotActiveDate,
                Groups = string.Join(", ", el.Groups),
                PositionId = (int)el.Positions.FirstOrDefault()?.Id,
                Position = el.Positions.FirstOrDefault()?.Name,
                Direction = positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Name,
                DirectionId = (int)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Id,
                IsTargetDirection = (bool)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.IsTarget,
                DirectionHasRO = (bool)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.HasRO,
            }).ToList();

            return result;
        }

        public async Task<TotalGrowByMonthAndDirection> GetTotalGrowByMonthDirectionBranchAsync(int year, int branchId, CancellationToken cancellationToken)
        {
            var result = new TotalGrowByMonthAndDirection();

            var positions = await this.positionService.GetAllAsync(cancellationToken);
            foreach (var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
            {
                result.Header.Add(new DirectionVM { Id = direction.Id, Name = direction.Name });
            }

            var staffs = await this.repoStaffService.GetAllByBranchIdAsync(branchId, cancellationToken);

            var lastMonth = (DateTime.Now.Year == year) ? DateTime.Now.Month : 12;

            for (int month = 1; month <= lastMonth; month++)
            {
                var monthInfo = new TotalGrowByMonthAndDirectionValues()
                {
                    Month = month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                };

                foreach (var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
                {
                    var totalGrowByDirection = new TotalGrowByDirection();
                    totalGrowByDirection.DirectionId = direction.Id;
                    var shortList = staffs.Where(el => el.Positions.Any(pos => direction.Childs.Any(cpos => cpos.Id == pos.Id)));

                    totalGrowByDirection.TotalGrowCount =
                        shortList.Where(el => el.FirstWorkingDate.Month == month && el.FirstWorkingDate.Year == year && !el.IsArived).Count() -
                        shortList.Where(el => el.NotActiveDate?.Month == month && el.NotActiveDate?.Year == year).Count() +
                        shortList.Where(el => el.ArivedDate?.Month == month && el.ArivedDate?.Year == year).Count();
                    monthInfo.TotalGrowByDirection.Add(totalGrowByDirection);
                }

                result.Values.Add(monthInfo);
            }
            var totalMonthInfo = new TotalGrowByMonthAndDirectionValues()
            {
                Month = 13,
                MonthName = "Итого",
            };

            foreach (var direction in positions.Where(el => el.Childs.Count != 0).OrderBy(el => el.Name))
            {
                var totalGrowByDirection = new TotalGrowByDirection();
                totalGrowByDirection.DirectionId = direction.Id;
                totalGrowByDirection.TotalGrowCount = result.Values.Sum(el => el.TotalGrowByDirection.Where(x => x.DirectionId == direction.Id).Sum(y => y.TotalGrowCount));
                totalMonthInfo.TotalGrowByDirection.Add(totalGrowByDirection);
            }

            result.Values.Add(totalMonthInfo);


            return result;
        }
    }
}
