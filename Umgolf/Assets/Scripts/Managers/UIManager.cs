using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Scene UI Elements")]
    [SerializeField] private GameObject _levelCountText;
    [SerializeField] private GameObject _coinCountText;
    [SerializeField] private GameObject _ballCountText;
    
    
    
    public void SwitchToGame()
    {
        StartCoroutine(GameSwitch());
    }

    private IEnumerator GameSwitch()
    {
        //Wait for animation to arrive
        yield return new WaitForSeconds(0.9f);
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


