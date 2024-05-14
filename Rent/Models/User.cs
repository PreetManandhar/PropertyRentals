using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;


    public string Type { get; set; } = null!;

    public virtual ICollection<Appointment> AppointmentManagers { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentTenants { get; set; } = new List<Appointment>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Property> PropertyManagers { get; set; } = new List<Property>();

    public virtual ICollection<Property> PropertyOwners { get; set; } = new List<Property>();
}
