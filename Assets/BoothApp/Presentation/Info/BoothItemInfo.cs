using System.Collections.Generic;
using UnityEngine;

namespace BoothApp.Presentation.Info
{
    public class BoothItemInfo
    {
        public string name = "";
        public int price = 0;
        public string owner = "";
        public List<string> itemTag = new();
        public string imageName;
        public Sprite image;
    }
}