using System;
using System.Data.SqlClient;


/*
< server name >: es el nombre del servidor de base de datos SQL al que desea conectarse. Puede ser una dirección IP o un nombre de host.

<database name>: es el nombre de la base de datos a la que desea conectarse.

Integrated Security=True: significa que la autenticación de Windows se utilizará para conectarse a la base de datos. Si está utilizando la autenticación de SQL Server, debe cambiar esto a Integrated Security=False y proporcionar un nombre de usuario y contraseña válidos en la cadena de conexión.
    */

namespace CreateStationsDataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string for SQL Server
            string connectionString = "Data Source=<server name>;Initial Catalog=<database name>;Integrated Security=True";

            // SQL query to create StationsData table
            string createTableQuery = @"CREATE TABLE StationsData (
                                          ID uniqueidentifier NOT NULL,
                                          IDFinca uniqueidentifier NOT NULL,
                                          iss_reception float,
                                          wind_speed_avg float,
                                          wind_speed_hi float,
                                          wind_dir_of_hi float,
                                          wind_chill float,
                                          deg_days_heat float,
                                          thw_index float,
                                          bar float,
                                          hum_out float,
                                          temp_out float,
                                          temp_out_lo float,
                                          wet_bulb float,
                                          temp_out_hi float,
                                          bar_alt float,
                                          arch_int float,
                                          wind_run float,
                                          dew_point_out float,
                                          rain_rate_hi_clicks float,
                                          wind_dir_of_prevail float,
                                          et float,
                                          air_density float,
                                          rainfall_in float,
                                          heat_index_out float,
                                          rainfall_mm float,
                                          deg_days_cool float,
                                          rain_rate_hi_in float,
                                          wind_num_samples float,
                                          emc float,
                                          rain_rate_hi_mm float,
                                          rev_type float,
                                          rainfall_clicks float,
                                          ts float,
                                          abs_press float,
                                          PRIMARY KEY (ID),
                                          CONSTRAINT IDFinca_FK FOREIGN KEY (IDFinca) REFERENCES Finca(ID)
                                      )";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection to the database
                    connection.Open();

                    // Create the StationsData table
                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("StationsData table created successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating StationsData table: {ex.Message}");
            }
        }
    }
}
