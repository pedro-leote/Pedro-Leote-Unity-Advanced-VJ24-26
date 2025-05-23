using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInteractable
{
    public UnityEvent OnThisCoinBeingCollectedEvent = new UnityEvent();

    public void Interact()
    {
        OnThisCoinBeingCollectedEvent?.Invoke();
    }
}
