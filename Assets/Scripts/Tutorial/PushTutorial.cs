using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTutorial : MonoBehaviour
{
    [SerializeField] TutorialProgression progression;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            if(progression.completedJump == true) {
            
                if(progression.completedPush == false)
                {
                    progression.completedPush = true;
                }
            }
        }

    }
}
