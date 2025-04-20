using UnityEngine;

public class LifeSaverLocomotion : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float forcePower;
    private Vector2 mouseStartPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInputManager.instance.playerInput.Player.MouseDrag.started += i => { mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); };
        PlayerInputManager.instance.playerInput.Player.MouseDrag.performed += i => SwingTheLifeSaver(mouseStartPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    void SwingTheLifeSaver(Vector2 startPos,Vector2 endPos)
    {
        Vector2 targetDirection = endPos - startPos;
        rb.AddForce(targetDirection.normalized * forcePower, ForceMode2D.Impulse);
    }
}
