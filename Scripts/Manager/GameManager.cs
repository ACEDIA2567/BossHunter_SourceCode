using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;

    public void IntroScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Intro]);
    }

    public void InGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.InGame]);
    }

    public void GameTime()
    {
        Time.timeScale = 1.0f;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void FightStart()
    {
        // 몬스터 생성
        RadeManager.Instance.minotaur.gameObject.transform.position = GameObject.Find("MonsterSpawnPos").transform.position;
        RadeManager.Instance.minotaur.gameObject.SetActive(true);
        // 카메라로 몬스터 보여주기
        CameraManager.Instance.SpawnCamera();
        // 그 후 다시 플레이어 위치로 이동
        // 플레이어로 위치하면 BGM 변경
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Fight]);
        // 타이머 가동 및 UI 활성화
        UIManager.Instance.StartStage();
    }

    // 게임승리 시 실행 됨
    public IEnumerator PlayerWin()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.StageClear();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }

    // 게임패배 시 실행 됨
    public IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.PlayerDie();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }
}
