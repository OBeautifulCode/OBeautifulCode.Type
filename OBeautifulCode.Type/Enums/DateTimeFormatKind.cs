// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Specifies the kind of formatting to apply to a <see cref="DateTimeFormatKind"/>.
    /// </summary>
    public enum DateTimeFormatKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// 2009-06-15T13:45:30 -> 6/15/2009 (en-US)
        /// 2009-06-15T13:45:30 -> 15/06/2009 (fr-FR)
        /// 2009-06-15T13:45:30 -> 2009/06/15 (ja-JP)
        /// </summary>
        ShortDatePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> Monday, June 15, 2009 (en-US)
        /// 2009-06-15T13:45:30 -> 15 июня 2009 г. (ru-RU)
        /// 2009-06-15T13:45:30 -> Montag, 15. Juni 2009 (de-DE)
        /// </summary>
        LongDatePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> Monday, June 15, 2009 1:45 PM (en-US)
        /// 2009-06-15T13:45:30 -> den 15 juni 2009 13:45 (sv-SE)
        /// 2009-06-15T13:45:30 -> Δευτέρα, 15 Ιουνίου 2009 1:45 μμ (el-GR)
        /// </summary>
        FullDateTimePatternShortTime,

        /// <summary>
        /// 2009-06-15T13:45:30 -> Monday, June 15, 2009 1:45:30 PM (en-US)
        /// 2009-06-15T13:45:30 -> den 15 juni 2009 13:45:30 (sv-SE)
        /// 2009-06-15T13:45:30 -> Δευτέρα, 15 Ιουνίου 2009 1:45:30 μμ (el-GR)
        /// </summary>
        FullDateTimePatternLongTime,

        /// <summary>
        /// 2009-06-15T13:45:30 -> 6/15/2009 1:45 PM (en-US)
        /// 2009-06-15T13:45:30 -> 15/06/2009 13:45 (es-ES)
        /// 2009-06-15T13:45:30 -> 2009/6/15 13:45 (zh-CN)
        /// </summary>
        GeneralDateTimePatternShortTime,

        /// <summary>
        /// 2009-06-15T13:45:30 -> 6/15/2009 1:45:30 PM (en-US)
        /// 2009-06-15T13:45:30 -> 15/06/2009 13:45:30 (es-ES)
        /// 2009-06-15T13:45:30 -> 2009/6/15 13:45:30 (zh-CN)
        /// </summary>
        GeneralDateTimePatternLongTime,

        /// <summary>
        /// 2009-06-15T13:45:30 -> June 15 (en-US)
        /// 2009-06-15T13:45:30 -> 15. juni (da-DK)
        /// 2009-06-15T13:45:30 -> 15 Juni (id-ID)
        /// </summary>
        MonthDayPattern,

        /// <summary>
        /// DateTime values:
        /// 2009-06-15T13:45:30 (DateTimeKind.Local) --> 2009-06-15T13:45:30.0000000-07:00
        /// 2009-06-15T13:45:30 (DateTimeKind.Utc) --> 2009-06-15T13:45:30.0000000Z
        /// 2009-06-15T13:45:30 (DateTimeKind.Unspecified) --> 2009-06-15T13:45:30.0000000
        ///
        /// DateTimeOffset values:
        /// 2009-06-15T13:45:30-07:00 --> 2009-06-15T13:45:30.0000000-07:00
        /// </summary>
        RoundtripDateTimePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> Mon, 15 Jun 2009 20:45:30 GMT
        /// </summary>
        Rfc1123Pattern,

        /// <summary>
        /// 2009-06-15T13:45:30 (DateTimeKind.Local) -> 2009-06-15T13:45:30
        /// 2009-06-15T13:45:30 (DateTimeKind.Utc) -> 2009-06-15T13:45:30
        /// </summary>
        SortableDateTimePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> 1:45 PM (en-US)
        /// 2009-06-15T13:45:30 -> 13:45 (hr-HR)
        /// 2009-06-15T13:45:30 -> 01:45 م (ar-EG)
        /// </summary>
        ShortTimePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> 1:45:30 PM (en-US)
        /// 2009-06-15T13:45:30 -> 13:45:30 (hr-HR)
        /// 2009-06-15T13:45:30 -> 01:45:30 م (ar-EG)
        /// </summary>
        LongTimePattern,

        /// <summary>
        /// With a DateTime value: 2009-06-15T13:45:30 -> 2009-06-15 13:45:30Z
        /// With a DateTimeOffset value: 2009-06-15T13:45:30 -> 2009-06-15 20:45:30Z
        /// </summary>
        UniversalSortableDateTimePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> Monday, June 15, 2009 8:45:30 PM (en-US)
        /// 2009-06-15T13:45:30 -> den 15 juni 2009 20:45:30 (sv-SE)
        /// 2009-06-15T13:45:30 -> Δευτέρα, 15 Ιουνίου 2009 8:45:30 μμ (el-GR)
        /// </summary>
        UniversalFullDateTimePattern,

        /// <summary>
        /// 2009-06-15T13:45:30 -> June 2009 (en-US)
        /// 2009-06-15T13:45:30 -> juni 2009 (da-DK)
        /// 2009-06-15T13:45:30 -> Juni 2009 (id-ID)
        /// </summary>
        YearMonthPattern,
    }
}
