using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _followDistance;

    [SerializeField]
    private float _followSpeed;

    private Rigidbody _rb;

    private bool _isEquipped = false;
    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ItemInteraction.ON_ITEM_PICKUP, this.OnPickup);
        EventBroadcaster.Instance.AddObserver(EventNames.ItemInteraction.ON_ITEM_DROP, this.OnDrop);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ItemInteraction.ON_ITEM_PICKUP);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ItemInteraction.ON_ITEM_DROP);
    }

    private void Update()
    {
        if(this._isEquipped)
        {
            Vector3 targetPosition = _player.position;
            targetPosition.y = _player.position.y;
            transform.position = Vector3.Lerp(transform.position, targetPosition, this._followSpeed * Time.deltaTime);
        }
    }

    private void OnPickup()
    {
        this._rb = this.gameObject.GetComponentInChildren<Rigidbody>();
        this._rb.useGravity = false;
        this._isEquipped = true;
    }

    private void OnDrop()
    {
        this._rb = this.gameObject.GetComponentInChildren<Rigidbody>();
        this._rb.useGravity = true;
        this._isEquipped = false;
    }
}
