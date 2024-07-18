using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [SerializeField]
    private float rotSpd = 100f;

    void Update()
    {
        this.transform.Rotate(0, 1 * rotSpd * Time.deltaTime, 0);
    }
}
