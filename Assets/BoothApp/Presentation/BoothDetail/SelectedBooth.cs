using System.Collections.Generic;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBooth : MonoBehaviour
    {
        #region Property

        public List<BoothItemWithAmountInfo> OriginalItemStatus =>
                    selectedBooth.boothInformationInfo.originalItemStatus;
        public List<BoothItemWithAmountInfo> PurchasedItemStatus =>
            selectedBooth.boothInformationInfo.purchasedItemStatus;
        
        public List<PurchaseReceiptInfo> PurchasedHistory =>
            selectedBooth.boothInformationInfo.purchasedHistory;

        #endregion
        
        
        public UnityEvent<string> onSelected;
        public UnityEvent onSelectedVoid;

        public BoothInfo selectedBooth = new();

        private BoothDataPresenter _presenter;

        private void Awake()
        {
            _presenter = FindObjectOfType<BoothDataPresenter>();
            onSelected.AddListener(SetSelectedBoothName);
        }

        private void SetSelectedBoothName(string boothName)
        {
            selectedBooth = _presenter.boothInfo.Find(x => x.boothInformationInfo.boothName == boothName);
            onSelectedVoid.Invoke();
        }
    }
}