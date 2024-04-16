using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class PhrasePanelController : MonoBehaviour
{
    TMP_Text m_Text;

    [Header("Animation")]
    [SerializeField]
    float stayTime;
    [SerializeField, Space]
    float showTime;
    [SerializeField]
    LeanTweenType showCurve;
    [SerializeField, Space]
    float hideTime;
    [SerializeField]
    LeanTweenType hideCurve;
    [SerializeField]
    float delayAfterPhrase;

    [SerializeField, Space]
    Vector3 hidePosition;
    [SerializeField]
    Vector3 showPosition;

    public event Action OnPhraseComplete;



    public void Initialize(string text)
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Text.text = text;
        Animate();
    }
    private void OnEnable()
    {
        OnPhraseComplete += SelfDestroy;
    }

    private void OnDisable()
    {
        OnPhraseComplete -= SelfDestroy;
    }

    void SelfDestroy() => Destroy(gameObject);

    public void Initialize(Phrase phraseToDraw)
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Text.text = phraseToDraw.phraseText;
        stayTime = phraseToDraw.holdTime;
        delayAfterPhrase = phraseToDraw.waitAfterTime;

        Animate();
    }

    public void Animate()
        {
            LeanTween.moveY(gameObject, showPosition.y, showTime).setEase(showCurve).setDelay(0.1f);
        LeanTween.moveY(gameObject, hidePosition.y, hideTime).setEase(hideCurve).setDelay(stayTime);

        Utils.Invoke(this, () => OnPhraseComplete.Invoke(), showTime + stayTime + hideTime + delayAfterPhrase);
            
        }


      

}


