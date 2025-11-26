using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void ShowSettingsMenu()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}