using System.Text;
using WebApiTastForRareCrewCompany.Domain;

namespace WebApiTastForRareCrewCompany.HTML
{
    public static class HtmlGenerator
    {
        public static string GenerateEmployeeTable(List<Employee> employees)
        {
            StringBuilder htmlTable = new StringBuilder("<table border='1'>");

            htmlTable.AppendLine("<table border='1'>");
            htmlTable.AppendLine("<tr>");

            htmlTable.AppendLine("<th>Name</th>");

            htmlTable.AppendLine("<th>TotalTimeWorked</th>");
            htmlTable.AppendLine("</tr>");
            foreach (var employee in employees)
            {

                var totalHours = employee.GetTotalWorkTime().TotalHours;
                string rowStyle = totalHours < 100 ? "style='background-color: lightcoral;'" : "";

                htmlTable.AppendLine($"<tr {rowStyle}>");
                if (employee.EmployeeName == null)
                {

                }
                htmlTable.AppendLine($"<td>{employee.EmployeeName}</td>");

                /// prikazujemo gresku ako je total time manje ili jednako nula


                htmlTable.AppendLine($"<td>{totalHours}</td>");



                htmlTable.AppendLine("</tr>");



            }
            htmlTable.AppendLine("</table>");
            return htmlTable.ToString();
        }
    }
}
