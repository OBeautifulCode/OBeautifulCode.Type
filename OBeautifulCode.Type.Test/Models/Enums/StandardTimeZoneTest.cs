// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardTimeZoneTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.IO;
    using OBeautifulCode.String.Recipes;
    using Xunit;
    using static System.FormattableString;

    public static class StandardTimeZoneTest
    {
        [Fact(Skip = "Used to true-up TimeZone.cs")]
        public static void Generate_TimeZone_Values()
        {
            var outputFilesRootDirectory = "d:\\";

            var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones();

            var enumValuesFilePath = Path.Combine(outputFilesRootDirectory, "time-zone-enum-values.txt");
            File.Delete(enumValuesFilePath);

            var allInfoFilePath = Path.Combine(outputFilesRootDirectory, "time-zones.csv");
            File.Delete(allInfoFilePath);

            var timeZoneInfoCaseStatementsFilePath = Path.Combine(outputFilesRootDirectory, "time-zone-info-case-statements.txt");
            File.Delete(timeZoneInfoCaseStatementsFilePath);

            File.AppendAllText(allInfoFilePath, Invariant($"id,display-name,standard-name,daylight-name,supports-daylight-savings{Environment.NewLine}"));

            File.AppendAllText(enumValuesFilePath, Invariant($"/// <summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// Unknown (default).{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// </summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"Unknown,{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Environment.NewLine);

            foreach (var timeZoneInfo in timeZoneInfos)
            {
                if (!timeZoneInfo.Id.Contains("Standard Time"))
                {
                    continue;
                }

                File.AppendAllText(allInfoFilePath, Invariant($"{timeZoneInfo.Id.ToCsvSafe()},{timeZoneInfo.DisplayName.ToCsvSafe()},{timeZoneInfo.StandardName.ToCsvSafe()},{timeZoneInfo.DaylightName.ToCsvSafe()},{timeZoneInfo.SupportsDaylightSavingTime}{Environment.NewLine}"));

                var enumValueName = timeZoneInfo.GetEnumValueName();

                File.AppendAllText(enumValuesFilePath, Invariant($"/// <summary>{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"/// {timeZoneInfo.DisplayName.Replace("&", "&amp;")}{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"/// </summary>{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"{enumValueName},{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Environment.NewLine);

                File.AppendAllText(timeZoneInfoCaseStatementsFilePath, Invariant($"case StandardTimeZone.{enumValueName}:{Environment.NewLine}"));
                File.AppendAllText(timeZoneInfoCaseStatementsFilePath, Invariant($"    serializedString = @\"{timeZoneInfo.ToSerializedString()}\";{Environment.NewLine}"));
                File.AppendAllText(timeZoneInfoCaseStatementsFilePath, Invariant($"    break;{Environment.NewLine}"));
            }
        }

        private static string GetEnumValueName(
            this TimeZoneInfo timeZoneInfo)
        {
            var result = timeZoneInfo.Id;

            var findAndReplaceItems = new[]
            {
                new { Find = "UTC-", ReplaceWith ="UtcMinus" },
                new { Find = "UTC+", ReplaceWith ="UtcPlus" },
                new { Find = "UTC", ReplaceWith ="Utc" },
                new { Find = "SA", ReplaceWith ="SouthAmerica" },
                new { Find = "E.", ReplaceWith ="Eastern" },
                new { Find = "W.", ReplaceWith ="Western" },
                new { Find = "N.", ReplaceWith ="Northern" },
                new { Find = "AUS", ReplaceWith ="Australia" },
                new { Find = "US", ReplaceWith ="UnitedStates" },
                new { Find = "GTB", ReplaceWith ="Gtb" },
                new { Find = "FLE", ReplaceWith ="Fle" },
                new { Find = "SE", ReplaceWith ="Southeast" },
                new { Find = "Aus ", ReplaceWith ="Australia" },
                new { Find = "Cen. ", ReplaceWith ="Central" },
                new { Find = "GMT", ReplaceWith ="Gmt" },
                new { Find = "Standard Time", ReplaceWith = string.Empty },
            };

            foreach (var findAndReplaceItem in findAndReplaceItems)
            {
                result = result.Replace(findAndReplaceItem.Find, findAndReplaceItem.ReplaceWith);
            }

            result = result.ToAlphanumeric();

            return result;
        }
    }
}
