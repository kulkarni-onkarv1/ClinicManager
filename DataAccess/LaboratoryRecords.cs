using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_DoctorVolumeMiniProj.DataAccess;
using CS_DoctorVolumeMiniProj.Models2;
using Microsoft.Data.SqlClient;

namespace CS_DoctorVolumeMiniProj.DataAccess
{
    internal class LaboratoryRecords
    {
        static string TestRN;
        static string Choice = String.Empty;
        public static void PatientRefLabTests(int PatientID, string DoctorRegNo)
        {
            PatientDataManagement patientDataManagement = new PatientDataManagement();
            do
            {
                try
                {
                    Console.WriteLine("Enter Test Reference Number");
                    TestRN = Console.ReadLine();

                    Console.WriteLine("Creating new Record");
                    var PatientRefLabTestsNew = new PatientRefLabTest()
                    {
                        PatientId = PatientID,
                        DoctorRegNo = DoctorRegNo,
                        TestRn = TestRN
                    };
                    Console.WriteLine("Passing");
                    var result = patientDataManagement.CreateLabRecordForPatientAsync(PatientRefLabTestsNew);
                    if (result != null)
                    {
                        Console.WriteLine("Insertion Success!");
                        Console.WriteLine("Are You Refering More Tests? If Yes Then Enter Yes, Else Enter Any Key To Proceed Further");
                        Choice = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        Console.WriteLine("Insertion Failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (Choice == "yes");
           
        }

        public async Task<int> GetPatientReferedLabtestsAsync(int PatientID)
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
                sqlCommand = new SqlCommand($"select PatientRefLabTests.PatientID,TestDescription,RegDate From PatientRefLabTests inner join LabTests  on PatientRefLabTests.TestRN=LabTests.TestRN where PatientId={PatientID}", sqlConnection);

                sqlDataReader1 = await sqlCommand.ExecuteReaderAsync();
                while (sqlDataReader1.Read())
                {
                    res = 1;
                    Console.WriteLine($"PatientID: {sqlDataReader1["PatientID"]}  Refered Test: {sqlDataReader1["TestDescription"]} Refered Date: {sqlDataReader1["RegDate"]}");
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
    }
}
