using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models
{
    public class Truck
    {
        public Truck()
        {
            ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(8)]
        public string RegistrationNumber { get; set; }

        [Required]
        public string VinNumber { get; set; } = null!;

        [MaxLength(1420)]
        public int TankCapacity { get; set; }

        [MaxLength(29000)]
        public int CargoCapacity { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public MakeType MakeType { get; set; }

        [Required]
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }
        public Despatcher Despatcher { get; set; } = null!;

        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
    }
}
