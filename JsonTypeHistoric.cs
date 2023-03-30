using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;



namespace QuickType
{

    public partial class Welcome
    {
        [JsonProperty("sensors")]
        public Sensor[] Sensors { get; set; }

        [JsonProperty("generated_at")]
        public long GeneratedAt { get; set; }

        [JsonProperty("station_id")]
        public long StationId { get; set; }
    }

    public partial class Sensor
    {
        [JsonProperty("lsid")]
        public long Lsid { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, double?>[] Data { get; set; }

        [JsonProperty("sensor_type")]
        public long SensorType { get; set; }

        [JsonProperty("data_structure_type")]
        public long DataStructureType { get; set; }
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
