using UnityEngine;

public class LifeSaverLocomotion : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float forcePower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerInputManager.instance.playerInput.Player.MouseDrag.performed += i => SwingTheLifeSaver(i.ReadValue<Vector2>().normalized * forcePower);
    }
    void SwingTheLifeSaver(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
