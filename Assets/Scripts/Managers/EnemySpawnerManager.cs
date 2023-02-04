using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public int chosenModeIdx;
    public GameObject[] availableModes;
    
    GameObject chosenMode;
    
    // Start is called before the first frame update
    void Start()
    {
        setMode(chosenModeIdx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setMode(int chosenIdx) {
        chosenMode = availableModes[chosenIdx];
        for (int i=0; i<availableModes.Length; i++) {
            if (i == chosenIdx) {
                availableModes[i].SetActive(true);
            } else {
                availableModes[i].SetActive(false);
            }
        }
    }
}
