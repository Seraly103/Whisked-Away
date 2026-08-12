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
        // First try to add to an existing stack
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == item)
            {
                slot.amount += amount;

                Debug.Log(item.itemName + " x" + slot.amount);

                RefreshUI();
                return;
            }
        }

        // Otherwise use the first empty slot
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.amount = amount;

                Debug.Log(item.itemName + " added!");

                RefreshUI();
                return;
            }
        }

        Debug.Log("Inventory full!");
    }

    
    public void SwapItems(int firstIndex, int secondIndex)
    {
        
        
        if (firstIndex < 0 || firstIndex >= inventory.Count)
            return;

        if (secondIndex < 0 || secondIndex >= inventory.Count)
            return;

        InventorySlot temp = inventory[firstIndex];

        inventory[firstIndex] = inventory[secondIndex];
        inventory[secondIndex] = temp;

        RefreshUI();
    }

    private void RefreshUI()
    {
        IventoryUI inventoryUI = FindFirstObjectByType<IventoryUI>();

        if (inventoryUI != null)
        {
            inventoryUI.RefreshInventoryUI();
        }

        ToolbarUI toolbarUI = FindFirstObjectByType<ToolbarUI>();

        if (toolbarUI != null)
        {
            toolbarUI.RefreshToolBar();
        }
    }
}
