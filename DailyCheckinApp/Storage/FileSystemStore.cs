using DailyCheckinApp.ViewModels;
using System.Text.Json;

namespace DailyCheckinApp.Storage
{
    internal class FileSystemStore : ICheckInDayStore
    {
        public void Write(DateTime dateKey, CheckInDay value)
        {
            var filePath = GetFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            dayToCheckInMap[dateKey.Day] = value;
            var serializedContent = JsonSerializer.Serialize(dayToCheckInMap);
            WriteFileContentAsync(filePath, serializedContent);
        }

        public CheckInDay Read(DateTime dateKey)
        {
            var filePath = GetFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            return dayToCheckInMap.GetValueOrDefault(dateKey.Day);
        }

        private Dictionary<int, CheckInDay> GetDayToCheckInMap(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileContent = this.ReadFileContent(filePath);
                var dayToCheckInMap = JsonSerializer.Deserialize<Dictionary<int, CheckInDay>>(fileContent);
                return dayToCheckInMap ?? new Dictionary<int, CheckInDay>();
            }
            else
            {
                return new Dictionary<int, CheckInDay>();
            }
        }

        private string ReadFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        private void WriteFileContentAsync(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        private string GetFilePath(DateTime dateKey)
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"CheckIns-{dateKey.ToString("MMMM")}-{dateKey.Year}.json");
        }
    }
}
