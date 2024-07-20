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
    }

    private void OnDestroy()
    {

    }

    public void OpenChest()
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
