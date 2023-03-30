using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using QuickType;
using Microsoft.EntityFrameworkCore;

/* Este programa consulta la API WeatherLink para obtener las condiciones meteorológicas actuales para una lista de estaciones meteorológicas.
   Primero configura una lista de ID de estación para consultar, así como los parámetros de autenticación de API necesarios (clave y secreto de API).
   Luego, itera a través de la lista de ID de estación, agregando la ID y un parámetro de marca de tiempo a los parámetros de autenticación, y luego calcula una firma de API para autenticar la solicitud.
   Luego construye una URL para consultar la API sobre las condiciones climáticas actuales para la estación especificada, utilizando la firma API calculada.
   Finalmente, envía una solicitud GET a la API usando la URL construida e imprime la respuesta JSON para cada estación.
*/

/*
class MainClass
{
    public static async Task Main(string[] args) 
    {
        try
        {

            List<string> stationIds = new List<string> { "148395", "148397", "148398", "148400", "148404", "148406", "148408", "148412", "148417" };

            // Parámetros requeridos por la API
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("api-key", "j2ribfc24fui0p8odstwzuogldjf2r7v");
            parameters.Add("api-secret", "idwzxqlu9ddiw0fc9emxhbzvtg8ogmur");

            // Iterar sobre cada ID de estación
            foreach (string stationId in stationIds)
            {
                // Agregar el ID de la estación actual a los parámetros
                parameters["station-id"] = stationId;
                parameters["t"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

                // Imprimir todos los parámetros
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

                parameters.Add("api-secret", "idwzxqlu9ddiw0fc9emxhbzvtg8ogmur");

                Console.Out.WriteLine("API Signature is: \"" + apiSignatureString + "\"");

                // Imprimir la URL de la API que se está solicitando
                Console.Out.WriteLine("v2 API URL: https://api.weatherlink.com/v2/current/" + parameters["station-id"] +
                  "?api-key=" + parameters["api-key"] +
                  "&t=" + parameters["t"] +
                  "&api-signature=" + apiSignatureString);

                // Construir la URL completa usando UriBuilder
                var builder = new UriBuilder("https://api.weatherlink.com/v2/current/" + parameters["station-id"])
                {
                    Query = $"api-key={parameters["api-key"]}&t={parameters["t"]}&api-signature={apiSignatureString}"
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

                // Imprime los datos JSON para la estación meteorológica actual en la consola
                Console.WriteLine($"JSON for station {stationId}:");
                var welcome = Welcome.FromJson(jsonString);
                var serialized = JsonConvert.SerializeObject(welcome, Formatting.Indented);
                Console.WriteLine(serialized);

                /*
                var jsonData = "..."; // Aquí debes tener el JSON original
                var datosOriginales = JsonConvert.DeserializeObject<DatosOriginales>(jsonData);

                var datosNuevos = new SensorDataClean
                {
                    iss_reception = datosOriginales.datos[0].iss_reception,
                    wind_speed_avg = datosOriginales.datos[0].wind_speed_avg
                };
                

                var jsonDataNuevo = JsonConvert.SerializeObject(datosNuevos);
                
                var sensorData = JsonConvert.DeserializeObject<SensorDataClean>(jsonString);
                using var context = new MyDbContext();
                context.SensorData.Add(sensorData);
                await context.SaveChangesAsync();
                /




            }

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}

*/
