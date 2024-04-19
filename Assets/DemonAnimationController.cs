using Assets.Scripts.Deals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimationController : MonoBehaviour
{

    [SerializeField]
    float teleportationTime;
    public static float TeleportationTime;
    [SerializeField]
    Vector3 teleportToPoint;
    [SerializeField]
    Vector3 teleportBackToPoint;
    [SerializeField]
    LeanTweenType _teleportationEase;

    SpriteMask _teleportationMask;
    Vector3 _teleportationMaskScale;

    private void Awake()
    {
        _teleportationMask = GetComponentInChildren<SpriteMask>(true);
        _teleportationMask.enabled = false;
        _teleportationMaskScale = _teleportationMask.transform.localScale;
        _teleportationMask.transform.localScale = Vector3.zero;
        CameraController.TransitionDelay = teleportationTime;

    }

    private void OnValidate()
    {
        TeleportationTime = teleportationTime;
    }

    private void Start()
    {
        DealController.instance.OnAcceptDeal += Teleport;
        DealController.instance.OnCompleteDeal += Teleport;
    }

    private void OnDisable()
    {
        DealController.instance.OnAcceptDeal -= Teleport;
        DealController.instance.OnCompleteDeal -= Teleport;
    }

    public void Teleport(Deal deal)
    {
        _teleportationMask.enabled = true;

        LeanTween.scale(_teleportationMask.gameObject, _teleportationMaskScale, teleportationTime).setEase(_teleportationEase)
            .setOnComplete(() => transform.position = DealController.IsOnDeal ? teleportBackToPoint : teleportToPoint);

        LeanTween.scale(_teleportationMask.gameObject, Vector3.zero, teleportationTime).setEase(_teleportationEase)
            .setDelay(CameraController.TransitionDelay + CameraController.BlendTime)
            .setOnComplete(() => _teleportationMask.enabled = false);
        

    }
}
