using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation
{
    public class BoothColumn : MonoBehaviour
    {
        #region Public Fields

        public TMP_Text boothName;
        public TMP_Text createdAt;
        public TMP_Text updatedAt;

        public RawImage image;
        
        public UIButton button;

        #endregion
    }
}