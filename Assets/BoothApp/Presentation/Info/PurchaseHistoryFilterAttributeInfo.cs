using System;
using System.Collections.Generic;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class PurchaseHistoryFilterAttributeInfo
    {
        /// <summary>
        /// 기간 설정 시 앞 부분
        /// </summary>
        public string limit1;
        /// <summary>
        /// 기간 설정 시 뒷 부분
        /// </summary>
        public string limit2;
        public List<string> selectedOwner = new();
        public List<string> selectedItemTags = new();
    }
}