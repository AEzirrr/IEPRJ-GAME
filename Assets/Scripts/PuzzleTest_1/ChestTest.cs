using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTest : MonoBehaviour
{
    [SerializeField] private GameObject keyObject;
    [SerializeField] private Transform keySpawnPos;
    [SerializeField] private Transform parent;
    
    

    private GameObject spawnedKey;

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
        spawnedKey = GameObject.Instantiate(keyObject, keySpawnPos.position, keyObject.transform.rotation, parent);

        spawnedKey.SetActive(true);

        Rigidbody rb = spawnedKey.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse); 
        }
    }
}
