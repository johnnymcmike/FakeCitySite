using System.ComponentModel.DataAnnotations;

namespace FakeCitySite.Shared;

public class City
{
    public City()
    {
        Id = Guid.NewGuid();
    }
    
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public bool IsReal { get; set; }
    public string Region { get; set; }
    public string? Country { get; set; }
    public string? UsState { get; set; }
    
    public int TimesChosen { get; set; }

    public string GetFullName()
    {
        if (Country is null)
            return Name;

        if (UsState is not null)
            return $"{Name}, {UsState}";

        return $"{Name}, {Country}";
    }
}