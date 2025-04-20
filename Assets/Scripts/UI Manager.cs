using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text healtText;
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private TMP_Text healtPointText;
    [SerializeField] private TMP_Text pointNumberText;
    [SerializeField] private TMP_Text endGameScorePointText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private float panelFinalAlpha;
    [SerializeField] private float panelFadeInTime;


    private void OnDisable()
    {
        GameManager.OnDamaged -= ChangeHealtPointText;
        GameManager.OnPointAcquired -= ChangePointText;
        GameManager.OnGameEnd -= EndGame;
    }

    private void Start()
    {
        GameManager.OnDamaged += ChangeHealtPointText;
        GameManager.OnPointAcquired += ChangePointText;
        GameManager.OnGameEnd += EndGame;
        pointNumberText.text = GameManager.instance.pointAcquired.ToString();
        healtPointText.text = GameManager.instance.healtPoint.ToString();
    }
    private void ChangePointText()
    {
        pointNumberText.text = GameManager.instance.pointAcquired.ToString();
    }

    //Research For Actions Which Used Without And With Paramaters
    private void ChangeHealtPointText(int holder)
    {
        healtPointText.text = GameManager.instance.healtPoint.ToString();
    }
    private void EndGame()
    {
        healtText.enabled = false;
        pointText.enabled = false;
        healtPointText.enabled = false;
        pointNumberText.enabled = false;
        endGameScorePointText.text = GameManager.instance.pointAcquired.ToString();
        endGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //This is not working
    private IEnumerator PanelFade(float time)
    {
        var panelImage = endGamePanel.GetComponent<UnityEngine.UI.Image>().color;
        float timer = 0f;
        while(time >= timer)
        {
            panelImage.a = Mathf.Lerp(panelImage.a,panelFinalAlpha,timer / time);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
