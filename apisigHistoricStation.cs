using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using QuickType;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Newtonsoft.Json;
using Serilog;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;







/* Este programa consulta la API WeatherLink para obtener las condiciones meteorológicas actuales para una lista de estaciones meteorológicas.
   Primero configura una lista de ID de estación para consultar, así como los parámetros de autenticación de API necesarios (clave y secreto de API).
   Luego, itera a través de la lista de ID de estación, agregando la ID y un parámetro de marca de tiempo a los parámetros de autenticación, y luego calcula una firma de API para autenticar la solicitud.
   Luego construye una URL para consultar la API sobre las condiciones climáticas actuales para la estación especificada, utilizando la firma API calculada.
   Finalmente, envía una solicitud GET a la API usando la URL construida e imprime la respuesta JSON para cada estación.
*/

class MainClass
{

    public PROD_StationsData SetStationData(SensorData sensorData, string stationId)
    {
        var stationData = new PROD_StationsData  
        {
            IDFincaStationData = int.Parse(stationId),
            FechaHora = DateTimeOffset.FromUnixTimeSeconds((long)sensorData.ts).ToLocalTime(),
            IssReception = Convert.ToDecimal(sensorData.iss_reception),
            WindSpeedAvg = Convert.ToDecimal(sensorData.wind_speed_avg),
            WindSpeedHi = Convert.ToDecimal(sensorData.wind_speed_hi),
            WindDirOfHi = Convert.ToDecimal(sensorData.wind_dir_of_hi),
            WindChill = Convert.ToDecimal(sensorData.wind_chill),
            DegDaysHeat = Convert.ToDecimal(sensorData.deg_days_heat),
            ThwIndex = Convert.ToDecimal(sensorData.thw_index),
            Bar = Convert.ToDecimal(sensorData.bar),
            HumOut = Convert.ToDecimal(sensorData.hum_out),
            TempOut = Convert.ToDecimal(sensorData.temp_out),
            TempOutLo = Convert.ToDecimal(sensorData.temp_out_lo),
            WetBulb = Convert.ToDecimal(sensorData.wet_bulb),
            TempOutHi = Convert.ToDecimal(sensorData.temp_out_hi),
            BarAlt = Convert.ToDecimal(sensorData.bar_alt),
            ArchInt = Convert.ToDecimal(sensorData.arch_int),
            WindRun = Convert.ToDecimal(sensorData.wind_run),
            DewPointOut = Convert.ToDecimal(sensorData.dew_point_out),
            RainRateHiClicks = Convert.ToDecimal(sensorData.rain_rate_hi_clicks),
            WindDirOfPrevail = Convert.ToDecimal(sensorData.wind_dir_of_prevail),
            Et = Convert.ToDecimal(sensorData.et),
            AirDensity = Convert.ToDecimal(sensorData.air_density),
            RainfallIn = Convert.ToDecimal(sensorData.rainfall_in),
            HeatIndexOut = Convert.ToDecimal(sensorData.heat_index_out),
            RainfallMm = Convert.ToDecimal(sensorData.rainfall_mm),
            DegDaysCool = Convert.ToDecimal(sensorData.deg_days_cool),
            RainRateHiIn = Convert.ToDecimal(sensorData.rain_rate_hi_in),
            WindNumSamples = Convert.ToDecimal(sensorData.wind_num_samples),
            Emc = Convert.ToDecimal(sensorData.emc),
            RevType = Convert.ToDecimal(sensorData.rev_type),
            RainfallClicks = Convert.ToDecimal(sensorData.rainfall_clicks),
            AbsPress = Convert.ToDecimal(sensorData.abs_press),
        };
        
        return stationData;
        // Llamada a la función
        //PROD_StationsData stationData = SetStationData(sensorData, stationId); 
    }


    
    public static async Task Main(string[] args) 
    {
        try //"148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" 
        {
            List<string> stationIds = new List<string> { "148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" };
            List<string> namesFincas = new List<string> { "NiñoPerdido", "AF_Concepcion", "LaLabranza", "CostaSol", "AF_SanJose", "AK_SanAgustinLasMinas", "AK_Holanda", "TropicultivosIII", "TropicultivosI" };
            List<string> fincaIds = new List<string> { "10", "2", "12", "8", "20", "11", "19", "6", "4" };

            // Parámetros requeridos por la API, API KEY y API SECRET
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("api-key", "j2ribfc24fui0p8odstwzuogldjf2r7v");
            parameters.Add("api-secret", "idwzxqlu9ddiw0fc9emxhbzvtg8ogmur");

            // Iterar sobre cada ID de estación
            foreach (string stationId in stationIds)
            {
                // Obtener la fecha y hora actual en formato UTC
                var now = DateTimeOffset.UtcNow;
                var yesterday = now.AddDays(-2).Date;
                var today = now.AddDays(-1).Date;
                //var yesterday = new DateTime(2023, 1, 1); 
                //var today = new DateTime(2023, 1, 2); 



                // Agregar el momento inicial y final de la consulta el ID de la estación actual a los parámetros
                parameters["end-timestamp"] = new DateTimeOffset(today.Year, today.Month, today.Day, 20, 59, 0, TimeSpan.Zero).ToUnixTimeSeconds().ToString();
                parameters["start-timestamp"] = new DateTimeOffset(yesterday.Year, yesterday.Month, yesterday.Day, 20, 59, 0, TimeSpan.Zero).ToUnixTimeSeconds().ToString();
                parameters["station-id"] = stationId;
                parameters["t"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                



                /*
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    Console.Out.WriteLine("Parameter name: \"" + entry.Key + "\" is \"" + entry.Value + "\"");
                }
                */


                string apiSecret = parameters["api-secret"];
                parameters.Remove("api-secret");

                // Crear una cadena de datos concatenando los parámetros restantes
                StringBuilder dataStringBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    dataStringBuilder.Append(entry.Key);
                    dataStringBuilder.Append(entry.Value);
                }
                string data = dataStringBuilder.ToString();

              //  Console.Out.WriteLine("Data string to hash is: \"" + data + "\"");

                // Encriptar la cadena de datos con el API secret usando HMACSHA256
                string apiSignatureString = null;
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret)))
                {
                    byte[] apiSignatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                    apiSignatureString = BitConverter.ToString(apiSignatureBytes).Replace("-", "").ToLower();
                }

