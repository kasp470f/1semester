using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Applikationen.DatabaseClasses
{
    public class IndustryRestriction
    {
        // We define IndustryRestriction variables
        public int RI_ID { get; set; }
        public int RI_R_ID { get; set; }
        public int RI_M_ID { get; set; }
        public int RI_I_ID { get; set; }
        public string RI_Text { get; set; }
        public DateTime RI_StartDate { get; set; }
        public DateTime RI_EndDate { get; set; }

        // Industry variables
        public string I_Name { get; set; }
        public string I_Code { get; set; }
        public string I_Description { get; set; }

        // Restriction variables
        public string R_Text { get; set; }

        // Natasha
		// Method to get industries from database
		public void DeleteIndustryRestriction(List<IndustryRestriction> list)
		{
			// We open the connection to the database
			SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
			try
			{
				cnn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter();
                foreach (IndustryRestriction item in list)
                {
                    string sql = "DELETE FROM IndustriesRestrictions WHERE RI_ID = " + item.RI_ID;

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }
                }
                cnn.Close();

                MessageBox.Show("De(n) valgte restriktion(er) på industri(er) er nu fjernet.");
            }
			catch (Exception e)
			{
				MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
			}
			finally
			{
				if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
			}
        }
	}
}
