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


/* Este programa consulta la API WeatherLink para obtener las condiciones meteorológicas actuales para una lista de estaciones meteorológicas.
   Primero configura una lista de ID de estación para consultar, así como los parámetros de autenticación de API necesarios (clave y secreto de API).
   Luego, itera a través de la lista de ID de estación, agregando la ID y un parámetro de marca de tiempo a los parámetros de autenticación, y luego calcula una firma de API para autenticar la solicitud.
   Luego construye una URL para consultar la API sobre las condiciones climáticas actuales para la estación especificada, utilizando la firma API calculada.
   Finalmente, envía una solicitud GET a la API usando la URL construida e imprime la respuesta JSON para cada estación.
*/

class MainClass
{
    public static async Task Main(string[] args) 
    {
        try //"148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" 
        {
            List<string> stationIds = new List<string> { "148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" };
            List<string> namesFincas = new List<string> { "NiñoPerdido", "AF_Concepcion", "LaLabranza", "CostaSol", "AF_SanJose", "AK_SanAgustinLasMinas", "AK_Holanda", "TropicultivosIII", "TropicultivosI" };
            List<string> fincaIds = new List<string> { "10", "2", "12", "8", "20", "11", "19", "6", "4" };

            // Parámetros requeridos por la API
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

                /*
                Console.Out.WriteLine("v2 API URL: https://api.weatherlink.com/v2/historic/" + parameters["station-id"] +
                  "?api-key=" + parameters["api-key"] +
                  "&t=" + parameters["t"] +
                  "&start-timestamp=" + parameters["start-timestamp"] +
                  "&end-timestamp=" + parameters["end-timestamp"] +
                  "&api-signature=" + apiSignatureString);
                */

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
                var rutaArchivo = @"C:\Users\EToledo\source\repos\TestHello\TestHello\dataweather.json";

                // Escribimos el archivo JSON en la ruta especificada
                File.WriteAllText(rutaArchivo, serialized);

                string json = jsonString;
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);

                int index = 0;

                while (true)
                {
                    // Verify if the current sensor has no data
                    if (rootObject.sensors[index].data.Count == 0)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        continue;
                    }

                    Sensor currentSensor = rootObject.sensors[index];

                    // Validar si el sensor tiene 24 valores en la cuenta
                    if (currentSensor.data.Count != 24)
                    {
                        index = (index + 1) % rootObject.sensors.Count;
                        continue;
                    }

                    // Validar si todos los valores de SensorData en el sensor son 0
                    bool allZero = true;
                    foreach (SensorData sensorData in currentSensor.data)
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
                        continue;
                    }

                    Console.WriteLine(stationId);

                    foreach (SensorData sensorData in currentSensor.data)
                    {
                        var timest = (long)sensorData.ts;
                        Console.Out.WriteLine("DATOS DE LA ESTACIÓN: " + stationId);
                        Console.WriteLine("Fecha y Hora: " + DateTimeOffset.FromUnixTimeSeconds(timest).ToLocalTime().ToString());
                        Console.WriteLine("iss_reception: " + sensorData.iss_reception.ToString());
                        Console.WriteLine("wind_speed_avg: " + sensorData.wind_speed_avg.ToString());
                        Console.WriteLine("wind_speed_hi: " + sensorData.wind_speed_hi.ToString());
                        Console.WriteLine("wind_dir_of_hi: " + sensorData.wind_dir_of_hi.ToString());
                        Console.WriteLine("wind_chill: " + sensorData.wind_chill.ToString());
                        Console.WriteLine("deg_days_heat: " + sensorData.deg_days_heat.ToString());
                        Console.WriteLine("thw_index: " + sensorData.thw_index.ToString());
                        Console.WriteLine("bar: " + sensorData.bar.ToString());
                        Console.WriteLine("hum_out: " + sensorData.hum_out.ToString());
                        Console.WriteLine("temp_out: " + sensorData.temp_out.ToString());
                        Console.WriteLine("temp_out_lo: " + sensorData.temp_out_lo.ToString());
                        Console.WriteLine("wet_bulb: " + sensorData.wet_bulb.ToString());
                        Console.WriteLine("temp_out_hi: " + sensorData.temp_out_hi.ToString());
                        Console.WriteLine("bar_alt: " + sensorData.bar_alt.ToString());
                        Console.WriteLine("arch_int: " + sensorData.arch_int.ToString());
                        Console.WriteLine("wind_run: " + sensorData.wind_run.ToString());
                        Console.WriteLine("dew_point_out: " + sensorData.dew_point_out.ToString());
                        Console.WriteLine("rain_rate_hi_clicks: " + sensorData.rain_rate_hi_clicks.ToString());
                        Console.WriteLine("wind_dir_of_prevail: " + sensorData.wind_dir_of_prevail.ToString());
                        Console.WriteLine("et: " + sensorData.et.ToString());
                        Console.WriteLine("air_density: " + sensorData.air_density.ToString());
                        Console.WriteLine("rainfall_in: " + sensorData.rainfall_in.ToString());
                        Console.WriteLine("heat_index_out: " + sensorData.heat_index_out.ToString());
                        Console.WriteLine("rainfall_mm: " + sensorData.rainfall_mm.ToString());
                        Console.WriteLine("deg_days_cool: " + sensorData.deg_days_cool.ToString());
                        Console.WriteLine("rain_rate_hi_in: " + sensorData.rain_rate_hi_in.ToString());
                        Console.WriteLine("wind_num_samples: " + sensorData.wind_num_samples.ToString());
                        Console.WriteLine("emc: " + sensorData.emc.ToString());
                        Console.WriteLine("rain_rate_hi_mm: " + sensorData.rain_rate_hi_mm.ToString());
                        Console.WriteLine("rev_type: " + sensorData.rev_type.ToString());
                        Console.WriteLine("rainfall_clicks: " + sensorData.rainfall_clicks.ToString());
                        Console.WriteLine("time_stamp: " + sensorData.ts.ToString());
                        Console.WriteLine("abs_press: " + sensorData.abs_press.ToString());
                        Console.WriteLine("\r");
                        
                    


                    }
                    break; // aquí es donde se debe romper el ciclo while

                    index++;

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

