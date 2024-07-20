using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTest3Controller : MonoBehaviour
{
    [SerializeField] private int _totalCheckers;
    [SerializeField] private PortalSpawner _spawner;
    private int _checkerCounter;
    void Start()
    {
        this._checkerCounter = 0;   
    }

    private void OnDestroy()
    {

    }
    public void AddCount()
    {
        this._checkerCounter++;
        if(this._checkerCounter == this._totalCheckers)
        {
            _spawner.OnPuzzleFinish();
            Debug.Log("Puzzle Complete");
        }
    }

    public void ReduceCount()
    {
        this._checkerCounter--;
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawn Enemy");
    }
}
