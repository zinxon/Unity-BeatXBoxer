using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class NoteLanesController : MonoBehaviour
{
    // [Header("基本變量")]


    // [Header("需要預制體")]


    [Header("需要的場景物體")]
    [SerializeField] private Transform noteSpawnTrans;

    [Header("內部變量")]
    private int pendingEventID = 0;
    private List<KoreographyEvent> noteEventList = new List<KoreographyEvent>();
    private Queue<NoteController> trackedNotes = new Queue<NoteController>();

    private void Update()
    {
        if (LevelManager.GetInstance().gameplayEnum == GameplayEnum.Pause)
            return;

        while (trackedNotes.Count > 0 && trackedNotes.Peek().IsNoteMissed())
        {
            trackedNotes.Dequeue();
            LevelManager.GetInstance().SetCombine(false);
            LevelManager.GetInstance().SetMessageText(0);
        }

        CheckNextNoteForSpawn();
        InputSetting();
    }

    private void InputSetting()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (trackedNotes.Peek().NoteID == 1)
                CheckNoteHit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (trackedNotes.Peek().NoteID == 2)
                CheckNoteHit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (trackedNotes.Peek().NoteID == 3)
                CheckNoteHit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (trackedNotes.Peek().NoteID == 4)
                CheckNoteHit();
        }
    }

    private void CheckNextNoteForSpawn()
    {
        int samplesToTarget = GetSpawnSampleOffset();
        int curTime = MusicPlayer.GetInstance().DelayedSampleTime;

        while (pendingEventID < noteEventList.Count && noteEventList[pendingEventID].StartSample < curTime + samplesToTarget)
        {
            KoreographyEvent evt = noteEventList[pendingEventID];
            NoteController note = MusicPlayer.GetInstance().SpawnNote(noteSpawnTrans.localPosition, evt.GetIntValue());

            note.InitNoteController(evt, transform.localPosition);
            trackedNotes.Enqueue(note);
            pendingEventID++;
        }
    }

    private int GetSpawnSampleOffset()
    {
        if (!noteSpawnTrans)
            return 0;

        float spawnPos = noteSpawnTrans.localPosition.x - transform.localPosition.x;
        float timeOfStartToEnd = spawnPos / MusicPlayer.GetInstance().NoteSpeed;

        return (int)timeOfStartToEnd * MusicPlayer.GetInstance().SampleRate;
    }

    private void CheckNoteHit()
    {
        if (trackedNotes.Count > 0)
        {
            NoteController note = trackedNotes.Peek();
            if (note.HitOffset > -10000)
            {
                MusicPlayer.GetInstance().PlayMusicplayerSource();

                trackedNotes.Dequeue();

                int hitLevel = note.NoteHittable();

                if (hitLevel > 0)
                {
                    LevelManager.GetInstance().SetCombine(true);
                    if (hitLevel == 2)
                    {
                        LevelManager.GetInstance().SetScore(true);
                        LevelManager.GetInstance().SetMessageText(2);
                    }
                    else
                    {
                        LevelManager.GetInstance().SetScore(false);
                        LevelManager.GetInstance().SetMessageText(1);
                    }
                }
                else
                {
                    LevelManager.GetInstance().SetCombine(false);
                    LevelManager.GetInstance().SetMessageText(0);
                }

                note.PlayHideAnim();
            }
        }
    }

    public void AddNoteEvent(KoreographyEvent evt)
    {
        noteEventList.Add(evt);
    }
}
