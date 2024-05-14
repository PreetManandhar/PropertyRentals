using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent.Models;

public partial class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int ManagerId { get; set; }

    public int TenantId { get; set; }

    public int ApartmentId { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User Tenant { get; set; } = null!;
}
