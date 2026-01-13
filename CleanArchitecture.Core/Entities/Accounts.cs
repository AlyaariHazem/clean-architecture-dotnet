using System.Text.Json.Serialization;

namespace CleanArchitecture.Core.Entities;

public class Accounts
{
    public int AccountID { get; set; }
    public string? AccountName { get; set; }
    public bool State { get; set; } = true;
    public string? Note { get; set; }
    public decimal? OpenBalance { get; set; }
    public bool TypeOpenBalance { get; set; }=false;
    public DateTime HireDate { get; set; }=DateTime.Now;
    public int TypeAccountID { get; set; }
    [JsonIgnore]
    public TypeAccount TypeAccount { get; set; }
    [JsonIgnore]
    public virtual ICollection<AccountStudentGuardian> AccountStudentGuardians { get; set; }
}
