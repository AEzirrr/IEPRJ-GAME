using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] GameObject slimePrefab;
    [SerializeField] List<Transform> spawns;

    private bool isPuzzleFinished = false;
    private bool isActivated = false;

    private Vector3 targetPosition;
    private List<GameObject> slimes = new List<GameObject>();

    public void OnPuzzleFinish()
    {
        isPuzzleFinished = true;

        targetPosition = new Vector3(portal.transform.position.x, portal.transform.position.y + 6f, portal.transform.position.z);

        // Clear any existing slimes
        foreach (GameObject slime in slimes)
        {
            if (slime != null)
            {
                Destroy(slime);
            }
        }
        slimes.Clear();

        // Spawn a slime for each spawn point
        foreach (Transform spawn in spawns)
        {
            GameObject newSlime = Instantiate(slimePrefab, spawn.position, Quaternion.identity);
            slimes.Add(newSlime);
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            float step = 2f * Time.deltaTime; // Movement speed
            float distanceToTarget = Vector3.Distance(portal.transform.position, targetPosition);

            if (distanceToTarget <= step)
            {
                // Snap to the target position if within the step distance
                portal.transform.position = targetPosition;
                isActivated = false;
                Debug.Log("Portal reached the target position");
            }
            else
            {
                // Move the portal towards the target position
                portal.transform.position = Vector3.MoveTowards(portal.transform.position, targetPosition, step);
            }
        }

        // Check slimes status in every update
        CheckSlimesStatus();
    }

    private void CheckSlimesStatus()
    {
        bool allDead = true; // Assume all dead initially
        foreach (GameObject slime in slimes)
        {
            if (slime != null)
            {
                allDead = false; // Found at least one slime alive
                break;
            }
        }

        if (allDead && isPuzzleFinished)
        {
            // All slimes are dead, proceed with portal activation
            portal.SetActive(true);
            isActivated = true;
            Debug.Log("Portal activation initiated");
        }
    }
}
