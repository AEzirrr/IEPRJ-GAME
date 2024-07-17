using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightInteract : MonoBehaviour
{
    [SerializeField] GameObject spotLight;
    [SerializeField] GameObject interactPanel;
    [SerializeField] BedroomProgression progression;

    [SerializeField] AudioClip lightSwitchSFX;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progression.interactedChair == true)
            {
                Debug.Log("Player in Light Switch Range");
                interactPanel.SetActive(true);
                playerInRange = true;
            }       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progression.interactedChair == true)
            {
                Debug.Log("Player left Light Switch Range");
                interactPanel.SetActive(false);
                playerInRange = false;
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            if (progression.interactedChair == true)
            {
                Debug.Log("Interacted with Light Switch");
                SFXManager.instance.PlaySfxClip(lightSwitchSFX, transform, .1f);
                spotLight.SetActive(!spotLight.activeSelf);
                progression.interactedLight = true;
            }
        }
    }
}
