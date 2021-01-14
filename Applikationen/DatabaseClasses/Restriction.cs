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
	public class Restriction
    {
		// We define restriction variables
		public int R_ID { get; set; }
		public string R_Text { get; set; }

		// Method to get restrictions from database
		public void GetRestriction()
		{
			// We create a list to store the restrictions from the database in
			List<object> restrictions = new List<object>();

			// We open the connection to the database
			SqlConnection cnn;
			cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
			cnn.Open();

			// We create the command and execute it
			SqlCommand command;
			SqlDataAdapter adapter = new SqlDataAdapter();

			string sql = "SELECT * FROM \"Restrictions\"";

			command = new SqlCommand(sql, cnn);

			var dataReader = command.ExecuteReader();

			// While it's reading the data we add it to a new object for each row of the Restrictions table
			while (dataReader.Read())
			{
				restrictions.Add(new Restriction() { R_ID = Convert.ToInt32(dataReader.GetValue(0)), R_Text = Convert.ToString(dataReader.GetValue(1)) });
			}

			// We write the restriction objects to the debug console
			foreach (Restriction restriction in restrictions)
			{
				Debug.WriteLine(restriction.R_ID + " " + restriction.R_Text);
			};

			// We delete the command and close the connection
			command.Dispose();
			cnn.Close();
		}
	}
}
