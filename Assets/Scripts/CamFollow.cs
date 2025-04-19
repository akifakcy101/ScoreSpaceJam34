using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 5f; 

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(transform.position.y, target.position.y, smoothSpeed * Time.deltaTime);//for smoothness
            transform.position = newPos;
        }
    }
}
