using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public static  Action<int> OnDamaged;
    public static  Action OnPointAcquired;
    public static  Action OnGameEnd;
    public static Action OnGameStateChanged;

    [HideInInspector]public int pointAcquired = 0;
    [SerializeField] private int gameStartHealt;
    [HideInInspector] public int healtPoint;
    [HideInInspector] public GameState gameState = GameState.Continue;


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
        healtPoint = gameStartHealt;
        pointAcquired = 0;
    }

    private void Start()
    {
        gameState = GameState.Continue;
        PlayerInputManager.instance.playerInput.Player.PauseButton.performed += i => { if (gameState != GameState.Ended) { OnGameStateChanged?.Invoke(); } };
    }
    private void OnEnable()
    {
        OnPointAcquired += IncreasePoint;
        OnGameStateChanged += ChangeGameState;
    }

    private void OnDisable()
    {
        OnPointAcquired -= IncreasePoint;
        OnGameStateChanged -= ChangeGameState;
    }


    private void IncreasePoint()
    {
        pointAcquired++;
    }
    
    //Write This System  In UI Manager
    private void ChangeGameState()
    {

        if(gameState == GameState.Continue)
        {
            Time.timeScale = 0f;
            gameState = GameState.Paused;
        }
        else if(gameState == GameState.Paused)
        {
            Time.timeScale = 1f;
            gameState = GameState.Continue;
        }
    }

    private void Update()
    {
        Debug.Log(gameState.ToString());
    }
}


public enum GameState
{
    Paused,
    Continue,
    Ended
}
