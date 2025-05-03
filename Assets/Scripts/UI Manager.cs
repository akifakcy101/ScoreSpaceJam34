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
    [SerializeField] private GameObject pauseScreenPanel;


    private void OnDisable()
    {
        GameManager.OnDamaged -= ChangeHealtPointText;
        GameManager.OnPointAcquired -= ChangePointText;
        GameManager.OnGameEnd -= EndGame;
        GameManager.OnGameStateChanged -= ChangePauseMenuPanelEnable;
    }

    private void Start()
    {
        GameManager.OnDamaged += ChangeHealtPointText;
        GameManager.OnPointAcquired += ChangePointText;
        GameManager.OnGameEnd += EndGame;
        GameManager.OnGameStateChanged += ChangePauseMenuPanelEnable;
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
        GameManager.instance.gameState = GameState.Ended;

        Debug.Log(GameData.playerName + " " + GameManager.instance.pointAcquired);
        Scoreboard.SubmitScoreStatic(GameData.playerName, GameManager.instance.pointAcquired, 30764);
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
        while (time >= timer)
        {
            panelImage.a = Mathf.Lerp(panelImage.a, panelFinalAlpha, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangePauseMenuPanelEnable()
    {
        if (GameManager.instance.gameState == GameState.Continue)
        {
            pauseScreenPanel.SetActive(false);
            healtText.enabled = true;
            healtPointText.enabled = true;
            pointText.enabled = true;
            pointNumberText.enabled = true;
        }
        else if (GameManager.instance.gameState == GameState.Paused)
        {
            pauseScreenPanel.SetActive(true);
            healtText.enabled = false;
            healtPointText.enabled = false;
            pointText.enabled = false;
            pointNumberText.enabled = false;
        }
    }


}