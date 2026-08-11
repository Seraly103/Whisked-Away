using UnityEngine;
using System.Collections.Generic;

public class IventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    private List<SlotUI> slots = new List<SlotUI>();

    void Start()
    {
        SetUp();
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryUI();
        }
    }

    void SetUp()
    {
        slots.AddRange(
            inventoryPanel.GetComponentsInChildren<SlotUI>(true)
        );

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].slotIndex = i;
        }

        Debug.Log("Found " + slots.Count + " inventory slots!");
    }

    public void ToggleInventoryUI()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            RefreshInventoryUI();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void RefreshInventoryUI()
    {
        List<InventorySlot> inventory =
            InventoryManager.Instance.inventory;

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.Count)
            {
                slots[i].SetSlot(inventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
