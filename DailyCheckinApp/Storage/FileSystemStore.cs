using DailyCheckinApp.Models;
using System.Text.Json;

namespace DailyCheckinApp.Storage
{
    internal class FileSystemStore : ICheckInDayStore
    {
        private readonly static ColorJsonConverter ColorConverter = new ColorJsonConverter();

        public void Write(DateTime dateKey, CheckInDay value)
        {
            var filePath = GetFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            dayToCheckInMap[dateKey.Day] = value;
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(ColorConverter);
            var serializedContent = JsonSerializer.Serialize(dayToCheckInMap, serializeOptions);
            WriteFileContentAsync(filePath, serializedContent);
        }

        public CheckInDay Read(DateTime dateKey)
        {
            var filePath = GetFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            return dayToCheckInMap.GetValueOrDefault(dateKey.Day);
        }

        public Dictionary<int, CheckInDay> ReadMonth(DateTime dateKey)
        {
            var filePath = GetFilePath(dateKey);
            return  this.GetDayToCheckInMap(filePath);
        }

        private Dictionary<int, CheckInDay> GetDayToCheckInMap(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileContent = this.ReadFileContent(filePath);
                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    return new Dictionary<int, CheckInDay>();
                }
                var deserializeOptions = new JsonSerializerOptions();
                deserializeOptions.Converters.Add(ColorConverter);
                var dayToCheckInMap = JsonSerializer.Deserialize<Dictionary<int, CheckInDay>>(fileContent, deserializeOptions);
                return dayToCheckInMap;
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
