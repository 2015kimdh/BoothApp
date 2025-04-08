using System;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class PurchaseItemInfo
    {
        public string hash = "";
        public int amount = 0;
        public int pricePerItem = 0;
    }
}