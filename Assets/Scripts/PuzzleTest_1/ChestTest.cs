using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTest : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;

    private Renderer _renderer;

    private void Start()
    {
        this._renderer = GetComponent<Renderer>();
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_1.PUZZLETEST_COMPLETE, this.OpenChest);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_1.PUZZLETEST_COMPLETE);
    }

    private void OpenChest()
    {
        this._renderer.material = _newMaterial;
    }
}
