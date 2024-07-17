using System.Collections;
using UnityEngine;

public class BedroomProgression : MonoBehaviour
{
    public bool interactedLight = false;
    public bool interactedChair = false;
    public bool completedMove = false;
    public bool completedTutorial = false;

    public bool interactedBed = false;
    

    [SerializeField] private GameObject chairTutorialPanel;
    [SerializeField] private GameObject lightTutorialPanel;
    [SerializeField] private GameObject moveTutorialPanel;
    [SerializeField] private GameObject gotoBedPanel;

    private bool pressedW = false;
    private bool pressedA = false;
    private bool pressedS = false;
    private bool pressedD = false;

    [SerializeField] AudioClip taskFinishSFX;

    private void Start()
    {
        StartCoroutine(BedroomTutorial());
    }

    private IEnumerator BedroomTutorial()
    {
        // MOVEMENT
        moveTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => completedMove);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        moveTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        // CHAIR INTERACT
        chairTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => interactedChair);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        chairTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        // LIGHT INTERACT
        lightTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => interactedLight);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        lightTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        completedTutorial = true;

        // BED INTERACT
        gotoBedPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => interactedBed);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        gotoBedPanel.SetActive(false);

        Debug.Log("Tutorial Completed");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            pressedW = true;
            Debug.Log("Completed W");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedA = true;
            Debug.Log("Completed A");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pressedS = true;
            Debug.Log("Completed S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedD = true;
            Debug.Log("Completed D");
        }

        if (pressedW && pressedA && pressedS && pressedD)
        {
            completedMove = true;
        }
    }
}
