using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void SwitchToGame()
    {
        StartCoroutine(GameSwitch());
    }

    private IEnumerator GameSwitch()
    {
        yield return SceneLoadManager.Instance.LoadScene("GameScene");
        SceneLoadManager.Instance.SwitchToScene("GameScene", 1);
    }
    
    public void StartTransitionAnimation(Animator animator)
    {
        animator.SetTrigger("FadeIn");
        
    }

    public void EndTransitionAnimation(Animator animator)
    {
        animator.SetTrigger("FadeOut");
    }
}   


