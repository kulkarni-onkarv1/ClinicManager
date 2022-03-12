using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class Doctor
    {
        public Doctor()
        {
            OutDoorPatients = new HashSet<OutDoorPatient>();
            PatientLabReports = new HashSet<PatientLabReport>();
            PatientRefLabTests = new HashSet<PatientRefLabTest>();
        }

        public string DoctorRegNo { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public string DoctorDegree { get; set; } = null!;
        public int FirstVisitFees { get; set; }
        public int SecondVisitFees { get; set; }
        public DateTime? RegDate { get; set; }

        public virtual ICollection<OutDoorPatient> OutDoorPatients { get; set; }
        public virtual ICollection<PatientLabReport> PatientLabReports { get; set; }
        public virtual ICollection<PatientRefLabTest> PatientRefLabTests { get; set; }
    }
}
