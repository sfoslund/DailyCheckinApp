namespace DailyCheckinApp.ViewModels
{
    public class ColorOption
    {
        public string Name { get; }

        public Color Color { get; }

        public ColorOption(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}
