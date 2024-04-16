using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New phrase", menuName = "Create new phrase")]
public class Phrase : ScriptableObject
{
    public string phraseText;
    public float holdTime = 1.75f;
    public float waitAfterTime = 0;

    
}
