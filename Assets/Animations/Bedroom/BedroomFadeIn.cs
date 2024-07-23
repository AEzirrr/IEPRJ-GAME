using System.Collections;
using UnityEngine;

public class BedroomFadeIn : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
 
        yield return new WaitForSeconds(1.0f);


        gameObject.SetActive(false);
    }
}
