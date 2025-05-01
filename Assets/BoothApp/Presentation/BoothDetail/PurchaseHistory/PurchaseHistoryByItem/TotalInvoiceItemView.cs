using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceItemView : MonoBehaviour
    {
        public UnityEvent onViewShow;

        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
    }
}