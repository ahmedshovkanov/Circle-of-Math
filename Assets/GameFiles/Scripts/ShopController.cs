using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    [SerializeField] private GameObject ShopWindowRef;

    private void Awake()
    {
        Instance = this;
        UpgradesBought = new int[HowManyUpgradesExist];
    }

    public int HowManyUpgradesExist = 4;
    public int[] UpgradesBought;
    public int[] BaseUpgradesCost;
    public int[] UpgradeOutputsValue;
    public bool CloseToRecords;
    public void InitController()
    {
        InitShopSession();
    }
    public void InitShopSession()
    {
        ResetRelatedData();
        ShopWindowRef.SetActive(true);
        MainMechController.Instance.GameStage = GameStage.Shop;
    }

    public void BuyUpgBtn(int id)
    {
        int cost = BaseUpgradesCost[id] + UpgradesBought[id] * UpgradeOutputsValue[id];
        if (GameFlowController.instance.CurCurency[0] >= cost)
        {
            GameFlowController.instance.SubtractCurency(0, cost, out bool s);
            UpgradesBought[id]++;
        }
    }
    
    public void CloseBtn()
    {
        if (CloseToRecords)
        {
            // init records
        } else
        {
            ShopWindowRef.SetActive(false);
            MainMechController.Instance.InitController();
            SaveLoadController.instance.Save();
        }

    }

    private void ResetRelatedData()
    {

    }
}
