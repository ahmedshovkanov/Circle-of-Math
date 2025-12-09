using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class RedCell : MonoBehaviour,  IPointerDownHandler
{
    public int Id;
    public int Value;
    public RedCell Pair;
    private void Awake()
    {
        SetValueAndUpdateText(0);
    }
    public void SetValueAndUpdateText(int v)
    {
        Value = v;
        if (Value == 0)
        {
            this.transform.GetChild(0).GetComponent<TMP_Text>().text = " ";
        } else
        {
            this.transform.GetChild(0).GetComponent<TMP_Text>().text = Value.ToString();
        }
        
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.Value != 0)
        {
            //Debug.Log(this.name);
            return;
        }
        //Debug.Log(this.name);
        if (MainMechController.Instance.GameStage == GameStage.Action)
        {
            if (MainMechController.Instance.CurSelectedGreen != null)
            {
                MainMechController.Instance.SetGreenCellToSlot(this);
            }
        }
    }
}
