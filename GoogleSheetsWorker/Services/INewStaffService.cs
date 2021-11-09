using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleSheetsWorker.Services
{
    public interface INewStaffService
    {
        void AddNewStaffToSheetAsync(string name, DateTime date, string direction);

    }
}
