using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent.Models;

public partial class Message
{
    [Key]
    public int MessageId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SendDate { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int AppointmentId { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
