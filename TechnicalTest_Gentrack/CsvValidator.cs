using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechnicalTest_Gentrack.Models;

namespace TechnicalTest_Gentrack
{
    public class CsvValidator
    {
        public bool IsValidCsvIntervalData(CsvIntervalData csvWholeString)
        {
            var cleanedCsvWholeString = csvWholeString.Value.Trim('\r', '\n', '\t').Trim();
            var pattern = @"\n";

            var rows = Regex.Split(cleanedCsvWholeString, pattern);

            if (!IsValidStartingRows(rows))
            {
                throw new ValidationException("Starting column not valid, can only be: (100, 200, 300, 900)");
            }
            if (!ContainsAtLeastOneEachValidNumber(rows))
            {
                throw new ValidationException("Element should contain at least one of: (100, 200, 300, 900)");
            }
            if (!IsSingleHeaderTrailer(rows))
            {
                throw new ValidationException("Element should have exactly one header and trailer row");
            }
            if (!IsContentWithinHeaderTrailer(rows))
            {
                throw new ValidationException("Content should be between header and trailer rows");
            }
            if (!IsValidContent(rows))
            {
                throw new ValidationException("Content 200 should be followed by at least one 300 row");
            }

            return true;
        }

        private bool IsValidStartingRows(string[] rows)
        {
            var validStartingRow = new List<string>() { "100", "200", "300", "900" };
            foreach (var row in rows)
            {
                var rowNumber = GetRowNumber(row);
                var isValidStartingRow = validStartingRow.Any(element => rowNumber == element);
                if (!isValidStartingRow)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ContainsAtLeastOneEachValidNumber(string[] rows)
        {
            var validStartingRow = new HashSet<string>() { "100", "200", "300", "900" };

            foreach (var row in rows)
            {
                var rowStartingNumber = GetRowNumber(row);
                if (validStartingRow.Contains(rowStartingNumber))
                {
                    validStartingRow.Remove(rowStartingNumber);
                }
            }
            if (validStartingRow.Count == 0)
            {
                return true;
            }

            return false;
        }

        private bool IsSingleHeaderTrailer(string[] rows)
        {
            var headerNumber = "100";
            var trailerNumber = "900";
            var hasHeader = false;
            var hasTrailer = false;

            foreach (var row in rows)
            {
                var rowStartingNumber = GetRowNumber(row);
                if (rowStartingNumber == headerNumber)
                {
                    if (!hasHeader)
                    {
                        hasHeader = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (rowStartingNumber == trailerNumber)
                {
                    if (!hasTrailer)
                    {
                        hasTrailer = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return hasHeader && hasTrailer;
        }

        private bool IsContentWithinHeaderTrailer(string[] rows)
        {
            var contentSet = new HashSet<string>() { "200", "300" };

            var headerRow = GetRowNumber(rows[0]);
            var trailerRow = GetRowNumber(rows[rows.Length - 1]);
            if (headerRow == "100" && trailerRow == "900")
            {
                for (int position = 1; position < rows.Length - 1; position++)
                {
                    var rowStartingNumber = GetRowNumber(rows[position]);
                    if (!contentSet.Contains(rowStartingNumber))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool IsValidContent(string[] rows)
        {
            for (int position = 1; position < rows.Length - 1; position++)
            {
                var rowStartingNumber = GetRowNumber(rows[position]);
                if (rowStartingNumber == "200")
                {
                    if ( GetRowNumber(rows[position + 1]) == "300")
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static string GetRowNumber(string row)
        {
            var columns = row.Split(",");
            if (columns.Length < 1) return "";
            return columns[0].Trim();
        }
    }
}
