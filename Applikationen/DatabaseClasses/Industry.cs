using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		// Method to get industries from database
		public void GetIndustry()
		{
			// We create a list to store the industries from the database in
			List<object> industries = new List<object>();

			// We open the connection to the database
			SqlConnection cnn;
			cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
			cnn.Open();

			// We create the command and execute it
			SqlCommand command;
			SqlDataAdapter adapter = new SqlDataAdapter();

			string sql = "SELECT * FROM \"Industries\"";

			command = new SqlCommand(sql, cnn);

			var dataReader = command.ExecuteReader();

			// While it's reading the data we add it to a new object for each row of the Industries table
			while (dataReader.Read())
			{
				industries.Add(new Industry() { I_ID = Convert.ToInt32(dataReader.GetValue(0)), I_Name = Convert.ToString(dataReader.GetValue(1)), I_Code = Convert.ToString(dataReader.GetValue(2)), I_Description = Convert.ToString(dataReader.GetValue(3)) });
			}

			// We write the industry objects to the debug console
			foreach (Industry industry in industries)
			{
				Debug.WriteLine(industry.I_ID + " " + industry.I_Name + " " + industry.I_Code + " " + industry.I_Description);
			};

			// We delete the command and close the connection
			command.Dispose();
			cnn.Close();
		}
	}
}
