using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker2 : MonoBehaviour
{
    [SerializeField] private string _checkerColor;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private PuzzleTest3Controller puzzleController;

    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pushable"))
        {
            if (other.gameObject.name == this._checkerColor)
            {
                this._renderer.material = this._newMaterial;
                puzzleController.AddCount();

                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                    Debug.Log("Correct Color - Rigidbody removed");
                }

                Debug.Log("Correct Color");
            }
            else
            {
                puzzleController.ReduceCount();
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
                puzzleController.ReduceCount(); 
            }
        }
    }
}
