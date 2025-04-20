using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField memberinput;
    public TextMeshProUGUI ymean;
   

    public void PlayGame()
    {
        string input = memberinput.text.Trim();

        if (!string.IsNullOrEmpty(input) && !input.Contains(" "))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            ymean.gameObject.SetActive(true); 
        }
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
