using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternChecker : MonoBehaviour
{
    public List<int> noteSequence;
    public int requiredPatternLength = 8; // Adjust as needed for the correct pattern length

    void Start()
    {
        noteSequence = new List<int>();
    }

    void Update()
    {
        if (noteSequence.Count == requiredPatternLength)
        {
            SequenceChecker();
        }
    }

    private void SequenceChecker()
    {
        if (noteSequence[0] == 1 && noteSequence[1] == 2 && noteSequence[2] == 3 &&
            noteSequence[3] == 4 && noteSequence[4] == 5 && noteSequence[5] == 6 &&
            noteSequence[6] == 7 && noteSequence[7] == 8)
        {
            Debug.Log("CORRECT PATTERN");
            EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_2.PUZZLETEST_COMPLETE);
        }
        else
        {
            EventBroadcaster.Instance.PostEvent(EventNames.PuzzleTest_2.ON_RESET_TARGET);
            Debug.Log("INCORRECT PATTERN");
        }
        noteSequence.Clear();
    }

    public void AddNoteToSequence(int note)
    {
        if (noteSequence.Count < requiredPatternLength)
        {
            noteSequence.Add(note);
        }
    }
}
