using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private TileManager tileManager;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    [SerializeField] private ToolbarUI toolbarUI;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = movementInput * speed;

        if (Keyboard.current != null &&
        Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UseSelectedTool();
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);

            foreach (Collider2D hit in hits)
            {
                CollectableManager collectable = hit.GetComponent<CollectableManager>();

                if (collectable != null)
                {
                    Debug.Log("Found collectable: " + hit.gameObject.name);

                    collectable.Collect();
                    break;
                }
            }
        }
    }

    private void UseSelectedTool()
    {
        ItemData selectedItem = toolbarUI.GetSelectedItem();

        if (selectedItem == null)
            return;

        if (selectedItem.itemName == "Hoe")
        {
            if (tileManager.IsTileInteractable(transform.position))
            {
                tileManager.SetTilledTile(transform.position);

                Debug.Log("Used hoe!");
            }
        }
    }

    
}


