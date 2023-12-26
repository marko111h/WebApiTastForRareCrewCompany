namespace WebApiTastForRareCrewCompany.Domain
{
    public class EmployeeShift
    {

        private string _employeeName;

        public EmployeeShift(Guid id, string employeeName, DateTime starTimeUtc, DateTime endTimeUtc, string entryNotes, DateTime? deletedOn)
        {

            Id = id;
            EmployeeName = employeeName;
            StarTimeUtc = starTimeUtc;
            EndTimeUtc = endTimeUtc;
            EntryNotes = entryNotes;
            DeletedOn = deletedOn;
        }

        public Guid Id { get; set; }
        public string EmployeeName
        {
            get { return _employeeName ?? "name is unknown"; }
            set { _employeeName = value; }
        }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }

        public DateTime? DeletedOn { get; set; } /// sluzi da znamo u kom trenutku je obrisan

        /// ukupno vreme rada
        public TimeSpan TimeWorked
        {
            get
            {
                if (DeletedOn == null && StarTimeUtc <= EndTimeUtc)
                {
                    var a = EndTimeUtc - StarTimeUtc;
                    return a;
                }
                return TimeSpan.Zero;
            }
        }



    }
}
