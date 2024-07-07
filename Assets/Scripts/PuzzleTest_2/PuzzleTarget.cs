using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTarget : MonoBehaviour
{
    public int noteValue; 
    [SerializeField] private PatternChecker patternChecker;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Material _defaultMaterial;

    [SerializeField]
    private AudioClip noteSFX;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        EventBroadcaster.Instance.AddObserver(EventNames.PuzzleTest_2.ON_RESET_TARGET, this.ResetMaterial);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PuzzleTest_2.ON_RESET_TARGET);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) 
        {
            SFXManager.instance.PlaySfxClip(noteSFX, transform, .01f);
            this._renderer.material = this._newMaterial;
            patternChecker.AddNoteToSequence(noteValue);
            Debug.Log("TARGET HIT, NOTE:" + noteValue);
        }
    }

    private void ResetMaterial()
    {
        this._renderer.material = this._defaultMaterial;
    }
}
