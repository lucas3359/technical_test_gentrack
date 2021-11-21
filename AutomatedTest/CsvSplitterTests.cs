using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest_Gentrack;
using Xunit;

namespace AutomatedTest
{
    [Collection(name: "XML to CSV parser")]
    public class CsvSplitterTests
    {
        private readonly CsvSplitter _csvSplitter;
        private readonly FileHandler _fileHandler;
        public CsvSplitterTests(CsvSplitter csvSplitter, FileHandler fileHandler)
        {
            _csvSplitter = csvSplitter;
            _fileHandler = fileHandler;
        }

        [Theory]
        [InlineData("testfile.xml",2)]
        [InlineData("testfile-empty-csv.xml", 0)]
        public void SplitCsv_Tests(string fileName, int numberOfCsvSplitted)
        {
            var xmlData = _fileHandler.LoadXmlFromFile(fileName);

            foreach (var data in xmlData)
            {
                var output = _csvSplitter.SplitCsv(data);
                Assert.Equal(numberOfCsvSplitted, output.Count);
            }
        }

    }
}
