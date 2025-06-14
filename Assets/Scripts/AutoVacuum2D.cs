using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AutoVacuum2D : MonoBehaviour
{
    [Header("Tốc độ")]
    public float moveSpeed = 2f;               
    public float rotationSpeed = 180f;         // Tốc độ quay (độ/giây)

    private Rigidbody2D rb;
    private bool isTurning = false;
    private Quaternion targetRotation;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        SetMoveDirectionFromRotation(); // Di chuyển ban đầu theo hướng cái đầu
    }

    void FixedUpdate()
    {
        if (!isTurning)
        {
            // Di chuyển theo hướng cái đầu
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;

            // Quay từ từ đến hướng mới
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

            // Khi quay xong thì cập nhật lại hướng di chuyển
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {
                transform.rotation = targetRotation;
                isTurning = false;
                SetMoveDirectionFromRotation();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTurning)
        {
            StartCoroutine(RotateAndChangeDirection());
        }
    }

    private IEnumerator RotateAndChangeDirection()
    {
        isTurning = true;

        // Chọn góc quay ngẫu nhiên (quay khác hẳn)
        float randomAngle = Random.Range(120f, 240f);
        float currentZ = transform.eulerAngles.z;
        float newZ = currentZ + randomAngle;
        targetRotation = Quaternion.Euler(0, 0, newZ);

        yield return null;
    }

    private void SetMoveDirectionFromRotation()
    {
        // Hướng cái đầu (trục Y của đối tượng)
        moveDirection = transform.up;
    }
}
