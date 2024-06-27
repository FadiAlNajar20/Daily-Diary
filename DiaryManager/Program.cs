namespace DiaryManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            // string pathFile = Path.Combine(Environment.CurrentDirectory, "diary.txt");
             string pathFile = "../../../diary.txt";
            var diary = new DailyDiary(pathFile);
            bool running = true;


            while (running)
            {
                Console.WriteLine("\nDaily Diary Manager");
                Console.WriteLine("1. Read Diary");
                Console.WriteLine("2. Add Entry");
                Console.WriteLine("3. Delete Entry");
                Console.WriteLine("4. Count Entries");
                Console.WriteLine("5. Get Entries by Date");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ReadDiary(diary);
                        break;
                    case "2":
                        AddEntry(diary);
                        break;
                    case "3":
                        DeleteEntry(diary);
                        break;
                    case "4":
                        CountEntries(diary);
                        break;
                    case "5":
                        GetEntriesByDate(diary);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void ReadDiary(DailyDiary diary)
        {
            try
            {
                var entries = diary.ReadDiaryFile();
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void AddEntry(DailyDiary diary)
        {
            Console.Write("Enter the date (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out var date))
            {
                Console.Write("Enter the content: ");
                string content = Console.ReadLine();
                var entry = new Entry(date, content);
                diary.AddEntry(entry);
                Console.WriteLine("Entry added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        static void DeleteEntry(DailyDiary diary)
        {
            Console.Write("Enter the date (YYYY-MM-DD) to delete: ");
            if (DateTime.TryParse(Console.ReadLine(), out var date))
            {
                diary.DeleteEntry(date);
                Console.WriteLine("Entry deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        static void CountEntries(DailyDiary diary)
        {
            var count = diary.TotalLines();
            Console.WriteLine($"Total entries: {count}");
        }

        static void GetEntriesByDate(DailyDiary diary)
        {
            Console.Write("Enter the date (YYYY-MM-DD) to retrieve: ");
            if (DateTime.TryParse(Console.ReadLine(), out var date))
            {
                var entries = diary.GetEntriesByDate(date);

                foreach (var entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }
    }
    
}
