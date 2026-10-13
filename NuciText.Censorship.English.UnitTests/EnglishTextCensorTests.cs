using System;
using System.Linq;
using NUnit.Framework;

namespace NuciText.Censorship.English.UnitTests
{
    [TestFixture]
    public sealed class EnglishTextCensorTests
    {
        private EnglishTextCensor subject = null!;

        [SetUp]
        public void SetUp()
            => subject = new EnglishTextCensor();

        [TestCase("You arse!", "You ****!")]
        [TestCase("You anal person.", "You **** person.")]
        [TestCase("arse you", "**** you")]
        [TestCase("What a bastard!", "What a ****!")]
        [TestCase("Don't be a bitch.", "Don't be a ****.")]
        [TestCase("That's shit.", "That's ****.")]
        [TestCase("Go to hell.", "Go to hell.")] // hell not in bad.txt
        [TestCase("You're a cunt.", "You're a ****.")]
        [TestCase("What the fuck?", "What the ****?")]
        [TestCase("Shit happens.", "**** happens.")]
        [TestCase("Damn it!", "**** it!")]
        public void GivenTextWithBadWord_WhenCensoring_ThenBadWordIsReplaced(
            string input,
            string expectedOutput)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(expectedOutput));

        [TestCase("ARSE", "****")]
        [TestCase("Arse", "****")]
        [TestCase("aRsE", "****")]
        [TestCase("Bastard", "****")]
        [TestCase("BITCH", "****")]
        [TestCase("SHIT", "****")]
        [TestCase("DAMN", "****")]
        [TestCase("CuNt", "****")]
        [TestCase("FuCk", "****")]
        public void GivenTextWithMixedCaseBadWord_WhenCensoring_ThenBadWordIsReplaced(
            string input,
            string expectedOutput)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(expectedOutput));

        [Test]
        public void GivenTextWithMultipleBadWords_WhenCensoring_ThenAllBadWordsAreReplaced()
            => Assert.That(
                subject.Censor("arse and anal"),
                Is.EqualTo("**** and ****"));

        [Test]
        public void GivenTextWithMultipleDifferentBadWords_WhenCensoring_ThenAllAreReplaced()
            => Assert.That(
                subject.Censor("arse bastard bitch cunt"),
                Is.EqualTo("**** **** **** ****"));

        [TestCase("Hello, World!")]
        [TestCase("Solaire of Astora said hello.")]
        [TestCase("Vasile Ciupitu is a good person.")]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("12345")]
        [TestCase("No bad words here.")]
        [TestCase("Clean text with numbers 123 and symbols @#$%.")]
        public void GivenCleanText_WhenCensoring_ThenTextIsReturned(string text)
            => Assert.That(
                subject.Censor(text),
                Is.EqualTo(text));

        [TestCase("Visit abercrombie.ac now.", "Visit **** now.")]
        [TestCase("Go to acmemail.com for details.", "Go to **** for details.")]
        [TestCase("Check out aol.com today.", "Check out **** today.")]
        [TestCase("Email me at hotmail.com", "Email me at ****")]
        [TestCase("Visit yahoo.com and aol.com", "Visit **** and ****")]
        public void GivenTextWithUrl_WhenCensoring_ThenUrlIsReplaced(
            string input,
            string expectedOutput)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(expectedOutput));

        [TestCase("arsenal")]
        [TestCase("analysis")]
        [TestCase("classical")]
        [TestCase("passionate")]
        [TestCase("assassin")]
        [TestCase("bass")]
        [TestCase("class")]
        [TestCase("grass")]
        [TestCase("massive")]
        [TestCase("pass")]
        public void GivenBadWordAsPartOfLargerWord_WhenCensoring_ThenLargerWordIsUnchanged(
            string text)
            => Assert.That(
                subject.Censor(text),
                Is.EqualTo(text));

        [Test]
        public void GivenTextWithBadWordAndUrl_WhenCensoring_ThenBothAreCensored()
            => Assert.That(
                subject.Censor("arse at abercrombie.ac"),
                Is.EqualTo("**** at ****"));

        [Test]
        public void GivenNullInput_WhenCensoring_ThenThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => subject.Censor(null!));

        [TestCase("arse!")]
        [TestCase("arse?")]
        [TestCase("arse.")]
        [TestCase("arse,")]
        [TestCase("arse;")]
        [TestCase("arse:")]
        public void GivenBadWordWithTrailingPunctuation_WhenCensoring_ThenBadWordIsReplaced(string input)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo("****" + input.Substring(4)));

        [TestCase("(arse)")]
        [TestCase("[arse]")]
        [TestCase("{arse}")]
        [TestCase("\"arse\"")]
        [TestCase("'arse'")]
        public void GivenBadWordWithSurroundingPunctuation_WhenCensoring_ThenBadWordIsReplaced(string input)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(input.Replace("arse", "****")));

        [TestCase("arse\n")]
        [TestCase("arse\t")]
        [TestCase("arse\r")]
        [TestCase("arse\r\n")]
        public void GivenBadWordWithWhitespace_WhenCensoring_ThenBadWordIsReplaced(string input)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo("****" + input.Substring(4)));

        [Test]
        public void GivenTextWithUrlAtStart_WhenCensoring_ThenUrlIsReplaced()
            => Assert.That(
                subject.Censor("yahoo.com is a search engine"),
                Is.EqualTo("**** is a search engine"));

        [Test]
        public void GivenTextWithUrlAtEnd_WhenCensoring_ThenUrlIsReplaced()
            => Assert.That(
                subject.Censor("Search on yahoo.com"),
                Is.EqualTo("Search on ****"));

        [Test]
        public void GivenTextWithMultipleUrls_WhenCensoring_ThenAllUrlsAreReplaced()
            => Assert.That(
                subject.Censor("Visit yahoo.com and aol.com and hotmail.com"),
                Is.EqualTo("Visit **** and **** and ****"));

        [Test]
        public void GivenTextWithUrlAndPunctuation_WhenCensoring_ThenUrlIsReplaced()
            => Assert.That(
                subject.Censor("Visit yahoo.com!"),
                Is.EqualTo("Visit ****!"));

        [Test]
        public void GivenTextWithSubdomain_WhenCensoring_ThenOnlyHostTldIsReplaced()
            => Assert.That(
                subject.Censor("Visit mail.google.com"),
                Is.EqualTo("Visit mail.google.com"));

        [Test]
        public void GivenTextWithIpAddress_WhenCensoring_ThenIpIsUnchanged()
            => Assert.That(
                subject.Censor("Connect to 192.168.1.1"),
                Is.EqualTo("Connect to 192.168.1.1"));

        [Test]
        public void GivenTextWithEmail_WhenCensoring_ThenEmailIsUnchanged()
            => Assert.That(
                subject.Censor("Email user@example.com"),
                Is.EqualTo("Email user@example.com"));

        [Test]
        public void GivenTextWithPathAfterUrl_WhenCensoring_ThenOnlyHostTldIsReplaced()
            => Assert.That(
                subject.Censor("Visit google.com/search"),
                Is.EqualTo("Visit google.com/search"));

        [Test]
        public void GivenTextWithPortAfterUrl_WhenCensoring_ThenOnlyHostTldIsReplaced()
            => Assert.That(
                subject.Censor("Connect to localhost:8080"),
                Is.EqualTo("Connect to localhost:8080"));

        [Test]
        public void GivenRepeatedCalls_WhenCensoring_ThenResultsAreConsistent()
        {
            string input = "arse bastard yahoo.com";
            string expected = "**** **** ****";

            for (int i = 0; i < 100; i++)
            {
                Assert.That(subject.Censor(input), Is.EqualTo(expected));
            }
        }

        [Test]
        public void GivenLongText_WhenCensoring_ThenPerformanceIsAcceptable()
        {
            string longText = string.Join(" ", Enumerable.Repeat("arse yahoo.com", 100));
            string expected = string.Join(" ", Enumerable.Repeat("**** ****", 100));

            string result = subject.Censor(longText);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GivenUnicodeText_WhenCensoring_ThenHandledCorrectly()
            => Assert.That(
                subject.Censor("arse café naïve résumé"),
                Is.EqualTo("**** café naïve résumé"));

        [Test]
        public void GivenEmojiText_WhenCensoring_ThenHandledCorrectly()
            => Assert.That(
                subject.Censor("arse 😀 🎉"),
                Is.EqualTo("**** 😀 🎉"));
    }
}
