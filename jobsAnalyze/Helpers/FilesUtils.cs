using jobsAnalyze.Helpers.Interfaces;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jobsAnalyze.Helpers
{
    public class CSVUtils : IFilesUtils
    {
        public MemoryStream CreateCSV<T>(List<T> items)
        {
            var csv = new StringBuilder();
            string[] columns = typeof(T).GetProperties().Select(p => p.Name).ToArray();
            //the initial value is a first item
            string header = columns.Aggregate((acc, x) => acc + "," + x);
            csv.AppendLine(header);
            for (int i = 0; i < items.Count; i++)
            {
                T item = items[i];
                string newLine = "";

                for (int j = 0; j < columns.Length; j++)
                {
                    string column = columns[j]; 
                    string value = item.GetType().GetProperty(column).GetValue(item, null).ToString();
                    newLine = j != 0 ? newLine + "," + value : newLine + value;
                }
                csv.AppendLine(newLine);
            }
            using (var memoryStream = new MemoryStream())
            {
                var file = System.Text.Encoding.GetEncoding("Windows-1252").GetBytes(csv.ToString());
                memoryStream.Write(file);
                return memoryStream;
            } 
        }
    }
}
