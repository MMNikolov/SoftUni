using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClothesMagazine
{
    public class Magazine
    {
        public Magazine(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;
            Clothes = new List<Cloth>();
        }

        public string Type { get; set; }
        public int Capacity { get; set; }
        public List<Cloth> Clothes { get; set; }

        public void AddCloth(Cloth cloth)
        {
            if (this.Clothes.Count < this.Capacity)
            {
                this.Clothes.Add(cloth);
            }
        }

        public bool RemoveCloth(string color)
        {
            Cloth cloth = this.Clothes.FirstOrDefault(c => c.Color == color);
            if (cloth != null)
            {
                this.Clothes.Remove(cloth);
                return true;
            }
            else return false;
        }

        public Cloth GetSmallestCloth() => this.Clothes.OrderBy(c => c.Size).FirstOrDefault();

        public Cloth GetCloth(string color) => this.Clothes.FirstOrDefault(c => c.Color == color);

        public int GetClothCount() => this.Clothes.Count;

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Type} magazine contains:");

            foreach (var cloth in this.Clothes.OrderBy(c => c.Size))
            {
                sb.AppendLine(cloth.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
