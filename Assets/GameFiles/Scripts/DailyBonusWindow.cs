using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusWindow : MonoBehaviour
{
    [Header("PlayerPrefs Settings")]
    [SerializeField] private string keyName = "PlayerBonus";
    [SerializeField] private string dateKeyName = "LastClaimDate";
    [SerializeField] private int defaultValue = 0;

    [Header("Debug")]
    [SerializeField] private int loadedValue; // View in Inspector
    [SerializeField] private string lastClaimDate; // View in Inspector
    public Transform Parent;
    public Sprite Claimed, NotClaimed;

    private void OnEnable()
    {
        LoadFromPlayerPrefs();
        CheckIfClaimedToday();
        UpdateUI();
    }

    private void CheckIfClaimedToday()
    {
        // Load the last claim date
        lastClaimDate = PlayerPrefs.GetString(dateKeyName, string.Empty);

        // If there's no saved date, it's the first time or was cleared
        if (string.IsNullOrEmpty(lastClaimDate))
        {
            return;
        }

        try
        {
            // Parse the saved date
            DateTime lastClaimDateTime = DateTime.Parse(lastClaimDate);
            DateTime today = DateTime.Today;

            // If already claimed today, close the window
            if (lastClaimDateTime.Date == today)
            {
                Debug.Log($"Already claimed today ({lastClaimDate}). Closing window.");
                this.gameObject.SetActive(false);
            }
        }
        catch (FormatException)
        {
            // If date format is invalid, clear it and continue
            Debug.LogWarning("Invalid date format in PlayerPrefs. Clearing date.");
            PlayerPrefs.DeleteKey(dateKeyName);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Parent.childCount; i++)
        {
            Parent.GetChild(i).GetComponent<Image>().sprite = NotClaimed;
        }

        if (loadedValue == 0)
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
        // Save today's date first
        SaveTodayDate();

        // Then process the claim
        loadedValue++;
        if (loadedValue >= 5)
        {
            loadedValue = 4;
        }
        UpdateUI();
        SaveValue(loadedValue);
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    private void SaveTodayDate()
    {
        // Save today's date as string
        string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
        PlayerPrefs.SetString(dateKeyName, todayDate);
        PlayerPrefs.Save();
        lastClaimDate = todayDate;
        Debug.Log($"Saved claim date: {todayDate}");
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
        PlayerPrefs.DeleteKey(dateKeyName); // Also clear the date
        Debug.Log($"Cleared '{keyName}' and '{dateKeyName}'");
    }

    // Optional: Clear all PlayerPrefs (use with caution!)
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared all PlayerPrefs");
    }

    // New method to check if can claim today
    public bool CanClaimToday()
    {
        string savedDate = PlayerPrefs.GetString(dateKeyName, string.Empty);
        if (string.IsNullOrEmpty(savedDate))
        {
            return true;
        }

        try
        {
            DateTime lastClaimDateTime = DateTime.Parse(savedDate);
            return lastClaimDateTime.Date != DateTime.Today;
        }
        catch (FormatException)
        {
            return true;
        }
    }

    // New method to get days since last claim
    public int GetDaysSinceLastClaim()
    {
        string savedDate = PlayerPrefs.GetString(dateKeyName, string.Empty);
        if (string.IsNullOrEmpty(savedDate))
        {
            return int.MaxValue; // Never claimed before
        }

        try
        {
            DateTime lastClaimDateTime = DateTime.Parse(savedDate);
            DateTime today = DateTime.Today;
            return (int)(today - lastClaimDateTime).TotalDays;
        }
        catch (FormatException)
        {
            return int.MaxValue;
        }
    }
}