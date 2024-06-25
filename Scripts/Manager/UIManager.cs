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

    // 스테이지 클리어 시간 저장 및 기록 갱신
    public void StageClear()
    {
        stageUI.SetActive(true);

        // 현재 클리어 min이 더 짧다면
        if (PlayerPrefs.GetInt("minTime") > min)
        {
            // 시간 저장
            PlayerPrefs.SetFloat("secTime", sec);
            PlayerPrefs.SetInt("minTime", min);
        }
        else if (PlayerPrefs.GetInt("minTime") == min)
        {
            // 현재 클리어 sec이 더 짧다면
            if (PlayerPrefs.GetFloat("secTime") > sec)
            {
                // 시간 저장
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
        // 보스 UI 생성/활성화
        bossHP.transform.parent.gameObject.SetActive(true);
        startCheck = true;
    }

    // 슬라이더 UI정보를 등록
    // 너무 무분별하므로 다음 프로젝트에서는 UI정보를 Dontdestroyload를
    // 이용하여 같이 이동하여 비/활성화시켜서 이용해야겠음
    public void SoundUISetting()
    {
        // 슬라이더 정보
        masterSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolum);
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
        
        // 토글 정보
        masterToggle.onValueChanged.AddListener(SoundManager.Instance.MasterVolumMute);
        bgmToggle.onValueChanged.AddListener(SoundManager.Instance.BGMVolumMute);
        sfxToggle.onValueChanged.AddListener(SoundManager.Instance.SFXVolumMute);
    }

    // 버튼 UI정보를 등록
    // 너무 무분별하므로 다음 프로젝트에서는 UI정보를 Dontdestroyload를
    // 이용하여 같이 이동하여 비/활성화시켜서 이용해야겠음
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
