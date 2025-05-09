using System.Linq;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class TotalInvoiceView : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseHistoryViewModel viewModel;

        [SerializeField] private TMP_Text invoiceText;

        [SerializeField] private PurchaseHistoryItemGroup itemGroup;

        [SerializeField] private PurchaseReceiptItemGroupLoopScrollVer loopScrollVerGroup;

        #endregion

        #region Method

        private void Awake()
        {
            itemGroup.onRefresh.AddListener(SetInvoice);
            loopScrollVerGroup.onRefresh.AddListener(SetInvoiceByLoopScroll);
        }

        public void SetInvoice()
        {
            invoiceText.text = ((int)Calculate()).ToString();
        }
        
        public void SetInvoiceByLoopScroll()
        {
            invoiceText.text = ((int)CalculateLoopScrollVer()).ToString();
        }

        // 활성화 된 객체들만 계산함
        private float Calculate()
        {
            var activeItem = itemGroup.receipts.Where(x => x.gameObject.activeInHierarchy).ToList();
            return activeItem.SelectMany(receipt => receipt.receiptInfo.items)
                .Sum(item => item.pricePerItem * item.amount);
            
            // return viewModel.PurchaseHistory
            //     .SelectMany(receipt => receipt.items)
            //     .Sum(item => item.pricePerItem * item.amount);
        }
        
        // 활성화 된 객체들만 계산함
        private float CalculateLoopScrollVer()
        {
            var activeItem = loopScrollVerGroup.FilteredData
                    .Select(x=> x.receiptInfo)
                    .SelectMany(item => item.items)
                    .Sum(target => target.pricePerItem * target.amount);
            return activeItem;
        }

        #endregion
    }
}