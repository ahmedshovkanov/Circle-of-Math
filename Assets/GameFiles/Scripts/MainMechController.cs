using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

using UnityEngine.UI;

public enum GameStage { Setup, Action, Resolve, Result, Shop, Records }
public class MainMechController : MonoBehaviour
{
    public static MainMechController Instance;
    private void Awake()
    {
        Instance = this;

        GameStage = GameStage.Setup;
    }

    [SerializeField] private Transform ENVParent;
	
	private GameStage _gameStage;
	public GameStage GameStage
    {
        get
        {
            return _gameStage;
        }
        set
        {
            _gameStage = value;
            OnGameStageChange?.Invoke(_gameStage);
        }
    }
    public event Action<GameStage> OnGameStageChange;

    public int CurCellPointerId;

    public TMP_Text CentralTextRef;
    public int CurTargetCenterValue;

    public GameObject RotationCellsDiv;

    public RotationCellsDiv rotationDiv;

    public int CurSumOfCells;

    public GreenCell CurSelectedGreen;
    public GreenCell GreenCellPrefab;
    public Transform GreenCellGridParent;

    public void InitController()
    {
        
        CurCellPointerId = 0;
        CurTargetCenterValue = 0;
        CentralTextRef.text = " ";
        CurSumOfCells = 0;
        InitNewSession();
    }

    public void InitNewSession()
    {
        GameStage = GameStage.Setup;
        ResetRelatedData();
        SpawnGreenCellsHandler(10);

        CurTargetCenterValue = UnityEngine.Random.Range(1, 15);
        CentralTextRef.text = CurTargetCenterValue.ToString();

        MoveToActionStageHandler();
    }

    private void MoveToActionStageHandler()
    {
        GameStage = GameStage.Action;
    }

    private IEnumerator MoveToResolveStageHandler()
    {
        GameStage = GameStage.Resolve;
        RedCell rc = GetRedCellWithId(CurCellPointerId);
        int curVals = rc.Value + rc.Pair.Value;
#if UNITY_EDITOR
        Debug.Log("Cur val " + curVals);
#endif

        if (curVals == CurTargetCenterValue)
        {
            WinHandler();
            yield break;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(NewRotationRoundHandler());
    }

    private IEnumerator NewRotationRoundHandler()
    {
        rotationDiv.TargetRotationZ = 30 + CurCellPointerId * 30f;
        yield return new WaitForSeconds(3f);
        CurCellPointerId++;
        SpawnGreenCellsHandler(1);

        MoveToActionStageHandler(); // not sure if this right. upd:it seeams ok
    }

    private void SpawnGreenCellsHandler(int howmany = 1)
    {
        //int HowManyCellsToInstall = 10; // Hardcoded value
        ////Debug.Log(GameStage);
        //while(GreenCellGridParent.childCount < HowManyCellsToInstall)
        //{
        //    Instantiate(GreenCellPrefab, GreenCellGridParent);
        //}
        for (int i = 0; i < howmany; i++)
        {
            Instantiate(GreenCellPrefab, GreenCellGridParent);
        }
    }

    public void SetGreenCellToSlot(RedCell setHere)
    {
        setHere.SetValueAndUpdateText(CurSelectedGreen.Value);
        Destroy(CurSelectedGreen.gameObject);
        CurSelectedGreen = null;

        StartCoroutine(MoveToResolveStageHandler());
    }

    
    public void ResetRelatedData()
    {
        StopAllCoroutines();
        CurSelectedGreen = null;
        RotationCellsDiv.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        CurCellPointerId = 0;
        CurTargetCenterValue = 0;
        CentralTextRef.text = " ";
        CurSumOfCells = 0;
        for (int i = 0; i < RotationCellsDiv.transform.childCount; i++)
        {
            RotationCellsDiv.transform.GetChild(i).GetComponent<RedCell>().SetValueAndUpdateText(0);
        }
    }

    private RedCell GetRedCellWithId(int id)
    {
        RedCell rc = null;

        for (int i = 0; i < RotationCellsDiv.transform.childCount; i++)
        {
            RedCell r = RotationCellsDiv.transform.GetChild(i).GetComponent<RedCell>();
            if (r.Id == id)
            {
                rc = r;
            }
        }

        return rc;
    }
    public GameObject WinWindow;
    [ContextMenu("WinHandler")]
    private void WinHandler()
    {
        //GameStage = GameStage.Result;
        WinWindow.gameObject.SetActive(true);
        CurTargetCenterValue = UnityEngine.Random.Range(1, 15);
        CentralTextRef.text = CurTargetCenterValue.ToString();
        GameFlowController.instance.AddCurency(0, 100);
        //ResetRelatedData();
        StartCoroutine(NewRotationRoundHandler());
    }

    public void ReplayBtn()
    {
        GameStage = GameStage.Result;
        InitNewSession();
    }
}
