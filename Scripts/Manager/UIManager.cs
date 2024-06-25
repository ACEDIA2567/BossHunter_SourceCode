using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject optionUI;
    public GameObject dieUI;
    public GameObject stageUI;
    public TextMeshProUGUI stageTimer;
    public TextMeshProUGUI currentStageTime;
    public TextMeshProUGUI bestClearTime;

    [Header("Bar")]
    public Image playerHP;
    public Image playerSP;
    public Image bossHP;

    [Header("SoundUI")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    [Header("FightStart")]
    private bool startCheck = false;
    private float sec = 0;
    private int min = 0;

    [Header("Button")]
    public Button clearReStart;
    public Button clearExit;
    public Button overReStart;
    public Button overExit;
    public Button optionReStart;
    public Button optionExit;
    public Button cancelButton;

    protected  void Awake()
    {
        if (Instance != null) return;
        else
        {
            Instance = this;
        }

        if (!PlayerPrefs.HasKey("secTime"))
        {
            PlayerPrefs.SetFloat("secTime", 50);
            PlayerPrefs.SetInt("minTime", 0);
        }
        SoundUISetting();
        ButtonUISetting();
    }

    private void Update()
    {
        if (startCheck)
        {
            sec += Time.deltaTime;
            if (sec >= 60f)
            {
                min += 1;
                sec = 0;
            }
            stageTimer.text = $"{min:D2}:{(int)sec:D2}";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionActive();
        }
    }

    public void OptionActive()
    {
        if (optionUI.activeSelf)
        {
            optionUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            optionUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PlayerDie()
    {
        dieUI.SetActive(true);
        Time.timeScale = 0;
    }

    // �������� Ŭ���� �ð� ���� �� ��� ����
    public void StageClear()
    {
        stageUI.SetActive(true);

        // ���� Ŭ���� min�� �� ª�ٸ�
        if (PlayerPrefs.GetInt("minTime") > min)
        {
            // �ð� ����
            PlayerPrefs.SetFloat("secTime", sec);
            PlayerPrefs.SetInt("minTime", min);
        }
        else if (PlayerPrefs.GetInt("minTime") == min)
        {
            // ���� Ŭ���� sec�� �� ª�ٸ�
            if (PlayerPrefs.GetFloat("secTime") > sec)
            {
                // �ð� ����
                PlayerPrefs.SetFloat("secTime", sec);
                PlayerPrefs.SetInt("minTime", min);
            }
        }

        currentStageTime.text = $"{min:D2}:{(int)sec:D2}";
        bestClearTime.text = $"{PlayerPrefs.GetInt("minTime"):D2}:{(int)PlayerPrefs.GetFloat("secTime"):D2}";
        Time.timeScale = 0;
    }

    public void StartStage()
    {
        stageTimer.gameObject.SetActive(true);
        // ���� UI ����/Ȱ��ȭ
        bossHP.transform.parent.gameObject.SetActive(true);
        startCheck = true;
    }

    // �����̴� UI������ ���
    // �ʹ� ���к��ϹǷ� ���� ������Ʈ������ UI������ Dontdestroyload��
    // �̿��Ͽ� ���� �̵��Ͽ� ��/Ȱ��ȭ���Ѽ� �̿��ؾ߰���
    public void SoundUISetting()
    {
        // �����̴� ����
        masterSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolum);
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
        
        // ��� ����
        masterToggle.onValueChanged.AddListener(SoundManager.Instance.MasterVolumMute);
        bgmToggle.onValueChanged.AddListener(SoundManager.Instance.BGMVolumMute);
        sfxToggle.onValueChanged.AddListener(SoundManager.Instance.SFXVolumMute);
    }

    // ��ư UI������ ���
    // �ʹ� ���к��ϹǷ� ���� ������Ʈ������ UI������ Dontdestroyload��
    // �̿��Ͽ� ���� �̵��Ͽ� ��/Ȱ��ȭ���Ѽ� �̿��ؾ߰���
    public void ButtonUISetting()
    {
        clearReStart.onClick.AddListener(GameManager.Instance.InGameScene);
        clearExit.onClick.AddListener(GameManager.Instance.GameExit);
        overReStart.onClick.AddListener(GameManager.Instance.InGameScene);
        overExit.onClick.AddListener(GameManager.Instance.GameExit);
        optionReStart.onClick.AddListener(GameManager.Instance.InGameScene);
        optionExit.onClick.AddListener(GameManager.Instance.GameExit);
        cancelButton.onClick.AddListener(GameManager.Instance.GameTime);
    }
}
