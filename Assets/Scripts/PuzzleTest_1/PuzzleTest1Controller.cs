using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTest1Controller : MonoBehaviour
{
    [SerializeField] private int _totalCheckers;
    
    private int _checkerCounter;
    void Start()
    {
        this._checkerCounter = 0;   
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_1.ON_CORRECT_COLOR, this.AddCount);
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_1.ON_INCORRECT_COLOR, this.SpawnEnemy);
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_1.CHECKER_EMPTY, this.ReduceCount);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_1.ON_CORRECT_COLOR);
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_1.ON_INCORRECT_COLOR);
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_1.CHECKER_EMPTY);
    }
    private void AddCount()
    {
        this._checkerCounter++;
        if(this._checkerCounter == this._totalCheckers)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_1.PUZZLETEST_COMPLETE);
            Debug.Log("Puzzle Complete");
        }
    }

    private void ReduceCount()
    {
        this._checkerCounter--;
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawn Enemy");
    }
}
