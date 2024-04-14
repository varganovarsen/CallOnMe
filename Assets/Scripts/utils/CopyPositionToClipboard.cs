using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.utils
{
    /// <summary>
    /// Found solution on Unity Forum (https://discussions.unity.com/t/how-to-get-mouse-click-world-position-in-the-scene-view-in-editor-script/175451)
    /// Thanks FlightOfOne!
    /// </summary>
 #if UNITY_EDITOR

    [ExecuteInEditMode]
    public class CopyPositionToClipboard : MonoBehaviour
    {
        [System.Obsolete]
        private void OnEnable()
        {
            if (!Application.isEditor)
            {
                Destroy(this);
            }
            SceneView.onSceneGUIDelegate += OnScene;
        }

        [System.Obsolete]
        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnScene;
        }

        void OnScene(SceneView scene)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 2)
            {
                //Debug.Log("Middle Mouse was pressed");

                Vector3 mousePos = e.mousePosition;
                float ppp = EditorGUIUtility.pixelsPerPoint;
                mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
                mousePos.x *= ppp;

                Vector3 mouseWorldPosition = scene.camera.ScreenToWorldPoint(mousePos);
                mouseWorldPosition.z = 0;
               CopyToClipboard(mouseWorldPosition);

                e.Use();
            }
        }

        private static void CopyToClipboard(Vector3 positionToCopy)
        {
                TextEditor textEditor = new TextEditor();
            textEditor.text = $"Vector3{positionToCopy}";
                textEditor.SelectAll();
                textEditor.Copy();
            
        }
    }

#endif
}