﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Models;
using SmartstaffApp.Services;
using System.Text.Json;
using SmartstaffApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartstaffApp.Pages
{
    /// <summary>
    /// Текущие данные по сотрудникам
    /// </summary>
    public class RatingModel : PageModel
    {
        public IList<SelectListItem> BranchesFilter { get; set; }

        public IList<StaffTurnoverByMonth> StaffTurnoverByMonths { get; set; }

        public TotalGrowByMonthAndDirection TotalGrowByMonthAndDirection { get; set; }

        public IList<ShortActiveStaffVM> ShortActiveStaffs { get; set; }

        public IList<string> UsersLooseInChat { get; set; } = new List<string>();
        public IList<string> UsersLooseInChanal { get; set; } = new List<string>();

        public RatingFilter Filter { get; set; } = new RatingFilter();
        public string JsonStaffChart { get; set; }

        private readonly ILogger<RatingModel> _logger;
        private readonly IStaffService staffService;
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;
        private readonly Repo.Services.IGroupService groupService;
        private readonly DataLoader.MyTeam.Services.IChatService chatService;

        public RatingModel(ILogger<RatingModel> logger, 
            IStaffService staffService, 
            DataLoader.Maketalents.Services.IMaketalentsService maketalentsService,
            Repo.Services.IGroupService groupService,
            DataLoader.MyTeam.Services.IChatService chatService)
        {
            _logger = logger;
            this.staffService = staffService;
            this.maketalentsService = maketalentsService;
            this.groupService = groupService;
            this.chatService = chatService;
        }


        public async Task OnGet(RatingFilter filter, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.QueryString.ToString()))
            {
                var sessionFilter = HttpContext.Session.Get<RatingFilter>("RatingFilter");
                if (sessionFilter != null)
                {
                    filter = sessionFilter;
                }
            }

            await this.FillPageData(filter, cancellationToken);
            this.Filter = filter;

            HttpContext.Session.Set<RatingFilter>("RatingFilter", filter);
        }

        private async Task FillPageData(RatingFilter filter, CancellationToken cancellationToken)
        {
            this.StaffTurnoverByMonths = await this.staffService.GetStaffTurnoverByMonth(filter.Year, cancellationToken);
            this.TotalGrowByMonthAndDirection = filter.BranchId == 0 ? await this.staffService.GetTotalGrowByMonthAndDirectionAsync(filter.Year, cancellationToken) : await this.staffService.GetTotalGrowByMonthDirectionBranchAsync(filter.Year, filter.BranchId, cancellationToken);


            this.BranchesFilter = (await this.groupService.GetBranchesAsync(cancellationToken)).Select(el => new SelectListItem { Value = el.Id.ToString(), Text = el.Name }).OrderBy(el => el.Text).ToList();
            this.BranchesFilter.Insert(0, new SelectListItem { Value = "0", Text = "Все" });


            var staff = filter.BranchId == 0 ? await this.staffService.GetStaffAsync(cancellationToken) : await this.staffService.GetStaffByBranchIdAsync(filter.BranchId, cancellationToken);
            this.ShortActiveStaffs = staff.Where(el => el.IsActive).GroupBy(el => el.Direction).Select(cl => new ShortActiveStaffVM
            {
                DirectionId = cl.First().DirectionId,
                DirectionName = cl.First().Direction,
                IsTarget = cl.First().IsTargetDirection,
                HasRO = cl.First().DirectionHasRO,
                StaffCount = cl.Count()
            }).OrderByDescending(el => el.StaffCount).ToList();

            var chatMembers = await this.chatService.GetMainChatMembersAsync(cancellationToken);
            if (chatMembers.ok)
            {
                this.UsersLooseInChat = staff.Where(el => el.IsActive == true && !chatMembers.members.Any(m => m.userId == el.Email)).Select(el => el.Direction + ": " + el.FullName).ToList();
                
                // Костыль увольнения пользоватетей
                foreach(var user in staff.Where(el => el.IsActive == true && !chatMembers.members.Any(m => m.userId == el.Email) && el.FirstWorkingDate <= DateTime.Now.Date.AddDays(-14)))
                {

                }
            }
            var chanalMembers = await this.chatService.GetMainChanalMembersAsync(cancellationToken);
            if (chanalMembers.ok)
            {
                this.UsersLooseInChanal = staff.Where(el => el.IsActive == true && !chanalMembers.members.Any(m => m.userId == el.Email)).Select(el => el.Direction + ": " + el.FullName).ToList();
            }
        }
    }

    /// <summary>
    /// Фильтр
    /// </summary>
    public class RatingFilter
    {
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Идентификатор филиала
        /// </summary>
        public int BranchId { get; set; }
    }
}