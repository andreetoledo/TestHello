using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;

/*

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
        public Datum[] Data { get; set; }

        [JsonProperty("sensor_type")]
        public long SensorType { get; set; }

        [JsonProperty("data_structure_type")]
        public long DataStructureType { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("bar_trend_3_hr", NullValueHandling = NullValueHandling.Ignore)]
        public double? BarTrend3_Hr { get; set; }

        [JsonProperty("pressure_last", NullValueHandling = NullValueHandling.Ignore)]
        public double? PressureLast { get; set; }

        [JsonProperty("ts")]
        public long Ts { get; set; }

        [JsonProperty("bar", NullValueHandling = NullValueHandling.Ignore)]
        public double? Bar { get; set; }

        [JsonProperty("bar_absolute", NullValueHandling = NullValueHandling.Ignore)]
        public double? BarAbsolute { get; set; }

        [JsonProperty("bar_trend", NullValueHandling = NullValueHandling.Ignore)]
        public long? BarTrend { get; set; }

        [JsonProperty("dew_point", NullValueHandling = NullValueHandling.Ignore)]
        public long? DewPoint { get; set; }

        [JsonProperty("et_day", NullValueHandling = NullValueHandling.Ignore)]
        public long? EtDay { get; set; }

        [JsonProperty("forecast_rule", NullValueHandling = NullValueHandling.Ignore)]
        public long? ForecastRule { get; set; }

        [JsonProperty("forecast_desc", NullValueHandling = NullValueHandling.Ignore)]
        public string ForecastDesc { get; set; }

        [JsonProperty("heat_index", NullValueHandling = NullValueHandling.Ignore)]
        public long? HeatIndex { get; set; }

        [JsonProperty("hum_out", NullValueHandling = NullValueHandling.Ignore)]
        public long? HumOut { get; set; }

        [JsonProperty("rain_15_min_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain15_MinClicks { get; set; }

        [JsonProperty("rain_15_min_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain15_MinIn { get; set; }

        [JsonProperty("rain_15_min_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain15_MinMm { get; set; }

        [JsonProperty("rain_60_min_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain60_MinClicks { get; set; }

        [JsonProperty("rain_60_min_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain60_MinIn { get; set; }

        [JsonProperty("rain_60_min_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain60_MinMm { get; set; }

        [JsonProperty("rain_24_hr_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain24_HrClicks { get; set; }

        [JsonProperty("rain_24_hr_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain24_HrIn { get; set; }

        [JsonProperty("rain_24_hr_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rain24_HrMm { get; set; }

        [JsonProperty("rain_day_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainDayClicks { get; set; }

        [JsonProperty("rain_day_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainDayIn { get; set; }

        [JsonProperty("rain_day_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainDayMm { get; set; }

        [JsonProperty("rain_rate_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainRateClicks { get; set; }

        [JsonProperty("rain_rate_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainRateIn { get; set; }

        [JsonProperty("rain_rate_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainRateMm { get; set; }

        [JsonProperty("rain_storm_clicks", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainStormClicks { get; set; }

        [JsonProperty("rain_storm_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainStormIn { get; set; }

        [JsonProperty("rain_storm_mm", NullValueHandling = NullValueHandling.Ignore)]
        public long? RainStormMm { get; set; }

        [JsonProperty("rain_storm_start_date")]
        public object RainStormStartDate { get; set; }

        [JsonProperty("solar_rad")]
        public object SolarRad { get; set; }

        [JsonProperty("temp_out", NullValueHandling = NullValueHandling.Ignore)]
        public double? TempOut { get; set; }

        [JsonProperty("thsw_index", NullValueHandling = NullValueHandling.Ignore)]
        public long? ThswIndex { get; set; }

        [JsonProperty("uv")]
        public object Uv { get; set; }

        [JsonProperty("wind_chill", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindChill { get; set; }

        [JsonProperty("wind_dir", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindDir { get; set; }

        [JsonProperty("wind_dir_of_gust_10_min", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindDirOfGust10_Min { get; set; }

        [JsonProperty("wind_gust_10_min", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindGust10_Min { get; set; }

        [JsonProperty("wind_speed", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindSpeed { get; set; }

        [JsonProperty("wind_speed_2_min", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindSpeed2_Min { get; set; }

        [JsonProperty("wind_speed_10_min", NullValueHandling = NullValueHandling.Ignore)]
        public long? WindSpeed10_Min { get; set; }

        [JsonProperty("wet_bulb", NullValueHandling = NullValueHandling.Ignore)]
        public double? WetBulb { get; set; }

        [JsonProperty("iss_solar_panel_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public double? IssSolarPanelVoltage { get; set; }

        [JsonProperty("last_gps_reading_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastGpsReadingTimestamp { get; set; }

        [JsonProperty("resyncs", NullValueHandling = NullValueHandling.Ignore)]
        public long? Resyncs { get; set; }

        [JsonProperty("transmitter_battery_state", NullValueHandling = NullValueHandling.Ignore)]
        public long? TransmitterBatteryState { get; set; }

        [JsonProperty("crc_errors", NullValueHandling = NullValueHandling.Ignore)]
        public long? CrcErrors { get; set; }

        [JsonProperty("tiva_application_firmware_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? TivaApplicationFirmwareVersion { get; set; }

        [JsonProperty("lead_acid_battery_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public long? LeadAcidBatteryVoltage { get; set; }

        [JsonProperty("iss_transmitter_battery_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public double? IssTransmitterBatteryVoltage { get; set; }

        [JsonProperty("beacon_interval", NullValueHandling = NullValueHandling.Ignore)]
        public long? BeaconInterval { get; set; }

        [JsonProperty("davistalk_rssi", NullValueHandling = NullValueHandling.Ignore)]
        public long? DavistalkRssi { get; set; }

        [JsonProperty("solar_panel_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public long? SolarPanelVoltage { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rank { get; set; }

        [JsonProperty("false_wakeup_rssi", NullValueHandling = NullValueHandling.Ignore)]
        public long? FalseWakeupRssi { get; set; }

        [JsonProperty("cell_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? CellId { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitude { get; set; }

        [JsonProperty("power_percentage_mcu", NullValueHandling = NullValueHandling.Ignore)]
        public long? PowerPercentageMcu { get; set; }

        [JsonProperty("mcc_mnc", NullValueHandling = NullValueHandling.Ignore)]
        public long? MccMnc { get; set; }

        [JsonProperty("iss_super_cap_voltage", NullValueHandling = NullValueHandling.Ignore)]
        public double? IssSuperCapVoltage { get; set; }

        [JsonProperty("false_wakeup_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? FalseWakeupCount { get; set; }

        [JsonProperty("etx", NullValueHandling = NullValueHandling.Ignore)]
        public long? Etx { get; set; }

        [JsonProperty("number_of_neighbors", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumberOfNeighbors { get; set; }

        [JsonProperty("last_parent_rtt_ping", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastParentRttPing { get; set; }

        [JsonProperty("bootloader_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? BootloaderVersion { get; set; }

        [JsonProperty("cme", NullValueHandling = NullValueHandling.Ignore)]
        public long? Cme { get; set; }

        [JsonProperty("cc1310_firmware_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? Cc1310FirmwareVersion { get; set; }

        [JsonProperty("power_percentage_rx", NullValueHandling = NullValueHandling.Ignore)]
        public long? PowerPercentageRx { get; set; }

        [JsonProperty("good_packet_streak", NullValueHandling = NullValueHandling.Ignore)]
        public long? GoodPacketStreak { get; set; }

        [JsonProperty("rpl_parent_node_id")]
        public object RplParentNodeId { get; set; }

        [JsonProperty("afc_setting", NullValueHandling = NullValueHandling.Ignore)]
        public long? AfcSetting { get; set; }

        [JsonProperty("overall_access_technology", NullValueHandling = NullValueHandling.Ignore)]
        public long? OverallAccessTechnology { get; set; }

        [JsonProperty("cell_channel", NullValueHandling = NullValueHandling.Ignore)]
        public long? CellChannel { get; set; }

        [JsonProperty("noise_floor_rssi", NullValueHandling = NullValueHandling.Ignore)]
        public long? NoiseFloorRssi { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitude { get; set; }

        [JsonProperty("cereg", NullValueHandling = NullValueHandling.Ignore)]
        public long? Cereg { get; set; }

        [JsonProperty("last_cme_error_timestamp")]
        public object LastCmeErrorTimestamp { get; set; }

        [JsonProperty("bluetooth_firmware_version")]
        public object BluetoothFirmwareVersion { get; set; }

        [JsonProperty("location_area_code", NullValueHandling = NullValueHandling.Ignore)]
        public long? LocationAreaCode { get; set; }

        [JsonProperty("link_layer_packets_received", NullValueHandling = NullValueHandling.Ignore)]
        public long? LinkLayerPacketsReceived { get; set; }

        [JsonProperty("reception_percent", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReceptionPercent { get; set; }

        [JsonProperty("rx_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? RxBytes { get; set; }

        [JsonProperty("link_uptime", NullValueHandling = NullValueHandling.Ignore)]
        public long? LinkUptime { get; set; }

        [JsonProperty("creg_cgreg", NullValueHandling = NullValueHandling.Ignore)]
        public long? CregCgreg { get; set; }

        [JsonProperty("health_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? HealthVersion { get; set; }

        [JsonProperty("inside_box_temp", NullValueHandling = NullValueHandling.Ignore)]
        public double? InsideBoxTemp { get; set; }

        [JsonProperty("tx_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? TxBytes { get; set; }

        [JsonProperty("elevation", NullValueHandling = NullValueHandling.Ignore)]
        public long? Elevation { get; set; }

        [JsonProperty("power_percentage_tx", NullValueHandling = NullValueHandling.Ignore)]
        public long? PowerPercentageTx { get; set; }

        [JsonProperty("rssi", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rssi { get; set; }

        [JsonProperty("last_rx_rssi", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastRxRssi { get; set; }

        [JsonProperty("rpl_mode", NullValueHandling = NullValueHandling.Ignore)]
        public long? RplMode { get; set; }

        [JsonProperty("uptime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Uptime { get; set; }

        [JsonProperty("platform_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? PlatformId { get; set; }
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