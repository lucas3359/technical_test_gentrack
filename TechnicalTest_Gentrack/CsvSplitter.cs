using System.Collections.Generic;
using System.Text.RegularExpressions;
using TechnicalTest_Gentrack.Models;

namespace TechnicalTest_Gentrack
{
    public class CsvSplitter
    {
        public List<CsvFilesData> SplitCsv(CsvIntervalData csvWholeString)
        {
            var cleanedCsvWholeString = csvWholeString.Value.Trim('\r', '\n', '\t').Trim();
            var pattern = @"\n(?=200|900)";

            var blocks = Regex.Split(cleanedCsvWholeString, pattern);
            var csvFile = new List<CsvFilesData>();

            var csvHeader = GetCsvHeader(blocks);
            var csvTrailer = GetCsvTrailer(blocks);

            foreach (var block in blocks)
            {
                var csv = new CsvFilesData();
                csv.Content = GetCsvContent(block);
                if (csv.Content != null)
                {
                    csv.Header = csvHeader;
                    csv.Trailer = csvTrailer;
                    csv.FileName = GetCsvFileName(block);
                    csvFile.Add(csv);
                }
            }

            return csvFile;
        }

        private string GetCsvHeader(string[] blocks)
        {
            foreach (var block in blocks)
            {
                var isHeader = block.StartsWith("100,");
                if (isHeader)
                {
                    return block;
                }
            }

            return null;
        }

        private string GetCsvTrailer(string[] blocks)
        {
            foreach (var block in blocks)
            {
                var isTrailer = block.StartsWith("900");
                if (isTrailer)
                {
                    return block;
                }
            }

            return null;
        }

        private string GetCsvFileName(string fileRow)
        {
            var fileName = fileRow.Split(",")[1];
            return fileName;
        }

        private string GetCsvContent(string block)
        {
            var isTrailer = block.StartsWith("200,");
            if (isTrailer)
            {
                return block;
            }

            return null;
        }
    }
}
