using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TopScorers;
using TopScorers.Model;

class Program
{
    static void Main()
    {
        try
        {
            string filePath = "TestData.csv"; 

            List<TestResult> results = TestResultProcessor.ReadCsvFile(filePath);

            if (results.Count > 0)
            {
                List<TestResult> topScorers = TestResultProcessor.GetTopScorers(results);

                TestResultProcessor.Display(topScorers);
            }
            else
            {
                Console.WriteLine("No data found in the CSV file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    
    
}



