using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent.Models;

public partial class Property
{
    [Key]
    public int PropertyId { get; set; }

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int OwnerId { get; set; }

    public int ManagerId { get; set; }

    public byte[]? Photo { get; set; }
    [NotMapped]
    [DisplayName("Photo")]
    public IFormFile? PhotoFile { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    public virtual User Manager { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;
}
