using System.Collections;
using UnityEngine;
using DevLocker.Utils;

namespace Assets.Scripts.Deals
{
    [CreateAssetMenu(fileName ="New deal", menuName ="Create new deal")]
    public class Deal : ScriptableObject
    {
        [SerializeField]
        public SceneReference sceneReference;

        [SerializeField]
        public int manaCount;

        [SerializeField]
        public int enemyCount;


    }
}