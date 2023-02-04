using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public static string playerName;
  public void ZenMode()
  {
    PlayerPrefs.SetString("gameMode", "Zen");
    PlayerPrefs.Save();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void WaveMode()
  {
    PlayerPrefs.SetString("gameMode", "Wave");
    PlayerPrefs.Save();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
  }

  public void RushMode()
  {
    PlayerPrefs.SetString("gameMode", "Rush");
    PlayerPrefs.Save();
    SceneManager.LoadScene("RushMode");
  }

  public void WaveScoreBoard()
  {
    SceneManager.LoadScene("Scoreboard");
  }

  public void ZenScoreBoard()
  {
    SceneManager.LoadScene("ZenScoreBoard");
  }

  public void returnToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void ReadStringInput(string s)
  {
    playerName = s;
    // PlayerPrefs.SetString("playerName", playerName);
    // PlayerPrefs.Save();
  }

}
