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
            //Set file path
            string filePath = "TestData.csv"; 

            //Read csv
            List<TestResult> results = TestResultProcessor.ReadCsvFile(filePath);

            // check if csv had content 
            if (results.Count > 0)
            {
                //find top scorers
                List<TestResult> topScorers = TestResultProcessor.GetTopScorers(results);

                //Display top scorers
                TestResultProcessor.Display(topScorers);
            }
            else
            {
                // If csv empty display message
                Console.WriteLine("No data found in the CSV file.");
            }
        }
        catch (Exception ex)
        {
            // if exception occurred display exception message
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    
    
}



