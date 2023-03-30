using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

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
*/






/*

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<SensorDataClean> SensorData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de modelos adicionales aquí
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        string connectionString = "Server=myServerName;Database=myDatabaseName;User Id=myUsername;Password=myPassword;";

         //Server=192.168.1.38;Database=Iktan65464;User Id=DevUser01;Password=Tak$$2023;

        optionsBuilder.UseSqlServer(connectionString);
    }
}


public class SensorDataClean
{
    public double iss_reception { get; set; }
    public int wind_speed_avg { get; set; }
    public double? uv_dose { get; set; }
    public int wind_speed_hi { get; set; }
    public int wind_dir_of_hi { get; set; }
    public double wind_chill { get; set; }
    public double? solar_rad_hi { get; set; }
    public double deg_days_heat { get; set; }
    public double thw_index { get; set; }
    public double bar { get; set; }
    public int hum_out { get; set; }
    public double? uv_index_hi { get; set; }
    public double temp_out { get; set; }
    public double temp_out_lo { get; set; }
    public double wet_bulb { get; set; }
    public double temp_out_hi { get; set; }
    public double? solar_rad_avg { get; set; }
    public double bar_alt { get; set; }
    public int arch_int { get; set; }
    public int wind_run { get; set; }
    public double? solar_energy { get; set; }
    public double dew_point_out { get; set; }
    public int rain_rate_hi_clicks { get; set; }
    public int wind_dir_of_prevail { get; set; }
    public double et { get; set; }
    public double air_density { get; set; }
    public int rainfall_in { get; set; }
    public double heat_index_out { get; set; }
    public double? thsw_index { get; set; }
    public int rainfall_mm { get; set; }
    public double? night_cloud_cover { get; set; }
    public double deg_days_cool { get; set; }
    public int rain_rate_hi_in { get; set; }
    public double? uv_index_avg { get; set; }
    public int wind_num_samples { get; set; }
    public double emc { get; set; }
    public int rain_rate_hi_mm { get; set; }
    public int rev_type { get; set; }
    public int rainfall_clicks { get; set; }
    public int ts { get; set; }
    public double abs_press { get; set; }
}






public class DataBlock
{
    public List<SensorDataClean> data { get; set; }
    public int sensor_type { get; set; }
    public int data_structure_type { get; set; }
}

public class RootObject
{
    public List<DataBlock> data_blocks { get; set; }
    public int generated_at { get; set; }
    public int station_id { get; set; }
}

*/
