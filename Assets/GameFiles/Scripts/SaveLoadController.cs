using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveLoadController : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (_isSavingCoroutineActive)
            StartCoroutine(SavingCoroutine());
    }


    public static SaveLoadController instance;

    [SerializeField] private int _chanceOfSaving = 1;

    [SerializeField] private bool _isSavingActive = false;
    [SerializeField] private bool _isSavingCoroutineActive = false;
    [SerializeField] private float _savingCoroutineTimer = 60;

    
    public void Save()
    {
        if (_isSavingActive == false)
        {
            Debug.LogWarning("Saving disabled");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.es");
#if UNITY_EDITOR
        Debug.Log("Save");
#endif
        SaveData data = new SaveData();

        // populate data inst
        data.DailyLastDate = GameFlowController.instance.DailyLastDate;
        data.Curency = new int[GameFlowController.instance.CurCurency.Length];
        data.CurDailyStreak = GameFlowController.instance.CurDailyStreak;
        data.UpgradesBought = ShopController.Instance.UpgradesBought;

        for (int i = 0; i < data.Curency.Length; i++)
        {
            data.Curency[i] = GameFlowController.instance.CurCurency[i];
        }

        bf.Serialize(file, data);
        file.Close();
    }
    
    public void Load()
    {
        if (_isSavingActive == false)
        {
            return;
        }
        if (File.Exists(Application.persistentDataPath + "/savedGame.es"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.es", FileMode.Open);
#if UNITY_EDITOR
            Debug.Log(Application.persistentDataPath + "/savedGame.es");
#endif

            SaveData data = (SaveData)bf.Deserialize(file);

            // fetch data from data inst and populate runtime fields

            for (int i = 0; i < data.Curency.Length; i++)
            {
                GameFlowController.instance.SetCurCurency(i, data.Curency[i]);
            }

            GameFlowController.instance.DailyLastDate = data.DailyLastDate;
            GameFlowController.instance.CurDailyStreak = data.CurDailyStreak;
            ShopController.Instance.UpgradesBought = data.UpgradesBought;
            file.Close();
        } else
        {
#if UNITY_EDITOR
            Debug.Log("Not Load Save");
#endif
            //_isNewGame = true;
        }
    }

    private void OnCurrencyUpdateAction(int id, int value)
    {
        if (UnityEngine.Random.Range(0, _chanceOfSaving) == 0)
        {
            Save();
        }
    }

    private IEnumerator SavingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_savingCoroutineTimer);
            Save();
        }
    }
}

[System.Serializable]
public struct SaveData
{
    public int DailyLastDate;
    public int CurDailyStreak;

    public int[] Curency;
    public int[] UpgradesBought;
}
