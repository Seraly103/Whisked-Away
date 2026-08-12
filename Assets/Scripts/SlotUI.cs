using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text amountText;

    public int slotIndex;
    private Canvas canvas;
    private Transform originalParent;

    [SerializeField] private GameObject highlightOne;
    [SerializeField] private GameObject highlightTwo;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        amountText.color = Color.black;
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

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotIndex >= InventoryManager.Instance.inventory.Count)
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
        itemIcon.transform.SetParent(originalParent);
        itemIcon.transform.localPosition = Vector3.zero;

        itemIcon.raycastTarget = true;
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
        highlightOne.SetActive(selected);
        highlightTwo.SetActive(selected);
    }
}