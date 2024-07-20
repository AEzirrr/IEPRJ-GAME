using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCube : MonoBehaviour
{
    [SerializeField] private GameObject topLayer;
    [SerializeField] private GameObject cube;
    [SerializeField] private float heightOffset = .01f;  // Height offset from the cube

    void Update()
    {
        // Get the bounds of the cube to center the topLayer
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            Vector3 cubeSize = cubeRenderer.bounds.size;
            Vector3 cubeCenter = cubeRenderer.bounds.center;

            // Position the topLayer on top of the cube
            topLayer.transform.position = new Vector3(
                cubeCenter.x,
                cubeCenter.y + (cubeSize.y / 2) + heightOffset,  // Centered on top of the cube plus offset
                cubeCenter.z
            );
        }

        // Match the rotation of the topLayer with the cube
        topLayer.transform.rotation = cube.transform.rotation;
    }
}
