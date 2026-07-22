using System;
using System.Collections.Generic;

namespace GodotGMTK2026.Scripts.Items.Inventory;

public class Inventory
{
    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;
    
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
        OnItemAdded?.Invoke(item);
        return true;
    }

    public Item GetFirst()
    {
        if (_items.Count == 0)
            return null;
        return _items[0];
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

    public bool RemoveItem(Item item)
    {
        if (_items.Remove(item))
        {
            OnItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    public void RemoveFirst()
    {
        if (_items.Count == 0)
            return;
        
        Item removed = _items[0];
        _items.RemoveAt(0);
        OnItemRemoved?.Invoke(removed);
    }

    public void SetCapacity(int capacity)
    {
        _capacity = capacity;
    }

    public int GetCapacity()
    {
        return _capacity;
    }

    public int Count()
    {
        return _items.Count;
    }
}