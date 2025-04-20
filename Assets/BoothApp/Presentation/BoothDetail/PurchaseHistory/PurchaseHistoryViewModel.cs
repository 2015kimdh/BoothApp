using System;
using System.Collections.Generic;
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

        #endregion
        
        #region Method

        /// <summary>
        /// 결제 시간으로 찾는 함수
        /// </summary>
        public void DeletePurchaseHistory(DateTime purchasedAt)
        {
            var target = PurchaseHistory.Find(x => x.purchasedAt == purchasedAt);
            PurchaseHistory.Remove(target);
            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
        }
        
        /// <summary>
        /// 결제 결과 대상으로 찾는 함수
        /// </summary>
        public void DeletePurchaseHistory(PurchaseReceiptInfo targetReceipt)
        {
            PurchaseHistory.Remove(targetReceipt);
            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
        }
        
        #endregion
    }
}