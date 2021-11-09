using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleSheetsWorker.Services.Implementation
{
    internal class NewStaffService: INewStaffService
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "SmartStaffApp";
        static readonly string sheet = "Сотрудники";
        static readonly string SpreadsheetId = "1AEw58Y4-y22KGRyHekqlt3xFdxfAzxhlup3H6MUD5pE";
        static SheetsService service;

        static void Init()
        {
            GoogleCredential credential;
            //Reading Credentials File...
            using (var stream = new FileStream("client_secrests.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            // Creating Google Sheets API service...
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        static void AddRow(string name, string date, string direction)
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:C";
            var valueRange = new ValueRange();
            // Data for another Student...
            var oblist = new List<object>() { name, date, direction };
            valueRange.Values = new List<IList<object>> { oblist };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }

        public void AddNewStaffToSheetAsync(string name, DateTime date, string direction)
        {
            Init();
            AddRow(name, date.ToString("dd.MM.yyyy"), direction);
        }
    }
}
