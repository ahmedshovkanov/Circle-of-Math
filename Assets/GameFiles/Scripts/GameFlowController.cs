using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameFlowController : MonoBehaviour
{
    public static GameFlowController instance;
    private void Awake()
    {
        instance = this;
        DailyLastDate = 999;
        //Screen.orientation = ScreenOrientation.Portrait;

        _curCurency = new int[1];
    }

    
	public AssetSettings AssetSettings;
	public Camera GlobalCameraRef;
    public bool IsBonusGameInThisGame = false;
    public GameObject MainMenuGO, IngameMenuGO, EnvGO, WinWindow, RulesWindow;
    public int DailyLastDate;
	public int CurDailyStreak;

    public string PrivacyUrl;

    private int[] _curCurency;
    public event Action<int, int> OnCurrencyChange;
    public int[] CurCurency => _curCurency;
    public void AddCurency(int id, int value)
    {
        _curCurency[id] += value;
        OnCurrencyChange?.Invoke(id, _curCurency[id]);
    }
    public void SubtractCurency(int id, int value, out bool isSucsess)
    {
        int cur = _curCurency[id];
        if (cur < 0)
        {
            isSucsess = false;
            return;
        } else
        {
            _curCurency[id] -= value;
            OnCurrencyChange?.Invoke(id, _curCurency[id]);
            isSucsess = true;
            return;
        }

    }
    public void SetCurCurency(int id,int value)
    {
        _curCurency[id] = value;
        OnCurrencyChange?.Invoke(id, _curCurency[id]);
    }
    
    public void PlayBtn()
    {
        ScreenBoundsAndCameraSizeHandler();
        SaveLoadController.instance.Load();


        RulesWindow.SetActive(true);
    }

    public void BonusGameBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseRulesBtn()
    {
        IngameMenuGO.SetActive(true);
        EnvGO.SetActive(true);
        RulesWindow.SetActive(false);
        SessionInit();
        MainMenuGO.SetActive(false);
    }

    private void SessionInit()
    {
        ResetRelatedData();
        MainMechController.Instance.InitController();
    }

    private void ScreenBoundsAndCameraSizeHandler()
    {

    }

    private void ResetRelatedData()
    {

    }
    
    public void ReplayBtn()
    {
        SessionInit();
    }
    
    public void HomeBtn()
    {
        SaveLoadController.instance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void PrivacyBtn()
    {
        Application.OpenURL(PrivacyUrl);
    }
}
