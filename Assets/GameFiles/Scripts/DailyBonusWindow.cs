using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusWindow : MonoBehaviour
{
    [Header("PlayerPrefs Settings")]
    [SerializeField] private string keyName = "PlayerBonus";
    [SerializeField] private int defaultValue = 0;

    [Header("Debug")]
    [SerializeField] private int loadedValue; // View in Inspector
    public Transform Parent;
    public Sprite Claimed, NotClaimed;
    private void OnEnable()
    {
        LoadFromPlayerPrefs();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Parent.childCount; i++)
        {
            Parent.GetChild(i).GetComponent<Image>().sprite = NotClaimed;
        }

        if(loadedValue == 0)
        {
            return;
        }
        for (int i = 0; i < loadedValue; i++)
        {
            Parent.GetChild(i).GetComponent<Image>().sprite = Claimed;
        }
    }

    public void Claim()
    {
        StartCoroutine(ClaimHandler());
    }

    private IEnumerator ClaimHandler()
    {
        loadedValue++;
        if(loadedValue >= 5)
        {
            loadedValue = 4;
        }
        UpdateUI();
        SaveValue(loadedValue);
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    private void LoadFromPlayerPrefs()
    {
        // PlayerPrefs.GetInt(key, defaultValue) - loads or returns default if not found
        loadedValue = PlayerPrefs.GetInt(keyName, defaultValue);

        Debug.Log($"Loaded '{keyName}': {loadedValue}");
        if (loadedValue >= 5)
        {
            loadedValue = 4;
        }

        // Optional: Call method to use the loaded value
        OnValueLoaded(loadedValue);
    }

    // Example method to use the loaded value
    protected virtual void OnValueLoaded(int value)
    {
        // Override this in your specific script
        Debug.Log($"Value loaded and ready to use: {value}");
    }

    // Optional: Method to save a value
    public void SaveValue(int valueToSave)
    {
        PlayerPrefs.SetInt(keyName, valueToSave);
        PlayerPrefs.Save(); // Important: Save to disk
        Debug.Log($"Saved '{keyName}': {valueToSave}");
    }

    // Optional: Clear the saved value
    public void ClearSavedValue()
    {
        PlayerPrefs.DeleteKey(keyName);
        Debug.Log($"Cleared '{keyName}'");
    }

    // Optional: Clear all PlayerPrefs (use with caution!)
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared all PlayerPrefs");
    }
}