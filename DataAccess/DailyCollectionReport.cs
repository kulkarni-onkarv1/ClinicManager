using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_DoctorVolumeMiniProj.DataAccess
{
    internal class DailyCollectionReport
    {
        public async Task<int> GetDateWiseDailyCollectionReport(String RegDate)
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
                sqlCommand1 = new SqlCommand($"select PatientId,Diease,Description,RegDate,Fees from PatientDiease where FORMAT(RegDate,'yyyy-MM-dd')='{RegDate}';", sqlConnection);

                sqlDataReader = await sqlCommand1.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    res = 1;
                    Console.WriteLine($"Patient ID:{sqlDataReader["PatientId"]}    Diease:{sqlDataReader["Diease"]}  Description:{sqlDataReader["Description"]}   RegDate:{sqlDataReader["RegDate"]}   Fees Collected:{sqlDataReader["Fees"]} ");
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
    }
}
