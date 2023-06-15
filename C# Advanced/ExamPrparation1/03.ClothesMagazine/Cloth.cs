namespace ClothesMagazine
{
    public class Cloth
    {
        public Cloth(string color, int size, string type)
        {
            Color = color;
            Size = size;
            Type = type;
        }

        public string Color { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"Product: {this.Type} with size {this.Size}, color {this.Color}";
        }
    }
}
