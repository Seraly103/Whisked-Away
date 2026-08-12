using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text amountText;

    public int slotIndex;
    private Canvas canvas;
    private Transform originalParent;

    [SerializeField] private GameObject highlightOne;
    [SerializeField] private GameObject highlightTwo;

    [SerializeField] private RectTransform inventoryPanel;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        amountText.color = Color.black;
    }

    public void SetInventoryPanel(RectTransform panel)
    {
        inventoryPanel = panel;
    }

    public void SetSlot(InventorySlot slot)
    {
        if (slot.item != null)
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.enabled = true;

             amountText.text = slot.amount > 1
            ? slot.amount.ToString()
            : "";
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotIndex >= InventoryManager.Instance.inventory.Count)
            return;

        if (InventoryManager.Instance.inventory[slotIndex].item == null)
            return;

        InventoryManager.Instance.SelectSlot(slotIndex);
    }
 
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotIndex >= InventoryManager.Instance.inventory.Count)
            return;

        if (InventoryManager.Instance.inventory[slotIndex].item == null)
            return;

        originalParent = itemIcon.transform.parent;

        itemIcon.transform.SetParent(canvas.transform);
        itemIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!itemIcon.enabled)
            return;

        itemIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         // Put icon back where it belongs
        itemIcon.transform.SetParent(originalParent);
        itemIcon.transform.localPosition = Vector3.zero;

        itemIcon.raycastTarget = true;

        if (slotIndex >= InventoryManager.Instance.inventory.Count)
            return;

        if (InventoryManager.Instance.inventory[slotIndex].item == null)
            return;

        // Only check for dropping outside if this SlotUI
        // actually belongs to the inventory
        if (inventoryPanel != null)
        {
            bool insideInventory =
                RectTransformUtility.RectangleContainsScreenPoint(
                    inventoryPanel,
                    eventData.position,
                    eventData.pressEventCamera
                );

            if (!insideInventory)
            {
                InventoryManager.Instance.DropItem(slotIndex);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotUI draggedSlot =
            eventData.pointerDrag.GetComponent<SlotUI>();

        if (draggedSlot == null)
            return;

        InventoryManager.Instance.SwapItems(
            draggedSlot.slotIndex,
            slotIndex
        );
    }

    public void SetSelected(bool selected)
    {
        if (highlightOne != null)
            highlightOne.SetActive(selected);

        if (highlightTwo != null)
            highlightTwo.SetActive(selected);
    }

   
}