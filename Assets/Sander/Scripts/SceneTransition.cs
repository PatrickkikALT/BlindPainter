using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
  public GameObject startMenu;
  public GameObject settingsMenu;
  public GameObject aboutMenu;

  public void ChangeScene(string sceneName) {
    SceneManager.LoadScene(sceneName);
  }

  public void Quit() {
    Application.Quit();
  }

  public void ShowStartMenu() {
    startMenu.SetActive(true);
    settingsMenu.SetActive(false);
    aboutMenu.SetActive(false);
  }

  public void ShowSettingsMenu() {
    startMenu.SetActive(false);
    settingsMenu.SetActive(true);
  }

  public void ShowAboutMenu() {
    startMenu.SetActive(false);
    aboutMenu.SetActive(true);
  }
}