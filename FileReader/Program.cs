using Medica.Uk.TechnicalDemonstration.Exceptions;
using Medica.Uk.TechnicalDemonstration.Logic;
using Medica.Uk.TechnicalDemonstration.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;

namespace Medica.Uk.TechnicalDemonstration
{
	public static class Program
	{
        private const string jsonTestData = "TestData\\JSONTest.json";
        private const string csvTestData = "TestData\\CSVTest.csv";

        static void Main()
		{
			try
			{
                //Setting up dependancy injection.
                ServiceProvider serviceProvider = new ServiceCollection().AddSingleton<IReadFromInterface, ReadFromFile>().BuildServiceProvider();

                var fileReaderService = serviceProvider.GetService<IReadFromInterface>();

                if (fileReaderService == null)
                    throw new ServiceException("Service provider failed to load.");

                //Create a list which will contain both the CSV and JSON values.
                List<CustomerRecord> allRecords = new List<CustomerRecord>();

                //Gets the directory location and then combine the test data file path.
                string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string jsonFilePath = Path.Combine(directory, jsonTestData);
                string csvFilePath = Path.Combine(directory, csvTestData);

                // Retrieve the data from the JSON record and convert it into the CustomerRecord object.
                List<CustomerRecord> jsonRecords = fileReaderService.FindRecords(jsonFilePath);

                allRecords.AddRange(jsonRecords);

                // Retrieve the data from the CSV record and convert it into the CustomerRecord object.
                List<CustomerRecord> csvRecords = fileReaderService.FindRecords(csvFilePath);

                allRecords.AddRange(csvRecords);

                //For presentations sake print out the records from the CSV and JSON files we turned into objects.
                foreach (CustomerRecord item in allRecords)
				{
					Console.WriteLine(item.CustomerID);
                    Console.WriteLine(item.CustomerName);
                    Console.WriteLine(item.AccountBalance);
                    Console.WriteLine(item.DateCreated);
                    Console.WriteLine("");
                }
            }
            //If an exception of any kind is caught throw it and write the exception message to the console.
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ServiceException(ex.Message);
            }
            catch (CSVException ex)
			{
				Console.WriteLine(ex.Message);
				throw new CSVException(ex.Message);
			}
			catch (JSONException ex)
			{
				Console.WriteLine(ex.Message);
				throw new JSONException(ex.Message);
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
			}
		}
	}
}
