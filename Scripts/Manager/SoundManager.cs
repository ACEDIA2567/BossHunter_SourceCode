using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SFXClip
{
    Attack1,
    Attack2,
    parrying,
    Die
}

public enum BGMClip
{
    Intro,
    InGame,
    Fight,
    End
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    public AudioClip[] clipBGM;
    private AudioSource sourceSFX;
    private AudioSource sourceBGM;

    protected override void Awake()
    {
        base.Awake();
        AudioInit();
    }

    // public으로 가능했지만 Resources를 이용하여 만들어봤음
    private void AudioInit()
    {
        GameObject bgm = Instantiate(Resources.Load<GameObject>("Sound/BGMSource"), transform);
        GameObject sfx = Instantiate(Resources.Load<GameObject>("Sound/SFXSource"), transform);
        // clipSFX는 각 오브젝트에 클립으로 추가를 했기 때문에 BGM클립만 존재함
        clipBGM = bgm.GetComponent<AudioClips>().clips;
        sourceBGM = bgm.GetComponent<AudioSource>();
        sourceSFX = sfx.GetComponent<AudioSource>();
    }

    // Master의 사운드바 조절
    public void SetMasterVolum(float sliderValue)
    {
        mixer.SetFloat("MusicVol" ,Mathf.Log10(sliderValue) * 20);
    }

    // Master의 음소검 토글
    public void MasterVolumMute(bool toggle)
    {
        AudioListener.volume = toggle ? 0 : 1;
    }

    // BGM의 음소검 토글
    public void BGMVolumMute(bool toggle)
    {
        sourceBGM.mute = toggle;
    }

    // SFX의 음소검 토글
    public void SFXVolumMute(bool toggle)
    {
        sourceSFX.mute = toggle;
    }

    // BGM의 사운드바 조절
    public void SetBGMVolume(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    // SFX의 사운드바 조절
    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    // BGM 변경(SoundManager에 BGM클립이 있으므로 다른 매니저에서 변경 가능)
    public void PlaySound(AudioClip clip)
    {
        sourceBGM.Stop();
        sourceBGM.clip = clip;
        sourceBGM.Play();
    }

    // SFX 실행(싱글톤으로 각 오브젝트의 불륨을 실행시킴)
    public void PlayShot(AudioClip clip)
    {
        sourceSFX.PlayOneShot(clip);
    }
}
