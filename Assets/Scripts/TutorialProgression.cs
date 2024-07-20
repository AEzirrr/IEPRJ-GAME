using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgression : MonoBehaviour
{
    public bool completedTransform = false;
    public bool completedJump = false;
    public bool completedPush = false;
    public bool completedAttack = false;
    public bool completedBlock = false;


    public bool completedpuzzle1 = false;

    [SerializeField] private GameObject transformTutorialPanel;
    [SerializeField] private GameObject transform2TutorialPanel;
    [SerializeField] public GameObject jumpTutorialPanel;
    [SerializeField] private GameObject pushTutorialPanel;
    [SerializeField] private GameObject attackTutorialPanel;
    [SerializeField] private GameObject blockTutorialPanel;


    [SerializeField] AudioClip taskFinishSFX;



    private void Start()
    {
        StartCoroutine(GameTutorial());
    }

    private IEnumerator GameTutorial()
    {
        // TRANSFORM
        transformTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => completedTransform);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        transformTutorialPanel.SetActive(false);

        transform2TutorialPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        transform2TutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);


        //JUMP
        jumpTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => completedJump);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        jumpTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);


        // PUSH
        pushTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => completedPush);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        pushTutorialPanel.SetActive(false);

        yield return new WaitUntil(() => completedpuzzle1);

        //ATTACK
        attackTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => completedAttack);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        attackTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);

        //BLOCK
        blockTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => completedBlock);
        SFXManager.instance.PlaySfxClip(taskFinishSFX, transform, .02f);

        blockTutorialPanel.SetActive(false);
        yield return new WaitForSeconds(1f);


        Debug.Log("Tutorial Completed");
    }









    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            completedTransform = true;
            Debug.Log("Completed Transform");
        }


        if(Input.GetKeyDown(KeyCode.Space)) {
        
            if(completedTransform)
            {
                completedJump = true;
                Debug.Log("Completed Jump");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (completedpuzzle1)
            {
                completedAttack = true;
                Debug.Log("Completed Attack");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (completedAttack)
            {
                completedBlock = true;
                Debug.Log("Completed Attack");
            }
        }

    }
}
