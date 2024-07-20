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
    private Light _orbPointLight; // Point light with halo

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
    private float _orbTimer; 

    public ManaSystem manaSystem;

    private Material _orbMaterial;
    private Color _originalEmissionColor;
    private float _blinkThreshold = 2f; // Time remaining to start blinking
    private float _blinkFrequency = 5f; 
    private float _originalLightIntensity;

    private void Awake()
    {
        if (_orbModel != null)
        {
            Renderer orbRenderer = _orbModel.GetComponent<Renderer>();
            if (orbRenderer != null)
            {
                _orbMaterial = orbRenderer.material;
                if (_orbMaterial.HasProperty("_EmissionColor"))
                {
                    _originalEmissionColor = _orbMaterial.GetColor("_EmissionColor");
                }
            }
            else
            {
                Debug.LogError("Renderer component not found on the orb model.");
            }
        }
        else
        {
            Debug.LogError("Orb model not assigned.");
        }

        if (_orbPointLight != null)
        {
            _originalLightIntensity = _orbPointLight.intensity;
        }
        else
        {
            Debug.LogError("Orb point light not assigned.");
        }

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

        if (_isOrb)
        {
            _orbTimer -= Time.deltaTime;
            if (_orbTimer <= 0f)
            {
                TransformBackToPlayer();
            }
            else if (_orbTimer <= _blinkThreshold)
            {
                BlinkOrb();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && !_isOnCooldown)
        {
            Debug.Log("PRESSED Q");
            if (_isOrb)
            {
                TransformBackToPlayer();
            }
            else
            {
                TransformToOrb();
            }

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

    private void TransformToOrb()
    {
        SFXManager.instance.PlaySfxClip(orbTransform, transform, .5f);
        _orbModel.SetActive(true);
        _playerModel.SetActive(false);
        _isOrb = true;
        _orbTimer = transformCooldown;
        TransformProperties.Form = ETransform.ORB_FORM;
    }

    private void TransformBackToPlayer()
    {
        SFXManager.instance.PlaySfxClip(playerTransform, transform, .5f);
        _orbModel.SetActive(false);
        _playerModel.SetActive(true);
        _isOrb = false;
        TransformProperties.Form = ETransform.HUMAN_FORM;
        _isOnCooldown = true;
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

    private void BlinkOrb()
    {
        float lerp = Mathf.PingPong(Time.time * _blinkFrequency, 1f);
        Color blinkColor = Color.Lerp(_originalEmissionColor, Color.black, lerp);
        _orbMaterial.SetColor("_EmissionColor", blinkColor);

        if (_orbPointLight != null)
        {
            _orbPointLight.intensity = Mathf.Lerp(_originalLightIntensity, 0f, lerp);
        }
    }
}
