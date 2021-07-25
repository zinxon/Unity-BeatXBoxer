using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class GameplayMenu : UIBaseClass
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            LoadScene("TitleScene");
            ShowUI("TitleMenu");
            CloseUI("GameplayMenu");
        }
    }

    public void LoadScene(string sceneName)
    {
        GameManager.GetInstance().LoadScene(sceneName);
    }
}
