using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Key : MonoBehaviour
{
    [SerializeField]
    private GameObject key;

    private GameObject interactPanel;

    private bool playerInRange = false;

    private void Start()
    {
        interactPanel = FindInteractPanel();

        if (interactPanel == null)
        {
            Debug.LogError("InteractPanel not found. Make sure it is tagged correctly in the scene.");
        }
    }

    private GameObject FindInteractPanel()
    {
        GameObject[] allPanels = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject panel in allPanels)
        {
            if (panel.CompareTag("InteractPanel"))
            {
                return panel;
            }
        }
        return null;
    }

    public void Pickup()
    {
        interactPanel.SetActive(false);
        GateController.Instance.OnKeyInteracted();
        StartCoroutine(KeyDestroyer());
    }

    private IEnumerator KeyDestroyer()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(key);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPanel.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left Key Range");
            interactPanel.SetActive(false);
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            Pickup();
        }
    }
}
