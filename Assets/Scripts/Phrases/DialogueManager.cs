using StoryPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private Queue<PhraseList> _dialoguesWaitlist;

    bool _isInDialogue = false;

    bool _isDrawingPhrase;

    public event Action<PhraseList> OnDialogueComplete;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Destroy(gameObject);
        }

        _dialoguesWaitlist = new Queue<PhraseList>();

    }

    //private void OnEnable()
    //{
    //    StoryPointInvoker.OnStoryPointReached += StartDialogue;
    //}
    //private void OnDisable()
    //{
    //    StoryPointInvoker.OnStoryPointReached -= StartDialogue;
    //}

    //public void StartDialogue(StoryPoint storyPoint)
    //{
    //    if (storyPoint.phrases.list.Count <= 0)
    //        return;

    //    _dialoguesWaitlist.Enqueue(storyPoint.phrases);
    //   //storyPoint.phrases.list.CopyTo(_currentPhraseList.list.ToArray(), storyPoint.phrases.list.Count - 1);

    //    if (!_isInDialogue )
    //    {
    //        StartCoroutine(nameof(Dialogue), _dialoguesWaitlist.Peek());
    //    }

    //}

    public void StartDialogue(PhraseList phraseList)
    {
        if (phraseList.list.Count <= 0)
            return;

        _dialoguesWaitlist.Enqueue(phraseList);
        //storyPoint.phrases.list.CopyTo(_currentPhraseList.list.ToArray(), storyPoint.phrases.list.Count - 1);

        if (!_isInDialogue)
        {
            StartCoroutine(nameof(Dialogue), _dialoguesWaitlist.Peek());
        }

    }

    IEnumerator Dialogue(PhraseList phraseList)
    {

        if (!_isDrawingPhrase)
        {
            foreach (Phrase phrase in phraseList.list)
            {
                _isDrawingPhrase = true;
                PhrasePanelController phrasePanel = DialogueVisualizer.instance.DrawPhrase(phrase);
                phrasePanel.OnPhraseComplete += () => _isDrawingPhrase = false;
                yield return new WaitUntil(() => !_isDrawingPhrase);
                phrasePanel.OnPhraseComplete -= () => _isDrawingPhrase = false;
                yield return new WaitForSeconds(0.5f);
            }

        }

        PhraseList _ = _dialoguesWaitlist.Dequeue();
        OnDialogueComplete?.Invoke(_);

        if (_dialoguesWaitlist.Count > 0)
            StartCoroutine(nameof(Dialogue), _dialoguesWaitlist.Peek());
        else
            _isInDialogue = false;
    }

}
