using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text resultText;
    public Transform cardParent;

    private GameObject[] cards; // Массив для хранения объектов карточек
    private bool[] isFaceUp = new bool[2]; // Массив для отслеживания состояния (вверх/вниз)
    private string[] cardValues = new string[2]; // Массив для хранения значений карт

    private int firstCardIndex = -1;
    private int secondCardIndex = -1;
    private bool isFirstCardSet;

    public GameObject ClosedCard, OpenedCard;

    void Awake()
    {
        CreateCards();
        isFirstCardSet = false;
        resultText.text = "";
    }

    void CreateCards()
    {
        cards = new GameObject[2];

        // Создаем первую карточку (закрытую)
        cards[0] = Instantiate(ClosedCard, cardParent);
        isFaceUp[0] = false;
        cardValues[0] = "A";

        // Создаем вторую карточку (открытый слот)
        cards[1] = Instantiate(OpenedCard, cardParent);
        isFaceUp[1] = true;
        cardValues[1] = "";
    }

    public void OnCardClick(int index)
    {
        if (index < 0 || index >= cards.Length) return;

        // Проверяем, что карточка закрыта и еще не была нажата
        if (!isFaceUp[index] && !isFirstCardSet)
        {
            isFirstCardSet = true;
            firstCardIndex = index;
            FlipCard(index);
        }
        else if (firstCardIndex != -1 && isFirstCardSet && firstCardIndex != index)
        {
            secondCardIndex = index;
            StartCoroutine(CompareCards());
        }
    }

    private IEnumerator CompareCards()
    {
        yield return new WaitForSeconds(1.0f); // Задержка для отображения карт

        if (cardValues[firstCardIndex] == cardValues[secondCardIndex])
        {
            resultText.text = "Победа!";
        }
        else
        {
            resultText.text = "Попробуйте еще раз...";

            // Обновляем состояние карточек для возврата к исходному состоянию
            isFaceUp[firstCardIndex] = !isFaceUp[firstCardIndex];
            isFaceUp[secondCardIndex] = !isFaceUp[secondCardIndex];

            FlipCards(firstCardIndex, secondCardIndex);
        }

        isFirstCardSet = false;
        firstCardIndex = -1;
        secondCardIndex = -1;
    }

    private void FlipCard(int index)
    {
        // Метод для переворачивания одной карточки
        isFaceUp[index] = !isFaceUp[index];
        //cards[index].GetComponent<Animator>().SetTrigger("Flip");
    }

    private void FlipCards(int index1, int index2)
    {
        // Метод для переворачивания двух карточек
        FlipCard(index1);
        FlipCard(index2);
    }
}
