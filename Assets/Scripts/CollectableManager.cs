using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private int amount = 1;


    public void Collect()
    {

        
        InventoryManager.Instance.AddItem(item, amount);

        Debug.Log(amount + " " + item.itemName + " collected!");

        Destroy(gameObject);
    }

    
    
}
