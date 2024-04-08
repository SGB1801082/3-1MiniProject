using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();// 컴퓨터의 모니터가 지원하는 해상도는 Scrren.resolutions 라는 배열에 들어있다, 따라서 Resolution타입의 List를 생성하여 모니터가 지원하는 화면 해상도 정보를 저장 할 수 있는 배열을 생성한다. 
    private FullScreenMode screenMode;

    public Dropdown resolutionDropDwon;// 이 드롭다운형식의 변수 resolutionDropDwon이 List resolutions의 값을 받아갈 것. Dropdown형식의 변수를 사용하려면 using UI 해야됨.
    private int resolutionNum;
    public Toggle btnFullScreen;
    private int dropdwonOptionValue;// 처음 시작했을 때 드롭다운의 선택된 값이 초기화되어있지 않으니 현재 해상도의 값과 해상도 목록을 비교해서 드롭다운의 벨류값을 변경해준다.

    private string compareRate;

    public void InitUI()
    {
        DefaultCompareRate();

        for (int i = 0; i < Screen.resolutions.Length; i++)// 컴퓨터의 모니터가 지원하는 화면 해상도 정보를 전부 resolutions 배열에 저장한다. 
        {
            if (Screen.resolutions[i].refreshRateRatio.ToString() == "60" )
            {
                if (Screen.resolutions[i].width == 1280 && Screen.resolutions[i].height == 720)
                {
                    resolutions.Add(Screen.resolutions[i]);
                }else

                if (Screen.resolutions[i].width == 1366 && Screen.resolutions[i].height == 768)
                {
                    resolutions.Add(Screen.resolutions[i]);
                }else

                if (Screen.resolutions[i].width == 1920 && Screen.resolutions[i].height == 1080)
                {
                    resolutions.Add(Screen.resolutions[i]);
                }

            }
        }
        resolutionDropDwon.options.Clear();// 기존의 드롭다운 변수에 어떤 값이 들어있을수도있으니 초기화 해 준다.

        dropdwonOptionValue = 0;

        foreach (Resolution item in resolutions)    //Resolution 데이터타입의 변수 item에 배열변수 resolutions에 저장된 값을 대입하는 반복문
        {
            Dropdown.OptionData option = new Dropdown.OptionData();// Dropdown의 Option 목록 리스트는 OptionData형식으로 되어있기 때문에, OptionData형식의 객체 option을 생성하고
            option.text = item.width + " x " + item.height + " " + item.refreshRateRatio + "hz";// 객체 option의 text변수에 해상도 값을 넣어 준 뒤에
            resolutionDropDwon.options.Add(option);// 목록에 추가한다.

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropDwon.value = dropdwonOptionValue;// 실행되었을때의 해상도값에 해당하는 드롭다운의 벨류값으로 맞추어준다.
            }
            dropdwonOptionValue++;
        }
        resolutionDropDwon.RefreshShownValue();// 목록의 내용이 변경되었으므로 새로고침을 해 준다.

        btnFullScreen.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false ;
    }

    private void Start()
    {
        InitUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Screen.SetResolution(1920,1080,FullScreenMode.Windowed);
            InitUI();
        }
    }

    public void DropBoxOptionChange(int ChangeValue)// 이 메서드가 pulbic이여야만 OnValueChanged에서 Dynamic Int 항목을 선택 할 수 있음.
    {
        resolutionNum = ChangeValue;// DropBoxOptionChange 메서드가 매개변수로 받은 값을 resolutionNum이 Dynamic Int옵션으로 받아서 옵션을 적용하는 형식인듯. 
    }

    public void BtnFullScreen(bool isFull)// 토글 버튼
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;// 참이면 전체화면, 거짓이면 창 모드
    }

    public void BtnOKClick()// 드롭다운에서 변경사항을 적용시킬 버튼 
    {
        /* 03-15 이전 기존의 코드
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode
            );
        */

        // 03-15 [94 - 105] 버튼을 클릭하면 현재 선택된 옵션을 저장하고 실행함. 다른 씬에서 지금 저장된 옵션에 접근할수있다면 데이터만 뽑아와서 적용시킬 수 있을듯?
        int width = resolutions[resolutionNum].width;
        int height = resolutions[resolutionNum].height;
        int fullscreen = screenMode == FullScreenMode.FullScreenWindow ? 1 : 0;

        // 설정한 값을 PlayerPref에 저장
        PlayerPrefs.SetInt("ScreenWidth", width);
        PlayerPrefs.SetInt("ScreenHeight", height);
        PlayerPrefs.SetInt("FullScreen", fullscreen);
        PlayerPrefs.Save();

        Screen.SetResolution(width, height, screenMode);
    }
    private string DefaultCompareRate()
    {
        compareRate = "60";
        return compareRate;
    }
}
