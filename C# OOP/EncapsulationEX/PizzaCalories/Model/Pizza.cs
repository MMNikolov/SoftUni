using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Model
{
    public class Pizza
    {
        public Pizza(string name, Dough dough)
        {
            Name = name;
            Dough = dough;
            toppings = new List<Toppings>();
        }

        private List<Toppings> toppings;
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                };
                name = value;
            }
        }

        private Dough dough;
        public Dough Dough
        {
            get { return dough; }
            set { dough = value; }
        }

        public int Count { get { return toppings.Count; } }
        public double TotalCalories
        {
            get
            {
                double totalCalories = this.dough.GetCalories();
                foreach (Toppings topping in toppings)
                {
                    totalCalories += topping.GetCalories();
                }

                return totalCalories;
            }
        }


        public void AddTopping(Toppings topping)
        {
            if (this.Count == 10)
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            toppings.Add(topping);
        }

        public override string ToString()
        => $"{this.name} - {this.TotalCalories:f2} Calories.";

    }
}
