using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    public InventorySlot()
    {
        item = null;
        amount = 0;
    }

    public InventorySlot(ItemData newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }
}