using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject phrasePnaelPrefab;

    private void Start()
    {
        Invoke(nameof(SendTestMessage), 2f);
    }

    // Debug
    [ContextMenu("Send test message")]
    void SendTestMessage()
    {
        GameObject _phrasePanel = Instantiate(phrasePnaelPrefab);
        _phrasePanel.transform.SetParent(transform, false);
        _phrasePanel.GetComponent<PhrasePanelController>().Initialize("This is a test phrase");
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
