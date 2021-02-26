using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartstaffApp.Models;
using SmartstaffApp.Services;

namespace SmartstaffApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;

        public StaffController(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService)
        {
            this.maketalentsService = maketalentsService;
        }

        /// <summary>
        /// Загружает информацию из МТ по сотрудникам
        /// </summary>
        [HttpPost("LoadAllStaff")]
        public async Task<IActionResult> LoadAllStaff(CancellationToken cancellationToken)
        {
            await this.maketalentsService.LoadNewStaffAsync(cancellationToken);
            var year = DateTime.Now.Year;
            await this.maketalentsService.UpdateFiredStaffAsync(year, cancellationToken);
            return Ok();
        }


        /// <summary>
        /// Загружает информацию из МТ по проведенным собесам
        /// </summary>
        [HttpPost("LoadIntervievInformation")]  
        public async Task<IActionResult> LoadIntervievInformation(CancellationToken cancellationToken)
        {            
            var year = DateTime.Now.Year;
            await this.maketalentsService.LoadIntervievInformationAsync(year, cancellationToken);
            return Ok();
        }
    }
}