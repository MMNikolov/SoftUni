using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClothesMagazine
{
    public class Magazine
    {

        public Magazine(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;

            this.Clothes = new List<Cloth>();
        }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public List<Cloth> Clothes { get; set; }

        public void AddCloth(Cloth cloth)
        {
            if (this.Clothes.Count < this.Capacity)
            {
                Clothes.Add(cloth);
            }
        }

        public bool RemoveCloth(string color)
        {
            Cloth cloth = this.Clothes.FirstOrDefault(x => x.Color == color);

            bool isRemoved = this.Clothes.Remove(cloth);

            return isRemoved;
        }

        public Cloth GetSmallestCloth() 
            => this.Clothes.OrderBy(x => x.Size)
            .FirstOrDefault();

        public Cloth GetCloth(string color)
        {
            return this.Clothes.FirstOrDefault(x => x.Color == color);
        }

        public int GetClothCount()
        {
            return Clothes.Count;
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{Type} magazine contains:");

            foreach (var cloth in this.Clothes.OrderBy(x => x.Size))
            {
                stringBuilder.AppendLine(cloth.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
        
    }
}
