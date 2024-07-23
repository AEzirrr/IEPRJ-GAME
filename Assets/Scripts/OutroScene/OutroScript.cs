using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private float vidTime = 22f;

    private void Start()
    {
        StartCoroutine(MainMenuLoader());
    }

    private IEnumerator MainMenuLoader()
    {
        yield return new WaitForSeconds(vidTime);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
