using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopScorers.Model;

namespace TopScorers
{
    public static class TestResultProcessor
    {
        public static List<TestResult> ReadCsvFile(string filePath)
        {
            List<TestResult> results = new List<TestResult>();

            using (var reader = new StreamReader(filePath))
            {
                // Skip the first line containing the headings
                reader.ReadLine();

                // Read lines and add them to list
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 3 && int.TryParse(values[2], out int score))
                    {
                        results.Add(new TestResult
                        {
                            FirstName = values[0],
                            SecondName = values[1],
                            Score = score
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Skipping invalid data: {line}");
                    }
                }
            }

            return results;
        }

        public static List<TestResult> GetTopScorers(List<TestResult> results)
        {
            int maxScore = results.Max(r => r.Score);
            return results
                .Where(r => r.Score == maxScore)
                .OrderBy(r => r.FirstName)
                .ThenBy(r => r.SecondName)
                .ToList();
        }

        public static void Display(List<TestResult> topScorers)
        {
            Console.WriteLine("Top Scorers:");
            foreach (var scorer in topScorers)
            {
                Console.WriteLine($"{scorer.FirstName} {scorer.SecondName}");
            }
            Console.WriteLine($"Score: {topScorers.FirstOrDefault().Score}");
        }
    }
}
