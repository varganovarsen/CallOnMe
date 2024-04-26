using Assets.Scripts.Deals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class DealVisualizer : MonoBehaviour
{
    [SerializeField]
    Button acceptDealButton;
    [SerializeField]
    Button returnButton;

    private Deal _currentDeal;
    public  Deal CurrentDeal => _currentDeal;

    ButtonAnimator _acceptDealButtonAnimator;
    ButtonAnimator _returnButtonAnimator;


    private void OnEnable()
    {
        DealController.instance.OnOfferDeal += ShowAcceptDealButton;
        DealController.instance.OnAllowReturn += ShowReturnButton;

        _acceptDealButtonAnimator = new ButtonAnimator(acceptDealButton, -20f, 80f);
        acceptDealButton.onClick.AddListener(DealController.instance.AcceptDeal);
        acceptDealButton.onClick.AddListener(_acceptDealButtonAnimator.ClickButton);

        _returnButtonAnimator = new ButtonAnimator(returnButton, -480f, -590f);
        returnButton.onClick.AddListener(DealController.instance.ReturnFromDeal);
        returnButton.onClick.AddListener(_returnButtonAnimator.ClickButton);


        
    }

    private void OnDisable()
    {
        DealController.instance.OnOfferDeal -= ShowAcceptDealButton;
        acceptDealButton.onClick.RemoveAllListeners();

        DealController.instance.OnAllowReturn -= ShowReturnButton;
        returnButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        
    }

    void ShowAcceptDealButton(Deal deal)
    {
        _currentDeal = deal;

        _acceptDealButtonAnimator.ShowButton();
    }

    [ContextMenu("Show return button")]
    void ShowReturnButton()
    {
        _currentDeal = null;

        _returnButtonAnimator.ShowButton();
    }

    void AcceptDeal()
    {
        _acceptDealButtonAnimator.ClickButton();
    }


   


}

public class ButtonAnimator
{
    GameObject _animationObject;
    Button _button;

    public float _showedY;
    public float _hiddenY;

    public void ShowButton()
    {
        LeanTween.moveLocalY(_animationObject, _showedY, 1f).setEaseOutElastic().setOnComplete(() => _button.interactable = true);
    }

    public void ClickButton()
    {
        _button.interactable = false;
        LeanTween.scale(_animationObject, Vector2.one * 2f, 0.3f).setEasePunch().setOnComplete(() => HideButton());
        //LeanTween.moveLocalY(_animationObject, 40f, 1f).setEaseInBounce().setDelay(0.4f);
    }

    public void HideButton()
    {
        LeanTween.moveLocalY(_animationObject, _hiddenY, 3f).setEaseOutQuart().setOnComplete(() => _button.interactable = false);
    }

    public ButtonAnimator(Button button, float showedY ,float hiddenY)
    {
        _animationObject = button.gameObject;
        _button = button;
        _showedY = showedY;
        _hiddenY = hiddenY;
    }
}

