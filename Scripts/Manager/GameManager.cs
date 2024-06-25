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
        // ���� ����
        RadeManager.Instance.minotaur.gameObject.transform.position = GameObject.Find("MonsterSpawnPos").transform.position;
        RadeManager.Instance.minotaur.gameObject.SetActive(true);
        // ī�޶�� ���� �����ֱ�
        CameraManager.Instance.SpawnCamera();
        // �� �� �ٽ� �÷��̾� ��ġ�� �̵�
        // �÷��̾�� ��ġ�ϸ� BGM ����
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.Fight]);
        // Ÿ�̸� ���� �� UI Ȱ��ȭ
        UIManager.Instance.StartStage();
    }

    // ���ӽ¸� �� ���� ��
    public IEnumerator PlayerWin()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.StageClear();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }

    // �����й� �� ���� ��
    public IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.PlayerDie();
        SoundManager.Instance.PlaySound(SoundManager.Instance.clipBGM[(int)BGMClip.End]);
    }
}
