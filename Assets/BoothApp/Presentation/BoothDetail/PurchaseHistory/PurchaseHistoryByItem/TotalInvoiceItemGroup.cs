using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceItemGroup : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryByItemViewModel purchaseHistoryByItemViewModel;
        [SerializeField] private PurchaseHistoryViewModel purchaseHistoryViewModel;
        [SerializeField] private TotalInvoiceItemMaker maker;
        [SerializeField] private LayoutGroupForcedRebuild reBuilder;

        #endregion

        #region Public Fields

        public List<TotalInvoiceItem> items = new();

        #endregion

        #region Unity Event

        public UnityEvent onRefresh;

        #endregion

        #region Property

        private BoothInfo SelectedBooth => purchaseHistoryByItemViewModel.SelectedBooth;

        private List<BoothItemWithAmountInfo> PurchasedItemStatus =>
            SelectedBooth.boothInformationInfo.purchasedItemStatus;

        #endregion

        #region Method

        private void Awake()
        {
            purchaseHistoryViewModel.onDelete.AddListener(Refresh);
        }

        public void Refresh()
        {
            RefreshItemList();
            SetItemList();
            reBuilder.ForceRebuildLayout();
            onRefresh.Invoke();
        }

        private void RefreshItemList()
        {
            var filteredItemResult = FilteringItem.FilteringWithTag(PurchasedItemStatus,
                purchaseHistoryByItemViewModel.FilterAttribute.selectedItemTags);
            filteredItemResult = FilteringItem.FilteringWithOwner(filteredItemResult,
                purchaseHistoryByItemViewModel.FilterAttribute.selectedOwner);
            var filteredReceipt = purchaseHistoryByItemViewModel.FilteredReceipt;

            filteredItemResult = filteredItemResult
                .Where(x => purchaseHistoryByItemViewModel.GetTotalAmount(filteredReceipt, x.itemInfo) != 0)
                .ToList();

            var gap = items.Count - filteredItemResult.Count;
            if (gap > 0)
            {
                for (int i = 0; i < gap; i++)
                {
                    maker.pool.Release(items[0]);
                    items.Remove(items[0]);
                }
            }
            else
                for (int i = gap; i < 0; i++)
                    items.Add(maker.pool.Get());
        }

        private void SetItemList()
        {
            var filteredItemResult = FilteringItem.FilteringWithTag(PurchasedItemStatus,
                purchaseHistoryByItemViewModel.FilterAttribute.selectedItemTags);
            filteredItemResult = FilteringItem.FilteringWithOwner(filteredItemResult,
                purchaseHistoryByItemViewModel.FilterAttribute.selectedOwner);
            var filteredReceipt = purchaseHistoryByItemViewModel.FilteredReceipt;

            filteredItemResult = filteredItemResult
                .Where(x => purchaseHistoryByItemViewModel.GetTotalAmount(filteredReceipt, x.itemInfo) != 0)
                .ToList();
            
            for (int i = 0; i < items.Count; i++)
            {
                int totalInvoice = purchaseHistoryByItemViewModel.GetTotalInvoice(purchaseHistoryByItemViewModel.FilteredReceipt, filteredItemResult[i].itemInfo);
                int totalAmount = purchaseHistoryByItemViewModel.GetTotalAmount(purchaseHistoryByItemViewModel.FilteredReceipt, filteredItemResult[i].itemInfo);
                items[i].SetUI(filteredItemResult[i].itemInfo, totalInvoice, totalAmount);
            }
        }

        #endregion
    }
}