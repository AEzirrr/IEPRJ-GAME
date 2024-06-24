using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool _isInRange;
    private ItemInteract _currentItem;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && _isInRange && _currentItem != null && !_currentItem._isEquipped && (TransformProperties.Form == ETransform.HUMAN_FORM))
        {
            _currentItem.Pickup();
            Debug.Log("Item picked up");
        }
        else if (Input.GetKeyUp(KeyCode.E) && _isInRange && _currentItem != null && _currentItem._isEquipped)
        {
            _currentItem.Drop();
            Debug.Log("Item dropped");
        }
        else if (TransformProperties.Form == ETransform.ORB_FORM && _currentItem != null)
        {
            _currentItem.Drop();
            Debug.Log("Item dropped");
        }

        if (_isInRange)
        {
            Debug.Log("IN RANGE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            _isInRange = true;
            _currentItem = other.gameObject.GetComponent<ItemInteract>();
            Debug.Log("Entered range of item: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (_currentItem != null && _currentItem.gameObject == other.gameObject)
            {
                _currentItem = null;
                _isInRange = false;
                Debug.Log("Exited range of item: " + other.gameObject.name);
            }
        }
    }
}
