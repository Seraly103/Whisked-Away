using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private TileManager tileManager;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = movementInput * speed;
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (tileManager.IsTileInteractable(transform.position))
            {
                Debug.Log("Im on a ho tile!");
                tileManager.SetTilledTile(transform.position);
            }
            else
            {
                Debug.Log("Im not on a ho tile!");
            }

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
}


