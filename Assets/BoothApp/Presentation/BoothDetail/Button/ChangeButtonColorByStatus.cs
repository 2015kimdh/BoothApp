using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.Button
{
    public class ChangeButtonColorByStatus : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Image image;
        [SerializeField] private Color toColor;
        
        #endregion

        private Color _originalColor;
        
        #region Method

        private void Awake()
        {
            _originalColor = image.color;
        }

        public void SetColor()
        {
            image.color = toColor;
        }

        public void ReturnColor()
        {
            image.color = _originalColor;
        }

        #endregion
    }
}