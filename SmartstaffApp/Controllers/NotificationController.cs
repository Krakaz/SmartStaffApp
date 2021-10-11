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
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Отправляет нотификацию в ЛС MyTeam о дне рождения сегодня
        /// </summary>
        [HttpPost("SendBirthdayNotification")]
        public async Task<IActionResult> SendBirthdayNotification(CancellationToken cancellationToken)
        {
            await this.notificationService.SendBirthdayNotificationAsync(cancellationToken);            
            return Ok();
        }
    }
}