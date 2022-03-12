using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_DoctorVolumeMiniProj.DataAccess;
using CS_DoctorVolumeMiniProj.Models2;

namespace CS_DoctorVolumeMiniProj.DataAccess.NewFolder
{
    internal class PatientDieasesRecord
    {
        DoctorVolumeContext doctorVolumeContext;
        PatientDiease patientDiease;
        public PatientDieasesRecord()
        {
            doctorVolumeContext = new DoctorVolumeContext();
            patientDiease = new PatientDiease();
        }
        public async Task<PatientDiease> patientDieasesRecordInsertionAsync(PatientDiease entity)
        {
            var result = await doctorVolumeContext.PatientDieases.AddAsync(entity);
            await doctorVolumeContext.SaveChangesAsync();
            return result.Entity; // Return newly CReated ENtity
        }

        public async Task<PatientPrescribedMedicine> patientMedicinessRecordInsertionAsync(PatientPrescribedMedicine entity)
        {
            var result = await doctorVolumeContext.PatientPrescribedMedicines.AddAsync(entity);
            await doctorVolumeContext.SaveChangesAsync();
            return result.Entity; // Return newly CReated ENtity
        }

    }
}
