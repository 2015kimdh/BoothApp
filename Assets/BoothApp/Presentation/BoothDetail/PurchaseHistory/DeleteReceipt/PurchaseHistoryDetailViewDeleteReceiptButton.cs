using System;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.DeleteReceipt
{
    public class PurchaseHistoryDetailViewDeleteReceiptButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private PurchaseHistoryViewModel viewModel;

        private void Awake()
        {
            button.onClickEvent.AddListener(DeleteReceipt);
            button.onClickEvent.AddListener(SendBackButtonSignal);
        }

        private void DeleteReceipt()
        {
            viewModel.DeletePurchaseHistory(viewModel.SelectedItem.receiptInfo);
        }

        private void SendBackButtonSignal()
        {
            SignalStream stream = SignalsService.GetStream(nameof(UISelectable), nameof(UIButton));
            stream.SendSignal(new UIButtonSignalData("BasicButton", "Back", ButtonTrigger.Click));
        }
    }
}