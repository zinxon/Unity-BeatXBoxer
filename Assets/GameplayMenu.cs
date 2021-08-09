using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class GameplayMenu : UIBaseClass
{
    private void Update()
    {
        if (LevelManager.GetInstance().gameplayEnum == GameplayEnum.Result && LevelManager.GetInstance().isResult == false)
        {
            ShowUI("FinishMenu");
            LevelManager.GetInstance().isResult = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        GameManager.GetInstance().LoadScene(sceneName);
    }
}
