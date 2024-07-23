using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    [SerializeField] private HealthAndManaManager healthAndManaManager;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthAndManaManager.PlayerDeath();
        }
    }
}
