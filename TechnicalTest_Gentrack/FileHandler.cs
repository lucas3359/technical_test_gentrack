using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnicalTest_Gentrack.Models;

namespace TechnicalTest_Gentrack
{
    public class FileHandler
    {
        public IEnumerable<CsvIntervalData> LoadXmlFromFile(string fileName)
        {
            var document = XDocument.Load(fileName);
            return ParseXml(document);
        }

        public IEnumerable<CsvIntervalData> LoadXmlFromString(string xml)
        {
            var document = XDocument.Parse(xml);
            return ParseXml(document);
        }

        private IEnumerable<CsvIntervalData> ParseXml(XDocument document)
        {
            var csvData = from transaction in document.Descendants("CSVIntervalData")
                          select new CsvIntervalData
                          {
                              Value = transaction.Value
                          };
            return csvData;
        }

        public void ExportCsv(string exportLocation, List<CsvFilesData> csvCollection)
        {
            foreach (var csvFile in csvCollection)
            {
                var csvData = new List<string>();
                csvData.Add(csvFile.Header);
                csvData.Add(csvFile.Content);
                csvData.Add(csvFile.Trailer);
                var csvFilePath = $"{exportLocation}\\{csvFile.FileName}.csv";
                System.IO.File.WriteAllLines(csvFilePath, csvData);

            }
        }
    }
}
