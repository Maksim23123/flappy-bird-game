using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLayout : MonoBehaviour, ILayout
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LevelManager levelManager;
    private string parameterName = "gameover";
    

    public void RestartButtonAction()
    {
        StartCoroutine(QiteStateDelay());
    } 

    IEnumerator PlayAnimationDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool(parameterName, true);
    }

    IEnumerator QiteStateDelay()
    {
        animator.SetBool(parameterName, false);
        yield return new WaitForSecondsRealtime(0.3f);
        levelManager.RestartGame();
    }

    public void OnWakeUp()
    {
        StartCoroutine(PlayAnimationDelay());
    }
}
