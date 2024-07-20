using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject interactPanel;
    [SerializeField] GameObject leverStick;
    [SerializeField] AudioClip leverSFX;

    private float moveSpeed = 1f;

    private bool playerInRange = false;
    private bool isInteracted = false;
    private bool isRotating = false;
    private Quaternion targetRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isInteracted)
            {
                Debug.Log("Player in Lever Range");
                interactPanel.SetActive(true);
                playerInRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isInteracted)
            {
                Debug.Log("Player left Lever Range");
                interactPanel.SetActive(false);
                playerInRange = false;
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E) && !isInteracted)
        {
            Debug.Log("Interacted with Lever");
            isInteracted = true;
            interactPanel.SetActive(false);
            targetRotation = Quaternion.Euler(leverStick.transform.rotation.eulerAngles.x - 60f,
                                              leverStick.transform.rotation.eulerAngles.y,
                                              leverStick.transform.rotation.eulerAngles.z);
            isRotating = true;
            SFXManager.instance.PlaySfxClip(leverSFX, transform, .1f);
            CubeGate.Instance.OnOpen();
        }

        if (isRotating)
        {
            float step = moveSpeed * Time.deltaTime * 30f; // Ensure speed scales correctly
            leverStick.transform.rotation = Quaternion.RotateTowards(leverStick.transform.rotation, targetRotation, step);

            if (Quaternion.Angle(leverStick.transform.rotation, targetRotation) < 0.1f)
            {
                leverStick.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }
}
