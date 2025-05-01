using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation
{
    public class LayoutGroupForcedRebuild : MonoBehaviour
    {
        [SerializeField] private RectTransform layoutGroup;

        public void ForceRebuildLayout()
        {
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(ForceRebuild());
        }

        private IEnumerator ForceRebuild()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
        }
    }
}