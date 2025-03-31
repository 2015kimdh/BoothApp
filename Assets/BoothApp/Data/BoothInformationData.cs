using System;
using System.Collections.Generic;

namespace BoothApp.Data
{
    [Serializable]
    public class BoothInformationData
    {
        public string boothName;

        public string imageName;

        /// <summary>
        /// 존재하는 아이템 태그들
        /// </summary>
        public List<string> itemTags = new();
        
        /// <summary>
        /// 최초 아이템 세팅 상태
        /// </summary>
        public List<BoothItemWithAmountData> originalItemStatus = new();
        
        /// <summary>
        /// 현재 판매된 아이템 상태
        /// </summary>
        public List<BoothItemWithAmountData> purchasedItemStatus = new();

        public List<PurchaseReceiptData> purchasedHistory = new();
        
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