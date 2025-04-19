using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Transform cameraTransform;         // Kameray� takip eder
    public GameObject backgroundPrefab;       // Tek bir deniz g�rseli prefab
    public float backgroundHeight = 10f;      // Bir g�rselin y�ksekli�i
    public float spawnDistanceAhead = 15f;    // Kamera bu kadar yakla�t���nda spawn edilir
    public float removeDistanceBehind = 20f;  // Kamera bu kadar uzakla�t���nda silinir

    private List<GameObject> backgrounds = new List<GameObject>();
    private float lastSpawnY;

    void Start()
    {
        // �lk background'u ba�lat
        GameObject first = Instantiate(backgroundPrefab, new Vector3(0, 5, 0.1f), Quaternion.identity);
        backgrounds.Add(first);
        lastSpawnY = 0;
    }

    void Update()
    {
        float camY = cameraTransform.position.y;

        // Spawn yeni arka plan (kameran�n yukar�s�na)
        if (camY + spawnDistanceAhead > lastSpawnY)
        {
            lastSpawnY += backgroundHeight;
            GameObject newBG = Instantiate(backgroundPrefab, new Vector3(0, lastSpawnY, 0.1f), Quaternion.identity);
            backgrounds.Add(newBG);
        }

        // �ok a�a��daki eski background'lar� sil
        for (int i = backgrounds.Count - 1; i >= 0; i--)
        {
            if (backgrounds[i].transform.position.y + backgroundHeight < camY - removeDistanceBehind)
            {
                Destroy(backgrounds[i]);
                backgrounds.RemoveAt(i);
            }
        }
    }
}
