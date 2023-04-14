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
        try //,"148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" 
        {
            List<string> stationIds = new List<string> {"148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" };
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
                DateTime horaHumana = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(parameters["t"])).ToLocalTime().DateTime;
                Console.Out.WriteLine(horaHumana);




                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    Console.Out.WriteLine("Parameter name: \"" + entry.Key + "\" is \"" + entry.Value + "\"");
                }

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

                Console.Out.WriteLine("Data string to hash is: \"" + data + "\"");

                // Encriptar la cadena de datos con el API secret usando HMACSHA256
                string apiSignatureString = null;
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret)))
                {
                    byte[] apiSignatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                    apiSignatureString = BitConverter.ToString(apiSignatureBytes).Replace("-", "").ToLower();
                }

                // Imprimir la firma de la API
                Console.Out.WriteLine("API Signature is: \"" + apiSignatureString + "\"");

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

                // Imprime los datos JSON para la estación meteorológica a                                                                                                                                                  ctual en la consola
                Console.WriteLine($"JSON for station {stationId}:");
                var welcome = Welcome.FromJson(jsonString);
                var serialized = JsonConvert.SerializeObject(welcome, Formatting.Indented);
                Console.WriteLine(serialized);

                // Definimos la ruta y nombre del archivo
                var rutaArchivo = @"C:\Users\EToledo\source\repos\TestHello\TestHello\dataweather.json";

                // Escribimos el archivo JSON en la ruta especificada
                File.WriteAllText(rutaArchivo, serialized);


                

                // ...

                string connectionString = "TuConnectionString"; // Reemplaza TuConnectionString con tu cadena de conexión
                string query = "INSERT INTO TuTabla (iss_reception, wind_speed_avg, uv_dose, wind_speed_hi, wind_dir_of_hi, wind_chill, solar_rad_hi, deg_days_heat, thw_index, bar, hum_out, uv_index_hi, temp_out, temp_out_lo, wet_bulb, temp_out_hi, solar_rad_avg, bar_alt, arch_int, wind_run, solar_energy, dew_point_out, rain_rate_hi_clicks, wind_dir_of_prevail, et, air_density, rainfall_in, heat_index_out, thsw_index, rainfall_mm, night_cloud_cover, deg_days_cool, rain_rate_hi_in, uv_index_avg, wind_num_samples, emc, rain_rate_hi_mm, rev_type, rainfall_clicks, ts, abs_press) VALUES (@iss_reception, @wind_speed_avg, @uv_dose, @wind_speed_hi, @wind_dir_of_hi, @wind_chill, @solar_rad_hi, @deg_days_heat, @thw_index, @bar, @hum_out, @uv_index_hi, @temp_out, @temp_out_lo, @wet_bulb, @temp_out_hi, @solar_rad_avg, @bar_alt, @arch_int, @wind_run, @solar_energy, @dew_point_out, @rain_rate_hi_clicks, @wind_dir_of_prevail, @et, @air_density, @rainfall_in, @heat_index_out, @thsw_index, @rainfall_mm, @night_cloud_cover, @deg_days_cool, @rain_rate_hi_in, @uv_index_avg, @wind_num_samples, @emc, @rain_rate_hi_mm, @rev_type, @rainfall_clicks, @ts, @abs_press)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iss_reception", 87.0625);
                        command.Parameters.AddWithValue("@wind_speed_avg", 5.0);
                        command.Parameters.AddWithValue("@uv_dose", DBNull.Value);
                        command.Parameters.AddWithValue("@wind_speed_hi", 17.0);
                        command.Parameters.AddWithValue("@wind_dir_of_hi", 3.0);
                        command.Parameters.AddWithValue("@wind_chill", 84.8);
                        command.Parameters.AddWithValue("@solar_rad_hi", DBNull.Value);
                        command.Parameters.AddWithValue("@deg_days_heat", 0.0);
                        command.Parameters.AddWithValue("@thw_index", 84.060005);
                        command.Parameters.AddWithValue("@bar", 30.033);
                        command.Parameters.AddWithValue("@hum_out", 40.0);
                        command.Parameters.AddWithValue("@uv_index_hi", DBNull.Value);
                        command.Parameters.AddWithValue("@temp_out", 84.8);
                        command.Parameters.AddWithValue("@temp_out_lo", 83.8);
                        command.Parameters.AddWithValue("@wet_bulb", 63.085876);
                        command.Parameters.AddWithValue("@temp_out_hi", 85.6);
                        command.Parameters.AddWithValue("@solar_rad_avg", DBNull.Value);
                        command.Parameters.AddWithValue("@bar_alt", 30.033);
                        command.Parameters.AddWithValue("@arch_int", 3600.0);
                        command.Parameters.AddWithValue("@wind_run", 5.0);
                        command.Parameters.AddWithValue("@solar_energy", DBNull.Value);
                        command.Parameters.AddWithValue("@dew_point_out", 57.79513);
                        command.Parameters.AddWithValue("@rain_rate_hi_clicks", 0
                




                         string json =  jsonString;
                        WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                        double reception = data.iss_reception;
                        double windSpeed = data.wind_speed_avg;
                        double maxWindSpeed = data.wind_speed_hi;
                // y así sucesivamente



            }
            como inserto los datos que obtuve del Json a la base de datos

            // Captura cualquier excepción
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}

