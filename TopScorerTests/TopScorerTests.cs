using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopScorers;
using TopScorers.Model;
using Xunit;

namespace TopScorerTests
{
    public class TopScorerTests
    {
        [Theory]
        [InlineData("First Name,Second Name,Score", "Bob,Builder,85")]
        public void ReadCsvFile_ShouldSkipHeaderAndReadData(params string[] csvLines)
        {
            // Arrange
            var tempCsvPath = Path.GetTempFileName();
            File.WriteAllLines(tempCsvPath, csvLines);

            // Act
            var results = TestResultProcessor.ReadCsvFile(tempCsvPath);

            // Assert
            Assert.Equal(csvLines.Length - 1, results.Count);

            var values = csvLines[1].Split(','); 
            var testResult = results.FirstOrDefault();
            Assert.Equal(values[0], testResult.FirstName);
            Assert.Equal(values[1], testResult.SecondName);
            Assert.Equal(int.Parse(values[2]), testResult.Score);

        }

        [Fact]
        public void GetTopScorers_ShouldReturnTopScorersInOrder()
        {
            // Arrange
            var results = new List<TestResult>
            {
                new TestResult { FirstName = "John", SecondName = "Doe", Score = 80 },
                new TestResult { FirstName = "Jane", SecondName = "Smith", Score = 75 },
                new TestResult { FirstName = "Jim", SecondName = "Brown", Score = 80 },
                new TestResult { FirstName = "Alice", SecondName = "Johnson", Score = 70 }
            };

            // Act
            var topScorers = TestResultProcessor.GetTopScorers(results);

            // Assert
            Assert.Equal(2, topScorers.Count);
            Assert.Equal("Jim", topScorers[0].FirstName);
            Assert.Equal("Brown", topScorers[0].SecondName);
            Assert.Equal("John", topScorers[1].FirstName);
            Assert.Equal("Doe", topScorers[1].SecondName);
        }

        [Fact]
        public void Display_ShouldPrintTopScorers()
        {
            // Arrange
            var topScorers = new List<TestResult>
            {
                new TestResult { FirstName = "John", SecondName = "Doe", Score = 80 },
                new TestResult { FirstName = "Jane", SecondName = "Smith", Score = 80 }
            };

            using var consoleOutput = new StringWriter();
            System.Console.SetOut(consoleOutput);

            // Act
            TestResultProcessor.Display(topScorers);

            // Assert
            var expectedOutput = "Top Scorers:\r\nJohn Doe\r\nJane Smith\r\nScore: 80\r\n";
            Assert.Equal(expectedOutput, consoleOutput.ToString());
        }
    }
}
