using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
  private Transform entryContainer;
  private Transform entryTemplate;
  // private List<HighscoreEntry> highscoreEntryList;
  private List<Transform> highScoreEntryTransformList;

  private void Awake()
  {
    entryContainer = transform.Find("highScoreEntryContainer");
    entryTemplate = entryContainer.Find("highScoreEntryTemplate");

    entryTemplate.gameObject.SetActive(false);

    string jsonString = PlayerPrefs.GetString("scoreTable");
    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    // highscoreEntryList = highscores.highscoreEntryList;

    // highscoreEntryList = new List<HighscoreEntry>(){
    //   new HighscoreEntry{score = 10000, name = "AAA", wave = 1},
    //   new HighscoreEntry{score = 9000, name = "BBB", wave = 2},
    //   new HighscoreEntry{score = 8000, name = "CCC", wave = 3},
    //   new HighscoreEntry{score = 5000, name = "DDD", wave = 4},
    //   new HighscoreEntry{score = 6000, name = "EEE", wave = 5},
    //   new HighscoreEntry{score = 5000, name = "FFF", wave = 6},
    //   new HighscoreEntry{score = 4000, name = "GGG", wave = 7},
    //   new HighscoreEntry{score = 3000, name = "HHH", wave = 8},
    //   new HighscoreEntry{score = 2000, name = "III", wave = 9},
    //   new HighscoreEntry{score = 1000, name = "JJJ", wave = 10},
    // };

    // Sort the entries
    for (int i = 0; i < highscores.highscoreEntryList.Count - 1; i++)
    {
      for (int j = 0; j < highscores.highscoreEntryList.Count - i - 1; j++)
      {
        if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[j + 1].score)
        {
          // HighscoreEntry tmp = highscores.highscoreEntryList[i];
          int tmpScore = highscores.highscoreEntryList[j].score;
          string tmpName = highscores.highscoreEntryList[j].name;
          int tmpWave = highscores.highscoreEntryList[j].wave;

          // highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
          highscores.highscoreEntryList[j].score = highscores.highscoreEntryList[j + 1].score;
          highscores.highscoreEntryList[j].name = highscores.highscoreEntryList[j + 1].name;
          highscores.highscoreEntryList[j].wave = highscores.highscoreEntryList[j + 1].wave;

          highscores.highscoreEntryList[j + 1].name = tmpName;
          highscores.highscoreEntryList[j + 1].score = tmpScore;
          highscores.highscoreEntryList[j + 1].wave = tmpWave;
        }
      }
    }
    Debug.Log(PlayerPrefs.GetString("scoreTable"));

    highScoreEntryTransformList = new List<Transform>();

    if (highscores.highscoreEntryList.Count > 10)
    {
      for (int i = 0; i < 10; i++)
      {
        CreateHighScoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highScoreEntryTransformList);
      }
    }
    else
    {
      foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
      {
        CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highScoreEntryTransformList);
      }
    }

    // Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
    // string json = JsonUtility.ToJson(highscores);
    // Debug.Log(json);
    // PlayerPrefs.SetString("scoreTable", json);
    // PlayerPrefs.Save();

  }

  private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
  {
    float templateHeight = 30f;
    Transform entryTransform = Instantiate(entryTemplate, container);
    RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
    entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
    entryTransform.gameObject.SetActive(true);

    int rank = transformList.Count + 1;
    string rankString;
    switch (rank)
    {
      default:
        rankString = rank + "TH"; break;
      case 1: rankString = "1ST"; break;
      case 2: rankString = "2ND"; break;
      case 3: rankString = "3RD"; break;
    }

    entryTransform.Find("posText").GetComponent<Text>().text = rankString;
    int score = highscoreEntry.score;
    entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
    int wave = highscoreEntry.wave;
    entryTransform.Find("waveText").GetComponent<Text>().text = wave.ToString();
    string name = highscoreEntry.name;
    entryTransform.Find("nameText").GetComponent<Text>().text = name;

    transformList.Add(entryTransform);
  }

  public static void AddHighscoreEntry(int score, string name, int wave)
  {
    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, wave = wave };
    string jsonString = PlayerPrefs.GetString("scoreTable");
    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    highscores.highscoreEntryList.Add(highscoreEntry);

    string json = JsonUtility.ToJson(highscores);
    PlayerPrefs.SetString("scoreTable", json);
    PlayerPrefs.Save();

  }

  private class Highscores
  {
    public List<HighscoreEntry> highscoreEntryList;
  }

  /*
  * Represents a single entry in the high score table.
  */
  [System.Serializable]
  private class HighscoreEntry
  {
    public int score;
    public int wave;
    public string name;
  }
}
