using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class PatientRefLabTest
    {
        public int UniqueId { get; set; }
        public int? PatientId { get; set; }
        public string? TestRn { get; set; }
        public string? DoctorRegNo { get; set; }
        public DateTime? RegDate { get; set; }

        public virtual Doctor? DoctorRegNoNavigation { get; set; }
        public virtual OutDoorPatient? Patient { get; set; }
        public virtual LabTest? TestRnNavigation { get; set; }
    }
}
