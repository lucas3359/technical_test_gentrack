using TechnicalTest_Gentrack;
using TechnicalTest_Gentrack.Models;
using Xunit;

namespace AutomatedTest
{
    [Collection(name: "XML to CSV parser")]
    public class CsvValidatorTests
    {
        private readonly CsvValidator _csvValidator;
        private readonly FileHandler _fileHandler;
        public CsvValidatorTests(CsvValidator csvValidator, FileHandler fileHandler)
        {
            _csvValidator = csvValidator;
            _fileHandler = fileHandler;
        }

        [Theory]
        [InlineData("testfile-invalid-starting.xml", "Starting column not valid, can only be: (100, 200, 300, 900)")]
        [InlineData("testfile-not-include-900.xml", "Element should contain at least one of: (100, 200, 300, 900)")]
        [InlineData("testfile-multiple-100.xml", "Element should have exactly one header and trailer row")]
        [InlineData("testfile-content-wrong-place.xml", "Content should be between header and trailer rows")]
        [InlineData("testfile-200-without-300.xml", "Content 200 should be followed by at least one 300 row")]
        public void ValidateCsv_ExceptionTests(string xmlFileName, string expectedExceptionMessage)
        {
            var xmlData = _fileHandler.LoadXmlFromFile(xmlFileName);

            foreach (var data in xmlData)
            {
                var exception = Assert.Throws<ValidationException>(() => _csvValidator.IsValidCsvIntervalData(data));
                Assert.Equal(expectedExceptionMessage, exception.Message);
            }
        }

    }
}
