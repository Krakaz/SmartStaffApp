using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataLoader.MyTeam.Services;
using DataLoader.Pozdravlala.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartstaffApp.Services;

namespace SmartstaffApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IStaffService staffService;
        private readonly IMessageService messageService;
        private readonly DataLoader.Pozdravlala.Services.IPozdravlalaService pozdravlalaService;

        public NotificationController(IStaffService staffService, 
            IMessageService messageService,
            DataLoader.Pozdravlala.Services.IPozdravlalaService pozdravlalaService)
        {
            this.staffService = staffService;
            this.messageService = messageService;
            this.pozdravlalaService = pozdravlalaService;
        }

        /// <summary>
        /// Отправляет нотификацию в ЛС MyTeam о дне рождения сегодня
        /// </summary>
        [HttpPost("SendBirthdayNotification")]
        public async Task<IActionResult> SendBirthdayNotification(CancellationToken cancellationToken)
        {
            var staffList = await this.staffService.GetStaffAsync(cancellationToken);
            var staffs = staffList.Where(el => el.IsActive)
                .Where(el => el.Birthday?.Month == DateTime.Now.Month && el.Birthday?.Day == DateTime.Now.Day)
                .Select(el => new
                {
                    Birthday = el.Birthday.Value,
                    FullName = el.FullName,
                    Direction = el.Direction,
                    Age = DateTime.Now.Year - el.Birthday.Value.Year,
                    Sex = el.Female.Value ? 1 : 0,
                    Email = el.Email,
            })
                .ToList();

            foreach(var staff in staffs)
            {
                var request = new CongratulationRequest
                {
                    Appeal = AppealEnum.You,
                    Type = CongratulationTypeEnum.Birthday,
                    Sex = (SexEnum) staff.Sex,
                    Length = ContgatulationLengthEnum.Middle,
                };
                var Congratulations = await this.pozdravlalaService.GetCongratulationAsync(request, cancellationToken);
                var age = staff.Sex == 0 ? staff.Age.ToString() : "";
                // Склонение месяца
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(staff.Birthday.Month);
                if (month[month.Length - 1] == 'ь' || month[month.Length - 1] == 'й')
                {
                    month = month.Substring(0, month.Length - 1) + "я";
                }
                else
                {
                    month = month + "а";
                }
                //-----------------

                var msg = $"{staff.Birthday.Day} {month} сотрудник @[{staff.Email}] из направления {staff.Direction} отмечает свой {age} день рождения. {Environment.NewLine}{Congratulations}";
                await this.messageService.SendMessage("denis.kuznetcov@simbirsoft.com", msg, cancellationToken);
            }
            
            return Ok();
        }
    }
}