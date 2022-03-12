using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class OutDoorPatient
    {
        public OutDoorPatient()
        {
            PatientDieases = new HashSet<PatientDiease>();
            PatientLabReports = new HashSet<PatientLabReport>();
            PatientPrescribedMedicines = new HashSet<PatientPrescribedMedicine>();
            PatientRefLabTests = new HashSet<PatientRefLabTest>();
        }

        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public string? DoctorRegNo { get; set; }
        public DateTime? RegDate { get; set; }
        public string PatientAddress { get; set; } = null!;
        public string PatientIdcardNumber { get; set; } = null!;

        public virtual Doctor? DoctorRegNoNavigation { get; set; }
        public virtual ICollection<PatientDiease> PatientDieases { get; set; }
        public virtual ICollection<PatientLabReport> PatientLabReports { get; set; }
        public virtual ICollection<PatientPrescribedMedicine> PatientPrescribedMedicines { get; set; }
        public virtual ICollection<PatientRefLabTest> PatientRefLabTests { get; set; }
    }
}
