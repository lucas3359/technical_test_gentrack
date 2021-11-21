using System;
using System.Collections.Generic;
using System.IO;
using TechnicalTest_Gentrack;
using Xunit;

namespace AutomatedTest
{
    [CollectionDefinition(name: "XML to CSV parser")]
    public class ServiceGeneratorDefinition : ICollectionFixture<XmlToCsvParser>, ICollectionFixture<FileHandler>,
                                         ICollectionFixture<CsvSplitter>, ICollectionFixture<CsvValidator> { }

    [Collection(name:"XML to CSV parser")]
    public class XmlToCsvParserTests 
    {
        private readonly XmlToCsvParser _xmlToCsvParser;

        public XmlToCsvParserTests(XmlToCsvParser xmlToCsvParser)
        {
            _xmlToCsvParser = xmlToCsvParser;
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void XmlToCsvParser_Tests(string xmlFileName, string location,
                                        string containingString, string[] csvFileNames)
        {
            _xmlToCsvParser.Parse(xmlFileName, location);
            foreach (var fileName in csvFileNames)
            {
                var outputFile = Path.Combine(location, fileName);
                var readFile = File.ReadAllText(outputFile);
                Assert.Contains(containingString, readFile);
                File.Delete(outputFile);
            }
        }

        public static IEnumerable<object[]> TestData()
        {
            var path = Path.GetTempPath();
            yield return new object[] { "testfile.xml", path, "MYENRGY", 
                                        new string[] { "12345678901.csv", "98765432109.csv" } };
        }

    }
}
