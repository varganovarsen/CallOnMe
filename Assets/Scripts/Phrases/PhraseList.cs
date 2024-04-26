using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New phrase list",  menuName = "New phrase list")]
public class PhraseList : ScriptableObject
{
    public List<Phrase> list;
    public DialogueCallbackType dialogueCallback;
    public Action Callback;

    private void OnValidate()
    {
        if (dialogueCallback == DialogueCallbackType.none)
        {
            Callback = null;
        }
        else
        {
            Callback = () =>
            {
                DealController.instance.AllowReturn();
            };
        }
    }
}

public enum DialogueCallbackType
{
    none,
    AllowReturn
}
