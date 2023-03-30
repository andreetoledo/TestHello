using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using QuickType;
using Microsoft.EntityFrameworkCore;


/*

class MainClass
{


    public static async Task Main(string[] args)
    {
        try
        {
            /*
            Aquí está la lista de parámetros que usaremos para este ejemplo.
            Usaremos un SortedDictionary para ordenar los parámetros por nombre de parámetro automáticamente.
            
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("api-key", "j2ribfc24fui0p8odstwzuogldjf2r7v");
            parameters.Add("api-secret", "idwzxqlu9ddiw0fc9emxhbzvtg8ogmur");
            parameters.Add("station-id", "148395");
            parameters.Add("t", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());


            /*
            Tomemos un momento para imprimir todos los parámetros con fines educativos y de depuración.
            
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                Console.Out.WriteLine("Parameter name: \"" + entry.Key + "\" is \"" + entry.Value + "\"");
            }

            /*
            Ahora calcularemos la firma API.
            El proceso de firma utiliza hash HMAC SHA-256 y usaremos el API Secret como la clave secreta de hash.
            Eso significa que no incluiremos el API Secret en el conjunto de parámetros proporcionados al algoritmo hash.
            
            //Guarde y elimine el API Secret y el ID de la estación del conjunto de parámetros

            string apiSecret = parameters["api-secret"];
            parameters.Remove("api-secret");
            parameters.Remove("station-id");

            StringBuilder dataStringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                dataStringBuilder.Append(entry.Key);
                dataStringBuilder.Append(entry.Value);
            }
            string data = dataStringBuilder.ToString();


            //Imprimamos los datos que vamos a codificar.

            Console.Out.WriteLine("Data string to hash is: \"" + data + "\"");


            //Calcule el hash HMAC SHA-256 que se utilizará como la firma API.

            string apiSignatureString = null;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret)))
            {
                byte[] apiSignatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                apiSignatureString = BitConverter.ToString(apiSignatureBytes).Replace("-", "").ToLower();
            }


            //Veamos cómo se ve la Firma API final.
            Console.Out.WriteLine("API Signature is: \"" + apiSignatureString + "\"");

            /*
            Ahora que se calcula la Firma API, veamos cuál es el resultado final.
            La URL de la API v2 se vería así en este escenario.
            
            Console.Out.WriteLine("https://api.weatherlink.com/v2/stations?api-key=" + parameters["api-key"] +
            "&t=" + parameters["t"] +
            "&api-signature=" + apiSignatureString);


            string url = "https://api.weatherlink.com/v2/stations?api-key=" + parameters["api-key"] + "&t=" + parameters["t"] + "&api-signature=" + apiSignatureString;

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var welcome = Welcome.FromJson(jsonString);
            Console.WriteLine($"generated_at: {welcome.GeneratedAt}");
            var serialized = JsonConvert.SerializeObject(welcome, Formatting.Indented);
            Console.WriteLine(serialized);



        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }


}
*/
