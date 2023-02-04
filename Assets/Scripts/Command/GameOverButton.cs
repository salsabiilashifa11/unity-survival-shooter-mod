using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButton : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameObject.Find("GameOverButton").SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
  }
}
