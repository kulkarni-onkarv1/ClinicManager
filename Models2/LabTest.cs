using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class LabTest
    {
        public LabTest()
        {
            PatientLabReports = new HashSet<PatientLabReport>();
            PatientRefLabTests = new HashSet<PatientRefLabTest>();
        }

        public string TestRn { get; set; } = null!;
        public string TestDescription { get; set; } = null!;
        public decimal TestCost { get; set; }

        public virtual ICollection<PatientLabReport> PatientLabReports { get; set; }
        public virtual ICollection<PatientRefLabTest> PatientRefLabTests { get; set; }
    }
}
