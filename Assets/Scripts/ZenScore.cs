using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenScore : MonoBehaviour
{
  private Transform zenEntryContainer;
  private Transform zenEntryTemplate;
  //   private List<ZenHighscoreEntry> ZenHighscoreEntryList;
  private List<Transform> ZenHighscoreEntryTransformList;

  private void Awake()
  {
    zenEntryContainer = transform.Find("ZenHighScoreEntryContainer");
    zenEntryTemplate = zenEntryContainer.Find("ZenHighScoreEntryTemplate");

    zenEntryTemplate.gameObject.SetActive(false);

    string jsonString = PlayerPrefs.GetString("ZenScoreTable");
    ZenHighscores ZenHighscores = JsonUtility.FromJson<ZenHighscores>(jsonString);
    Debug.Log(jsonString);
    // ZenHighscoreEntryList = ZenHighscores.ZenHighscoreEntryList;

    // ZenHighscoreEntryList = new List<ZenHighscoreEntry>() {
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "AAA"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "BBB"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "CCC"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "DDD"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "EEE"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "FFF"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "GGG"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "HHH"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "III"},
    //     new ZenHighscoreEntry{time = 1,timeString = "00:00:01", name = "JJJ"},
    // };

    // Sort the entries
    for (int i = 0; i < ZenHighscores.ZenHighscoreEntryList.Count - 1; i++)
    {
      for (int j = 0; j < ZenHighscores.ZenHighscoreEntryList.Count - i - 1; j++)
      {
        if (ZenHighscores.ZenHighscoreEntryList[j].time < ZenHighscores.ZenHighscoreEntryList[j + 1].time)
        {
          // ZenHighscoreEntry tmp = ZenHighscores.ZenHighscoreEntryList[i];
          int tmpTime = ZenHighscores.ZenHighscoreEntryList[j].time;
          string tmpTimeString = ZenHighscores.ZenHighscoreEntryList[j].timeString;
          string tmpName = ZenHighscores.ZenHighscoreEntryList[j].name;

          ZenHighscores.ZenHighscoreEntryList[j].timeString = ZenHighscores.ZenHighscoreEntryList[j + 1].timeString;
          ZenHighscores.ZenHighscoreEntryList[j].time = ZenHighscores.ZenHighscoreEntryList[j + 1].time;
          ZenHighscores.ZenHighscoreEntryList[j].name = ZenHighscores.ZenHighscoreEntryList[j + 1].name;

          ZenHighscores.ZenHighscoreEntryList[j + 1].timeString = tmpTimeString;
          ZenHighscores.ZenHighscoreEntryList[j + 1].time = tmpTime;
          ZenHighscores.ZenHighscoreEntryList[j + 1].name = tmpName;
        }
      }
    }

    ZenHighscoreEntryTransformList = new List<Transform>();

    if (ZenHighscores.ZenHighscoreEntryList.Count > 10)
    {
      for (int i = 0; i < 10; i++)
      {
        CreateZenHighScoreEntryTransform(ZenHighscores.ZenHighscoreEntryList[i], zenEntryContainer, ZenHighscoreEntryTransformList);
      }
    }
    else
    {
      for (int i = 0; i < ZenHighscores.ZenHighscoreEntryList.Count; i++)
      {
        CreateZenHighScoreEntryTransform(ZenHighscores.ZenHighscoreEntryList[i], zenEntryContainer, ZenHighscoreEntryTransformList);
      }
    }

    // ZenHighscores highscores = new ZenHighscores { ZenHighscoreEntryList = ZenHighscoreEntryList };
    // string json = JsonUtility.ToJson(highscores);

    // PlayerPrefs.SetString("ZenScoreTable", json);
    // PlayerPrefs.Save();
    // Debug.Log(PlayerPrefs.GetString("ZenScoreTable"));
  }

  private void CreateZenHighScoreEntryTransform(ZenHighscoreEntry zenHighscoreEntry, Transform container, List<Transform> transformList)
  {
    float templateHeight = 30f;
    Transform entryTransform = Instantiate(zenEntryTemplate, container);
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

    string timeString = zenHighscoreEntry.timeString;
    entryTransform.Find("timeText").GetComponent<Text>().text = timeString;

    entryTransform.Find("nameText").GetComponent<Text>().text = zenHighscoreEntry.name;

    transformList.Add(entryTransform);
  }

  public static void AddZenHighScoreEntry(int time, string timeString, string name)
  {
    ZenHighscoreEntry zenHighscoreEntry = new ZenHighscoreEntry { time = time, timeString = timeString, name = name };

    string jsonString = PlayerPrefs.GetString("ZenScoreTable");
    ZenHighscores zenHighscores = JsonUtility.FromJson<ZenHighscores>(jsonString);
    zenHighscores.ZenHighscoreEntryList.Add(zenHighscoreEntry);
    Debug.Log(zenHighscores.ZenHighscoreEntryList);

    string json = JsonUtility.ToJson(zenHighscores);
    PlayerPrefs.SetString("ZenScoreTable", json);
    PlayerPrefs.Save();
  }

  private class ZenHighscores
  {
    public List<ZenHighscoreEntry> ZenHighscoreEntryList;
  }

  /*
  * Represents a single entry in the high score table.
  */
  [System.Serializable]
  private class ZenHighscoreEntry
  {
    public int time;
    public string timeString;
    public string name;
  }
}
