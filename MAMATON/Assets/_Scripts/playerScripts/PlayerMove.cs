using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{

    public static PlayerMove instance;
    public float speed = 5f;      // Forward movement speed
    public float jumpForce = 5f;  // Jump strength
    private Rigidbody rb;
    public bool canMove = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent physics from rotating the player
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
        // Move the player forward automatically
    }
}
