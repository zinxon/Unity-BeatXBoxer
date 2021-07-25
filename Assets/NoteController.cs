using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [Header("內部變量")]
    private int noteID;
    public int NoteID { get => noteID; }
    [SerializeField] private int hitOffset;
    public int HitOffset { get => hitOffset; }
    private bool isRunning = true;
    private Vector2 targetPos;
    private KoreographyEvent trackedEvent;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InitNoteController(KoreographyEvent evt, Vector2 targetPos)
    {
        if (trackedEvent == null)
        {
            trackedEvent = evt;
            noteID = trackedEvent.GetIntValue();
        }
        this.targetPos = targetPos;
    }

    private void Update()
    {
        if (LevelManager.GetInstance().gameplayEnum == GameplayEnum.Pause)
            return;

        UpdatePosition();
        GetHitOffset();

        if (transform.position.x <= targetPos.x - 5f)
        {
            HideNote();
        }
    }

    private void UpdatePosition()
    {
        if (trackedEvent == null || !isRunning)
            return;

        Vector2 pos = targetPos;
        pos.x -= (MusicPlayer.GetInstance().DelayedSampleTime - trackedEvent.StartSample) / (float)MusicPlayer.GetInstance().SampleRate * MusicPlayer.GetInstance().NoteSpeed;

        transform.position = pos;
    }

    public void HideNote()
    {
        isRunning = false;
        gameObject.SetActive(false);
    }

    public void PlayHideAnim()
    {
        isRunning = false;
        if (anim)
        {
            anim.Play("Click");
            StartCoroutine(IEHideAnim());
        }
        else
        {
            HideNote();
        }
    }

    private IEnumerator IEHideAnim(){
        yield return new WaitForSeconds(1f);
        HideNote();
    }

    private void GetHitOffset()
    {
        if (!isRunning)
            return;

        int curTime = MusicPlayer.GetInstance().DelayedSampleTime;
        int noteTime = trackedEvent.StartSample;
        int hitWindow = MusicPlayer.GetInstance().HitWindowRangeInSamples;

        hitOffset = hitWindow - Mathf.Abs(noteTime - curTime);
    }

    public int NoteHittable()
    {
        int hitLevel = 0;

        if (hitOffset >= 0)
        {
            if (hitOffset >= 9000 && hitOffset <= 13500)
            {
                hitLevel = 2;
            }
            else
            {
                hitLevel = 1;
            }
        }
        else
        {
            this.enabled = false;
        }

        return hitLevel;
    }

    public bool IsNoteMissed()
    {
        bool isMissed = true;

        if (enabled)
        {
            int curTime = MusicPlayer.GetInstance().DelayedSampleTime;
            int noteTime = trackedEvent.StartSample;
            int hitWindow = MusicPlayer.GetInstance().HitWindowRangeInSamples;

            isMissed = curTime - noteTime > hitWindow;
        }

        return isMissed;
    }
}
