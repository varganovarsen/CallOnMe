using StoryPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private PhraseList _currentPhraseList = new PhraseList();

    bool _isInDialogue = false;

    bool _isDrawingPhrase;

    

    ///TODO: Add waitlist

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
           // Destroy(gameObject);
        }

        _currentPhraseList.list = new List<Phrase>();
    }

    private void OnEnable()
    {
        StoryPointInvoker.OnStoryPointReached += StartDialogue;
    }
    private void OnDisable()
    {
        StoryPointInvoker.OnStoryPointReached -= StartDialogue;
    }

    public void StartDialogue(StoryPoint storyPoint)
    {
        if (storyPoint.phrases.list.Count <= 0)
            return;

        _currentPhraseList.list = _currentPhraseList.list.Concat(storyPoint.phrases.list).ToList();
       //storyPoint.phrases.list.CopyTo(_currentPhraseList.list.ToArray(), storyPoint.phrases.list.Count - 1);
        
        if (!_isInDialogue )
        {
            StartCoroutine(nameof(Dialogue));
        }

    }

    IEnumerator Dialogue()
    {
        while (_currentPhraseList.list.Count > 0) 
        {
            if (!_isDrawingPhrase)
            {
                _isDrawingPhrase = true;
                PhrasePanelController phrasePanel =  DialogueVisualizer.instance.DrawPhrase(_currentPhraseList.list[0]);
                phrasePanel.OnPhraseComplete += NextPhrase;
                yield return new WaitUntil(() => !_isDrawingPhrase);
                phrasePanel.OnPhraseComplete -= NextPhrase;
                yield return new WaitForSeconds(0.5f);
            }
            
        }

        _isInDialogue = false;
    }

    public void NextPhrase()
    {
        _currentPhraseList.list.RemoveAt(0);
        _isDrawingPhrase = false;
    }
}
