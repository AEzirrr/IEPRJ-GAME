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

    void Update()
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
}
