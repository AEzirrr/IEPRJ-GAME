using Cinemachine.PostFX;
using Cinemachine.PostFX.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private CinemachinePostProcessing post;
    private DepthOfField dof;
    private ChromaticAberration ca;
    private Bloom b;

    [SerializeField]
    private CinemachinePostProcessing post2;
    private Bloom b2;

    private float timeElapsed;
    private float timer;
    private bool idk = false;
    private bool stop = false;
    private int blinkCount = 0;


    private void Start()
    {
        this.anim = GetComponent<Animator>();
        post.m_Profile.TryGetSettings(out dof);
        post.m_Profile.TryGetSettings(out ca);
        post.m_Profile.TryGetSettings(out b);

        this.dof.focalLength.value = default;
        this.ca.intensity.value = default;
        this.b.intensity.value = default;
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        timer += Time.deltaTime;

        if(timeElapsed >= 10f && !idk && !stop)
        {
            anim.SetTrigger("isBlinking");
            idk = true;
            timeElapsed = 0f;
            blinkCount++;
        }

        if(timeElapsed >= 3f && idk && !stop)
        {
            anim.SetTrigger("isBlinking");
            timeElapsed = 0f;
            blinkCount++;
        }

        if(timeElapsed >= 0.8f && blinkCount == 4 && !stop)
        {
            changeEnvironment();
            stop = true;
        }

        if(idk && !stop)
        {
            this.changeChromaticAberration();
            this.changeDepthOfField();
            this.changeBloom();
        }

        if(stop)
        {
            decreaseBloom();
        }

        if(timer >= 24f)
        {
            SceneManager.LoadScene("Environment 1", LoadSceneMode.Single);
        }
    }

    private void changeEnvironment()
    {
        this.dof.focalLength.value = default;
        this.ca.intensity.value = default;
        this.b.intensity.value = 50;
    }

    private void changeDepthOfField()
    {
        this.dof.focalLength.value += 50 * Time.deltaTime;
    }

    private void changeChromaticAberration()
    {
        this.ca.intensity.value += 1f * Time.deltaTime;
    }

    private void changeBloom()
    {
        this.b.intensity.value += 8 * Time.deltaTime;
    }

    private void decreaseBloom()
    {
        this.b.intensity.value -= 40 * Time.deltaTime;
        if (this.b.intensity.value == 0)
        {
            this.b.intensity.value = 0;
        }
    }
}
