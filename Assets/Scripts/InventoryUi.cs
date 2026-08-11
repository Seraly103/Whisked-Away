using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    //public Player player;

    //public List <Slot_UI> slots = new List<SlotUI>();
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    void SetUp()
    {
        
    }
}
