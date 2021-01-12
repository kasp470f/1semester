using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.SqlClient;

// Insert 
// MunicipalityFunctions.Municipality municipality = new MunicipalityFunctions.Municipality();
// municipality.GetMunicipality();
// On page where municipalities need to be called

// Whole section is made by Natasha
namespace Applikationen.MunicipalityFunctions
{
	public class Municipality
	{
		// We define municipalities variables
		public int M_ID { get; set; }
		public string Name { get; set; }

		// Method to get municipalities from database
		public void GetMunicipality()
		{
			// We create a list to store the municipalities from the database in
			List<object> municipalities = new List<object>();

			// We open the connection to the database
			SqlConnection cnn;
			cnn = new SqlConnection(@"Data Source= DATAMATIKERDATA;Initial Catalog = team1; User ID = t1login; Password =t1login12345");
			cnn.Open();

			// We create the command and execute it
			SqlCommand command;
			SqlDataAdapter adapter = new SqlDataAdapter();
			String sql = "";

			sql = "SELECT * FROM \"Municipalities\"";
 
			command = new SqlCommand(sql, cnn);

			var dataReader = command.ExecuteReader();

			// While it's reading the data we add it to a new object for each row of the Municipalities table
			while (dataReader.Read())
			{
				municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
			}

			// We write the municipality objects to the debug console
			foreach(Municipality municipality in municipalities)
            {
				Debug.WriteLine(municipality.M_ID + " " + municipality.Name);
			};

			// We delete the command and close the connection
			command.Dispose();
			cnn.Close();
		}
	}
}
