using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    private Transform _player;

    [SerializeField]
    private float _followDistance;

    [SerializeField]
    private float _followSpeed;

    private Rigidbody _rb;

    public bool _isEquipped = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        this._player = GameObject.Find("ItemTargetPos").GetComponent<Transform>();
    }

    private void Update()
    {
        if(_isEquipped)
        {
            Vector3 targetPosition = _player.position;
            targetPosition.y = _player.position.y;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }
    }

    public void Pickup()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true; 
        _isEquipped = true;
        Debug.Log("Item equipped: " + gameObject.name);
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false; 
        Debug.Log("Item dropped: " + gameObject.name);
    }
}
