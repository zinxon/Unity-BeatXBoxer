using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : UIBaseClass
{
    private Button startBtn, exitBtn, clickBtn;

    private void Start()
    {
        startBtn = UnityHelper.FindChildNode(gameObject, "StartBtn").GetComponent<Button>();
        exitBtn = UnityHelper.FindChildNode(gameObject, "ExitBtn").GetComponent<Button>();
        clickBtn = UnityHelper.FindChildNode(gameObject, "ClickBtn").GetComponent<Button>();


        startBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);


        RigisterButtonOnClick("StartBtn", p => {
            SceneManager.LoadScene("GameplayScene");
            ShowUI("GameplayMenu");
            CloseUI("TitleMenu");
        });
        RigisterButtonOnClick("ExitBtn", p => Application.Quit());
        RigisterButtonOnClick("ClickBtn", p => ClickButtonSetting());
    }

    private void ClickButtonSetting()
    {
        clickBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);
    }
}
