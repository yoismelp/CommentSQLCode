using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommentSelects
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fieldAlias = new List<string>()
            {
                "Metric Name", "EKG DI date", "PtUID", "EKG Availability"
            };

            List<string> allLines = new List<string>();
            allLines.AddRange(System.IO.File.ReadLines(@"C:\Users\yperez\Desktop\SQLCommenter\PDMP.sql"));
            int iterator = 1;

            foreach (string st in fieldAlias)
            {
                int index = allLines.FindIndex(ind => ind.Contains(st));

                if (index != -1)
                {
                    Console.WriteLine(index);
                    allLines[index] = $"--{allLines[index]}";
                    Console.WriteLine(allLines[index]);

                    while ( index > 0 && !allLines[index - iterator].LastOrDefault().Equals(',') )
                    {
                        allLines[index - iterator] = $"--{allLines[index - iterator]}";
                        if (iterator == index)
                            break;
                        else
                            iterator++; 
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(@"C:\Users\yperez\Desktop\SQLCommenter\PDMPResult.sql"))
            {
                foreach(string s  in allLines)
                    writer.Write(s + System.Environment.NewLine);
            }
            Console.ReadLine();
        }
    }
}
