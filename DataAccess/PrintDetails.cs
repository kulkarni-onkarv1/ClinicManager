using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_DoctorVolumeMiniProj.DataAccess
{
    internal class PrintDetails
    {

        public static void FetchingRecords()
        {
            string DoctoName = String.Empty;
            PatientDataManagement patientDataManagement = new PatientDataManagement();
            var patients = patientDataManagement.GetAsync().Result;


            try
            {
                SqlConnection sqlConnection = null;
                sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                SqlDataReader sqlDataReader = null;
                SqlDataReader sqlDataReader1 = null;
                Console.WriteLine("Patient ID    PatientName   Doctor Name     PatientAddress       PatientIDCardNumber         Reg Date And Time");
                foreach (var patient in patients)
                {
                    try
                    {

                        sqlCommand = new SqlCommand($"select DoctorName as DoctorName From Doctors Inner Join OutDoorPatients on OutDoorPatients.DoctorRegNo = Doctors.DoctorRegNo where OutDoorPatients.DoctorRegNo in (select DoctorRegNo From Doctors where DoctorRegNo = '{patient.DoctorRegNo}')", sqlConnection);

                        sqlDataReader = sqlCommand.ExecuteReader();

                        if (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{patient.PatientId} {patient.PatientName} {sqlDataReader["DoctorName"]}    {patient.PatientAddress}     {patient.PatientIdcardNumber}          {patient.RegDate}");
                        }
                        sqlDataReader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                //sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async Task<int>  FetchingRecordsAsync(int PatientID)
        {
            string DoctoName = String.Empty;
            int res = 0;
            PatientDataManagement patientDataManagement = new PatientDataManagement();
            var patient = patientDataManagement.GetByIdAsync(PatientID).Result;
            if (patient != null)
            {
                SqlConnection sqlConnection = null;
                sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                SqlDataReader sqlDataReader = null;
                //SqlDataReader sqlDataReader1 = null;
                

                try
                {

                    sqlCommand = new SqlCommand($"select DoctorName as DoctorName From Doctors Inner Join OutDoorPatients on OutDoorPatients.DoctorRegNo = Doctors.DoctorRegNo where OutDoorPatients.DoctorRegNo in (select DoctorRegNo From Doctors where DoctorRegNo = '{patient.DoctorRegNo}')", sqlConnection);

                    sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                   if (sqlDataReader.Read())
                    {
                        res = 1;
                        Console.WriteLine($"Patient ID: {patient.PatientId} PatientName : {patient.PatientName}  Doctor Name: {sqlDataReader["DoctorName"]}    PatientAddress: {patient.PatientAddress}     PatientIDCardNumber: {patient.PatientIdcardNumber}        Reg Date And Time: {patient.RegDate}");
                    }
                    sqlDataReader.Close();
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

            }
            return res;          
        }

        public async Task<int> FetchingRecordsByPatientNameAsync(String PatientName)
        {
            string DoctoName = String.Empty;
            int res = 0;
            PatientDataManagement patientDataManagement = new PatientDataManagement();
           
            
                SqlConnection sqlConnection = null;
                sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                SqlDataReader sqlDataReader = null;

            try
            {

                sqlCommand = new SqlCommand($"select * from OutDoorPatients where PatientName like '%{PatientName}%'", sqlConnection);
                //await sqlCommand.ExecuteNonQueryAsync();

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    res = 1;
                    Console.WriteLine($"Patient ID: {sqlDataReader["PatientId"]} PatientName: {sqlDataReader["PatientName"]} PatientAddress: {sqlDataReader["PatientAddress"]}    PatientIDCardNumber: {sqlDataReader["PatientIDCardNumber"]}      Reg Date And Time: {sqlDataReader["RegDate"]}");
                }
                    sqlDataReader.Close();
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

        public static void FetchingPatientMedicalRecords(int PatientID)
        {
            try
            {
                string DoctoName = String.Empty;
                /*PatientDataManagement patientDataManagement = new PatientDataManagement();
                var patient = patientDataManagement.GetPatientDieasesDiagonisis(PatientID).Result;
                if (patient.PatientId<=0)
                {*/
                    SqlConnection sqlConnection = null;
                    sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
                    sqlConnection.Open();
                    SqlCommand sqlCommand = null;
                    SqlDataReader sqlDataReader = null;

                    try
                    {
                        sqlCommand = new SqlCommand($"select PatientName as PatientName From OutDoorPatients Inner Join PatientDieases on OutDoorPatients.PatientID = PatientDieases.PatientID where OutDoorPatients.PatientID in (select PatientID From PatientDieases where PatientID = '')", sqlConnection);

                        sqlDataReader = sqlCommand.ExecuteReader();

                        if (sqlDataReader.Read())
                        {
                            //Console.WriteLine($"Patient ID:{patient.PatientId}    PatientName:{sqlDataReader["PatientName"]}  Disese:{patient.Diease} Description:{patient.Description} Next AppointMent Date:{patient.NextAppointmentDate}");
                        }
                        sqlDataReader.Close();
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



               /* }
                else
                {
                    Console.WriteLine("No Record Exists For Patient ID {0}", PatientID);
                }*/
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    
}
 

