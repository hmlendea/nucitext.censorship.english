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
        private const string CensoredWordReplacement = "****";

        private static readonly Lazy<Regex> BadWordPattern = new(BuildBadWordPattern, true);
        private static readonly Lazy<Regex> UrlPattern = new(BuildUrlPattern, true);

        /// <inheritdoc/>
        public string Censor(string text)
        {
            ArgumentNullException.ThrowIfNull(text);

            string result = BadWordPattern.Value.Replace(text, CensoredWordReplacement);
            result = UrlPattern.Value.Replace(result, CensoredWordReplacement);

            return result;
        }

        /// <summary>
        /// Builds the compiled regex pattern for matching bad words at word boundaries.
        /// </summary>
        private static Regex BuildBadWordPattern()
        {
            IEnumerable<string> badWords = LoadWordList("bad.txt");
            string alternation = string.Join("|", badWords.Select(Regex.Escape));

            return new Regex(
                $@"\b({alternation})\b",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// Builds the compiled regex pattern for matching host.tld URLs at word boundaries.
        /// </summary>
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

        /// <summary>
        /// Loads a word list from an embedded resource.
        /// </summary>
        /// <param name="fileName">The name of the embedded resource file (e.g., "bad.txt").</param>
        /// <returns>An enumerable of non-empty, trimmed lines from the resource.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the embedded resource is not found.</exception>
        private static IEnumerable<string> LoadWordList(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"NuciText.Censorship.English.Data.{fileName}";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
            }

            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string content = reader.ReadToEnd();

            return content
                .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(line => line.Length > 0);
        }
    }
}
