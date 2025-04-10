using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public class RectTransformConfigure : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField, Header("크기를 변경할 대상. STRETCH 모드로만")]
        private RectTransform targetRectTransform;

        #endregion

        #region Target

        [Header("변경할 변")] 
        public bool top = false;
        public bool bottom = false;
        public bool right = false;
        public bool left = false;

        [Header("변경할 사이즈")]
        public int topChangeSize;
        public int bottomChangeSize;
        public int rightChangeSize;
        public int leftChangeSize;

        #endregion

        #region Method

        public void SetRectSize()
        {
            if (top)
                targetRectTransform.offsetMax = new Vector2(targetRectTransform.anchorMax.x, -topChangeSize);
            if (bottom)
                targetRectTransform.offsetMin = new Vector2(targetRectTransform.anchorMax.x, bottomChangeSize);
            if (right)
                targetRectTransform.offsetMax = new Vector2(-rightChangeSize, targetRectTransform.anchorMax.y);
            if (left)
                targetRectTransform.offsetMin = new Vector2(leftChangeSize, targetRectTransform.anchorMax.y);
        }

        public void SetSizeToZero()
        {
            targetRectTransform.offsetMax = Vector2.zero;
            targetRectTransform.offsetMin = Vector2.zero;
        }
        
        #endregion
    }
}