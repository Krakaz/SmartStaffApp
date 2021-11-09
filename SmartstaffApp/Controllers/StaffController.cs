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
        private readonly Business.Services.IInterviewInformationService interviewInformationService;
        private readonly IApplicantsService applicantsService;
        private readonly Business.Services.IStaffService staffService;

        public StaffController(Business.Services.IInterviewInformationService interviewInformationService,
            IApplicantsService applicantsService,
            Business.Services.IStaffService staffService)
        {
            this.interviewInformationService = interviewInformationService;
            this.applicantsService = applicantsService;
            this.staffService = staffService;
        }

        /// <summary>
        /// Загружает информацию из МТ по сотрудникам
        /// </summary>
        [HttpPost("LoadAllStaff")]
        public async Task<IActionResult> LoadAllStaff(CancellationToken cancellationToken)
        {
            await this.staffService.UpsertNewStaffAsync(cancellationToken);
            return Ok();
        }


        /// <summary>
        /// Загружает информацию из МТ по проведенным собесам
        /// </summary>
        [HttpPost("LoadIntervievInformation")]  
        public async Task<IActionResult> LoadIntervievInformation(CancellationToken cancellationToken)
        {            
            var year = DateTime.Now.Year;
            await this.interviewInformationService.LoadIntervievInformationAsync(year, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Загружает информацию из МТ по соискателям
        /// </summary>
        [HttpPost("LoadApplicants")]
        public async Task<IActionResult> LoadApplicants(CancellationToken cancellationToken)
        {
            await this.applicantsService.LoafApplicantsAsync(cancellationToken);
            return Ok();
        }

    }
}