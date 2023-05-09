
CREATE TABLE PROD_FincaStationData (
  IDFincaStationData INT PRIMARY KEY,
  IDFinca TINYINT,
  Descripcion VARCHAR(50),
  Activo BIT NOT NULL DEFAULT(1)
);

ALTER TABLE PROD_FincaStationData WITH CHECK
	ADD CONSTRAINT FK_FincaStationData_Fincas
	FOREIGN KEY (IDFinca)
	REFERENCES M_Fincas(IDFinca);



INSERT INTO PROD_FincaStationData (IDFincaStationData, IDFinca) VALUES
(148395, 10),
(148397, 2),
(148398, 12),
(148400, 8),
(148404, 20),
(148406, 11),
(148408, 19),
(148412, 6),
(148417, 4);


CREATE TABLE PROD_StationsData (
    IDLecture int IDENTITY(1,1) PRIMARY KEY,
    IDFincaStationData int,
    FechaHora datetimeoffset(0) null,
    IssReception decimal(19,4) null,
    WindSpeedAvg decimal(19,4) null,
    WindSpeedHi decimal(19,4) null,
    WindDirOfHi decimal(19,4) null,
    WindChill decimal(19,4) null,
    DegDaysHeat decimal(19,4) null,
    ThwIndex decimal(19,4) null,
    Bar decimal(19,4) null,
    HumOut decimal(19,4) null,
    TempOut decimal(19,4) null,
    TempOutLo decimal(19,4) null,
    WetBulb decimal(19,4) null,
    TempOutHi decimal(19,4) null,
    BarAlt decimal(19,4) null,
    ArchInt decimal(19,4) null,
    WindRun decimal(19,4) null,
    DewPointOut decimal(19,4) null,
    RainRateHiClicks decimal(19,4) null,
    WindDirOfPrevail decimal(19,4) null,
    Et decimal(19,4)null,
    AirDensity decimal(19,4) null,
    RainfallIn decimal(19,4) null,
    HeatIndexOut decimal(19,4) null,
    RainfallMm decimal(19,4) null,
    DegDaysCool decimal(19,4) null,
    RainRateHiIn decimal(19,4) null,
    WindNumSamples decimal(19,4) null,
    Emc decimal(19,4) null,
    RevType decimal(19,4) null,
    RainfallClicks decimal(19,4) null,
    AbsPress decimal(19,4) null
);

ALTER TABLE PROD_StationsData WITH CHECK
	ADD CONSTRAINT FK_StationsData_FincaStationData
	FOREIGN KEY (IDFincaStationData)
	REFERENCES PROD_FincaStationData(IDFincaStationData);



INSERT INTO PROD_StationsData (
    IDFincaStationData,
    FechaHora,
    IssReception,
    WindSpeedAvg,
    WindSpeedHi,
    WindDirOfHi,
    WindChill,
    DegDaysHeat,
    ThwIndex,
    Bar,
    HumOut,
    TempOut,
    TempOutLo,
    WetBulb,
    TempOutHi,
    BarAlt,
    ArchInt,
    WindRun,
    DewPointOut,
    RainRateHiClicks,
    WindDirOfPrevail,
    Et,
    AirDensity,
    RainfallIn,
    HeatIndexOut,
    RainfallMm,
    DegDaysCool,
    RainRateHiIn,
    WindNumSamples,
    Emc,
    RevType,
    RainfallClicks,
    AbsPress
)
VALUES
(148398, '2021-05-21 12:34:56-05:00', 3.0000, 3.1111, 2.2222, 2.3333, 2.4444, 4.5555, 8.6666, 5.7777, 9.8888, 3.9999, 5.00, 8.111, 3.2222, 2.3333, 2.4444, 2.5555, 2.6666, 2.705, 2.863, 2.9999, 3.55, 3.1111, 3.2222, 3.3333, 3.7852, 3.5555, 3.6666, 3.750, 3.8888, 3.9999, 4.575),
(148400, '2021-05-21 12:34:56-05:00', 4.0000, 4.1111, 23.2222, 3.3333, 3.4444, 6.5555, 2.6666, 7.7777, 4.8888, 5.9999, 5.0000, 7.1411, 3.2222, 3.3333, 3.4444, 3.5555, 3.6666, 3.852, 3.752, 3.9999, 4.222, 4.1111, 4.2222, 4.3333, 4.505, 4.5555, 4.6666, 4.7777, 4.8888, 4.9999, 5.7577);


SELECT* FROM PROD_StationsData