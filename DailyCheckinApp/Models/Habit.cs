namespace DailyCheckinApp.Models
{
    public class Habit
    {
        public string Name { get; }
        public bool Done { get; set; }

        public Habit(string name, bool done)
        {
            Name = name;
            Done = done;
        }
    }
}
