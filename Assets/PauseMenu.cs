using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class PauseMenu : UIBaseClass
{
    private void Start() {
        LevelManager.GetInstance().SetGameplayEnum(GameplayEnum.Pause);
        RigisterButtonOnClick("ResumeBtn", p => {
            CloseUI("PauseMenu");
            FindObjectOfType<MusicPlayer>().PauseAudioPlaying();
            LevelManager.GetInstance().SetGameplayEnum(GameplayEnum.Playing);
        });
        RigisterButtonOnClick("ExitBtn", p => {
            GameManager.GetInstance().LoadScene("TitleScene");
            CloseUI("PauseMenu");
            CloseUI("GameplayMenu");
            ShowUI("TitleMenu");
        });
    }
}
