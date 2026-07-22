using System.Collections.Generic;

namespace GodotGMTK2026.Scripts.Items.Inventory;

public class Inventory
{
    private int _capacity;
    private List<Item> _items = new List<Item>();
    

    public Inventory(int capacity)
    {
        _capacity = capacity;
    }
    
    /// <summary>
    ///  Returns true if the item was successfully added to the inventory.
    ///  Returns false if the capacity was already reached!
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        if (_items.Count >= _capacity)
            return false;
        
        _items.Add(item);
        return true;
    }

    public Item GetFirstItemOfType(ItemEnum type)
    {
        foreach (Item item in _items)
            if (item.Type == type)
                return item;

        return null;
    }
    
    public List<Item> GetAllOfType(ItemEnum type)
    {
        List<Item> itemsOfType = new List<Item>();
        foreach (Item item in _items)
            if (item.Type == type)
                itemsOfType.Add(item);

        return itemsOfType;
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }

    public void SetCapacity(int capacity)
    {
        _capacity = capacity;
    }

    public int GetCapacity()
    {
        return _capacity;
    }
}