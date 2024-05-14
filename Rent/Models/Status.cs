using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent.Models;

public partial class Status
{
    [Key]
    public int StatusId { get; set; }

    public string StatusType { get; set; } = null!;

    public int ManagerId { get; set; }

    public int ApartmentId { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;
}
