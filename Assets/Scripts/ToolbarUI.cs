using UnityEngine;
using UnityEngine.InputSystem;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private SlotUI[] toolbarSlots = new SlotUI[9];

    private SlotUI selectedSlot;

    
    void Start()
    {
        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            toolbarSlots[i].slotIndex = i;
        }

        RefreshToolBar();
    }
    public void RefreshToolBar()
    {
        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            InventorySlot inventorySlot =
                InventoryManager.Instance.inventory[i];

            toolbarSlots[i].SetSlot(inventorySlot);
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= toolbarSlots.Length)
            return;

        // If this slot is already selected, deselect it
        if (selectedSlot == toolbarSlots[index])
        {
            selectedSlot.SetSelected(false);
            selectedSlot = null;

            Debug.Log("Toolbar slot deselected");
            return;
        }

        // Turn off the previous selected slot
        if (selectedSlot != null)
        {
            selectedSlot.SetSelected(false);
        }

        // Select the new slot
        selectedSlot = toolbarSlots[index];
        selectedSlot.SetSelected(true);

        Debug.Log($"Selected Slot: {selectedSlot.slotIndex}");
    }

    public void SelectSlot1(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(0);
    }

    public void SelectSlot2(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(1);
    }

    public void SelectSlot3(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(2);
    }

    public void SelectSlot4(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(3);
    }

    public void SelectSlot5(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(4);
    }

    public void SelectSlot6(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(5);
    }

    public void SelectSlot7(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(6);
    }

    public void SelectSlot8(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(7);
    }

    public void SelectSlot9(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectSlot(8);
    }
}