using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityArea : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsOpacity = new List<GameObject>();

    private List<Material> objectMaterials = new List<Material>();
    private Coroutine fadeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the materials list
        foreach (GameObject obj in objectsOpacity)
        {
            Material mat = obj.GetComponent<Renderer>()?.material;
            if (mat != null)
            {
                objectMaterials.Add(mat);
            }
            else
            {
                Debug.LogWarning($"Material not found on {obj.name}. Ensure the object has a Renderer component.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED OPACITYAREA RANGE");
            // Start fading to 50% opacity
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeTo(0.5f, 1.0f)); // Fade to 50% opacity over 1 second
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER EXIT OPACITYAREA RANGE");
            // Start fading back to 100% opacity
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeTo(1.0f, 1.0f)); // Fade to 100% opacity over 1 second
        }
    }

    private IEnumerator FadeTo(float targetOpacity, float duration)
    {
        // Get the start opacity for all materials
        float startOpacity = objectMaterials.Count > 0 ? objectMaterials[0].color.a : 1.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsed / duration);
            SetObjectsOpacity(newOpacity);
            yield return null;
        }

        SetObjectsOpacity(targetOpacity); // Ensure final opacity is set
    }

    private void SetObjectsOpacity(float opacity)
    {
        foreach (Material mat in objectMaterials)
        {
            if (mat != null)
            {
                Color color = mat.color;
                color.a = opacity;
                mat.color = color;
            }
        }
    }
}
