using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace BoothApp.Data
{
    [Serializable]
    public class PurchaseItemData
    {
        public string hash = "";
        public int amount = 0;
        public int pricePerItem = 0;
    }
}