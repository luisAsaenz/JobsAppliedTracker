﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace JobApplications
{
    class Program
    {
        static void Main(string[] args)
        {
            string date = DateTime.Now.ToString("d");
            
            //Use these two lines of code to apply the file path and file to the console application so you can read and write to that file
            //string userspath = Console.ReadLine();
            //string path = $@"{userspath}";


            //This is my file that has all the jobs i applied for and the titles
            string path = @"C:\Users\LuisA\OneDrive\Documents\Jobs.txt";
            Choices(date, path);
        }

        private static void Choices(string date, string path)
        {
            Console.Clear();
            Console.Write("Do you want to view last or add to list?? (a / v) ");
            Console.WriteLine();
            ConsoleKeyInfo cki = Console.ReadKey();
            switch (cki.Key)
            {
                case ConsoleKey.A:

                    Console.WriteLine("\bEnter Company and Job Title: ");

                    var c = Console.ReadLine();
                    string[] input = c.Split("-");

                    Console.WriteLine("Enter Date Applied (Date does not need to be added if applied today): ");
                    var d = Console.ReadLine();

                    var newjob = input[0] + input[1] + d;
                    if (!string.IsNullOrEmpty(newjob))
                    {
                        if (String.IsNullOrEmpty(d))
                        {
                            d = date;
                        }
                        newjob = input[0] + " - " + input[1] + ", " + d;
                        File.AppendAllText(@"C:\Users\LuisA\OneDrive\Documents\Jobs.txt", newjob);
                    }
                    Choices(date, path);
                    break;
                case ConsoleKey.V:
                    ListOfJobs(path);
                    break;
            }
        }



        private static void ListOfJobs(string path)
        {
            var word = new string[] { };

            string date = DateTime.Now.ToString("d");
            // Keep the console window open in debug mode.
            int counter = 0;
            string job;
            Console.WriteLine("\bLate time file was opened was " + File.GetLastAccessTime(path));
            Console.Write("\nWould you like list ordered by Company, Title, or Date? (c / t / d) ");
            var choice2 = Char.Parse(Console.ReadLine());
            // Read the file and display it line by line.  
            StreamReader file =
                new StreamReader(@"C:\Users\LuisA\OneDrive\Documents\Jobs.txt");
            var l = new List<KeyValuePair<string, (string, string)>>();

            while ((job = file.ReadLine()) != null)
            {

                word = job.Split(new string[] { "-", "," }, StringSplitOptions.None);

                // string val = word[1].Trim();

                l.Add(new KeyValuePair<string, (string, string)>(word[0].Trim(), (word[1].Trim(), word[2].Trim())));
                counter++;
            }
            file.Close();

            List<string> lines = word.ToList();
            lines.Clear();

            if (choice2 == 'c')
            {
                OrderByCompany(path, counter, file, l, lines);
                Console.ReadKey();
                Console.Clear();
                Choices(date, path);
            }
            else if (choice2 == 't')
            {
                OrderByTitle(path, counter, file, l, lines);
                Console.ReadKey();
                Console.Clear();
                Choices(date, path);
            }
            else if (choice2 == 'd')
            {
                OrderByDate(path, counter, file, l, lines);
                Console.ReadKey();
                Console.Clear();
                Choices(date, path);
            }
            
        }

        private static void OrderByCompany(string path, int counter, StreamReader file, List<KeyValuePair<string, (string, string)>> l, List<string> lines)
        {
            List<KeyValuePair<string, (string, string)>> orderedlist = l.OrderBy(x => x.Key).ThenBy(x => x.Value.Item1).ToList();

            foreach (KeyValuePair<string, (string, string)> i in orderedlist)
            {
                //string key = orderedlist.Where(x => x.Value.Item1 == i.Value.Item1).Select(x => x.Key).FirstOrDefault();
                string line = i.Key + " - " + i.Value.Item1 + ", " + i.Value.Item2;
                lines.Add(line);
            }

            File.WriteAllLines(path, lines);
            Console.WriteLine(File.ReadAllText(path));


            file.Close();
            Console.WriteLine("There were {0} lines.", counter);
        }
        private static void OrderByTitle(string path, int counter, StreamReader file, List<KeyValuePair<string, (string, string)>> l, List<string> lines)
        {
            List<KeyValuePair<string, (string, string)>> orderedlist = l.OrderBy(x => x.Value.Item1).ThenBy(x => x.Key).ToList();

            foreach (KeyValuePair<string, (string, string)> i in orderedlist)
            {
                //string key = orderedlist.Where(x => x.Value.Item1 == i.Value.Item1).Select(x => x.Key).FirstOrDefault();
                string line = i.Key + " - " + i.Value.Item1 + ", " + i.Value.Item2;
                lines.Add(line);
            }

            File.WriteAllLines(path, lines);
            Console.WriteLine(File.ReadAllText(path));


            file.Close();
            Console.WriteLine("There were {0} lines.", counter);
        } private static void OrderByDate(string path, int counter, StreamReader file, List<KeyValuePair<string, (string, string)>> l, List<string> lines)
        {
            List<KeyValuePair<string, (string, string)>> orderedlist = l.OrderBy(x => DateTime.Parse(x.Value.Item2)).ThenBy(x => x.Key).ThenBy(x => x.Value.Item1).ToList();

            foreach (KeyValuePair<string, (string, string)> i in orderedlist)
            {
                //string key = orderedlist.Where(x => x.Value.Item1 == i.Value.Item1).Select(x => x.Key).FirstOrDefault();
                string line = i.Key + " - " + i.Value.Item1 + ", " + i.Value.Item2;
                lines.Add(line);
            }

            File.WriteAllLines(path, lines);
            Console.WriteLine(File.ReadAllText(path));


            file.Close();
            Console.WriteLine("There were {0} lines.", counter);
        }
    }
}