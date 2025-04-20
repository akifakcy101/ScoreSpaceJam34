using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public static  Action<int> OnDamaged;
    public static  Action OnPointAcquired;
    public static  Action OnGameEnd;

    private int pointAcquired = 0;


    private void OnEnable()
    {
        OnPointAcquired += IncreasePoint;
    }

    private void OnDisable()
    {
        OnPointAcquired -= IncreasePoint;
    }

    private void IncreasePoint()
    {
        pointAcquired++;
    }

    private void Update()
    {
        Debug.Log("Puan:" + pointAcquired);
    }
}
