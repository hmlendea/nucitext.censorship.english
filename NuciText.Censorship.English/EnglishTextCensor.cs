using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NuciText.Censorship.English
{
    /// <summary>
    /// Censors English text by replacing bad words and URLs with asterisks.
    /// </summary>
    public sealed class EnglishTextCensor : ITextCensor
    {
        private static string CensoredWordReplacement => "****";

        private static readonly Regex BadWordPattern = BuildBadWordPattern();
        private static readonly Regex UrlPattern = BuildUrlPattern();

        /// <inheritdoc/>
        public string Censor(string text)
        {
            string result = BadWordPattern.Replace(text, CensoredWordReplacement);
            result = UrlPattern.Replace(result, CensoredWordReplacement);

            return result;
        }

        private static Regex BuildBadWordPattern()
        {
            IEnumerable<string> badWords = LoadWordList("bad.txt");
            string alternation = string.Join("|", badWords.Select(Regex.Escape));

            return new Regex(
                $@"\b({alternation})\b",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        private static Regex BuildUrlPattern()
        {
            IEnumerable<string> hosts = LoadWordList("hosts.txt");
            IEnumerable<string> tlds = LoadWordList("tlds.txt");

            string hostAlternation = string.Join("|", hosts.Select(Regex.Escape));
            string tldAlternation = string.Join("|", tlds.Select(Regex.Escape));

            return new Regex(
                $@"\b({hostAlternation})\.({tldAlternation})\b",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        private static IEnumerable<string> LoadWordList(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"NuciText.Censorship.English.Data.{fileName}";

            using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            return content
                .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                .Where(line => !string.IsNullOrWhiteSpace(line));
        }
    }
}
