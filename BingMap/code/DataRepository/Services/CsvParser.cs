using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace DataRepository.Services
{
    public class CsvParser
    {
        public static List<string[]> Parse(string path)
        {
            var list = new List<string[]>();
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    list.Add(fields);
                }
            }

            return list;
        }
    }
}
