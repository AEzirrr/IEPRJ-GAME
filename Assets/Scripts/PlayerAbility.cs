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

    [SerializeField]
    private float transformCooldown = 10f;

    private bool _isOrb;
    private bool _isOnCooldown;
    private float _cooldownTimer;

    public ManaSystem manaSystem;

    private void Awake()
    {
        manaSystem = GetComponent<ManaSystem>();
    }

    void Start()
    {
        _orbModel.SetActive(false);
        _isOrb = false;
        _isOnCooldown = false;
    }

    void Update()
    {
        if (_isOnCooldown)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _isOnCooldown = false;
                _cooldownTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && !_isOnCooldown)
        {
            if (_isOrb)
            {
                SFXManager.instance.PlaySfxClip(playerTransform, transform, .5f);
                _orbModel.SetActive(false);
                _playerModel.SetActive(true);
                _isOrb = false;
                TransformProperties.Form = ETransform.HUMAN_FORM;
            }
            else
            {
                SFXManager.instance.PlaySfxClip(orbTransform, transform, .5f);
                _orbModel.SetActive(true);
                _playerModel.SetActive(false);
                _isOrb = true;
                TransformProperties.Form = ETransform.ORB_FORM;
            }


            _isOnCooldown = true;
            _cooldownTimer = transformCooldown;
        }

        if (_isOrb)
        {
            MouseAimInput();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (manaSystem.CanShoot(1f))
                {
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        Instantiate(_projectile, _spawnPos.position, _spawnPos.rotation);
        manaSystem.ShootProjectile(1f);
    }

    private void MouseAimInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            _spawnPos.LookAt(new Vector3(_hit.point.x, _spawnPos.position.y, _hit.point.z));
        }
    }
}
