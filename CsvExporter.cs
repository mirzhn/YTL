using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YTL
{
    static class CsvExporter
    {
       public static void saveToCsv(string fullname, List<string[]> stringlist)
       {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            StreamWriter sw = new StreamWriter(fullname, true, encoding);
            sw.Write(getCsvString(stringlist, ",", "\""));
            sw.Close();
            //File.WriteAllText(fullname, getCsvString(stringlist, ",", "\""));
       }

       private static string getCsvString(List<string[]> stringlist, string separator, string qualifier)
       {
            string csvstring = ""; 
            foreach(string[] row in stringlist)
            {
                foreach (string column in row)
                {
                    csvstring = csvstring + qualifier + column + qualifier + separator;
                }
                csvstring = csvstring.Remove(csvstring.Length - qualifier.Length); ;
                csvstring = csvstring + "\r\n";
            }
            return csvstring;
       }
    }
}
