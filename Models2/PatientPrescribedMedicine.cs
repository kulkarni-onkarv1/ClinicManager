using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class PatientPrescribedMedicine
    {
        public int Urn { get; set; }
        public string? MedicineName { get; set; }
        public int? PatientId { get; set; }

        public virtual OutDoorPatient? Patient { get; set; }
    }
}
