using System;

namespace BoothApp.Data
{
    [Serializable]
    public class PurchaseItemData
    {
        public string name = "";
        public string owner = "";
        public int price = 0;
        public int amount = 0;
    }
}