using Assets.Scripts.Deals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealVisualizer : MonoBehaviour
{
    [SerializeField]
    Button acceptDealButton;

    private Deal _currentDeal;
    public  Deal CurrentDeal => _currentDeal;

    AcceptDealButtonAnimator _acceptDealButtonAnimator;

    private void OnEnable()
    {
        DealController.instance.OnOfferDeal += ShowAcceptDealButton;
        acceptDealButton.onClick.AddListener(DealController.instance.AcceptDeal);
        acceptDealButton.onClick.AddListener(AcceptDeal);
    }

    private void Start()
    {
        _acceptDealButtonAnimator = new AcceptDealButtonAnimator(acceptDealButton);
    }

    void ShowAcceptDealButton(Deal deal)
    {
        _currentDeal = deal;

        _acceptDealButtonAnimator.ShowButton();
    }

    void AcceptDeal()
    {
        _acceptDealButtonAnimator.AcceptDeal();
    }


    private void OnDisable()
    {
        DealController.instance.OnOfferDeal -= ShowAcceptDealButton;
        acceptDealButton.onClick.AddListener(DealController.instance.AcceptDeal);
    }


}

public class AcceptDealButtonAnimator
{
    GameObject _animationObject;
    Button _acceptDealButton;

    public void ShowButton()
    {
        LeanTween.moveLocalY(_animationObject, -20f, 1f).setEaseOutElastic().setOnComplete(() => _acceptDealButton.interactable = true);
    }

    public void AcceptDeal()
    {
        _acceptDealButton.interactable = false;
        LeanTween.scale(_animationObject, Vector2.one * 2f, 0.3f).setEasePunch();
        LeanTween.moveLocalY(_animationObject, 40f, 1f).setEaseInBounce().setDelay(0.4f);
    }

    public void HideButton()
    {
        LeanTween.moveLocalY(_animationObject, 80f, 3f).setEaseOutQuart().setOnComplete(() => _acceptDealButton.interactable = false);
    }

    public AcceptDealButtonAnimator(Button acceptDealButton)
    {
        _animationObject = acceptDealButton.gameObject;
        _acceptDealButton = acceptDealButton;
    }
}

