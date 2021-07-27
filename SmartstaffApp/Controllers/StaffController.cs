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
        private readonly IApplicantsService applicantsService;

        public StaffController(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService,
            IApplicantsService applicantsService)
        {
            this.maketalentsService = maketalentsService;
            this.applicantsService = applicantsService;
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

        /// <summary>
        /// Загружает информацию из МТ по соискателям
        /// </summary>
        [HttpPost("LoadApplicants")]
        public async Task<IActionResult> LoadApplicants(CancellationToken cancellationToken)
        {
            await this.applicantsService.GetApplicantsListAsync(cancellationToken);
            return Ok();
        }

    }
}