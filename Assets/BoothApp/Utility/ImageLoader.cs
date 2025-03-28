using System.IO;
using UnityEngine;

namespace BoothApp.Utility
{
    public static class ImageLoader
    {
        public static Sprite LoadImageWithName(string imageName)
        {
            byte[] fileData = File.ReadAllBytes(DataPath.ImagePath + "/" + imageName + FileExtension.Png);
            string savePath = DataPath.ImagePath;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            Texture2D tex = new Texture2D(0, 0);
            var result = tex.LoadImage(fileData);
            if (!result)
                return null;
            
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            return Sprite.Create(tex, rect,new Vector2(0.5f, 0.5f));
        }
    }
}