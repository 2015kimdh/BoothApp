using System;
using System.Collections.Generic;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    /// <summary>
    /// 판매 대상이 될 아이템들에 대한 정보를 저장 중인 ViewModel
    /// </summary>
    public class PurchaseSelectedItemViewModel : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onSetFailedByAmount;
        public UnityEvent onSetFailedByWrongHash;
        public UnityEvent onSetSuccess;
        public UnityEvent onPurchase;

        #endregion

        #region Serialize Field

        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private List<PurchaseItemInfo> purchaseItemInfos = new();

        #endregion

        #region Public Method

        /// <summary>
        /// Set이기 때문에 Amount로 들어오는 숫자를 더하는 것이 아니라 Amount에 들어온
        /// 숫자로 세팅하는 함수
        /// </summary>
        /// <param name="hash">아이템의 고유값</param>
        /// <param name="amount">설정하고자 하는 아이템 개수</param>
        /// <returns></returns>
        public bool SetPurchaseItemInfo(string hash, int amount)
        {
            var target = GetSelectedItemFromOriginal(hash);

            if (target == null)
            {
                onSetFailedByWrongHash.Invoke();
                return false;
            }

            if (target.amount < amount)
            {
                onSetFailedByAmount.Invoke();
                return false;
            }

            var targetInPurchase = GetSelectedPurchaseItem(hash);
            if (targetInPurchase == null)
                purchaseItemInfos.Add(MakePurchaseItemInfo(target.itemInfo, amount));
            else
                AddExistPurchaseItemInfo(target.itemInfo, amount);

            onSetSuccess.Invoke();
            return true;
        }

        public void CancelPurchaseItemInfo(string hash)
        {
            var targetInPurchase = GetSelectedPurchaseItem(hash);
            if (targetInPurchase != null)
            {
                var target = GetSelectedPurchaseItem(hash);
                purchaseItemInfos.Remove(target);
                onSetSuccess.Invoke();
            }
        }

        public void PurchaseItem()
        {
            if (purchaseItemInfos.Count != 0)
            {
                PurchaseReceiptInfo newReceipt = new()
                {
                    items = ClassCopy.CopyClass(purchaseItemInfos),
                    purchasedAt = DateTime.Now
                };
                purchaseItemInfos.Clear();
                ChangePurchaseItemStatusInfo(newReceipt);
                selectedBoothViewModel.selectedBooth.boothInformationInfo.purchasedHistory.Add(newReceipt);
                onPurchase.Invoke();
                selectedBoothViewModel.onDataChanged.Invoke();
            }
        }

        #endregion

        #region Private Method

        private BoothItemWithAmountInfo GetSelectedItemFromOriginal(string hash) =>
            selectedBoothViewModel.selectedBooth.GetOriginalItem(hash);

        private BoothItemWithAmountInfo GetSelectedItemFromPurchaseStatus(string hash) =>
            selectedBoothViewModel.selectedBooth.GetPurchasedItem(hash);

        /// <summary>
        /// 판매 할 항목에서 찾기
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private PurchaseItemInfo GetSelectedPurchaseItem(string hash) =>
            purchaseItemInfos.Find(x => x.hash == hash);

        private PurchaseItemInfo MakePurchaseItemInfo(BoothItemInfo info, int amount)
        {
            PurchaseItemInfo newPurchaseItem = new()
            {
                hash = info.hash,
                pricePerItem = info.price,
                amount = amount
            };
            return newPurchaseItem;
        }

        /// <summary>
        /// 판매 할 항목에 적용 (아직 결제 전)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="amount"></param>
        private void AddExistPurchaseItemInfo(BoothItemInfo info, int amount)
        {
            var target = GetSelectedPurchaseItem(info.hash);

            // 함수 사용 이전에 null 검사 및 개수 검사를 마치기 때문에 따로 검사하지 않음
            target.amount = amount;
        }

        /// <summary>
        /// 판매된 항목에 적용 (결제 이후)
        /// </summary>
        private void ChangePurchaseItemStatusInfo(PurchaseReceiptInfo receiptInfo)
        {
            foreach (var purchasedItem in receiptInfo.items)
            {
                var original = GetSelectedItemFromOriginal(purchasedItem.hash);
                original.amount -= purchasedItem.amount;
                var purchased = GetSelectedItemFromPurchaseStatus(purchasedItem.hash);
                if (purchased != null)
                    purchased.amount += purchasedItem.amount;
                else
                {
                    BoothItemWithAmountInfo newItem = new()
                    {
                        itemInfo = original.itemInfo,
                        amount = purchasedItem.amount
                    };
                    selectedBoothViewModel.selectedBooth.boothInformationInfo.purchasedItemStatus.Add(newItem);
                }
            }
            selectedBoothViewModel.selectedBooth.UpdateModifyTime();
        }

        #endregion
    }
}