using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Applikationen.DatabaseClasses
{
	// Section is made by Natasha, and modified by Keemon and Kasper
    public class Industry
    {
		// We define industry variables
		public int I_ID { get; set; }
		public string I_Name { get; set; }
		public string I_Code { get; set; }
		public string I_Description { get; set; }

		// We create a list to store the industries from the database in
		List<Industry> industries = new List<Industry>();

		// Method to get industries from database
		public List<Industry> GetIndustry()
		{

			// We open the connection to the database
			SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
			try
			{
				cnn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter();
				string sql = "SELECT * FROM \"Industries\"";

				using (SqlCommand command = new SqlCommand(sql, cnn))
				{
					var dataReader = command.ExecuteReader();

					// While it's reading the data we add it to a new object for each row of the Restrictions table
					while (dataReader.Read())
					{
						industries.Add(new Industry() { I_ID = Convert.ToInt32(dataReader.GetValue(0)), I_Name = Convert.ToString(dataReader.GetValue(1)), I_Code = Convert.ToString(dataReader.GetValue(2)), I_Description = Convert.ToString(dataReader.GetValue(3)) });
					}

					// We delete the command and close the connection
					command.Dispose();
					cnn.Close();

					return industries;
				}
			}
			catch (Exception e)
			{
				List<Industry> emptyList = new List<Industry>();
				MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
				return emptyList;
			}
			finally
			{
				if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
			}
		}
	}
}
