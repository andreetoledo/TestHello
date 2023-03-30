# TestHello
 Consumo de API de WeatherLink v2 para la obtención de data climatica de las estaciones meteorológicas propias.

 # Programa de consulta de condiciones meteorológicas actuales de WeatherLink

Este programa utiliza la API de WeatherLink para obtener las condiciones meteorológicas actuales de una lista de estaciones meteorológicas. A continuación, se proporciona una descripción detallada del funcionamiento del programa:

## Configuración de parámetros
El programa requiere los siguientes parámetros de entrada:

- Una lista de ID de estación meteorológica para consultar
- Una clave de API de WeatherLink
- Un secreto de API de WeatherLink

Estos parámetros se deben proporcionar en el archivo de configuración `config.json`. El archivo de configuración debe tener la siguiente estructura:

```json
{
    "station_ids": ["ID1", "ID2", "ID3"],
    "api_key": "your-api-key",
    "api_secret": "your-api-secret"
}
```

## Autenticación de API

Para autenticar las solicitudes a la API de WeatherLink, el programa utiliza un esquema de autenticación basado en firma. El esquema de autenticación utiliza la clave de API y el secreto de API proporcionados en el archivo de configuración, así como un parámetro de marca de tiempo y la ID de estación actual.

Para cada estación en la lista de estaciones meteorológicas, el programa calcula la firma de API correspondiente y agrega los parámetros de autenticación a la URL de consulta.

## Consulta de la API
El programa construye una URL para consultar la API de WeatherLink para cada estación en la lista de estaciones meteorológicas. La URL incluye los parámetros de autenticación necesarios y especifica el tipo de datos que se deben recuperar (condiciones meteorológicas actuales).

El programa utiliza el método HTTP GET para enviar la solicitud a la API y espera una respuesta en formato JSON. Una vez recibida la respuesta, el programa imprime los datos meteorológicos actuales para cada estación en la lista.

## Uso del programa
Para utilizar el programa, siga los siguientes pasos:

  1. Descargue los archivos del programa desde el repositorio de GitHub: https://github.com/user/weatherlink-api-consulta

  2. Configure los parámetros de entrada en el archivo  según se describe anteriormente. Puede utilizar visual estudio 2022 y cargar toda la carpeta a un nuevo proyecto

  3. Ejecute el archivo apisigHistoricStation.cs en su terminal:

  ```
  
  ```

  4. Espere a que el programa finalice la consulta a la API y muestre los datos meteorológicos actuales para cada estación en la lista.

¡Gracias por utilizar nuestro programa para consultar las condiciones meteorológicas actuales de WeatherLink!