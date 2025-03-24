using System;
using UnityEngine.Serialization;

namespace BoothApp.Data
{
    [Serializable]
    public class BoothItemWithAmountData
    {
        public BoothItemData itemData;
        public int amount;
    }
}