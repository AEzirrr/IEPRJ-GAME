using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool _isInRange;
    private ItemInteract _currentItem;
    private ItemInteract _equippedItem;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && _isInRange)
        {
            if (_equippedItem == null)
            {
                _currentItem.Pickup();
                _equippedItem = _currentItem;
                _equippedItem._isEquipped = true;
            }
            else
            {
                _equippedItem.Drop();
                _equippedItem._isEquipped = false;
                _equippedItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            _isInRange = true;
            _currentItem = other.gameObject.GetComponent<ItemInteract>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            _currentItem = null;
            _isInRange = false;
        }
    }
}
