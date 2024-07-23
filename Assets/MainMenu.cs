using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fadeImage;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private float transitionTime = 1.0f;
    [SerializeField] private AudioClip playSFX;

    public void PlayGame(string sceneName)
    {
        SFXManager.instance.PlaySfxClip(playSFX, transform, 1f);
        fadeImage.SetActive(true);
        StartCoroutine(PlayGameAfterFade(sceneName));
    }

    private IEnumerator PlayGameAfterFade(string sceneName)
    {
        // Start the fade out animation
        animator.SetBool("FadeIn", true);

        // Wait for the animation to complete
        yield return new WaitForSeconds(transitionTime);

        // Switch to the next scene
        LoadingScreenManager.Instance.SwitchToScene(sceneName);
    }

    public void OnOptionsClicked()
    {
        optionsPanel.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
