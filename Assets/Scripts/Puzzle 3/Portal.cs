using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("LEVEL 1 DONE");
            SceneManager.LoadScene("Bedroom", LoadSceneMode.Single);
        }
    }
}
