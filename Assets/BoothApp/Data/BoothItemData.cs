using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Data
{
    [Serializable]
    public class BoothItemData
    {
        public string name = "";

        public int price = 0;

        public string owner = "";

        public List<string> itemTag = new();

        public string imageName;
    }
}