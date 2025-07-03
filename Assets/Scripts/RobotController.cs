using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 180f;

    private Rigidbody2D rb;
    private float moveInput;
    private float rotateInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");       
        rotateInput = -Input.GetAxisRaw("Horizontal");  
        
    }

    void FixedUpdate()
    {
        Vector2 movement = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        float rotation = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }
}
