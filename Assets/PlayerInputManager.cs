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

            playerInput.Player.MouseDrag.performed += i => { Debug.Log("A"); };

        }
    }


}
