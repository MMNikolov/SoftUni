using System.Text;

namespace AutomotiveRepairShop
{
    public class RepairShop
    {
        public RepairShop(int capacity)
        {
            Capacity = capacity;

			Vehicles = new List<Vehicle>();
        }

		private int capacity;

		public int Capacity
		{
			get { return capacity; }
			set { capacity = value; }
		}

		private List<Vehicle> vehicles;

		public List<Vehicle> Vehicles
		{
			get { return vehicles; }
			set { vehicles = value; }
		}


		public void AddVehicle(Vehicle vehicle)
		{
			if (Vehicles.Count < Capacity)
			{
				Vehicles.Add(vehicle);
			}
		}

		public bool RemoveVehicle(string vin)
		{
			return Vehicles.Remove(Vehicles.FirstOrDefault(v => v.VIN == vin));
		}

		public int GetCount()
		{
			return Vehicles.Count;
		}

		public Vehicle GetLowestMileage()
		{

			int lowestMileage = int.MaxValue;
			for (int i = 0; i < Vehicles.Count; i++)
			{
				if (Vehicles[i].Mileage < lowestMileage)
				{
					lowestMileage = Vehicles[i].Mileage;
				}
			}

			Vehicle vehicle = Vehicles.Find(x => x.Mileage == lowestMileage);
			return vehicle;
		}

		public string Report()
		{
			StringBuilder sb = new();

			sb.AppendLine("Vehicles in the preparatory:");

			foreach (Vehicle vehicle in Vehicles)
			{
				sb.AppendLine(vehicle.ToString());
			}

			return sb.ToString().TrimEnd();
		}

	}
}
