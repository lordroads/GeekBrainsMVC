namespace Orders.DAL.Entities;

public class Buyer : NamedEntity
{
    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public DateTime Birthday { get; set; }

    public override string ToString()
    {
        return $"{LastName} {Name} {Patronymic} {Birthday}";
    }
}