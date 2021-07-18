using System;
using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //
    public string eventID;
    public NoteController noteObject;

    public Koreography playingKoreo;
    public AudioSource audioSource;

    public int SampleRate
    {
        get
        {
            return playingKoreo.SampleRate;
        }
    }
    //音符速度
    public float noteSpeed = 1;
    //音符點擊窗口的範圍(ms)
    [SerializeField, Range(8f, 300f)]
    private float hitWindowRangeInMS;
    public float WindowSizeInUnit
    {
        get
        {
            return noteSpeed * (hitWindowRangeInMS * 0.001f);
        }
    }
    private int hitWindowInSample;

    [Header("Time")]
    public float leadInTime = 1;
    private float leadIntTimeLeft;
    private float timeLeftToPlay;
    public int DelayedSampleTime
    {
        get
        {
            return playingKoreo.GetLatestSampleTime() - SampleRate * (int)leadIntTimeLeft;
        }
    }

    private List<KoreographyEvent> rawEvents = new List<KoreographyEvent>();
    private int pendingEventID = 0;
    private Queue<NoteController> trackedNotes = new Queue<NoteController>();

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        InitLeadIn();
        InitKoreographer();
    }

    private void Update()
    {
        if (timeLeftToPlay > 0)
        {
            timeLeftToPlay -= Time.unscaledDeltaTime;

            if (timeLeftToPlay <= 0)
            {
                audioSource.Play();
                timeLeftToPlay = 0;
            }
        }

        if (leadIntTimeLeft > 0)
        {
            leadIntTimeLeft = Mathf.Max(leadIntTimeLeft - Time.unscaledDeltaTime, 0);
        }

        CheckSpawnNext();
    }

    private void InitLeadIn()
    {
        if (leadInTime > 0)
        {
            leadIntTimeLeft = leadInTime;
            timeLeftToPlay = leadInTime;
        }
        else
        {
            audioSource.Play();
        }
    }

    private void InitKoreographer()
    {
        playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
        KoreographyTrackBase track = playingKoreo.GetTrackByID(eventID);
        rawEvents = track.GetAllEvents();

        hitWindowInSample = (int)(SampleRate * hitWindowRangeInMS * 0.001f);
    }

    public void SpawnNote(KoreographyEvent evt)
    {
        if (noteObject == null)
            return;

        NoteController obj = Instantiate(noteObject, new Vector2(-7.5f, 0), Quaternion.identity);
        obj.InitNote(evt);
        trackedNotes.Enqueue(obj);
    }

    private int GetSpawnSampleOffset()
    {
        float spawnDistToTarget = 7.5f - -7.5f;
        float spawnPosToTargetTime = spawnDistToTarget / noteSpeed;

        return (int)spawnPosToTargetTime * SampleRate;
    }

    private void CheckSpawnNext()
    {
        int samplesToTarget = GetSpawnSampleOffset();
        int curTime = DelayedSampleTime;

        while (pendingEventID < rawEvents.Count && rawEvents[pendingEventID].StartSample < curTime + samplesToTarget)
        {
            KoreographyEvent evt = rawEvents[pendingEventID];
            SpawnNote(evt);
            pendingEventID++;
        }
    }
}
