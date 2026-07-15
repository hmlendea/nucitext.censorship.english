using NUnit.Framework;

using NuciText.Censorship.English;

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
        public void GivenTextWithBadWord_WhenCensoring_ThenBadWordIsReplaced(
            string input,
            string expectedOutput)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(expectedOutput));

        [TestCase("ARSE", "****")]
        [TestCase("Arse", "****")]
        [TestCase("aRsE", "****")]
        public void GivenTextWithUppercaseBadWord_WhenCensoring_ThenBadWordIsReplaced(
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

        [TestCase("Hello, World!")]
        [TestCase("Solaire of Astora said hello.")]
        [TestCase("Vasile Ciupitu is a good person.")]
        [TestCase("")]
        public void GivenCleanText_WhenCensoring_ThenTextIsReturned(string text)
            => Assert.That(
                subject.Censor(text),
                Is.EqualTo(text));

        [TestCase("Visit abercrombie.ac now.", "Visit **** now.")]
        [TestCase("Go to acmemail.com for details.", "Go to **** for details.")]
        public void GivenTextWithUrl_WhenCensoring_ThenUrlIsReplaced(
            string input,
            string expectedOutput)
            => Assert.That(
                subject.Censor(input),
                Is.EqualTo(expectedOutput));

        [TestCase("arsenal")]
        [TestCase("analysis")]
        [TestCase("classical")]
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
    }
}
