using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingPuzzle : MonoBehaviour, IPuzzle
{
    public string[] words;
    private string currentWord;
    private string playerInput = "";
    public TMP_Text wordDisplay;
    public TMP_Text inputDisplay;
    private float timeRemaining = 10f; // example time limit for the puzzle
    public TMP_Text timerDisplay;
    private bool isActive = false;

    void Start()
    {
        StartPuzzle();
    }

    public void StartPuzzle()
    {
        // Select a random word from the list
        currentWord = words[Random.Range(0, words.Length)];
        wordDisplay.text = currentWord;
        playerInput = "";
        inputDisplay.text = playerInput;
        timeRemaining = 10f; // Reset time
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
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Backspace
                {
                    if (playerInput.Length > 0)
                    {
                        playerInput = playerInput.Substring(0, playerInput.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r')) // Enter
                {
                    CheckInput();
                }
                else
                {
                    playerInput += c;
                }

                inputDisplay.text = playerInput;
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

    private void CheckInput()
    {
        if (playerInput == currentWord)
        {
            isActive = false;
            // Call GameController's PuzzleSolved method to load the next puzzle
            GameController gameController = FindObjectOfType<GameController>();
            gameController.PuzzleSolved();
            Debug.Log("Puzzle solved: " + playerInput);
        }
        else
        {
            Debug.Log("Incorrect input: " + playerInput);
        }
    }

    private void UpdateTimerDisplay()
    {
        timerDisplay.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }
}