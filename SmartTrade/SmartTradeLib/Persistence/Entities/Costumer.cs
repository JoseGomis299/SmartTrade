namespace SmartTradeLib.Entities;

public partial class Costumer : User
{
    public string DNI { get; set; }
    public DateTime BirthDate { get; set; }

    public virtual Adress BillingAddress { get; set; }
    public virtual ICollection<Adress> Adresses { get; set; }
    public virtual ICollection<IPayMethod> PayMethods { get; set; }
    public virtual ICollection<Alert> Orders { get; set; }
}