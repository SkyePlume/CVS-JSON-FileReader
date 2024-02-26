using FileHelpers;
using Medica.Uk.TechnicalDemonstration.Exceptions;
using Medica.Uk.TechnicalDemonstration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medica.Uk.TechnicalDemonstration.Logic
{
    public class ReadFromFile : IReadFromInterface
    {
        public ReadFromFile() { }

        /// <summary>
        /// Identifies the file type of the supplied path and then determines which logic is used to convert the file into the expected C# object.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="JSONException"></exception>
        /// <exception cref="CSVException"></exception>
        /// <exception cref="Exception"></exception>
        public List<CustomerRecord> FindRecords(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    throw new Exception("File location was not provided.");

                switch (path)
                {
                    case string s when path.EndsWith(".json"):
                         return FindJSONRecords(path);
   

                    case string s when path.EndsWith(".csv"):
                         return FindCSVRecords(path);

                    default:
                        throw new Exception("Unknown File type");
                }
            }
            catch (JSONException ex)
            {
                throw new JSONException(ex.Message);
            }
            catch (CSVException ex)
            {
                throw new CSVException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Intakes a file path as a string then using it to find the JSON file location and converts the file to a string which is then deserialized into the C# object.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="JSONException"></exception>
        public List<CustomerRecord> FindJSONRecords(string path)
        {
            //Supplying the stream reader the location of the file and then storing it into a string.
            using StreamReader reader = new (path);
            string json = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
                throw new JSONException("No data was read from file.");

            //Takes the result of the stream reader and then deserializes the JSON content into the Customer Record object.
            List<CustomerRecord>? jsonRecords = JsonConvert.DeserializeObject<List<CustomerRecord>>(json);

            if (jsonRecords == null || jsonRecords.Count <= 0)
                throw new JSONException("Could not read values from JSON file.");

            return jsonRecords;
        }

        /// <summary>
        /// Intakes a file path as a string then using it to find the CSV file location and uses the FileHelperEngine to convert that CSV into the C# object.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="CSVException"></exception>
        public List<CustomerRecord> FindCSVRecords(string path)
        {
            //Setting up the Filer Helper Engine and allowing it to read the file from the supplied path.
            FileHelperEngine<CustomerRecord> engine = new();
            List<CustomerRecord> csvRecords = engine.ReadFileAsList(path);

            if (csvRecords == null || csvRecords.Count <= 0)
                throw new CSVException("Could not read values from CSV file.");

            return csvRecords;
        }
    }
}
