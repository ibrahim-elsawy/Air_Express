using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Phone
{
    public int PhoneId { get; set; }

    public int? AccountId { get; set; }

    public string Phone1 { get; set; }

    public string PhoneType { get; set; }

    public bool? PrimaryPhone { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual Account Account { get; set; }
}
