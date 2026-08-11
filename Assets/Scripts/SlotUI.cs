using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text amountText;

    void Awake()
    {
        amountText.color = Color.white;
    }


    public void SetSlot(InventorySlot slot)
    {
        if (slot.item != null)
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.enabled = true;

            amountText.text = slot.amount.ToString();
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;

        amountText.text = "";
    }
}