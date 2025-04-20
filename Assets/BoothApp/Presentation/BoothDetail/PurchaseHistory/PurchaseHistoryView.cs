using BoothApp.Presentation.View;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryView : ViewBase
    {
        public UnityEvent onViewShow;
        public UnityEvent<PurchaseHistoryViewStatus> onViewStatusChange;
        
        #region Property

        public PurchaseHistoryViewStatus ViewStatus
        {
            get => _viewStatus;
            set
            {
                if (_viewStatus != value)
                {
                    _viewStatus = value;
                    onViewStatusChange.Invoke(_viewStatus);
                }
            }
        }

        #endregion

        #region Private Fields

        private PurchaseHistoryViewStatus _viewStatus;

        #endregion

        private void Start()
        {
            _viewStatus = PurchaseHistoryViewStatus.Normal;
        }
        
        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
    }
}