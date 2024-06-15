using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{ 
    public Player player;
    public Enemy enemy;
    public PuzzleManager puzzleManager;

    private float timeSinceLastPuzzleSolved = 0;
    private bool isBattleActive = true;


    // Start is called before the first frame update
    void Start()
    {
     if(puzzleManager != null)
        {
            puzzleManager.loadNextPuzzle();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleActive)
        {
            timeSinceLastPuzzleSolved += Time.deltaTime;
            
            if(timeSinceLastPuzzleSolved >= 1f)
            {
                // Apply damage based on enemy's damage per second
                player.TakeDamage(enemy.damagePerSecond);
                timeSinceLastPuzzleSolved = 0f;
            }
        }

        
    }

    public void PuzzleSolved()
    {
        // Reset timer
        timeSinceLastPuzzleSolved = 0f;
        // Load next puzzle
        if (puzzleManager != null)
        {
            puzzleManager.loadNextPuzzle();
        }
    }
}
