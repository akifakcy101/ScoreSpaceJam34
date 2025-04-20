using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public static  Action<int> OnDamaged;
    public static  Action OnPointAcquired;
    public static  Action OnGameEnd;

    [HideInInspector]public int pointAcquired = 0;
    [SerializeField] private int gameStartHealt;
    [HideInInspector] public int healtPoint;
    [HideInInspector] public GameState gameState = GameState.Continue;
    [SerializeField] private GameObject pauseScreenPanel;


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
    private void OnEnable()
    {
        OnPointAcquired += IncreasePoint;
        //Not Working Without Lambda Expression(Search)
        PlayerInputManager.instance.playerInput.Player.PauseButton.performed += i => { ChangeGameState(); };
    }

    private void OnDisable()
    {
        OnPointAcquired -= IncreasePoint;
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
            pauseScreenPanel.SetActive(true);
            gameState = GameState.Paused;
        }
        else if(gameState == GameState.Paused)
        {
            Time.timeScale = 1f;
            pauseScreenPanel.SetActive(false);
            gameState = GameState.Continue;
        }
    }

    private void Update()
    {
        Debug.Log("Puan:" + pointAcquired);
    }
}


public enum GameState
{
    Paused,
    Continue
}
