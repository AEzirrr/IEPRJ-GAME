using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternChecker : MonoBehaviour
{
    public List<int> noteSequence;
    public int requiredPatternLength = 8; 
    [SerializeField] private AudioClip failedSFX;

    void Start()
    {
        noteSequence = new List<int>();
    }

    public void AddNoteToSequence(int note)
    {
        if (noteSequence.Count < requiredPatternLength)
        {
            noteSequence.Add(note);
   
            CheckSequence();
        }
    }

    private void CheckSequence()
    {

        for (int i = 0; i < noteSequence.Count; i++)
        {
            if (noteSequence[i] != i + 1)
            {
 
                EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_2.ON_RESET_TARGET);
                SFXManager.instance.PlaySfxClip(failedSFX, transform, 0.03f);
                Debug.Log("INCORRECT PATTERN");
                noteSequence.Clear();
                return; 
            }
        }

   
        if (noteSequence.Count == requiredPatternLength)
        {
            Debug.Log("CORRECT PATTERN");
            EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_2.PUZZLETEST2_COMPLETE);
            noteSequence.Clear(); 
        }
    }
}