                // Imprimir la firma de la API
                //Console.Out.WriteLine("API Signature is: \"" + apiSignatureString + "\"");

                // Agregar el API secret nuevamente a los parámetros
                parameters.Add("api-secret", "idwzxqlu9ddiw0fc9emxhbzvtg8ogmur");

                //https://api.weatherlink.com/v2/historic/148400?api-key=j2ribfc24fui0p8odstwzuogldjf2r7v&t=1678732610&start-timestamp=1678568400&end-timestamp=1678654740&api-signature=5adc4b0f1e0a56d0eb5d1bbf785b530a5ce99fe69f55f2418eb1fb44ed449cdd

                
                Console.Out.WriteLine("v2 API URL: https://api.weatherlink.com/v2/historic/" + parameters["station-id"] +
                  "?api-key=" + parameters["api-key"] +
                  "&t=" + parameters["t"] +
                  "&start-timestamp=" + parameters["start-timestamp"] +
                  "&end-timestamp=" + parameters["end-timestamp"] +
                  "&api-signature=" + apiSignatureString);
                

                // Construir la URL completa usando UriBuilder
                var builder = new UriBuilder("https://api.weatherlink.com/v2/historic/" + parameters["station-id"])
                {
                    Query = $"api-key={parameters["api-key"]}&t={parameters["t"]}&start-timestamp={parameters["start-timestamp"]}&end-timestamp={parameters["end-timestamp"]}&api-signature={apiSignatureString}"
                };
                string url = builder.ToString();

                // Crea un nuevo objeto HttpClient
                using var client = new HttpClient();
                // Enviar una solicitud GET a la URL del extremo de la API y obtener la respuesta
                var response = await client.GetAsync(url);
                // Asegúrese de que la respuesta tenga un código de estado correcto (es decir, no un error)
                response.EnsureSuccessStatusCode();
                // Lee el contenido de la respuesta como una cadena (JSON)
                var jsonString = await response.Content.ReadAsStringAsync();

                //serializado del json
                var welcome = Welcome.FromJson(jsonString);
                var serialized = JsonConvert.SerializeObject(welcome, Formatting.Indented);
                
                //Console.WriteLine(serialized);

                // Definimos la ruta y nombre del archivo
                //var rutaArchivo = @"C:\Users\EToledo\source\repos\TestHello\TestHello\dataweather.json";

                // Escribimos el archivo JSON en la ruta especificada
                //File.WriteAllText(rutaArchivo, serialized);

                string json = jsonString;
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                
                Sensor currentSensor = null;
                int index = 0;

                while (currentSensor == null && index < rootObject.sensors.Count)
                {
                    // Verify if all sensors have no data or data is nulL
                    if (rootObject.sensors.All(sensor => sensor.data.Count == 0 || sensor.data.All(data => data == null))) 
                    { 
                        continue; 
                    }

                    // Verify if the current sensor has no data
                    if (rootObject.sensors[index].data.Count == 0)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        index++;
                        continue;
                    }

                    Sensor sensor = rootObject.sensors[index];

                    // Validar si todos los valores de SensorData en el sensor son NULL
                    bool allNull = true;
                    foreach (SensorData sensorData in sensor.data)
                    {
                        if (sensorData.iss_reception != null)
                        {
                            allNull = false;
                            break;
                        }
                    }

                    if (allNull)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        index++;
                        continue;
                    }

                    // Validar si el sensor tiene 24 valores en la cuenta
                    if (sensor.data.Count != 24)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        index++;
                        continue;
                    }

                    // Validar si todos los valores de SensorData en el sensor son 0
                    bool allZero = true;
                    foreach (SensorData sensorData in sensor.data)
                    {
                        if (sensorData.iss_reception != 0)
                        {
                            allZero = false;
                            break;
                        }
                    }

                    if (allZero)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        index++;
                        continue;
                    }

                    // If the sensor passes all conditions, assign it to currentSensor
                    currentSensor = sensor;

                    var Connectbuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                    var configuration = Connectbuilder.Build();

                    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));


                    using (var context = new DataContext(optionsBuilder.Options))
                    {
                        
           

                        foreach (SensorData sensorData in currentSensor.data)
                        {

                            Console.Out.WriteLine("DATOS DE LA ESTACIÓN: " + stationId);
                            MainClass mainClass = new MainClass();
                            PROD_StationsData stationData = mainClass.SetStationData(sensorData, stationId);
                            context.PROD_StationsData.Add(stationData);
                            context.SaveChanges();
                           
                        }
                    }
                     // exit the while loop if a valid sensor has been found
                    break;

                }



            }
            // Captura cualquier excepción
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}

