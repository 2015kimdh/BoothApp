using BoothApp.Presentation.LoopScroll.Implement;
using BoothApp.Presentation.View;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseReceiptSelectButton : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private UIButton button;
        [SerializeField] private PurchaseHistoryReceiptItemLoopScroll itemLoopScroll;
        [SerializeField] private PurchaseHistoryReceiptItem item;

        #endregion

        #region Private Fields

        private readonly PurchaseHistoryViewStatus _targetStatus = PurchaseHistoryViewStatus.Normal;

        private PurchaseHistoryViewModel _viewModel;
        private PurchaseHistoryView _view;

        #endregion

        #region Method

        private void Awake()
        {
            _viewModel = FindObjectOfType<PurchaseHistoryViewModel>();
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(PurchaseHistoryView)) as PurchaseHistoryView;
            button.onClickEvent.AddListener(ToDetailView);
        }

        private void ToDetailView()
        {
            if (_view.ViewStatus == _targetStatus)
            {
                _viewModel.SetSelectedItem(Mapping());
                SignalStream stream = SignalsService.GetStream(nameof(UISelectable), nameof(UIButton));
                stream.SendSignal(new UIButtonSignalData("ViewChangeButton", "ToPurchaseHistoryDetail", ButtonTrigger.Click));
            }
        }

        private PurchaseHistoryReceiptItem Mapping()
        {
            item.receiptInfo = itemLoopScroll.receiptData.receiptInfo;
            item.purchaseHistoryViewModel = _viewModel;
            return item;
        }
        
        #endregion
    }
}