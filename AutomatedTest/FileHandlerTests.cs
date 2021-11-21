using TechnicalTest_Gentrack;
using Xunit;

namespace AutomatedTest
{
    [Collection(name: "XML to CSV parser")]
    public class FileHandlerTests
    {
        private readonly FileHandler _fileHandler;
        private readonly CsvValidator _csvValidator;

        public FileHandlerTests(FileHandler fileHandler, CsvValidator csvValidator)
        {
            _fileHandler = fileHandler;
            _csvValidator = csvValidator;
        }

        [Fact]
        public void LoadFromXmlTest()
        {
            var csvData = _fileHandler.LoadXmlFromFile("testfile.xml");

            foreach (var csv in csvData) {
                
                Assert.Contains("URENRGY", csv.Value);
                var valid = _csvValidator.IsValidCsvIntervalData(csv);
                Assert.True(valid);
            }
        }

    }
}
