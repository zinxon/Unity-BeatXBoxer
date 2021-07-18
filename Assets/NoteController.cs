using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private KoreographyEvent evt;

    public void InitNote(KoreographyEvent evt)
    {
        this.evt = evt;
    }

    private void ResetNote()
    {
        Destroy(gameObject);
    }

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 pos = new Vector2(-7.5f, 0);
        pos.x -= (GameManager.instance.DelayedSampleTime - evt.StartSample) / (float)(GameManager.instance.SampleRate * GameManager.instance.noteSpeed);

        transform.position = pos;
    }
}
