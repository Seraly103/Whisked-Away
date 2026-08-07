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
                Debug.Log("Im on a interactable tile!");
                tileManager.SetInteracted(transform.position);

            }
            else
            {
                Debug.Log("Im not on a interactable tile!");
                
            }
        }
    }
}


