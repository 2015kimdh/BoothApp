using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Presentation.LoopScroll;
using BoothApp.Presentation.LoopScroll.Implement;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseReceiptItemGroupLoopScrollVer : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private PurchaseHistoryView view;
        [SerializeField] private LoopScrollView<PurchaseHistoryReceiptData> scrollView;

        #endregion

        #region Public Fields

        public List<PurchaseHistoryReceiptData> receipts = new();

        #endregion

        #region Property

        private BoothInfo SelectedBooth => viewModel.SelectedBoothViewModel.selectedBooth;

        private PurchaseHistoryFilterAttributeInfo FilterAttributeInfo =>
            SelectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;

        #endregion

        #region Private Fields

        [SerializeField]
        private List<PurchaseReceiptInfo> _receiptInfos = new();

        private List<PurchaseHistoryReceiptData> _beDisplayTarget = new();

        #endregion

        #region Unity Event

        public UnityEvent onRefresh;

        #endregion

        private void Awake()
        {
            view.onViewShow.AddListener(Refresh);
            viewModel.onDelete.AddListener(Refresh);
        }
        
        public void Refresh()
        {
            RefreshReceiptList();
            SortByTime();
            _beDisplayTarget = FilteringReceiptByDate(Mapping(_receiptInfos));
            scrollView.SetData(_beDisplayTarget);
            onRefresh.Invoke();
        }

        private void RefreshReceiptList()
        {
            // ViewModel과 자신의 교집합
            var intersect = _receiptInfos.Intersect(viewModel.PurchaseHistory);
            // ViewModel에만 있는 객체 추리기
            var insert = viewModel.PurchaseHistory.Except(_receiptInfos);
            _receiptInfos = intersect.Union(insert).ToList();
        }

        private void SortByTime()
        {
            _receiptInfos = _receiptInfos.OrderByDescending(x => x.purchasedAt).ToList();
        }

        private List<PurchaseReceiptInfo> FilteringReceiptByDate()
        {
            var list = viewModel.PurchaseHistory.AsEnumerable();

            if (FilterAttributeInfo.limit1 != "")
            {
                var from = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit1);
                list = list.Where(x => x.purchasedAt >= from);
            }

            if (FilterAttributeInfo.limit2 != "")
            {
                var to = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit2);
                list = list.Where(x => x.purchasedAt <= to);
            }

            return list.ToList();
        }
        
        private List<PurchaseHistoryReceiptData> FilteringReceiptByDate(List<PurchaseHistoryReceiptData> infoList)
        {
            var list = infoList.AsEnumerable();

            if (FilterAttributeInfo.limit1 != "")
            {
                var from = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit1);
                list = list.Where(x => x.receiptInfo.purchasedAt >= from);
            }

            if (FilterAttributeInfo.limit2 != "")
            {
                var to = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit2);
                list = list.Where(x => x.receiptInfo.purchasedAt <= to);
            }

            return list.ToList();
        }

        private List<PurchaseHistoryReceiptData> Mapping(List<PurchaseReceiptInfo> original)
        {
            var result = new List<PurchaseHistoryReceiptData>();
            for (int i = 0; i < original.Count; i++)
            {
                var mapped = new PurchaseHistoryReceiptData();
                mapped.receiptInfo = original[i];
                mapped.purchaseHistoryViewModel = viewModel;
                mapped.SetIndex(original.Count - i);
                result.Add(mapped);
            }

            return result;
        }
    }
}