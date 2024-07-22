using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndManaManager : MonoBehaviour
{
    public static HealthAndManaManager Instance;

    [SerializeField]
    private Slider _manaSlider;

    private float _maxMana = 10f;
    private float _startingMana = 10f;
    [SerializeField]
    private float _manaRegenRate = 0.8f;
    private float _currentMana;

    [SerializeField]
    private Slider _healthSlider;

    private float _maxHealth = 100f;
    private float _startinghealth = 100f;
    [SerializeField]
    private float _healthRegenRate = 15f;
    private float _currentHealth;

    private bool _isTakingDamage = false;
    private Coroutine _healthRegenCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this._currentMana = this._startingMana;
        this._currentHealth = this._startinghealth;
        this.UpdateManaUI();
        this.UpdateHealthUI();
    }

    private void Update()
    {
        this.RegenerateMana();
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(3);
        while (!this._isTakingDamage && this._currentHealth < this._maxHealth)
        {
            this._currentHealth += this._healthRegenRate * Time.deltaTime;
            this._currentHealth = Mathf.Clamp(this._currentHealth, 0f, this._maxHealth);
            this.UpdateHealthUI();
            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {
        this._currentHealth -= damage;
        this._currentHealth = Mathf.Clamp(this._currentHealth, 0f, _maxHealth);
        this.UpdateHealthUI();

        this._isTakingDamage = true;
        if (this._healthRegenCoroutine != null)
        {
            StopCoroutine(this._healthRegenCoroutine);
        }
        this._healthRegenCoroutine = StartCoroutine(WaitAndStartHealthRegen());
    }

    private IEnumerator WaitAndStartHealthRegen()
    {
        yield return new WaitForSeconds(3);
        this._isTakingDamage = false;
        this._healthRegenCoroutine = StartCoroutine(RegenerateHealth());
    }

    private void RegenerateMana()
    {
        if (this._currentMana < this._maxMana)
        {
            this._currentMana += this._manaRegenRate * Time.deltaTime;
            this._currentMana = Mathf.Clamp(this._currentMana, 0f, this._maxMana);
            this.UpdateManaUI();
        }
    }

    private void UpdateManaUI()
    {
        if (this._manaSlider != null)
        {
            this._manaSlider.value = this._currentMana / this._maxMana;
        }
    }

    private void UpdateHealthUI()
    {
        if (this._healthSlider != null)
        {
            this._healthSlider.value = this._currentHealth / this._maxHealth;
        }
    }

    public bool CanShoot(float manaCost)
    {
        return this._currentMana >= manaCost;
    }

    public void ShootProjectile(float manaCost)
    {
        this._currentMana -= manaCost;
        this._currentMana = Mathf.Clamp(this._currentMana, 0f, this._maxMana);
        this.UpdateManaUI();
    }
}
