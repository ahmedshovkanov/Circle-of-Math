using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GreenCell : MonoBehaviour, IPointerDownHandler
{
    public int Value;

    private void Awake()
    {
        SetValueAndUpdateText(UnityEngine.Random.Range(1, 9));
        this.gameObject.name = Value.ToString();
    }
    private void Start()
    {
        MainMechController.Instance.OnGameStageChange += OnGameStageChange;
    }
    public void SetValueAndUpdateText(int v)
    {
        Value = v;

        this.transform.GetChild(0).GetComponent<TMP_Text>().text = Value.ToString();
    }

    private void OnGameStageChange(GameStage stage)
    {
        //if (stage == GameStage.Setup)
        //{
        //    StopAllCoroutines();
        //    Destroy(this.gameObject);
        //} else 
        if (stage == GameStage.Result)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (MainMechController.Instance.CurSelectedGreen != null)
            MainMechController.Instance.CurSelectedGreen.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;

        if (MainMechController.Instance.GameStage == GameStage.Action)
        {
            MainMechController.Instance.CurSelectedGreen = this;
            transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        }
    }

    private void OnDestroy()
    {
        MainMechController.Instance.OnGameStageChange -= OnGameStageChange;
    }
}
