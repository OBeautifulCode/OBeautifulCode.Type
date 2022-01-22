// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureKindTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Globalization;
    using System.IO;
    using OBeautifulCode.String.Recipes;
    using Xunit;
    using static System.FormattableString;

    public static class CultureKindTest
    {
        [Fact(Skip = "Used to true-up CultureKind.cs and CultureKindExtensions.cs")]
        public static void Generate_Culture_Related_Code()
        {
            var outputFilesRootDirectory = "d:\\";

            var enumValuesFilePath = Path.Combine(outputFilesRootDirectory, "culture-enum-values.txt");
            File.Delete(enumValuesFilePath);

            var allInfoFilePath = Path.Combine(outputFilesRootDirectory, "cultures.csv");
            File.Delete(allInfoFilePath);

            var cultureNameDictionaryEntriesFilePath = Path.Combine(outputFilesRootDirectory, "culture-name-dictionary-entries.txt");
            File.Delete(cultureNameDictionaryEntriesFilePath);

            File.AppendAllText(allInfoFilePath, Invariant($"name,english-name,display-name,two-letter-iso-language-name,lcid{Environment.NewLine}"));

            File.AppendAllText(enumValuesFilePath, Invariant($"/// <summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// Unknown (default).{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// </summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"Unknown,{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Environment.NewLine);

            File.AppendAllText(enumValuesFilePath, Invariant($"/// <summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// Invariant culture.{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"/// </summary>{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Invariant($"Invariant,{Environment.NewLine}"));
            File.AppendAllText(enumValuesFilePath, Environment.NewLine);

            var cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var cultureInfo in cultureInfos)
            {
                File.AppendAllText(allInfoFilePath, Invariant($"{cultureInfo.Name.ToCsvSafe()},{cultureInfo.EnglishName.ToCsvSafe()},{cultureInfo.DisplayName.ToCsvSafe()},{cultureInfo.TwoLetterISOLanguageName.ToCsvSafe()},{cultureInfo.LCID}{Environment.NewLine}"));

                var enumValueName = cultureInfo.GetEnumValueName();

                File.AppendAllText(enumValuesFilePath, Invariant($"/// <summary>{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"/// {cultureInfo.Name}: {cultureInfo.EnglishName.Replace("&", "&amp;")}{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"/// </summary>{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Invariant($"{enumValueName},{Environment.NewLine}"));
                File.AppendAllText(enumValuesFilePath, Environment.NewLine);

                File.AppendAllText(cultureNameDictionaryEntriesFilePath, Invariant($"{{ CultureKind.{enumValueName}, @\"{cultureInfo.Name}\" }},{Environment.NewLine}"));
            }
        }

        private static string GetEnumValueName(
            this CultureInfo cultureInfo)
        {
            var result = cultureInfo.EnglishName;

            var findAndReplaceItems = new[]
            {
                new { Find = "siSwati", ReplaceWith = "SiSwati" },
                new { Find = "isiZulu", ReplaceWith = "IsiZulu" },
                new { Find = "isiXhosa", ReplaceWith = "IsiXhosa" },
                new { Find = "SAR", ReplaceWith = "Sar" },
                new { Find = "DRC", ReplaceWith = "Drc" },
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
