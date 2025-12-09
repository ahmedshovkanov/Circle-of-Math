using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICurencyUpdateHandler : MonoBehaviour
{
    [SerializeField] private int _idOFCurency;

    private void Start()
    {
        GameFlowController.instance.OnCurrencyChange += OnCurrencyUpdateAction;
    }

    private void OnCurrencyUpdateAction(int id, int value)
    {
        if (id != _idOFCurency)
        {
            return;
        }

        UpdateRelatedUI(value);
    }

    private void UpdateRelatedUI(int value)
    {
        this.GetComponent<TMP_Text>().text = $"Score: {value}";
    }

    private void OnEnable()
    {
        if (GameFlowController.instance != null)
        {
            if (GameFlowController.instance.CurCurency != null)
            {
                this.GetComponent<TMP_Text>().text = $"Score: {GameFlowController.instance.CurCurency[_idOFCurency]}";
            }
            else
            {
                this.GetComponent<TMP_Text>().text = "Score: ";
            }
        } else
        {
            this.GetComponent<TMP_Text>().text = "Score: ";
        }
    }

    private void OnDestroy()
    {
        GameFlowController.instance.OnCurrencyChange -= OnCurrencyUpdateAction;
    }
}
