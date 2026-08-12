using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private int inventorySize = 80;

    public List<InventorySlot> inventory = new List<InventorySlot>();

    private int selectedSlotIndex = -1;

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

    void Update()
    {
        if (Keyboard.current != null &&
        Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (selectedSlotIndex != -1)
            {
                DropItem(selectedSlotIndex);
            }
        }
    }

    public void SelectSlot(int index)
    {
        selectedSlotIndex = index;

        Debug.Log("Selected inventory slot: " + index);
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
        IventoryUI inventoryUI = FindAnyObjectByType<IventoryUI>();

        if (inventoryUI != null)
        {
            inventoryUI.RefreshInventoryUI();
        }

        ToolbarUI toolbarUI = FindAnyObjectByType<ToolbarUI>();

        if (toolbarUI != null)
        {
            toolbarUI.RefreshToolBar();
        }
    }

    public void DropItem(int slotIndex, int amount = 1)
    {
        if (slotIndex < 0 || slotIndex >= inventory.Count)
            return;

        InventorySlot slot = inventory[slotIndex];

        if (slot.item == null || slot.amount <= 0)
            return;

        ItemData itemToDrop = slot.item;

        if (itemToDrop.dropPrefab == null)
        {
            Debug.LogWarning(itemToDrop.itemName + " has no drop prefab!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 dropPosition = player.transform.position + new Vector3(0.5f, 0f, 0f);

        Instantiate(itemToDrop.dropPrefab, dropPosition, Quaternion.identity);

        slot.amount -= amount;

        slot.amount -= amount;

        if (slot.amount <= 0)
        {
            slot.item = null;
            slot.amount = 0;

            if (selectedSlotIndex == slotIndex)
            {
                selectedSlotIndex = -1;
            }
        }

        FindAnyObjectByType<IventoryUI>()?.RefreshInventoryUI();
        FindAnyObjectByType<ToolbarUI>()?.RefreshToolBar();
    }
}
