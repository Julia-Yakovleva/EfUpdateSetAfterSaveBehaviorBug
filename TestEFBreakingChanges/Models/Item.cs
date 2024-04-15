namespace TestEFBreakingChanges.Models;

public class Item
{
    public string ItemCode { get; set; }
    public string Name { get; set; }

    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }

    public Item(){}

    public Item(string itemCode, string name)
    {
        var now = DateTime.UtcNow.ToString();
        CreatedAt = now;
        UpdatedAt = now;

        ItemCode = itemCode;
        Name = name;
    }
}
