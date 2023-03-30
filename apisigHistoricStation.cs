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



            }

            // Captura cualquier excepción
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}

