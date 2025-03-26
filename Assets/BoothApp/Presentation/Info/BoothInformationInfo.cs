using System;
using System.Collections.Generic;
using BoothApp.Data;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class BoothInformationInfo
    {
        public string boothName;
        
        public string imageName;

        /// <summary>
        /// 최초 아이템 세팅 상태
        /// </summary>
        public List<BoothItemWithAmountInfo> originalItemStatus = new();
        
        /// <summary>
        /// 현재 판매된 아이템 상태
        /// </summary>
        public List<BoothItemWithAmountInfo> purchasedItemStatus = new();

        public List<PurchaseReceiptInfo> purchasedHistory = new();

        /// <summary>
        /// 생성된 시간
        /// </summary>
        public string createdAt;

        /// <summary>
        /// 변경된 시간
        /// </summary>
        public string modifyAt;
    }
}