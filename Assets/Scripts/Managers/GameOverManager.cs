using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
  public Text warningText;
  public PlayerHealth playerHealth;
  public float restartDelay = 5f;

  public GameObject gameOverButton;

  Animator anim;
  float restartTimer;


  void Awake()
  {
    anim = GetComponentInParent<Animator>();
    gameOverButton = GameObject.Find("GameOverButton");
    gameOverButton.SetActive(false);

    if (PlayerPrefs.GetString("gameMode") == "Wave")
    {
      int score = PlayerPrefs.GetInt("score");
      int wave = PlayerPrefs.GetInt("wave") + 1;
      // string playerName = PlayerPrefs.GetString("playerName");
      string playerName = MainMenu.playerName;
      Debug.Log(playerName);
      HighScoreTable.AddHighscoreEntry(score, playerName, wave);
      Debug.Log("Wave Score Added");
    }
    else if (PlayerPrefs.GetString("gameMode") == "Zen")
    {
      int time = PlayerPrefs.GetInt("time");
      Debug.Log(time);
      string timeString = PlayerPrefs.GetString("timeString");
      Debug.Log(timeString);
      // string playerName = PlayerPrefs.GetString("playerName");
      string playerName = MainMenu.playerName;
      Debug.Log(playerName);
      ZenScore.AddZenHighScoreEntry(time, timeString, playerName);
      Debug.Log("Zen Score Added");
    }
  }


  void Update()
  {
    if (playerHealth.currentHealth <= 0)
    {
      anim.SetTrigger("GameOver");

      gameOverButton.SetActive(true);
    }
  }

  public void ShowWarning(float enemyDistance)
  {
    if (playerHealth.currentHealth > 0)
    {
      warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
      anim.SetTrigger("Warning");

    }
  }

  public void MainMenuButton()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void NewGameButton()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
