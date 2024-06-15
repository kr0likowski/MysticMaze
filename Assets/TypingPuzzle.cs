using UnityEngine;
using TMPro;

public class TypingPuzzle : MonoBehaviour, IPuzzle
{
    public string[] words; // List of words for the puzzle
    private string currentWord; // Current word to be typed
    private int currentIndex = 0; // Current index the player is typing
    public TMP_Text wordDisplay; // TextMesh Pro component for displaying the word
    private float timeRemaining = 10f; // Time limit for the puzzle, in seconds
    public TMP_Text timerDisplay; // TextMesh Pro component for displaying the timer
    private bool isActive = false; // Whether the puzzle is active
    private string playerInput = ""; // Track all characters typed

    void Start()
    {
        StartPuzzle();
    }

    public void StartPuzzle()
    {
        // Select a random word from the list
        currentWord = words[Random.Range(0, words.Length)];
        currentIndex = 0;
        playerInput = "";
        UpdateWordDisplay();
        timeRemaining = 10f; // Reset time limit
        isActive = true;
        UpdateTimerDisplay();
        Debug.Log("Starting Typing Puzzle: " + currentWord);
    }

    public void ResetPuzzle()
    {
        // Reset the puzzle state
        StartPuzzle();
    }

    void Update()
    {
        if (isActive && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            // Check for player input
            if (Input.anyKeyDown)
            {
                foreach (char c in Input.inputString)
                {
                    if (c == '\b' && currentIndex > 0)
                    {
                        // Handle backspace
                        currentIndex--;
                        playerInput = playerInput.Substring(0, playerInput.Length - 1);
                        UpdateWordDisplay();
                    }
                    else if (c == '\n' || c == '\r')
                    {
                        // Handle enter (do nothing for now)
                    }
                    else if (currentIndex < currentWord.Length)
                    {
                        if (c == currentWord[currentIndex])
                        {
                            playerInput += c;
                            currentIndex++;
                        }
                        // Only update display after handling input
                        UpdateWordDisplay();
                    }

                    // If player completes the word correctly
                    if (currentIndex >= currentWord.Length && playerInput == currentWord)
                    {
                        isActive = false;
                        // Call GameController's PuzzleSolved method to load the next puzzle
                        GameController gameController = FindObjectOfType<GameController>();
                        if (gameController != null)
                        {
                            gameController.PuzzleSolved();
                        }
                        Debug.Log("Puzzle solved: " + currentWord);
                    }
                }
            }

            // If time runs out
            if (timeRemaining <= 0)
            {
                isActive = false;
                // Handle time out (e.g., deal damage to the player)
                Debug.Log("Puzzle failed: Time out");
            }
        }
    }

    private void UpdateWordDisplay()
    {
        // Update the displayed word with color highlighting
        string displayText = "";

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (i < currentIndex)
            {
                displayText += "<color=green>" + currentWord[i] + "</color>";
            }
            else if (i == currentIndex && playerInput.Length > currentIndex && playerInput[currentIndex] != currentWord[currentIndex])
            {
                displayText += "<color=red>" + currentWord[i] + "</color>";
            }
            else
            {
                displayText += currentWord[i];
            }
        }

        wordDisplay.text = displayText;
    }

    private void UpdateTimerDisplay()
    {
        timerDisplay.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }
}