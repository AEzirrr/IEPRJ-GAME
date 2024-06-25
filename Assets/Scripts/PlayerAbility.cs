using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerModel;

    [SerializeField]
    private GameObject _orbModel;

    [SerializeField]
    private AudioClip orbTransform;

    [SerializeField]
    private AudioClip playerTransform;

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private Transform _spawnPos;

    private bool _isOrb;

    public ManaSystem manaSystem;

    private void Awake()
    {
        manaSystem = GetComponent<ManaSystem>();
    }

    void Start()
    {
        this._orbModel.SetActive(false);
        this._isOrb = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(this._isOrb)
            {
                SFXManager.instance.PlaySfxClip(playerTransform, transform, .5f);
                this._orbModel.SetActive(false);
                this._playerModel.SetActive(true);
                this._isOrb = false;
                TransformProperties.Form = ETransform.HUMAN_FORM;
            }
            else
            {
                SFXManager.instance.PlaySfxClip(orbTransform, transform, .5f);
                this._orbModel.SetActive(true);
                this._playerModel.SetActive(false);
                this._isOrb = true;
                TransformProperties.Form = ETransform.ORB_FORM;
            }
        }

        if(this._isOrb)
        {
            this.MouseAimInput();
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(manaSystem.CanShoot(2f))
                {
                    this.Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        Instantiate(this._projectile, this._spawnPos.position, this._spawnPos.rotation);
        manaSystem.ShootProjectile(2f);
    }

    private void MouseAimInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);   

        if(Physics.Raycast(_ray, out _hit))
        {
            this._spawnPos.LookAt(new Vector3(_hit.point.x, this._spawnPos.position.y, _hit.point.z));
        }
    }
}
