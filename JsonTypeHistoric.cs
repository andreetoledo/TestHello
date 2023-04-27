using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;



public class SensorData
{
    public double iss_reception { get; set; }
    public double wind_speed_avg { get; set; }
    public double wind_speed_hi { get; set; }
    public double? wind_dir_of_hi { get; set; }
    public double wind_chill { get; set; }
    public double deg_days_heat { get; set; }
    public double thw_index { get; set; }
    public double bar { get; set; }
    public double hum_out { get; set; }
    public double temp_out { get; set; }
    public double temp_out_lo { get; set; }
    public double wet_bulb { get; set; }
    public double temp_out_hi { get; set; }
    public double bar_alt { get; set; }
    public double arch_int { get; set; }
    public double wind_run { get; set; }
    public double dew_point_out { get; set; }
    public double rain_rate_hi_clicks { get; set; }
    public double? wind_dir_of_prevail { get; set; }
    public double et { get; set; }
    public double air_density { get; set; }
    public double rainfall_in { get; set; }
    public double heat_index_out { get; set; }
    public double rainfall_mm { get; set; }
    public double deg_days_cool { get; set; }
    public double rain_rate_hi_in { get; set; }
    public double wind_num_samples { get; set; }
    public double emc { get; set; }
    public double rain_rate_hi_mm { get; set; }
    public double rev_type { get; set; }
    public double rainfall_clicks { get; set; }
    public double ts { get; set; }
    public double abs_press { get; set; }
}

public class Sensor
{
    public double lsid { get; set; }
    public List<SensorData> data { get; set; }
    public double sensor_type { get; set; }
    public double data_structure_type { get; set; }
}

public class RootObject
{
    public List<Sensor> sensors { get; set; }
    public double generated_at { get; set; }
    public string station_id { get; set; }
}

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