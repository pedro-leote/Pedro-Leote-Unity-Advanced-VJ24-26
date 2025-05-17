using UnityEngine;
using UnityEngine.Events;

public class HoleHandling : MonoSingleton<HoleHandling>
{
    public UnityEvent OnBallEnterHoleEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Golf Ball"))
        {
            OnBallEnterHoleEvent.Invoke();
        }
    }
}
