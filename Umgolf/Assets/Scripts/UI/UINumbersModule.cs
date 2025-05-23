using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


//Fiquei desapontado que os Animator events não fossem bem Events, por isso uso isto para mudar variables usando esses events.
public class UINumbersModule : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private TMP_Text _text;
    
    public void SetIntValue(int value)
    {
        _value = value;
        _text.text = _value.ToString();
    }

    public void IncrementValue()
    {
        ++_value;
        _text.text = _value.ToString();
    }
}
