using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePrefabs;
    private GameObject currentPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        loadNextPuzzle();   
    }

    public void loadNextPuzzle()
    {
        if(currentPuzzle != null)
        {
            Destroy(currentPuzzle);
        }
        // Randomly select the next puzzle 
        int randomIndex = Random.Range(0, puzzlePrefabs.Length);
        currentPuzzle = Instantiate(puzzlePrefabs[randomIndex], transform);
        IPuzzle puzzle = currentPuzzle.GetComponent<IPuzzle>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
