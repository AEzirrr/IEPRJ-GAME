using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool _isInRange;
    private bool _isEquipped = false;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && this._isInRange && !this._isEquipped)
        {
            this._isEquipped = true;
            EventBroadcaster.Instance.PostEvent(EventNames.ItemInteraction.ON_ITEM_PICKUP);
        }

        else if(Input.GetKeyUp(KeyCode.E) && this._isInRange && this._isEquipped)
        {
            this._isEquipped = false;
            EventBroadcaster.Instance.PostEvent(EventNames.ItemInteraction.ON_ITEM_DROP);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            this._isInRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            this._isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            this._isInRange = false;
        }
    }
}
