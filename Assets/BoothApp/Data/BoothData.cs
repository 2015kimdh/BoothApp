using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace BoothApp.Data
{
    [Serializable]
    public class BoothData
    {
        public BoothInformationData boothInformationData = new();

        public string savedAt;
    }
}