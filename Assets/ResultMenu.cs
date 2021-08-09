using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFramework;
using UnityEngine;

public class ResultMenu : UIBaseClass
{
    private TextMeshProUGUI perfectValueTxt, greatValueTxt, missValueTxt, totalComboTxt;

    private void Start()
    {
        perfectValueTxt = UnityHelper.FindChildNode(gameObject, "PerfectValueTxt").GetComponent<TextMeshProUGUI>();
        greatValueTxt = UnityHelper.FindChildNode(gameObject, "GreatValueTxt").GetComponent<TextMeshProUGUI>();
        missValueTxt = UnityHelper.FindChildNode(gameObject, "MissValueTxt").GetComponent<TextMeshProUGUI>();
        totalComboTxt = UnityHelper.FindChildNode(gameObject, "TotalComboTxt").GetComponent<TextMeshProUGUI>();
    
        LevelManager levelMgr = LevelManager.GetInstance();

        perfectValueTxt.text = levelMgr.PerfectValue.ToString();
        greatValueTxt.text = levelMgr.GreatBValue.ToString();
        missValueTxt.text = levelMgr.MissValue.ToString();
        totalComboTxt.text = (levelMgr.PerfectValue + levelMgr.GreatBValue).ToString();
    }
}
