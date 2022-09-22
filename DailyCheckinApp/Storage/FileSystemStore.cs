using DailyCheckinApp.Models;
using System.Text.Json;

namespace DailyCheckinApp.Storage
{
    internal class FileSystemStore : IStore
    {
        private readonly static ColorJsonConverter ColorConverter = new ColorJsonConverter();

        public void WriteCheckIn(DateTime dateKey, CheckInDay value)
        {
            var filePath = GetCheckInFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            dayToCheckInMap[dateKey.Day] = value;
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(ColorConverter);
            var serializedContent = JsonSerializer.Serialize(dayToCheckInMap, serializeOptions);
            WriteFileContent(filePath, serializedContent);
        }

        public CheckInDay ReadCheckIn(DateTime dateKey)
        {
            var filePath = GetCheckInFilePath(dateKey);
            var dayToCheckInMap = this.GetDayToCheckInMap(filePath);
            return dayToCheckInMap.GetValueOrDefault(dateKey.Day);
        }

        public Dictionary<int, CheckInDay> ReadMonthCheckIns(DateTime dateKey)
        {
            var filePath = GetCheckInFilePath(dateKey);
            return this.GetDayToCheckInMap(filePath);
        }

        public void WriteHabits(IEnumerable<string> habits)
        {
            var filePath = GetHabitsFilePath();
            var content = JsonSerializer.Serialize(habits);
            WriteFileContent(filePath, content);
        }

        public IEnumerable<string> ReadHabits()
        {
            var filePath = GetHabitsFilePath();
            if (File.Exists(filePath))
            {
                var content = ReadFileContent(filePath);
                var habits = JsonSerializer.Deserialize<List<string>>(content);
                return habits;
            }
            else
            {
                return new List<string>();
            }
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

        private void WriteFileContent(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        private string GetCheckInFilePath(DateTime dateKey)
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"CheckIns-{dateKey.ToString("MMMM")}-{dateKey.Year}.json");
        }

        private string GetHabitsFilePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"habits.json");
        }
    }
}
