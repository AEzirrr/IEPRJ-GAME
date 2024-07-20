using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;
    [SerializeField] private GameObject Gate;
    [SerializeField] private GameObject KeyHole;
    [SerializeField] private float fadeDuration = 1.0f;

    [SerializeField] private TutorialProgression progression;

    [SerializeField]
    private AudioClip GateOpenSFX;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Key"))
        {
            Renderer keyholeRenderer = KeyHole.GetComponent<Renderer>();
            if (keyholeRenderer != null)
            {
                keyholeRenderer.material = _newMaterial;
            }
            SFXManager.instance.PlaySfxClip(GateOpenSFX, transform, .01f);
            StartCoroutine(KeyholeDestroyer());
            Destroy(collider.gameObject);
            StartCoroutine(GateOpener());
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color initialColor = material.color;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
                material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                yield return null;
            }
            material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0.0f);
        }

        Destroy(obj);
    }

    private IEnumerator KeyholeDestroyer()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutAndDestroy(KeyHole));
    }

    private IEnumerator GateOpener()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutAndDestroy(Gate));
        progression.completedpuzzle1 = true;
    }
}
