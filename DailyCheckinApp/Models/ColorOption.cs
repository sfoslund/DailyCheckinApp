namespace DailyCheckinApp.Models
{
    public class ColorOption
    {
        public string Name { get; }

        public Color Color { get; }

        public ColorOption(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}
