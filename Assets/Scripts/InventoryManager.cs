using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private int inventorySize = 80;

    public List<InventorySlot> inventory = new List<InventorySlot>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for(int i = 0; i < inventorySize; i++)
        {
            inventory.Add(new InventorySlot());
        }
    }

    public void AddItem(ItemData item, int amount)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == item)
            {
                slot.amount += amount;

                Debug.Log(item.itemName + " x" + slot.amount);
                return;
            }
        }

        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.amount = amount;

                Debug.Log(item.itemName + " added!");
                return;
            }
        }

        Debug.Log("Inventory full!");

        Debug.Log(item.itemName + " x" + amount);
    }

    
    public void SwapItems(int firstIndex, int secondIndex)
    {
        
        
        InventorySlot temp = inventory[firstIndex];

        inventory[firstIndex] = inventory[secondIndex];
        inventory[secondIndex] = temp;

        FindFirstObjectByType<IventoryUI>().RefreshInventoryUI();
    }
}
