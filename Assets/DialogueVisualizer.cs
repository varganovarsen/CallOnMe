using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject phrasePnaelPrefab;

    public static DialogueVisualizer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Invoke(nameof(SendTestMessage), 2f);
    }

    public PhrasePanelController DrawPhrase(Phrase phraseToDraw)
    {
        GameObject _phrasePanel = Instantiate(phrasePnaelPrefab);
        _phrasePanel.transform.SetParent(transform, false);
        _phrasePanel.GetComponent<PhrasePanelController>().Initialize(phraseToDraw);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        return _phrasePanel.GetComponent<PhrasePanelController>();

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
