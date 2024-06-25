using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    // 객체마다 사용할 클립모음
    public AudioClip[] clips;

    public void PlayBGM(SFXClip clip)
    {
        SoundManager.Instance.PlaySound(clips[(int)clip]);
    }

    public void PlaySFX(SFXClip clip)
    {
        SoundManager.Instance.PlayShot(clips[(int)clip]);
    }
}
