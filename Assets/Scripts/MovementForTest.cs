using UnityEngine;

public class MovementForTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.up * moveSpeed;
    }
}
