using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryView : MonoBehaviour
    {
        public UnityEvent onViewShow;

        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
    }
}