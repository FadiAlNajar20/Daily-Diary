using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiaryManager
{
    public class DailyDiary
    {
        private readonly string _filePath;

        public DailyDiary(string filePath)
        {
            _filePath = filePath;
        }
        public List<Entry> ReadDiaryFile()
        {
            var entries = new List<Entry>();
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("Diary file not found.");
                return entries;
            }

            var lines = File.ReadAllLines(_filePath);
            for (int i = 0; i < lines.Length; i += 2)
            {
                if (i + 1 < lines.Length && DateTime.TryParse(lines[i], out var date))
                {
                    var content = lines[i + 1];
                    entries.Add(new Entry(date, content));
                }
                else
                {
                    Console.WriteLine($"Invalid line: {lines[i]}"); 
                }
            }
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            var entryLine = entry;
            File.AppendAllText(_filePath, entryLine + Environment.NewLine);
        }

        public void DeleteEntry(DateTime date)
        {
            var entries = ReadDiaryFile();
            var newEntries = entries.FindAll(e => e.Date != date);
            WriteEntriesToFile(newEntries);
        }

        public int TotalLines()
        {
            try
            {
                string[] lines = File.ReadAllLines(_filePath);
                int count = 0;
                foreach (string line in lines)
                {
                    if (DateTime.TryParse(line, out _))
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting entries: {ex.Message}");
                return 0;
            }
        }


        public List<Entry> GetEntriesByDate(DateTime date)
        {
            var entries = ReadDiaryFile();
            Console.WriteLine("Get it from file:");
            var filteredEntries = entries.FindAll(e => e.Date == date);
            return filteredEntries;
        }

        private void WriteEntriesToFile(List<Entry> entries)
        {
            var lines = new List<string>();
            foreach (var entry in entries)
            {
                lines.Add(entry.ToString());
            }
            File.WriteAllLines(_filePath, lines);
        }
    }
}
