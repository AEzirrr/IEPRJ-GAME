using UnityEngine;

public class CubeGate : MonoBehaviour
{
    public static CubeGate Instance { get; private set; }

    [SerializeField] private GameObject cubeGate;
    private float moveSpeed = 1f;
    private bool isOpening = false;
    private Vector3 targetPosition;

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

    private void Update()
    {
        if (isOpening)
        {
            var step = moveSpeed * Time.deltaTime;
            cubeGate.transform.position = Vector3.MoveTowards(cubeGate.transform.position, targetPosition, step);

            if (cubeGate.transform.position == targetPosition)
            {
                isOpening = false;
            }
        }
    }

    public void OnOpen()
    {
        targetPosition = new Vector3(cubeGate.transform.position.x, cubeGate.transform.position.y + 3.25f, cubeGate.transform.position.z);
        isOpening = true;
    }
}
