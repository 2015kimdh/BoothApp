using System;
using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using BoothApp.Utility;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class ReceiptDetailViewDateTimeSetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text dateTimeText;
        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private PurchaseReceiptDetailView view;

        private void Awake()
        {
            view.onViewShow.AddListener(SetUI);
        }

        private void SetUI()
        {
            dateTimeText.text =
                DateTimeUtil.DateTimeStringForPurchaseHistoryItem(viewModel.SelectedItem.receiptInfo.purchasedAt);
        }
    }
}