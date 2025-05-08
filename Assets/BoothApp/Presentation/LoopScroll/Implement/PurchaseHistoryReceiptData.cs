using System;
using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using BoothApp.Presentation.Info;

namespace BoothApp.Presentation.LoopScroll.Implement
{
    [Serializable]
    public class PurchaseHistoryReceiptData
    {
        #region Public Field

        /// <summary>
        /// 표시를 위한 기본적인 데이터 집합
        /// </summary>
        public PurchaseReceiptInfo receiptInfo;

        public PurchaseHistoryViewModel purchaseHistoryViewModel;

        #endregion

        #region Property

        public BoothInfo SelectedBooth => purchaseHistoryViewModel.SelectedBooth;
        public int ReceiptIndex => _receiptIndex;

        #endregion

        #region Private Field

        public readonly int maxDisplayAmount = 3;
        private int _receiptIndex = 0;

        #endregion
        
        #region Method

        public void SetIndex(int index)
        {
            _receiptIndex = index;
        }
        
        public void SetData(PurchaseHistoryViewModel viewModel, PurchaseReceiptInfo info)
        {
            receiptInfo = info;
            DependencyInject(viewModel);
        }
        
        private void DependencyInject(PurchaseHistoryViewModel viewModel)
        {
            purchaseHistoryViewModel = viewModel;
        }

        #endregion
    }
}