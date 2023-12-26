namespace WebApiTastForRareCrewCompany.Domain
{
    public class Employee
    {
        public string EmployeeName { get; private set; }
        private IList<EmployeeShift> Shifts { get; }
        public Employee(string employeeName)
        {
            Shifts = new List<EmployeeShift>();
            EmployeeName = employeeName;
        }
        public TimeSpan GetTotalWorkTime()
        {
            return TimeSpan.FromTicks(Shifts.Sum(x => x.TimeWorked.Ticks));
        }
        public void AddShift(EmployeeShift shift)
        {
            Shifts.Add(shift);
        }
        public void AddShifts(IEnumerable<EmployeeShift> shifts)
        {

            foreach (var shift in shifts)
            {
                Shifts.Add(shift);
            }
        }
    }
}
