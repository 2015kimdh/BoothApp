using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.Filter
{
    public class ItemTagFilterMarker : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private GameObject markerObject;
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;

        #endregion

        #region Property

        private PurchaseHistoryFilterAttributeInfo FilterAttribute =>
            selectedBoothViewModel.selectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;

        #endregion

        #region Method

        /// <summary>
        /// 필터링이 설정되어있을 경우 마커를 표시하는 방식
        /// </summary>
        public void SetMarker()
        {
            // 둘 다 비어있다면 SetActive False;
            // 이외에는 True
            markerObject.SetActive(!(FilterAttribute.selectedOwner.Count == 0 && FilterAttribute.selectedItemTags.Count == 0));
        }

        #endregion
    }
}