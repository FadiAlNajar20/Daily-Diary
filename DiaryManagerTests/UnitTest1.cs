using System;
using System.IO;
using System.Linq;
using Xunit;
using DiaryManager;

namespace DiaryManagerTests
{
    public class DailyDiaryTests
    {
        private readonly string _testFilePath = "../../../Filetest.txt";

        [Fact]
        public void ReadDiaryFile_ShouldReturnCorrectEntries()
        {
            File.WriteAllText(_testFilePath, "2024-06-27\nTest entry");

            // Act
            DailyDiary diary = new DailyDiary(_testFilePath);
            diary.ReadDiaryFile();
        }

        [Fact]
        public void AddEntry_ShouldIncreaseEntryCount()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }

            DailyDiary diary = new DailyDiary(_testFilePath);
            var newEntry = new Entry(new DateTime(2024, 6, 29), "Some thing");

            // Act
            int entriesNumberBeforeAdding = diary.TotalLines();
            diary.AddEntry(newEntry);

            // Assert
            Assert.Equal(diary.TotalLines(), entriesNumberBeforeAdding + 1);
        }
    }
}
