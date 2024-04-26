using Assets.Scripts.Deals;
using StoryPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using utils;

public class Phone : MonoBehaviour
{
    private const string animatorID_ringing = "ringing";
    [SerializeField]
    Rigidbody2D _phoneHandRB;

    [SerializeField]
    Animator animator;


    bool _isRinging = false;

    public bool IsRinging { get => _isRinging; set { _isRinging = value; ToggleAnimation(value); } }

    private void OnMouseDown()
    {
        if (IsRinging)
        {
            AnswerPhoneCall();
        }
    }

    private void OnEnable()
    {
        StoryPointInvoker.OnStoryPointReached += StartPhonecall;
    }
    private void OnDisable()
    {
        StoryPointInvoker.OnStoryPointReached -= StartPhonecall;
    }

    private void StartPhonecall(StoryPoint storyPoint)
    {

        if (storyPoint.actionType.Contains(StoryActionType.StartPhonecall))
        {
            IsRinging = true;
        }

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
       // Utils.Invoke(this, () => IsRinging = true, 3f);
    }


    public void AnswerPhoneCall()
    {
        DialogueManager.instance.StartDialogue(DealController.instance._dealQueue.Peek().phonecallPhraseList);
        DialogueManager.instance.OnDialogueComplete += OfferDeal;
        IsRinging = false;

    }

    public void OfferDeal(PhraseList phraseList)
    {
        if (phraseList == DealController.instance._dealQueue.Peek().phonecallPhraseList)
        {
            DealController.instance.OfferDeal();
            DialogueManager.instance.OnDialogueComplete -= OfferDeal;
        }
    }


    private void ToggleAnimation(bool toggle)
    {
        animator.SetBool(animatorID_ringing, toggle);
    }


}