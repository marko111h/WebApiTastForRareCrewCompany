using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiTastForRareCrewCompany.Domain;
using WebApiTastForRareCrewCompany.HTML;

namespace WebApiTastForRareCrewCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
      
        public async Task<IActionResult> GetTimeEntries()
        {
            try
            {
                var kljuc = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

                // Pravljenje HTTP klijenta i dodavanje ključa u URL
                var client = _httpClientFactory.CreateClient("ReareCrewApi");
                var urlWithCode = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=" + kljuc;
                client.BaseAddress = new Uri(urlWithCode);
                
                var response = await client.GetAsync(urlWithCode);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<EmployeeShiftDTO>>(jsonResponse);
                    var shifts = model.Select(x => new EmployeeShift(x.Id, x.EmployeeName, x.StarTimeUtc, x.EndTimeUtc, x.EntryNotes, x.DeletedOn)).ToList();
                    var shiftPerEmployee = shifts.GroupBy(x => x.EmployeeName);

                    var employees = new List<Employee>();

                    foreach (var employeeShift in shiftPerEmployee)
                    {
                        var employee = new Employee(employeeShift.Key);
                        employee.AddShifts(employeeShift.ToList());
                        employees.Add(employee);
                    }

                    string htmlTable = HtmlGenerator.GenerateEmployeeTable(employees);

                    return Content(htmlTable, "text/html");
                }

                return BadRequest("Error fetching data from API");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
