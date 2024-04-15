namespace TestEFBreakingChanges.Models;

public class Store
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }

    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }

    public Store(){}

    public Store(int id, string name)
    {
        var now = DateTime.UtcNow.ToString();
        CreatedAt = now;
        UpdatedAt = now;

        StoreId = id;
        Name = name;
    }

    public void Update(string name)
    {
        UpdatedAt = DateTime.UtcNow.ToString();

        Name = name;
    }
}
