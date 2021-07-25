using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class TitleMenu : UIBaseClass
{
    private void Start()
    {
        RigisterButtonOnClick("StartButton", p =>
        {
            LoadScene("GameplayScene");
            ShowUI("GameplayMenu");
            CloseUI("TitleMenu");
        });
    }

    public void LoadScene(string sceneName)
    {
        GameManager.GetInstance().LoadScene(sceneName);
    }
}
