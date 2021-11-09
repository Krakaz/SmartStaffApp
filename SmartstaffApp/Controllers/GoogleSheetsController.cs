using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoogleSheetsWorker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartstaffApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleSheetsController : ControllerBase
    {
        private readonly INewStaffService newStaffService;

        public GoogleSheetsController(INewStaffService newStaffService)
        {
            this.newStaffService = newStaffService;
        }
        /// <summary>
        /// Добавляет строчку в файл по выдаче WelcomeBox
        /// </summary>
        [HttpPost("AddTestRowToSheet")]
        public async Task<IActionResult> SendBirthdayNotification(CancellationToken cancellationToken)
        {
            this.newStaffService.AddNewStaffToSheetAsync("Тестовый Тест Тестович", new DateTime(2022, 1, 1), "Тест");
            return Ok();
        }
    }
}