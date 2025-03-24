using System;
using UnityEngine;

namespace BoothApp.Data
{
    [Serializable]
    public class BoothItemData
    {
        public string name = "";

        public int price = 0;

        public string owner = "";

        public string imageName;
    }
}