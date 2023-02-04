using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeManager : MonoBehaviour
{
    public Button DiagonalButton;
    public Button FasterButton;
    public Text DiagText;
    public Text FasterText;

    int DiagLvl;
    int MaxDiagLvl = 3;
    int FasterLvl;
    int MaxFasterLvl = 8;
    // Start is called before the first frame update
    void Start()
    {
        DiagLvl = 1;
        FasterLvl = 1;
        DiagText.text = "lv. "+DiagLvl;
        FasterText.text = "lv. "+FasterLvl;
    }

    // Update is called once per frame
    void Update()
    {
        DiagText.text = "lv. "+DiagLvl;
        FasterText.text = "lv. "+FasterLvl;
    }

    public void showButtons()
    {
        if (DiagLvl < MaxDiagLvl){
            DiagonalButton.gameObject.SetActive(true);
        }
        if (FasterLvl < MaxFasterLvl){
            FasterButton.gameObject.SetActive(true);
        }
    }

    public void clickDiagonalButton()
    {
        if (DiagLvl < MaxDiagLvl){
            DiagLvl++;
        }
        DiagonalButton.gameObject.SetActive(false);
        FasterButton.gameObject.SetActive(false);
    }

    public void clickFasterButton()
    {
        if (FasterLvl < MaxFasterLvl){
            FasterLvl++;
        }
        DiagonalButton.gameObject.SetActive(false);
        FasterButton.gameObject.SetActive(false);
    }
}
