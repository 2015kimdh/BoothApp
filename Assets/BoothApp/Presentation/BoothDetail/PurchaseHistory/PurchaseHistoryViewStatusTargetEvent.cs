using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryViewStatusTargetEvent : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseHistoryViewStatus targetStatus;
        [SerializeField] private PurchaseHistoryView view;

        #endregion

        #region Event

        public UnityEvent onStatusEnter;
        public UnityEvent onStatusExit;

        #endregion

        private void Awake()
        {
            view.onViewStatusChange.AddListener(ListenStatus);
        }

        private void ListenStatus(PurchaseHistoryViewStatus status)
        {
            if (status == targetStatus)
                onStatusEnter.Invoke();
            else
                onStatusExit.Invoke();
        }
    }
}