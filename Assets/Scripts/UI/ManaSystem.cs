using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [SerializeField]
    private Slider _manaSlider;

    private float _maxMana = 10f;
    private float _startingMana = 10f;
    private float _manaRegenRate = 0.8f;

    private float _currentMana;

    private void Start()
    {
        this._currentMana = this._startingMana;
        this.UpdateManaUI();
    }

    private void Update()
    {
        this.RegenerateMana();
    }

    private void RegenerateMana()
    {
        if(this._currentMana < this._maxMana)
        {
            this._currentMana += this._manaRegenRate * Time.deltaTime;
            this._currentMana = Mathf.Clamp(this._currentMana, 0f, this._maxMana);
            this.UpdateManaUI();
        }
    }
    public void UpdateManaUI()
    {
        if(this._manaSlider != null)
        {
            this._manaSlider.value = this._currentMana / this._maxMana;
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
