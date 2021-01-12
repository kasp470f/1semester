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

namespace Applikationen.MunicipalityFunctions
{
	public class Municipality
	{
		public int M_ID { get; set; }
		public string Name { get; set; }

		public void GetMunicipality()
		{
			List<object> municipalities = new List<object>();

			
			SqlConnection cnn;
			cnn = new SqlConnection(@"Data Source= DATAMATIKERDATA;Initial Catalog = team1; User ID = t1login; Password =t1login12345");
			cnn.Open();


			SqlCommand command;
			SqlDataAdapter adapter = new SqlDataAdapter();
			String sql = "";

			sql = "SELECT * FROM \"Municipalities\"";
 
			command = new SqlCommand(sql, cnn);

			var dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
			}

			foreach(Municipality municipality in municipalities)
            {
				Debug.WriteLine(municipality.M_ID + " " + municipality.Name);
			};

			command.Dispose();
			cnn.Close();
		}
	}
}
