using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent.Models;

public partial class Apartment
{
    [Key]
    public int ApartmentId { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }
    [DisplayName("Rent Price")]
    public decimal RentPrice { get; set; }

    public byte[]? Photo { get; set; }
    [NotMapped]
    [DisplayName("Photo")]
    public IFormFile? PhotoFile { get; set; }

    [Required(ErrorMessage = "Please select a property.")]
    public int PropertyId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Property Property { get; set; } = null!;
}
