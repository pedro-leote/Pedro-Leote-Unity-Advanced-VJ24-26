using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void SwitchToGame()
    {
        SceneLoadManager.Instance.SwitchToScene("GameScene", 3);
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


