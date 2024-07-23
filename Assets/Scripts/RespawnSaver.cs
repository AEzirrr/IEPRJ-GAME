using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSaver : MonoBehaviour
{
    [SerializeField] private HealthAndManaManager healthAndManaManager;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthAndManaManager.SaveRespawnPoint(spawnPoint);
        }
    }
}
