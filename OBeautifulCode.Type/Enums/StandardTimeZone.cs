// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardTimeZone.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;
    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Specifies an ISO standard time zone.
    /// </summary>
    /// <remarks>
    /// Enum values generated via unit test.
    /// </remarks>
    public enum StandardTimeZone
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// (UTC-12:00) International Date Line West
        /// </summary>
        Dateline,

        /// <summary>
        /// (UTC-10:00) Aleutian Islands
        /// </summary>
        Aleutian,

        /// <summary>
        /// (UTC-10:00) Hawaii
        /// </summary>
        Hawaiian,

        /// <summary>
        /// (UTC-09:30) Marquesas Islands
        /// </summary>
        Marquesas,

        /// <summary>
        /// (UTC-09:00) Alaska
        /// </summary>
        Alaskan,

        /// <summary>
        /// (UTC-08:00) Baja California
        /// </summary>
        PacificMexico,

        /// <summary>
        /// (UTC-08:00) Pacific Time (US &amp; Canada)
        /// </summary>
        Pacific,

        /// <summary>
        /// (UTC-07:00) Arizona
        /// </summary>
        UnitedStatesMountain,

        /// <summary>
        /// (UTC-07:00) Chihuahua, La Paz, Mazatlan
        /// </summary>
        MountainMexico,

        /// <summary>
        /// (UTC-07:00) Mountain Time (US &amp; Canada)
        /// </summary>
        Mountain,

        /// <summary>
        /// (UTC-07:00) Yukon
        /// </summary>
        Yukon,

        /// <summary>
        /// (UTC-06:00) Central America
        /// </summary>
        CentralAmerica,

        /// <summary>
        /// (UTC-06:00) Central Time (US &amp; Canada)
        /// </summary>
        Central,

        /// <summary>
        /// (UTC-06:00) Easter Island
        /// </summary>
        EasterIsland,

        /// <summary>
        /// (UTC-06:00) Guadalajara, Mexico City, Monterrey
        /// </summary>
        CentralMexico,

        /// <summary>
        /// (UTC-06:00) Saskatchewan
        /// </summary>
        CanadaCentral,

        /// <summary>
        /// (UTC-05:00) Bogota, Lima, Quito, Rio Branco
        /// </summary>
        SouthAmericaPacific,

        /// <summary>
        /// (UTC-05:00) Chetumal
        /// </summary>
        EasternMexico,

        /// <summary>
        /// (UTC-05:00) Eastern Time (US &amp; Canada)
        /// </summary>
        Eastern,

        /// <summary>
        /// (UTC-05:00) Haiti
        /// </summary>
        Haiti,

        /// <summary>
        /// (UTC-05:00) Havana
        /// </summary>
        Cuba,

        /// <summary>
        /// (UTC-05:00) Indiana (East)
        /// </summary>
        UnitedStatesEastern,

        /// <summary>
        /// (UTC-05:00) Turks and Caicos
        /// </summary>
        TurksAndCaicos,

        /// <summary>
        /// (UTC-04:00) Asuncion
        /// </summary>
        Paraguay,

        /// <summary>
        /// (UTC-04:00) Atlantic Time (Canada)
        /// </summary>
        Atlantic,

        /// <summary>
        /// (UTC-04:00) Caracas
        /// </summary>
        Venezuela,

        /// <summary>
        /// (UTC-04:00) Cuiaba
        /// </summary>
        CentralBrazilian,

        /// <summary>
        /// (UTC-04:00) Georgetown, La Paz, Manaus, San Juan
        /// </summary>
        SouthAmericaWestern,

        /// <summary>
        /// (UTC-04:00) Santiago
        /// </summary>
        PacificSouthAmerica,

        /// <summary>
        /// (UTC-03:30) Newfoundland
        /// </summary>
        Newfoundland,

        /// <summary>
        /// (UTC-03:00) Araguaina
        /// </summary>
        Tocantins,

        /// <summary>
        /// (UTC-03:00) Brasilia
        /// </summary>
        EasternSouthAmerica,

        /// <summary>
        /// (UTC-03:00) Cayenne, Fortaleza
        /// </summary>
        SouthAmericaEastern,

        /// <summary>
        /// (UTC-03:00) City of Buenos Aires
        /// </summary>
        Argentina,

        /// <summary>
        /// (UTC-03:00) Greenland
        /// </summary>
        Greenland,

        /// <summary>
        /// (UTC-03:00) Montevideo
        /// </summary>
        Montevideo,

        /// <summary>
        /// (UTC-03:00) Punta Arenas
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Magallanes", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Magallanes,

        /// <summary>
        /// (UTC-03:00) Saint Pierre and Miquelon
        /// </summary>
        SaintPierre,

        /// <summary>
        /// (UTC-03:00) Salvador
        /// </summary>
        Bahia,

        /// <summary>
        /// (UTC-02:00) Mid-Atlantic - Old
        /// </summary>
        MidAtlantic,

        /// <summary>
        /// (UTC-01:00) Azores
        /// </summary>
        Azores,

        /// <summary>
        /// (UTC-01:00) Cabo Verde Is.
        /// </summary>
        CapeVerde,

        /// <summary>
        /// (UTC+00:00) Dublin, Edinburgh, Lisbon, London
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gmt", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Gmt,

        /// <summary>
        /// (UTC+00:00) Monrovia, Reykjavik
        /// </summary>
        Greenwich,

        /// <summary>
        /// (UTC+00:00) Sao Tome
        /// </summary>
        SaoTome,

        /// <summary>
        /// (UTC+01:00) Casablanca
        /// </summary>
        Morocco,

        /// <summary>
        /// (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna
        /// </summary>
        WesternEurope,

        /// <summary>
        /// (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague
        /// </summary>
        CentralEurope,

        /// <summary>
        /// (UTC+01:00) Brussels, Copenhagen, Madrid, Paris
        /// </summary>
        Romance,

        /// <summary>
        /// (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb
        /// </summary>
        CentralEuropean,

        /// <summary>
        /// (UTC+01:00) West Central Africa
        /// </summary>
        WesternCentralAfrica,

        /// <summary>
        /// (UTC+02:00) Amman
        /// </summary>
        Jordan,

        /// <summary>
        /// (UTC+02:00) Athens, Bucharest
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gtb", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Gtb,

        /// <summary>
        /// (UTC+02:00) Beirut
        /// </summary>
        MiddleEast,

        /// <summary>
        /// (UTC+02:00) Cairo
        /// </summary>
        Egypt,

        /// <summary>
        /// (UTC+02:00) Chisinau
        /// </summary>
        EasternEurope,

        /// <summary>
        /// (UTC+02:00) Damascus
        /// </summary>
        Syria,

        /// <summary>
        /// (UTC+02:00) Gaza, Hebron
        /// </summary>
        WestBank,

        /// <summary>
        /// (UTC+02:00) Harare, Pretoria
        /// </summary>
        SouthAfrica,

        /// <summary>
        /// (UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fle", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Fle,

        /// <summary>
        /// (UTC+02:00) Jerusalem
        /// </summary>
        Israel,

        /// <summary>
        /// (UTC+02:00) Juba
        /// </summary>
        SouthSudan,

        /// <summary>
        /// (UTC+02:00) Kaliningrad
        /// </summary>
        Kaliningrad,

        /// <summary>
        /// (UTC+02:00) Khartoum
        /// </summary>
        Sudan,

        /// <summary>
        /// (UTC+02:00) Tripoli
        /// </summary>
        Libya,

        /// <summary>
        /// (UTC+02:00) Windhoek
        /// </summary>
        Namibia,

        /// <summary>
        /// (UTC+03:00) Baghdad
        /// </summary>
        Arabic,

        /// <summary>
        /// (UTC+03:00) Istanbul
        /// </summary>
        Turkey,

        /// <summary>
        /// (UTC+03:00) Kuwait, Riyadh
        /// </summary>
        Arab,

        /// <summary>
        /// (UTC+03:00) Minsk
        /// </summary>
        Belarus,

        /// <summary>
        /// (UTC+03:00) Moscow, St. Petersburg
        /// </summary>
        Russian,

        /// <summary>
        /// (UTC+03:00) Nairobi
        /// </summary>
        EasternAfrica,

        /// <summary>
        /// (UTC+03:00) Volgograd
        /// </summary>
        Volgograd,

        /// <summary>
        /// (UTC+03:30) Tehran
        /// </summary>
        Iran,

        /// <summary>
        /// (UTC+04:00) Abu Dhabi, Muscat
        /// </summary>
        Arabian,

        /// <summary>
        /// (UTC+04:00) Astrakhan, Ulyanovsk
        /// </summary>
        Astrakhan,

        /// <summary>
        /// (UTC+04:00) Baku
        /// </summary>
        Azerbaijan,

        /// <summary>
        /// (UTC+04:00) Port Louis
        /// </summary>
        Mauritius,

        /// <summary>
        /// (UTC+04:00) Saratov
        /// </summary>
        Saratov,

        /// <summary>
        /// (UTC+04:00) Tbilisi
        /// </summary>
        Georgian,

        /// <summary>
        /// (UTC+04:00) Yerevan
        /// </summary>
        Caucasus,

        /// <summary>
        /// (UTC+04:30) Kabul
        /// </summary>
        Afghanistan,

        /// <summary>
        /// (UTC+05:00) Ashgabat, Tashkent
        /// </summary>
        WestAsia,

        /// <summary>
        /// (UTC+05:00) Ekaterinburg
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ekaterinburg", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Ekaterinburg,

        /// <summary>
        /// (UTC+05:00) Islamabad, Karachi
        /// </summary>
        Pakistan,

        /// <summary>
        /// (UTC+05:00) Qyzylorda
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Qyzylorda", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Qyzylorda,

        /// <summary>
        /// (UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi
        /// </summary>
        India,

        /// <summary>
        /// (UTC+05:30) Sri Jayawardenepura
        /// </summary>
        SriLanka,

        /// <summary>
        /// (UTC+05:45) Kathmandu
        /// </summary>
        Nepal,

        /// <summary>
        /// (UTC+06:00) Astana
        /// </summary>
        CentralAsia,

        /// <summary>
        /// (UTC+06:00) Dhaka
        /// </summary>
        Bangladesh,

        /// <summary>
        /// (UTC+06:00) Omsk
        /// </summary>
        Omsk,

        /// <summary>
        /// (UTC+06:30) Yangon (Rangoon)
        /// </summary>
        Myanmar,

        /// <summary>
        /// (UTC+07:00) Bangkok, Hanoi, Jakarta
        /// </summary>
        SoutheastAsia,

        /// <summary>
        /// (UTC+07:00) Barnaul, Gorno-Altaysk
        /// </summary>
        Altai,

        /// <summary>
        /// (UTC+07:00) Hovd
        /// </summary>
        WesternMongolia,

        /// <summary>
        /// (UTC+07:00) Krasnoyarsk
        /// </summary>
        NorthAsia,

        /// <summary>
        /// (UTC+07:00) Novosibirsk
        /// </summary>
        NorthernCentralAsia,

        /// <summary>
        /// (UTC+07:00) Tomsk
        /// </summary>
        Tomsk,

        /// <summary>
        /// (UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi
        /// </summary>
        China,

        /// <summary>
        /// (UTC+08:00) Irkutsk
        /// </summary>
        NorthAsiaEast,

        /// <summary>
        /// (UTC+08:00) Kuala Lumpur, Singapore
        /// </summary>
        Singapore,

        /// <summary>
        /// (UTC+08:00) Perth
        /// </summary>
        WesternAustralia,

        /// <summary>
        /// (UTC+08:00) Taipei
        /// </summary>
        Taipei,

        /// <summary>
        /// (UTC+08:00) Ulaanbaatar
        /// </summary>
        Ulaanbaatar,

        /// <summary>
        /// (UTC+08:45) Eucla
        /// </summary>
        AustraliaCentralWestern,

        /// <summary>
        /// (UTC+09:00) Chita
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Transbaikal", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Transbaikal,

        /// <summary>
        /// (UTC+09:00) Osaka, Sapporo, Tokyo
        /// </summary>
        Tokyo,

        /// <summary>
        /// (UTC+09:00) Pyongyang
        /// </summary>
        NorthKorea,

        /// <summary>
        /// (UTC+09:00) Seoul
        /// </summary>
        Korea,

        /// <summary>
        /// (UTC+09:00) Yakutsk
        /// </summary>
        Yakutsk,

        /// <summary>
        /// (UTC+09:30) Adelaide
        /// </summary>
        CentralAustralia,

        /// <summary>
        /// (UTC+09:30) Darwin
        /// </summary>
        AustraliaCentral,

        /// <summary>
        /// (UTC+10:00) Brisbane
        /// </summary>
        EasternAustralia,

        /// <summary>
        /// (UTC+10:00) Canberra, Melbourne, Sydney
        /// </summary>
        AustraliaEastern,

        /// <summary>
        /// (UTC+10:00) Guam, Port Moresby
        /// </summary>
        WestPacific,

        /// <summary>
        /// (UTC+10:00) Hobart
        /// </summary>
        Tasmania,

        /// <summary>
        /// (UTC+10:00) Vladivostok
        /// </summary>
        Vladivostok,

        /// <summary>
        /// (UTC+10:30) Lord Howe Island
        /// </summary>
        LordHowe,

        /// <summary>
        /// (UTC+11:00) Bougainville Island
        /// </summary>
        Bougainville,

        /// <summary>
        /// (UTC+11:00) Magadan
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Magadan", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        Magadan,

        /// <summary>
        /// (UTC+11:00) Norfolk Island
        /// </summary>
        Norfolk,

        /// <summary>
        /// (UTC+11:00) Sakhalin
        /// </summary>
        Sakhalin,

        /// <summary>
        /// (UTC+11:00) Solomon Is., New Caledonia
        /// </summary>
        CentralPacific,

        /// <summary>
        /// (UTC+12:00) Auckland, Wellington
        /// </summary>
        NewZealand,

        /// <summary>
        /// (UTC+12:00) Fiji
        /// </summary>
        Fiji,

        /// <summary>
        /// (UTC+12:00) Petropavlovsk-Kamchatsky - Old
        /// </summary>
        Kamchatka,

        /// <summary>
        /// (UTC+12:45) Chatham Islands
        /// </summary>
        ChathamIslands,

        /// <summary>
        /// (UTC+13:00) Nuku'alofa
        /// </summary>
        Tonga,

        /// <summary>
        /// (UTC+13:00) Samoa
        /// </summary>
        Samoa,

        /// <summary>
        /// (UTC+14:00) Kiritimati Island
        /// </summary>
        LineIslands,
    }
}
