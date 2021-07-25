using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : UnitySingleton<LevelManager>
{
    public GameplayEnum gameplayEnum { get; private set; } = GameplayEnum.Start;

    public int CombineValue { get; private set; }
    public int ScoreValue { get; private set; }

    private TextMeshProUGUI combineTxt, scoreTxt, msgTxt;
    private float msgTimer;
    private float msgMaxTime = 1;

    private void Start()
    {
        gameplayEnum = GameplayEnum.Playing;

        combineTxt = GameObject.Find("CombineTxt").GetComponent<TextMeshProUGUI>();
        if (combineTxt)
            combineTxt.text = "Combine: 0";

        scoreTxt = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        if (scoreTxt)
            scoreTxt.text = "Score: 0";

        msgTxt = GameObject.Find("MsgTxt").GetComponent<TextMeshProUGUI>();
        if (msgTxt)
            msgTxt.enabled = false;

        msgTimer = 0;
    }

    private void Update()
    {
        if (msgTimer > 0)
        {
            msgTimer -= Time.deltaTime;
            msgTxt.color = new Color(msgTxt.color.r, msgTxt.color.g, msgTxt.color.b, msgTimer);
        }
        else
        {
            if (msgTxt)
                msgTxt.enabled = false;
        }
    }

    public void SetMessageText(int state)
    {
        msgTxt.enabled = true;
        switch (state)
        {
            case 0:
                msgTxt.text = "Miss";
                msgTxt.color = new Color(0.75f, 0.75f, 0.75f);
                break;
            case 1:
                msgTxt.color = new Color(0.95f, 0.83f, 0.33f);
                msgTxt.text = "Great";
                break;
            case 2:
                msgTxt.color = new Color(0.52f, 0.94f, 0.32f);
                msgTxt.text = "Perfect";
                break;
            default:
                break;
        }

        msgTimer = msgMaxTime;
    }

    public void SetCombine(bool isClickSuccess)
    {
        if (isClickSuccess)
            CombineValue++;
        else
            CombineValue = 0;

        if (combineTxt)
            combineTxt.text = string.Format("Combine: {0}", CombineValue);
    }

    public void SetScore(bool isPerfect)
    {
        if (isPerfect)
        {
            ScoreValue += 200;
        }
        else
        {
            ScoreValue += 100;
        }

        if (scoreTxt)
            scoreTxt.text = string.Format("Score: {0}", ScoreValue);
    }
}

public enum GameplayEnum { Start, Playing, Pause, Result }

