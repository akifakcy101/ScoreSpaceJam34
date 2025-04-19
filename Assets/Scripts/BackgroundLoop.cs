using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Transform cameraTransform;         // Kamerayý takip eder
    public GameObject backgroundPrefab;       // Tek bir deniz görseli prefab
    public float backgroundHeight = 10f;      // Bir görselin yüksekliði
    public float spawnDistanceAhead = 15f;    // Kamera bu kadar yaklaþtýðýnda spawn edilir
    public float removeDistanceBehind = 20f;  // Kamera bu kadar uzaklaþtýðýnda silinir

    private List<GameObject> backgrounds = new List<GameObject>();
    private float lastSpawnY;

    void Start()
    {
        // Ýlk background'u baþlat
        GameObject first = Instantiate(backgroundPrefab, new Vector3(0, 5, 0.1f), Quaternion.identity);
        backgrounds.Add(first);
        lastSpawnY = 0;
    }

    void Update()
    {
        float camY = cameraTransform.position.y;

        // Spawn yeni arka plan (kameranýn yukarýsýna)
        if (camY + spawnDistanceAhead > lastSpawnY)
        {
            lastSpawnY += backgroundHeight;
            GameObject newBG = Instantiate(backgroundPrefab, new Vector3(0, lastSpawnY, 0.1f), Quaternion.identity);
            backgrounds.Add(newBG);
        }

        // Çok aþaðýdaki eski background'larý sil
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
