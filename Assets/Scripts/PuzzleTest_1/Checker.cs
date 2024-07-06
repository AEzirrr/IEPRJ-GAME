using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private string _checkerColor;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Material _defaultMaterial;

    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pushable"))
        {
            if(other.gameObject.name == this._checkerColor)
            {
                this._renderer.material = this._newMaterial;
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_1.ON_CORRECT_COLOR);
                Debug.Log("Correct Color");
            }
            else
            {
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_1.ON_INCORRECT_COLOR);
                Debug.Log("Incorrect Color");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Pushable"))
        {
            if(other.gameObject.name == this._checkerColor)
            {
                this._renderer.material = this._defaultMaterial;
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_1.CHECKER_EMPTY);
            }
        }
    }
}
