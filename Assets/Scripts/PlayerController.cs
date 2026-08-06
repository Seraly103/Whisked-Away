using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (rb != null)
        {
            rb.linearVelocity = movementInput * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager gameManager = GameManager.Instance;

            if (gameManager != null && gameManager.tileManager != null)
            {
                if (gameManager.tileManager.IsInteractableTile(transform.position))
                {
                    Debug.Log("Player is on an interactable tile!");
                }
            }
        }
    }
}


