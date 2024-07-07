using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float targetYPosition = 5.0f; // The Y position to move the door to
    [SerializeField] private float moveSpeed = 2.0f; // Speed of the door movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = new Vector3(initialPosition.x, targetYPosition, initialPosition.z);
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_2.PUZZLETEST_COMPLETE, this.OnDoorOpen);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_2.PUZZLETEST_COMPLETE);
    }

    void Update()
    {
        if (isOpening)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Stop moving when the door reaches the target position
        if (transform.position == targetPosition)
        {
            isOpening = false;
        }
    }

    private void OnDoorOpen()
    {
        isOpening = true;
    }

}
