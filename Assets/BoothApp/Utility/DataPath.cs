using UnityEngine;

namespace BoothApp.Utility
{
    public static class DataPath
    {
        public static readonly string ImagePath = Application.persistentDataPath + "/Image";
    }

    public static class FileExtension
    {
        public static readonly string Json = ".json";
        public static readonly string Png = ".png";
    }
}