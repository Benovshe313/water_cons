using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumption
{
    internal class Helper
    {
        public static double FindDailyNorm(double weight)
        {
            return weight / 20;
        }
        public static void SaveDailyAmount(DateTime date, double amount)
        {
            string dateStr = date.ToString("dd/MM/yyyy");
            List<string> lines = new List<string>();
            bool dateExists = false;

            if (File.Exists(@"C:\Users\User\Desktop\consumption.txt"))
            {
                lines.AddRange(File.ReadAllLines(@"C:\Users\User\Desktop\consumption.txt"));
            }

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(':');
                if (parts[0] == dateStr)
                {
                    double waterIntake = double.Parse(parts[1]) + amount;
                    //double waterIntake = amount;
                    lines[i] = $"{dateStr}:{Math.Round(waterIntake, 2)}";
                    dateExists = true;
                    break;
                }
            }

            if (!dateExists)
            {
                lines.Add($"{dateStr}:{amount}");
            }

            File.WriteAllLines(@"C:\Users\User\Desktop\consumption.txt", lines);
        }

        public static double GetAmount(DateTime date)
        {
            if (File.Exists(@"C:\Users\User\Desktop\consumption.txt"))
            {
                string[] lines = File.ReadAllLines(@"C:\Users\User\Desktop\consumption.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts[0] == date.ToString("dd/MM/yyyy"))
                    {
                        return double.Parse(parts[1]);
                    }
                }
            }
            return 0;
        }

        public static void ShowHistory()
        {
            if (File.Exists(@"C:\Users\User\Desktop\consumption.txt"))
            {
                string[] lines = File.ReadAllLines(@"C:\Users\User\Desktop\consumption.txt");
                Console.WriteLine("History:");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                        Console.WriteLine($"{parts[0]} {parts[1]} l");
                }
            }
        }

        public static void SaveNameAndWeight(string filePath, ref string name, ref double weight)
        {
            Console.Write("Name: ");
            name = Console.ReadLine();

            Console.Write("Weight: ");
            weight = User.validWeight();

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(name);
                sw.WriteLine(weight);
            }
        }
    }
}