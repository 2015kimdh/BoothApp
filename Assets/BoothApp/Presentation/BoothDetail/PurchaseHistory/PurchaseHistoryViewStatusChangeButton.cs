using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryViewStatusChangeButton : MonoBehaviour
    {
        [SerializeField] private PurchaseHistoryViewStatus targetStatus;
        [SerializeField] private UIButton button;
        private PurchaseHistoryView _view;

        private void Start()
        {
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(PurchaseHistoryView)) as PurchaseHistoryView;
            button.onClickEvent.AddListener(SetStatus);
        }

        private void SetStatus()
        {
            if (targetStatus == _view.ViewStatus)
                _view.ViewStatus = PurchaseHistoryViewStatus.Normal;
            else
                _view.ViewStatus = targetStatus;
        }
    }
}