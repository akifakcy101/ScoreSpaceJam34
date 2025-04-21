using UnityEngine;

public class LifeSaverManager : MonoBehaviour
{


    private void OnEnable()
    {
        GameManager.OnDamaged += ChangeLifeSaverHealt;
    }
    private void OnDisable()
    {
        GameManager.OnDamaged -= ChangeLifeSaverHealt;
    }

    private void ChangeLifeSaverHealt(int amount)
    {
        GameManager.instance.healtPoint += amount;
        if (GameManager.instance.healtPoint <= 0)
        {
            GameManager.OnGameEnd.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            GameManager.OnDamaged?.Invoke(-1);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Healt")
        {
            GameManager.OnDamaged?.Invoke(1);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "People")
        {
            GameManager.OnPointAcquired?.Invoke();
            Destroy(collision.gameObject);
        }

    }

    private void Update()
    {
    }
}
