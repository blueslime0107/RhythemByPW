using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    FullScreenMode screenMode;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    bool IsScreenFull;
    List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum;
    Resolution CurrentResolution;

    [SerializeField] GameObject OptionMenu;
    [SerializeField] GameObject PlayQuitButton;
    [SerializeField] GameObject WarningUI;

    

    void Start()
    {
        IsScreenFull = false;

        InitUI();
        //OptionMenuEnter.SetActive(true);
        OptionMenu.SetActive(false);
        screenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1024, 728, screenMode);
        CurrentResolution.width = 1024;
        CurrentResolution.height = 728;
        CurrentResolution.refreshRate = 144;
        
        Resizing();
    }

    void InitUI()
    {
        
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if ((Screen.resolutions[i].refreshRate == 60 || Screen.resolutions[i].refreshRate == 144)
                && (Screen.resolutions[i].width >= 1000 && Screen.resolutions[i].height >= 700))
                    resolutions.Add(Screen.resolutions[i]);
        }
        
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = item.width + " X " + item.height + " (" + item.refreshRate + "hz)";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionMenu.activeSelf == false) OptionEnter();
            else Warning();
        }
    }

    // ���� �޴� Ȱ��ȭ
    public void OptionEnter()
    {
        GameManager.Instance.MusicPlaying = false;
        OptionMenu.SetActive(true);
        WarningUI.SetActive(false);
        PlayQuitButton.SetActive(GameManager.Instance.PlayQuitButtonActive);

        if (Screen.fullScreen == true) { screenMode = FullScreenMode.FullScreenWindow; fullscreenBtn.isOn = true; }
        else if (Screen.fullScreen == false) { screenMode = FullScreenMode.Windowed; fullscreenBtn.isOn = false; }
        resolutionDropdown.value = resolutionNum;

    }

    // ��Ӵٿ� �ػ�
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    // Ǯ��ũ�� �¿��� (���)
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        IsScreenFull = isFull;
    }

    // ���� ����
    public void OkBtnclick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);

        CurrentResolution.width = resolutions[resolutionNum].width;
        CurrentResolution.height = resolutions[resolutionNum].height;
        CurrentResolution.refreshRate = resolutions[resolutionNum].refreshRate;

        Debug.Log("Apply Screen Resize : " + Screen.width + " " + Screen.height + " \n Is FullScreen : " + screenMode);
        Resizing();
    }

    // ����� ���������� ä���
    public void Resizing()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (���� / ����)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
        OnPreCull();
    }

    private void OnPreCull() 
    {
        GL.Clear(true, true, Color.black);
    }


    public void Warning()
    {
        // ������ �������� �ʾ��� ��
        
        if (CurrentResolution.width != resolutions[resolutionNum].width
            || CurrentResolution.height != resolutions[resolutionNum].height
            || CurrentResolution.refreshRate != resolutions[resolutionNum].refreshRate)
        {
            Debug.Log("Resolution Nonidentical " + Screen.currentResolution + " " + resolutions[resolutionNum] + " " + resolutionNum);
            WarningUI.SetActive(true);
        }
        else if ((Screen.fullScreen == false && screenMode == FullScreenMode.FullScreenWindow)
            || (Screen.fullScreen == true && screenMode == FullScreenMode.Windowed))
        {
            Debug.Log("FullScreenMode Nonidentical " + Screen.fullScreen + " " + screenMode);
            WarningUI.SetActive(true);
        }
        // ������ ����� ������ ��
        else
        {
            OptionExit();
        }
    }

    public void Continue()
    {
        WarningUI.SetActive(false);
    }

    // ���� �޴� ������
    public void OptionExit()
    {
        
        OptionMenu.SetActive(false);
        //OptionMenuEnter.SetActive(true);
        PlayQuitButton.SetActive(GameManager.Instance.PlayQuitButtonActive);

        if (GameManager.Instance.OnSelecting == false) GameManager.Instance.MusicPlaying = true;
               
    }

}
