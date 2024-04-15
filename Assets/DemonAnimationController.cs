using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimationController : MonoBehaviour
{

    [SerializeField]
    float teleportationTime;
    public static float TeleportationTime;
    [SerializeField]
    Transform teleportToPoint;
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
        LevelLoader.Instance.OnUpworldLoaded += Teleport;
    }

    public void Teleport()
    {
        _teleportationMask.enabled = true;

        LeanTween.scale(_teleportationMask.gameObject, _teleportationMaskScale, teleportationTime).setEase(_teleportationEase)
            .setOnComplete(() => transform.position = teleportToPoint.position);

        LeanTween.scale(_teleportationMask.gameObject, Vector3.zero, teleportationTime).setEase(_teleportationEase)
            .setDelay(Mathf.Clamp(CameraController.BlendTime + CameraController.TransitionDelay, 0f, Mathf.Infinity))
            .setOnComplete(() => _teleportationMask.enabled = false);
        

    }
}
