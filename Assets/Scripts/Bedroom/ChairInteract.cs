using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInteract : MonoBehaviour
{
    [SerializeField] GameObject chair;
    [SerializeField] GameObject interactPanel;
    [SerializeField] BedroomProgression progression;

    private Animator animator;
    private bool playerInRange = false;
    private bool chairIn = false;

    private void Awake()
    {
        animator = chair.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progression.completedMove == true)
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
            if (progression.completedMove == true)
            {
                Debug.Log("Player left chair Range");
                interactPanel.SetActive(false);
                playerInRange = false;
            }
        }
                
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            if(progression.completedMove == true)
            {
                if(chairIn == false)
                {
                    Debug.Log("Interacted with Chair");
                    animator.SetBool("IsChairIn", true);
                    progression.interactedChair = true;
                    chairIn = true;
                }
                else
                {
                    Debug.Log("Interacted with Chair");
                    animator.SetBool("IsChairIn", false);
                    chairIn = false;
                }
            }
        }
    }
}
