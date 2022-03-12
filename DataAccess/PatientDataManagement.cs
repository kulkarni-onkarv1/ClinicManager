using CS_DoctorVolumeMiniProj.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_DoctorVolumeMiniProj;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CS_DoctorVolumeMiniProj.DataAccess
{
    internal class PatientDataManagement
    {
        DoctorVolumeContext doctorVolumeContext;
        PatientRefLabTest patientRefLabTest;
        public PatientDataManagement()
        {
            doctorVolumeContext = new DoctorVolumeContext();
            patientRefLabTest = new PatientRefLabTest();

        }
        public async Task<OutDoorPatient> CreateAsync(OutDoorPatient entity)
        {
            var result = await doctorVolumeContext.OutDoorPatients.AddAsync(entity);
            await doctorVolumeContext.SaveChangesAsync();
            return result.Entity; // Return newly CReated ENtity
        }

        public async Task<IEnumerable<OutDoorPatient>> GetAsync()
        {
            var result = await doctorVolumeContext.OutDoorPatients.ToListAsync();
            return result;
        }

        public async Task<OutDoorPatient> GetByIdAsync(int PatientID)
        {
            var deptToFind = await doctorVolumeContext.OutDoorPatients.FindAsync(PatientID);
            if (deptToFind == null)
            {
                return null;
            }
            return deptToFind;
        }

        public async Task<int> GetPatientDieasesDiagonisis(int PatientID)
        {
            int res = 0;
            /*var deptToFind = await doctorVolumeContext.PatientDieases.FindAsync(6);
            if (deptToFind == null)
            {
                return null;
            }
            return deptToFind;*/
            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
            sqlConnection.Open();
            //SqlCommand sqlCommand = null;
            SqlCommand sqlCommand1 = null;
            SqlDataReader sqlDataReader = null;
            //SqlDataReader sqlDataReader1=null;

            try
            {
                //sqlCommand = new SqlCommand($"select TestRN From PatientRefLabTests where PatientId={PatientID}", sqlConnection);
                sqlCommand1 = new SqlCommand($"select PatientId,Diease,Description,HDL,LDL,Weight,RegDate,NextAppointmentDate from PatientDiease where PatientID={PatientID}", sqlConnection);

                sqlDataReader = await sqlCommand1.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    res = 1;
                    Console.WriteLine($"Patient ID:{sqlDataReader["PatientId"]}    Diease:{sqlDataReader["Diease"]}  Description:{sqlDataReader["Description"]}    HDL:{sqlDataReader["HDL"]}   LDL:{sqlDataReader["LDL"]}   Weight:{sqlDataReader["Weight"]}   RegDate:{sqlDataReader["RegDate"]}  Next AppointMent Date:{sqlDataReader["NextAppointmentDate"]}");
                }
                sqlDataReader.Close();

                //sqlDataReader1 = await sqlCommand.ExecuteReaderAsync();
/*
                while (sqlDataReader1.Read())
                {
                    Console.WriteLine($"Patient Refered Lab Test: {sqlDataReader1["TestRN"]}"); 
                }

                sqlDataReader1.Close();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            //sqlDataReader.Close();
            return res;
        }


        public int GetPatientPrescribedMedicinesDiary(int PatientID)
        {
            int res = 0;
            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
            sqlConnection.Open();
            SqlCommand sqlCommand = null;
            SqlCommand sqlCommand1 = null;
            SqlDataReader sqlDataReader1 = null;

            try
            {
                sqlCommand = new SqlCommand($"select PatientID,MedicineName From PatientPrescribedMedicines where PatientId={PatientID}", sqlConnection);

                sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    res = 1;
                    Console.WriteLine($"PatientID: {sqlDataReader1["PatientID"]}  Medicine Name: {sqlDataReader1["MedicineName"]}");
                }

                sqlDataReader1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            //sqlDataReader.Close();
            return res;
        }

        public async Task<PatientRefLabTest> CreateLabRecordForPatientAsync(PatientRefLabTest entity)
        {
            var result = await doctorVolumeContext.PatientRefLabTests.AddAsync(entity);
            await doctorVolumeContext.SaveChangesAsync();
            return result.Entity; // Return newly CReated ENtity
        }
    }
}
