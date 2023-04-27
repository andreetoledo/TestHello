using System;
using System.Data.SqlClient;


/*
< server name >: es el nombre del servidor de base de datos SQL al que desea conectarse. Puede ser una dirección IP o un nombre de host.

<database name>: es el nombre de la base de datos a la que desea conectarse.

Integrated Security=True: significa que la autenticación de Windows se utilizará para conectarse a la base de datos. Si está utilizando la autenticación de SQL Server, debe cambiar esto a Integrated Security=False y proporcionar un nombre de usuario y contraseña válidos en la cadena de conexión.
    */

/*

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
                                          IssReception nvarchar(50) float,
                                          WindSpeedAvg float,
                                          WindSpeedHi float,
                                          WindDirOfHi float,
                                          WindChill float,
                                          DegDaysHeat float,
                                          ThwIndex float,
                                          Bar float,
                                          HumOut float,
                                          TempOut float,
                                          TempOutLo float,
                                          WetBulb float,
                                          TempOutHi float,
                                          BarAlt float,
                                          ArchInt float,
                                          WindRun float,
                                          DewPointOut float,
                                          RainRateHiClicks float,
                                          WindDirOfPrevail float,
                                          Et float,
                                          AirDensity float,
                                          RainfallIn float,
                                          HeatIndexOut float,
                                          RainfallMm float,
                                          DegDaysCool float,
                                          RainRateHiIn float,
                                          WindNumSamples float,
                                          Emc float,
                                          RainRateHiMm float,
                                          RevType float,
                                          RainfallClicks float,
                                          TimeStamp float,
                                          AbsPress float,
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
*/