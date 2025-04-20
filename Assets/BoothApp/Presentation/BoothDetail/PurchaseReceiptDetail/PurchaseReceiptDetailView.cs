using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class PurchaseReceiptDetailView : MonoBehaviour
    {
        #region UnityEvent

        public UnityEvent onViewShow;

        #endregion
        
        #region Serialize Field

        [SerializeField] private PurchaseHistoryViewModel viewModel;

        #endregion

        #region Method

        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
        
        #endregion
    }
}