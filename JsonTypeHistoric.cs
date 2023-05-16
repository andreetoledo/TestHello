using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;





public class SensorData
{
    public decimal? iss_reception { get; set; }
    public decimal? wind_speed_avg { get; set; }
    public decimal? wind_speed_hi { get; set; }
    public decimal? wind_dir_of_hi { get; set; }
    public decimal? wind_chill { get; set; }
    public decimal? deg_days_heat { get; set; }
    public decimal? thw_index { get; set; }
    public decimal? bar { get; set; }
    public decimal? hum_out { get; set; }
    public decimal? temp_out { get; set; }
    public decimal? temp_out_lo { get; set; }
    public decimal? wet_bulb { get; set; }
    public decimal? temp_out_hi { get; set; }
    public decimal? bar_alt { get; set; }
    public decimal? arch_int { get; set; }
    public decimal? wind_run { get; set; }
    public decimal? dew_point_out { get; set; }
    public decimal? rain_rate_hi_clicks { get; set; }
    public decimal? wind_dir_of_prevail { get; set; }
    public decimal? et { get; set; }
    public decimal? air_density { get; set; }
    public decimal? rainfall_in { get; set; }
    public decimal? heat_index_out { get; set; }
    public decimal? rainfall_mm { get; set; }
    public decimal? deg_days_cool { get; set; }
    public decimal? rain_rate_hi_in { get; set; }
    public decimal? wind_num_samples { get; set; }
    public decimal? emc { get; set; }
    public decimal? rain_rate_hi_mm { get; set; }
    public decimal? rev_type { get; set; }
    public decimal? rainfall_clicks { get; set; }
    public decimal? ts { get; set; }
    public decimal? abs_press { get; set; }
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




public class PROD_FincaStationData {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IDFincaStationData { get; set; }
    public string Descripcion { get; set; }
    public bool Activo { get; set; } = true;

}


public class PROD_StationsData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Range(1, 1)]
    public int IDLecture { get; set; }
    [ForeignKey("PROD_FincaStationData")]
    public int IDFincaStationData { get; set; }
    public PROD_FincaStationData PROD_FincaStationData { get; set; }
    public DateTimeOffset FechaHora { get; set; }
    public decimal? IssReception { get; set; }
    public decimal? WindSpeedAvg { get; set; }
    public decimal? WindSpeedHi { get; set; }
    public decimal? WindDirOfHi { get; set; }
    public decimal? WindChill { get; set; }
    public decimal? DegDaysHeat { get; set; }
    public decimal? ThwIndex { get; set; }
    public decimal? Bar { get; set; }
    public decimal? HumOut { get; set; }
    public decimal? TempOut { get; set; }
    public decimal? TempOutLo { get; set; }
    public decimal? WetBulb { get; set; }
    public decimal? TempOutHi { get; set; }
    public decimal? BarAlt { get; set; }
    public decimal? ArchInt { get; set; }
    public decimal? WindRun { get; set; }
    public decimal? DewPointOut { get; set; }
    public decimal? RainRateHiClicks { get; set; }
    public decimal? WindDirOfPrevail { get; set; }
    public decimal? Et { get; set; }
    public decimal? AirDensity { get; set; }
    public decimal? RainfallIn { get; set; }
    public decimal? HeatIndexOut { get; set; }
    public decimal? RainfallMm { get; set; }
    public decimal? DegDaysCool { get; set; }
    public decimal? RainRateHiIn { get; set; }
    public decimal? WindNumSamples { get; set; }
    public decimal? Emc { get; set; }
    public decimal? RevType { get; set; }
    public decimal? RainfallClicks { get; set; }
    public decimal? AbsPress { get; set; }

}



public class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<PROD_StationsData> PROD_StationsData  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PROD_StationsData>().HasKey(s => s.IDLecture);
    }
}


