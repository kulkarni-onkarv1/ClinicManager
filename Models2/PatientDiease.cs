using System;
using System.Collections.Generic;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class PatientDiease
    {
        public int Unr { get; set; }
        public int? PatientId { get; set; }
        public string Diease { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? NextAppointmentDate { get; set; }
        public string? Hdl { get; set; }
        public string? Ldl { get; set; }
        public DateTime RegDate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Fees { get; set; }

        public virtual OutDoorPatient? Patient { get; set; }
    }
}
