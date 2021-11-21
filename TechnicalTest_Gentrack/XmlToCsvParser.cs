using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnicalTest_Gentrack.Models;

namespace TechnicalTest_Gentrack
{
    public class XmlToCsvParser
    {
        private CsvValidator _csvValidator;
        private CsvSplitter _csvSplitter;
        private FileHandler _fileHandler;

        public XmlToCsvParser()
        {
            _csvValidator = new();
            _csvSplitter  = new();
            _fileHandler = new();
        }

        public void Parse(string filename, string destinationFolder)
        {
            var csvIntervalDatas = _fileHandler.LoadXmlFromFile(filename);

            foreach (var data in csvIntervalDatas)
            {
                if (_csvValidator.IsValidCsvIntervalData(data))
                {
                    var csvData = _csvSplitter.SplitCsv(data);
                    _fileHandler.ExportCsv(destinationFolder, csvData);
                }
            }
        }
    }
}
