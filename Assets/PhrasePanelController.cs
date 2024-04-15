using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField, Space]
    Vector3 hidePosition;
    [SerializeField]
    Vector3 showPosition;

    public void Initialize(string text)
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Text.text = text;
        Animate();
    }

        public void Animate()
        {
            LeanTween.moveY(gameObject, showPosition.y, showTime).setEase(showCurve).setDelay(0.1f);
            LeanTween.moveY(gameObject, hidePosition.y, hideTime).setEase(hideCurve).setDelay(stayTime)
            .setOnComplete(() => Destroy(gameObject, 0.2f));
        }
      

}


