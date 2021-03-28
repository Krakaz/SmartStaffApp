using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartstaffApp.Models;

namespace SmartstaffApp.Pages
{
    public class TimePadEventsModel : PageModel
    {
        private readonly DataLoader.Timepad.Services.ITimePadEventService timePadEventService;

        public List<TimePadEventListVM> EventList { get; set; }

        public TimePadEventsModel(DataLoader.Timepad.Services.ITimePadEventService timePadEventService)
        {
            this.timePadEventService = timePadEventService;
        }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var eventList = await this.timePadEventService.GetEventListAsync(cancellationToken);
            this.EventList = eventList.values.Select(el => new TimePadEventListVM 
                { 
                    Date = DateTime.Parse(el.starts_at), 
                    Name = el.name, 
                    Url = el.url, 
                    City = el.location.city,
                    Address = el.location.address
                }).OrderBy(el => el.Date).ToList();
        }
    }
}