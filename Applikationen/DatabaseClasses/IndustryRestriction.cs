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
        public int RI_ID { get; set; }
        public int RI_R_ID { get; set; }
        public int RI_M_ID { get; set; }
        public int RI_I_ID { get; set; }
        public string RI_Text { get; set; }
        public string RI_StartDate { get; set; }
        public string RI_EndDate { get; set; }

        // Industry variables
        public string I_Name { get; set; }
        public string I_Code { get; set; }
        public string I_Description { get; set; }

        // Restriction variables
        public string R_Text { get; set; }

        // Municipality variables
        public string M_Name { get; set; }

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
                    // Select R_ID
                    string sql1 = "SELECT * FROM Restrictions WHERE R_Text = '" + item.R_Text + "'";

                    using (SqlCommand command = new SqlCommand(sql1, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_R_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }

                    // Select I_ID
                    string sql2 = "SELECT * FROM Industries WHERE I_Name = '" + item.I_Name + "'";

                    using (SqlCommand command = new SqlCommand(sql2, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_I_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }

                    // Select M_ID
                    string sql3 = "SELECT * FROM Municipalities WHERE M_Name = '" + item.M_Name + "'";

                    using (SqlCommand command = new SqlCommand(sql3, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_M_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }

                    // Select RI_ID
                    string sql4 = "SELECT * FROM IndustriesRestrictions WHERE RI_R_ID = '" + item.RI_R_ID + "' AND RI_I_ID = '" + item.RI_I_ID + "' AND RI_M_ID = '" + item.RI_M_ID + "'";

                    using (SqlCommand command = new SqlCommand(sql4, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();

                    }

                    string sql5 = "DELETE FROM IndustriesRestrictions WHERE RI_ID = " + item.RI_ID;

                    using (SqlCommand command = new SqlCommand(sql5, cnn))
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

        public void InsertIndustryRestriction(List<IndustryRestriction> list)
        {
            // We open the connection to the database
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                foreach (IndustryRestriction item in list)
                {
                    // Select R_ID
                    string sql1 = "SELECT * FROM Restrictions WHERE R_Text = '" + item.R_Text + "'";

                    using (SqlCommand command = new SqlCommand(sql1, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_R_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }

                    // Select I_ID
                    string sql2 = "SELECT * FROM Industries WHERE I_Name = '" + item.I_Name + "'";

                    using (SqlCommand command = new SqlCommand(sql2, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_I_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }

                    // Select M_ID
                    string sql3 = "SELECT * FROM Municipalities WHERE M_Name = '" + item.M_Name + "'";

                    using (SqlCommand command = new SqlCommand(sql3, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            item.RI_M_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }
                    // INSERT IndustryRestricitons
                    string sql4 = "INSERT INTO IndustriesRestrictions (RI_I_ID, RI_M_ID, RI_R_ID, RI_StartDate, RI_EndDate, RI_Text) VALUES (" + item.RI_I_ID + ", " + item.RI_M_ID + ", " + item.RI_R_ID + ", " + item.RI_StartDate + ", " + item.RI_EndDate + ", 'Text')";

                    using (SqlCommand command = new SqlCommand(sql4, cnn))
                    {
                        var dataReader = command.ExecuteReader();

                        // We delete the command and close the connection
                        dataReader.Close();
                        command.Dispose();
                    }
                    
                }
                cnn.Close();

                MessageBox.Show("De(n) valgte restriktion(er) på industri(er) er nu tilføjet.");
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

