using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private GameObject _orbModel;

    [SerializeField] private AudioClip orbTransform;
    [SerializeField] private AudioClip playerTransform;

    private bool _isOrb;

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
    }
}
