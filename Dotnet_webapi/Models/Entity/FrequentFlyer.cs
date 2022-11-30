using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class FrequentFlyer
{
    public int FrequentFlyerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Title { get; set; }

    public string CardNum { get; set; }

    public int Level { get; set; }

    public int AwardPoints { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
