using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public enum EquipmentCategory { Boots, Ski, Jacket, Helmet, Gloves, Backpack , Other}
public class Equipment : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string ImageURL { get; private set; }
    public EquipmentCategory Category { get; private set; }

    public Equipment(string name, string? description, string imageURL, EquipmentCategory equipmentCategory)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        Name = name;
        Description = description;
        ImageURL = imageURL;
        Category = equipmentCategory;

    }
}
