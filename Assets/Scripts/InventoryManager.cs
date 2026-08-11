using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
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

        inventory.Add(new InventorySlot(item, amount));

        Debug.Log(item.itemName + " x" + amount);
    }

    

}
