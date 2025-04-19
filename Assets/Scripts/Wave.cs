using UnityEngine;

public class Wave : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
