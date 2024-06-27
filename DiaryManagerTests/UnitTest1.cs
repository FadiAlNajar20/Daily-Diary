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
            // Arrange
            var diary = new DailyDiary(_testFilePath);

            // Act
            var entries = diary.ReadDiaryFile();

            // Assert
            Assert.Equal(3, entries.Count);
            Assert.Equal(new DateTime(2024, 6, 26), entries[0].Date);
            Assert.Equal("Some thing", entries[0].Content);
            Assert.Equal(new DateTime(2024, 6, 27), entries[1].Date);
            Assert.Equal("Some thing", entries[1].Content);
            Assert.Equal(new DateTime(2024, 6, 28), entries[2].Date);
            Assert.Equal("Some thing", entries[2].Content);
        }

        [Fact]
        public void AddEntry_ShouldIncreaseEntryCount()
        {
            // Arrange
            var diary = new DailyDiary(_testFilePath);
            var newEntry = new Entry(new DateTime(2024, 6, 29), "Some thing");

            // Act
            diary.AddEntry(newEntry);
            var entries = diary.ReadDiaryFile();

            // Assert
            Assert.Equal(new DateTime(2024, 6, 29), entries.Last().Date);
            Assert.Equal("Some thing", entries.Last().Content);
        }


    }
}
