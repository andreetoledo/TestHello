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
namespace QuickType
{


    public partial class Welcome
    {
        [JsonProperty("stations")]
        public Station[] Stations { get; set; }

        [JsonProperty("generated_at")]
        public long GeneratedAt { get; set; }
    }

    public partial class Station
    {
        [JsonProperty("station_id")]
        public long StationId { get; set; }

        [JsonProperty("station_name")]
        public string StationName { get; set; }

        [JsonProperty("gateway_id")]
        public long GatewayId { get; set; }

        [JsonProperty("gateway_id_hex")]
        public string GatewayIdHex { get; set; }

        [JsonProperty("product_number")]
        public string ProductNumber { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("recording_interval")]
        public long RecordingInterval { get; set; }

        [JsonProperty("firmware_version")]
        public object FirmwareVersion { get; set; }

        [JsonProperty("imei")]
        public string Imei { get; set; }

        [JsonProperty("registered_date")]
        public long RegisteredDate { get; set; }

        [JsonProperty("subscription_end_date")]
        public long SubscriptionEndDate { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

*/
