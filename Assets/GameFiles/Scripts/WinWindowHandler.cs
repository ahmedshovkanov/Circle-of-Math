using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWindowHandler : MonoBehaviour
{
    [SerializeField] private float _showDuration = 2f;

    private void OnEnable()
    {
        StartCoroutine(ShowTimerCoroutine());
    }

    private IEnumerator ShowTimerCoroutine()
    {
        yield return new WaitForSeconds(_showDuration);
        Close();
    }

    private void Close()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
}
