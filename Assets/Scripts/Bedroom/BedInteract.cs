using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteract : MonoBehaviour
{
    [SerializeField] GameObject interactPanel;
    [SerializeField] BedroomProgression progression;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progression.completedTutorial == true)
            {
                interactPanel.SetActive(true);
                playerInRange = true;
            }
                    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progression.completedTutorial == true)
            {
                Debug.Log("Player left Bed Range");
                interactPanel.SetActive(false);
                playerInRange = false;
            }
                
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            if (progression.completedTutorial == true)
            {
                Debug.Log("Interacted with Bed");
                progression.interactedBed = true;
            }
        }
    }
}
