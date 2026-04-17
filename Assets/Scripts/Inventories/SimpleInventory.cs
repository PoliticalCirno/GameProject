using System.Collections.Generic;

public class SimpleInventory : PersistentSingleton<SimpleInventory>
{
    public int currency = 0;
    public int wood = 0;
    public int metal = 0;
    public int meals = 0;
    public bool hasSword = false;
    public bool hasSpear = false;
    public bool hasShield = false;
    public bool hasHelmet = false;
}

public class ArrayInventory : PersistentSingleton<ArrayInventory>
{
    public Item[] backpack = new Item[8];
    public Item[] homeChest = new Item[64];
    public List<Item> dynamicSizeBackpack = new List<Item>();
}

[System.Serializable]
public class Item 
{
    public bool isStackable = false; // Set to true if you want to stack the same item on the same location in inventory
}
