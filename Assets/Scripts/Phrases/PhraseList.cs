using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New phrase list",  menuName = "New phrase list")]
public class PhraseList : ScriptableObject
{
    public List<Phrase> list;
}
