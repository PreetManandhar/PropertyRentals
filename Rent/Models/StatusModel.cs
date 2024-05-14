using System.ComponentModel.DataAnnotations;

namespace Rent.Models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }

        public string StatusType { get; set; } = null!;

        public int ManagerId { get; set; }

        public int ApartmentId { get; set; }
    }
}
