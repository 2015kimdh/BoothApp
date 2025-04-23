using System;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryReceiptItem : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TMP_Text purchasedAt;
        [SerializeField] private TMP_Text purchasedItemList;
        [SerializeField] private TMP_Text purchaseCost;
        [SerializeField] private TMP_Text receiptIndex;
        
        #endregion

        #region Public Field

        /// <summary>
        /// 표시를 위한 기본적인 데이터 집합
        /// </summary>
        public PurchaseReceiptInfo receiptInfo;
        public PurchaseHistoryViewModel purchaseHistoryViewModel;
        
        #endregion

        #region Private Field

        private readonly int _maxDisplayAmount = 3;
        private int _receiptIndex = 0;
        #endregion

        #region Property

        private BoothInfo SelectedBooth => purchaseHistoryViewModel.SelectedBooth;
        public int ReceiptIndex => _receiptIndex;
        #endregion
        
        #region Method

        public void SetIndex(int index)
        {
            _receiptIndex = index;
            receiptIndex.text = "No." + index;
        }
        
        public void SetDataAndUI(PurchaseHistoryViewModel viewModel, PurchaseReceiptInfo info)
        {
            receiptInfo = info;
            DependencyInject(viewModel);
            SetUI();
        }
        
        private void DependencyInject(PurchaseHistoryViewModel viewModel)
        {
            purchaseHistoryViewModel = viewModel;
        }

        public void SetUI()
        {
            SetPurchasedAt();
            SetPurchaseItemList();
            SetPurchaseCost();
        }

        private void SetPurchasedAt()
        {
            purchasedAt.text = DateTimeUtil.DateTimeStringForPurchaseHistoryItem(receiptInfo.purchasedAt);
        }

        private void SetPurchaseItemList()
        {
            var display = receiptInfo.items.Take(_maxDisplayAmount)
                .Select(item => $"{SelectedBooth.GetPurchasedItem(item.hash).itemInfo.name} {item.amount}개")
                .ToList();

            int remain = receiptInfo.items.Count - _maxDisplayAmount;
            string result = string.Join(", ", display);
            if (remain > 0)
                result += $" 외 {remain}개 항목";
            purchasedItemList.text = result;
        }

        private void SetPurchaseCost()
        {
            int result = receiptInfo.items
                .Select(item => item.amount * item.pricePerItem)
                .Sum();
            purchaseCost.text = result.ToString();
        }

        #endregion
    }
}