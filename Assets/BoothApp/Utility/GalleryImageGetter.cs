using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Utility
{
    public class GalleryImageGetter
    {
        public RawImage rawImage;
        private const int FileSizeLimit = 1024 * 1024 * 5; // 5MB

        public async void GetImageFromGallery()
        {
            string image1 = "";
            NativeGallery.GetImageFromGallery((image) =>
            {
                FileInfo selectedImage = new FileInfo(image);

                if (selectedImage.Length > FileSizeLimit)
                {
                    return;
                }
            });
            if (!string.IsNullOrEmpty(image1))
            {
                var ta = Task.Run(() => AsyncLoadImage(image1));
                await ta;
            }
            
        }

        public async Task<Texture2D> AsyncLoadImage(string path)
        {
            byte[] fileData = File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path).Split('.')[0];
            string savePath = Application.persistentDataPath + "/Image";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            await File.WriteAllBytesAsync(savePath + fileName + ".png", fileData);
            var temp = File.ReadAllBytes(savePath + fileName + ".png");
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(temp);
            return tex;
        }
    }
}