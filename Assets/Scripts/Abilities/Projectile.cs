using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _projectileSpeed;

    private Vector3 _firingPoint;

    private float _elapsedTime;
    void Start()
    {
        this._firingPoint = transform.position;
    }

    void FixedUpdate()
    {
        this._elapsedTime += Time.deltaTime;
        this.MoveProjectile();

        if(this._elapsedTime >= 4f )
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pushable"))
        {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 hitNormal = contactPoint.normal;
            Vector3 forceDirection = -hitNormal;

            collision.rigidbody.AddForce(forceDirection * 1000f, ForceMode.Force);

            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Note"))
        {
            Debug.Log("NOTE HIT");
            Destroy(this.gameObject);
        }

    }

}
