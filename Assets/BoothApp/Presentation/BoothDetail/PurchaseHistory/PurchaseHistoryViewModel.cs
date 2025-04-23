using System;
using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryViewModel : MonoBehaviour
    {
        #region
        
        [Header("기록 삭제 이후 호출")]
        public UnityEvent onDelete;
        
        #endregion
        
        #region Serialize Field

        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;

        [SerializeField] private BoothDataPresenter presenter;
        #endregion

        #region Property

        public BoothInfo SelectedBooth => selectedBoothViewModel.selectedBooth;
        public SelectedBoothViewModel SelectedBoothViewModel => selectedBoothViewModel;
        public List<PurchaseReceiptInfo> PurchaseHistory => SelectedBooth.boothInformationInfo.purchasedHistory;

        public PurchaseHistoryReceiptItem SelectedItem
        {
            get => _selectedItem;
        }
        
        #endregion

        #region Private Fields

        private PurchaseHistoryReceiptItem _selectedItem;

        #endregion
        
        #region Method

        /// <summary>
        /// 결제 시간으로 찾는 함수
        /// </summary>
        public void DeletePurchaseHistory(DateTime purchasedAt)
        {
            var target = PurchaseHistory.Find(x => x.purchasedAt == purchasedAt);
            PurchaseHistory.Remove(target);
            RemoveItemAmountFromPurchasedItem(target);
            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
            onDelete.Invoke();
        }
        
        /// <summary>
        /// 결제 결과 대상으로 찾는 함수
        /// </summary>
        public void DeletePurchaseHistory(PurchaseReceiptInfo targetReceipt)
        {
            PurchaseHistory.Remove(targetReceipt);
            RemoveItemAmountFromPurchasedItem(targetReceipt);
            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
            onDelete.Invoke();
        }
        
        /// <summary>
        /// 결제 결과 대상으로 찾는 함수
        /// </summary>
        public void DeletePurchaseHistory(List<PurchaseReceiptInfo> targetReceipts)
        {
            var remain = PurchaseHistory.Except(targetReceipts).ToList();
            SelectedBooth.boothInformationInfo.purchasedHistory = remain;
            foreach (var target in targetReceipts)
                RemoveItemAmountFromPurchasedItem(target);
            
            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
            onDelete.Invoke();
        }

        public void SetSelectedItem(PurchaseHistoryReceiptItem item)
        {
            _selectedItem = item;
        }
        
        #endregion

        #region Private Method

        private void RemoveItemAmountFromPurchasedItem(PurchaseReceiptInfo receiptInfo)
        {
            foreach (var item in receiptInfo.items)
            {
                var purchasedItem = SelectedBooth.GetPurchasedItem(item.hash);
                purchasedItem.amount -= item.amount;
                if (purchasedItem.amount <= 0)
                    SelectedBooth.boothInformationInfo.purchasedItemStatus.Remove(purchasedItem);
            }
        }

        #endregion
    }
}