using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance { get; private set; }

    public PlayerInput playerInput;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Enable();

            //playerInput.Player.MouseDrag.started += i => { Debug.Log("Starteed"); };
            //playerInput.Player.MouseDrag.performed += i => { Debug.Log("Performed"); };
            //playerInput.Player.MouseDrag.canceled += i => { Debug.Log("Canceled"); };

        }
    }
    private void OnDisable()
    {
        if(playerInput != null)
        {
            playerInput.Disable();
        }
    }


}
