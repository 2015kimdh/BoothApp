using System.Collections.Generic;
using UnityEngine;

namespace BoothApp.Utility
{
    public static class ImageHub
    {
        public static List<Sprite> LoadedImage = new();

        public static bool CheckImageExist(string imageName)
        {
            var result = LoadedImage.Find(x => x.name == imageName);

            if (result == null)
                return false;
            else
                return true;
        }

        public static Sprite GetImageWithName(string imageName) =>
            LoadedImage.Find(x => x.name == imageName);
    }
}