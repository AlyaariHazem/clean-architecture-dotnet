using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities;

public class TypeAccount
{
    public Guid TypeAccountID { get; set; }
    public string TypeAccountName { get; set; } = string.Empty;
    public virtual ICollection<Accounts> Accounts { get; set; } = new List<Accounts>();
}
