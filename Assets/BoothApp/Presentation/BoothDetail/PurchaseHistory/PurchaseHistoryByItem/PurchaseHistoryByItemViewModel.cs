using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class PurchaseHistoryByItemViewModel : MonoBehaviour
    {
        #region Serialize Field

        /// <summary>
        /// 판매된 물품들을 가져오기 위해
        /// </summary>
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;

        #endregion

        #region Property

        public BoothInfo SelectedBooth => selectedBoothViewModel.selectedBooth;
        public SelectedBoothViewModel SelectedBoothViewModel => selectedBoothViewModel;
        public List<PurchaseReceiptInfo> PurchaseHistory => SelectedBooth.boothInformationInfo.purchasedHistory;

        public PurchaseHistoryFilterAttributeInfo FilterAttribute =>
            SelectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;
        public List<BoothItemWithAmountInfo> PurchasedItemStatus =>
            SelectedBooth.boothInformationInfo.purchasedItemStatus;
        public List<PurchaseReceiptInfo> FilteredReceipt
        {
            get
            {
                var purchasedItems = PurchasedItemStatus;

                var filteredReceipt = FilteringItem.FilteringReceiptByDate(
                    PurchaseHistory,
                    FilterAttribute.limit1,
                    FilterAttribute.limit2);

                filteredReceipt = FilteringItem.FilteringReceiptByOwners(purchasedItems, filteredReceipt,
                    FilterAttribute.selectedOwner);

                filteredReceipt = FilteringItem.FilteringReceiptByItemTags(purchasedItems, filteredReceipt,
                    FilterAttribute.selectedItemTags);
                return filteredReceipt;
            }
        }
        
        public TotalInvoiceItem SelectedItem
        {
            get => _selectedItem;
        }

        #endregion

        #region Private Fields

        private TotalInvoiceItem _selectedItem;

        #endregion

        #region Public Method

        public void SetSelectedItem(TotalInvoiceItem item)
        {
            _selectedItem = item;
        }

        public int GetTotalInvoice(BoothItemInfo item)
        {
            var filteredList = PurchaseHistory
                .SelectMany(x => x.items)
                .Where(x => x.hash == item.hash)
                .ToList();
            return filteredList.Select(x => x.pricePerItem * x.amount).Sum();
        }
        
        public int GetTotalInvoice(List<PurchaseReceiptInfo> original, BoothItemInfo item)
        {
            var filteredList = original
                .SelectMany(x => x.items)
                .Where(x => x.hash == item.hash)
                .ToList();
            return filteredList.Select(x => x.pricePerItem * x.amount).Sum();
        }

        public int GetTotalAmount(BoothItemInfo item)
        {
            var filteredList = PurchaseHistory
                .SelectMany(x => x.items)
                .Where(x => x.hash == item.hash)
                .ToList();
            return filteredList.Select(x => x.amount).Sum();
        }
        
        public int GetTotalAmount(List<PurchaseReceiptInfo> original, BoothItemInfo item)
        {
            var filteredList = original
                .SelectMany(x => x.items)
                .Where(x => x.hash == item.hash)
                .ToList();
            return filteredList.Select(x => x.amount).Sum();
        }

        public List<PurchaseReceiptInfo> GetListOfReceipt(BoothItemInfo item)
        {
            return PurchaseHistory
                .Where(x =>
                    x.items.Find(r => r.hash == item.hash) != null)
                .ToList();
        }

        public List<PurchaseReceiptInfo> GetListOfReceipt(List<PurchaseReceiptInfo> original, BoothItemInfo item)
        {
            return original
                .Where(x =>
                    x.items.Find(r => r.hash == item.hash) != null)
                .ToList();
        }
        
        public List<PurchaseReceiptInfo> GetFilteredListOfReceipt()
        {
            var filteredList =
                FilteringItem.FilteringReceiptByDate(PurchaseHistory,
                    FilterAttribute.limit1, FilterAttribute.limit2);
            filteredList =
                FilteringItem.FilteringReceiptByItemTags(SelectedBooth.boothInformationInfo.purchasedItemStatus,
                    filteredList,FilterAttribute.selectedItemTags);
            filteredList=
                FilteringItem.FilteringReceiptByOwners(SelectedBooth.boothInformationInfo.purchasedItemStatus,
                    filteredList,FilterAttribute.selectedOwner);
            return filteredList;
        }

        #endregion
    }
}