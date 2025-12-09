using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MemoryCardGame : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject closedCardPrefab;
    [SerializeField] private GameObject openCardPrefab;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text roundResultText;
    [SerializeField] private TMP_Text centerSymbolText;
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private float cardSpacing = 150f;

    [Header("Game Settings")]
    [SerializeField] private int totalCards = 5;
    [SerializeField] private float showTime = 1f;
    [SerializeField] private int pointsPerRound = 10;

    private List<GameObject> currentCards = new List<GameObject>(); // Cards currently on screen
    private List<char> cardSymbols = new List<char>(); // Symbols for each card position
    private char targetSymbol;
    private int targetCardIndex;
    private int score = 0;
    private bool canSelect = false;
    private bool gameInProgress = false;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        UpdateScoreUI();
        roundResultText.text = "";
        centerSymbolText.text = "";
        StartNewRound();
    }

    private void StartNewRound()
    {
        if (gameInProgress) return;

        gameInProgress = true;
        ClearBoard();
        GenerateCardSymbols();
        CreateAndShowOpenCards();
        StartCoroutine(GameRound());
    }

    private void GenerateCardSymbols()
    {
        cardSymbols.Clear();

        // Generate unique random symbols for each card
        List<char> availableSymbols = new List<char>();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            availableSymbols.Add(c);
        }

        // Shuffle and pick symbols
        for (int i = 0; i < totalCards; i++)
        {
            int randomIndex = Random.Range(0, availableSymbols.Count);
            char randomChar = availableSymbols[randomIndex];
            availableSymbols.RemoveAt(randomIndex);
            cardSymbols.Add(randomChar);
        }
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene(0);
    }

    private void CreateAndShowOpenCards()
    {
        // Clear current cards
        ClearBoard();

        Destroy(gameBoard.transform.GetChild(0).gameObject);
        Destroy(gameBoard.transform.GetChild(1).gameObject);

        // Calculate starting position for cards (horizontal line)
        float totalWidth = (totalCards - 1) * cardSpacing;
        Vector2 startPos = new Vector2(-totalWidth / 2, 0);

        // Create open cards to show symbols initially
        for (int i = 0; i < totalCards; i++)
        {
            Vector2 position = startPos + new Vector2(i * cardSpacing, 0);

            GameObject openCard = Instantiate(openCardPrefab, gameBoard.transform);
            openCard.transform.localPosition = position;

            // Set symbol on open card
            TMP_Text symbolText = openCard.GetComponentInChildren<TMP_Text>();
            if (symbolText != null)
            {
                symbolText.text = cardSymbols[i].ToString();
            }

            // Add to current cards list
            currentCards.Add(openCard);
        }
    }

    private void ReplaceWithClosedCards()
    {
        // Clear current cards from screen
        ClearBoard();

        // Calculate starting position for cards (horizontal line)
        float totalWidth = (totalCards - 1) * cardSpacing;
        Vector2 startPos = new Vector2(-totalWidth / 2, 0);

        // Create closed cards for player interaction
        for (int i = 0; i < totalCards; i++)
        {
            Vector2 position = startPos + new Vector2(i * cardSpacing, 0);

            // Instantiate closed card prefab
            GameObject closedCard = Instantiate(closedCardPrefab, gameBoard.transform);
            closedCard.transform.localPosition = position;

            // Ensure closed card has Button component
            Button cardButton = closedCard.GetComponent<Button>();
            if (cardButton == null)
            {
                cardButton = closedCard.AddComponent<Button>();
            }

            // Clear any existing listeners and add new one
            cardButton.onClick.RemoveAllListeners();
            int cardIndex = i;
            cardButton.onClick.AddListener(() => OnCardClicked(cardIndex));

            // Ensure the closed card is active and interactable
            closedCard.SetActive(true);
            if (cardButton != null)
            {
                cardButton.interactable = true;
            }

            // Add to current cards list
            currentCards.Add(closedCard);
        }
    }

    private IEnumerator GameRound()
    {
        // Step 1: Show all cards with symbols (open cards)
        canSelect = false;
        roundResultText.text = "MEMORIZE THE SYMBOLS!";

        // Wait for memorization time
        yield return new WaitForSeconds(showTime);

        // Step 2: Replace open cards with closed cards
        roundResultText.text = "";
        ReplaceWithClosedCards();

        yield return new WaitForSeconds(0.5f);

        // Step 3: Show target symbol in center
        SelectTargetSymbol();
        centerSymbolText.text = $"FIND: {targetSymbol}";

        yield return new WaitForSeconds(1f);

        // Step 4: Hide center symbol and allow selection
        centerSymbolText.text = "";
        roundResultText.text = "WHERE WAS THIS SYMBOL?";
        canSelect = true;
    }

    private void SelectTargetSymbol()
    {
        // Choose a random card as the target
        targetCardIndex = Random.Range(0, totalCards);
        targetSymbol = cardSymbols[targetCardIndex];
    }

    private void OnCardClicked(int cardIndex)
    {
        if (!canSelect || !gameInProgress) return;

        canSelect = false;
        gameInProgress = false;

        // Check if correct
        if (cardIndex == targetCardIndex)
        {
            // Show correct card
            ShowSelectedCard(cardIndex, true);
            score += pointsPerRound;
            UpdateScoreUI();
            roundResultText.text = $"CORRECT! +{pointsPerRound} POINTS";
        }
        else
        {
            // Show both selected card and correct card
            ShowSelectedCard(cardIndex, false);
            ShowSelectedCard(targetCardIndex, true);
            roundResultText.text = $"WRONG! CORRECT WAS CARD #{targetCardIndex + 1}";
        }

        // Disable all closed card buttons
        DisableAllCardButtons();

        // Show all other cards after short delay
        StartCoroutine(ShowAllCardsWithDelay(1f));
    }

    private void ShowSelectedCard(int cardIndex, bool isCorrect)
    {
        if (cardIndex < 0 || cardIndex >= currentCards.Count) return;

        Vector3 position = currentCards[cardIndex].transform.localPosition;
        Destroy(currentCards[cardIndex]);

        GameObject openCard = Instantiate(openCardPrefab, gameBoard.transform);
        openCard.transform.localPosition = position;

        // Set symbol
        TMP_Text symbolText = openCard.GetComponentInChildren<TMP_Text>();
        if (symbolText != null)
        {
            symbolText.text = cardSymbols[cardIndex].ToString();

            if (isCorrect)
            {
                symbolText.color = Color.green;
                Image cardImage = openCard.GetComponent<Image>();
                if (cardImage != null)
                {
                    cardImage.color = new Color(0.7f, 1f, 0.7f);
                }
            }
            else
            {
                symbolText.color = Color.red;
                Image cardImage = openCard.GetComponent<Image>();
                if (cardImage != null)
                {
                    cardImage.color = new Color(1f, 0.7f, 0.7f);
                }
            }
        }

        // Remove button from revealed card
        Button button = openCard.GetComponent<Button>();
        if (button != null)
        {
            button.enabled = false;
        }

        currentCards[cardIndex] = openCard;
    }

    private void DisableAllCardButtons()
    {
        foreach (GameObject card in currentCards)
        {
            Button button = card.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
        }
    }

    private IEnumerator ShowAllCardsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reveal all remaining closed cards
        for (int i = 0; i < currentCards.Count; i++)
        {
            // Skip already revealed cards (they have TMP_Text component with non-default color)
            TMP_Text existingText = currentCards[i].GetComponentInChildren<TMP_Text>();
            if (existingText != null && existingText.color != Color.white)
                continue;

            Vector3 position = currentCards[i].transform.localPosition;
            Destroy(currentCards[i]);

            GameObject openCard = Instantiate(openCardPrefab, gameBoard.transform);
            openCard.transform.localPosition = position;

            // Set symbol
            TMP_Text symbolText = openCard.GetComponentInChildren<TMP_Text>();
            if (symbolText != null)
            {
                symbolText.text = cardSymbols[i].ToString();
                symbolText.color = Color.gray;
            }

            currentCards[i] = openCard;
        }

        // Start next round after showing all cards
        StartCoroutine(NextRoundWithDelay(2f));
    }

    private IEnumerator NextRoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNewRound();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"SCORE: {score}";
    }

    private void ClearBoard()
    {
        foreach (GameObject card in currentCards)
        {
            Destroy(card);
        }
        currentCards.Clear();
    }

    // Keyboard controls for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitializeGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Number keys 1-5 for card selection (for testing)
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                OnCardClicked(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                OnCardClicked(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                OnCardClicked(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                OnCardClicked(3);
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                OnCardClicked(4);
        }
    }
}