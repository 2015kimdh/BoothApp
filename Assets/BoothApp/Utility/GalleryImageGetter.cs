using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Utility
{
    public class GalleryImageGetter : MonoBehaviour
    {
        public RawImage rawImage;
        private const int FileSizeLimit = 1024 * 1024 * 5; // 5MB

        public IEnumerator GetImageFromGallery()
        {
            bool successToGet = true;
            string image1 = "";
            Debug.Log("이미지 가져오기");

            NativeGallery.GetImageFromGallery((image) =>
            {
                FileInfo selectedImage = new FileInfo(image);
                image1 = image;
                if (selectedImage.Length > FileSizeLimit)
                {
                    Debug.Log("이미지 가져오기 실패 = " + selectedImage.Length);
                    successToGet = false;
                }
            });
            if (!successToGet)
                yield break;

            if (!string.IsNullOrEmpty(image1))
            {
                AsyncLoadImage(image1);
                Debug.Log("이미지 가져오기 + " + rawImage.texture.name);
                yield return null;
            }
        }

        public void AsyncLoadImage(string path)
        {
            byte[] fileData = File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path).Split('.')[0];
            string savePath = Application.persistentDataPath + "/Image";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(fileData);
            tex = ResizeTexture(tex, tex.width/4, tex.height/4);
            File.WriteAllBytes(savePath + "/" + fileName + ".png", tex.EncodeToPNG());
            rawImage.texture = tex;
        }

        private Texture2D ResizeTexture(Texture2D original, int width, int height)
        {
            // 새로운 텍스처 생성
            Texture2D newTexture = new Texture2D(width, height);

            // 텍스처 리사이즈
            Color[] pixels = original.GetPixels(0, 0, original.width, original.height);
            Color[] resizedPixels = newTexture.GetPixels();

            // 텍스처 리사이징 (단순 리샘플링)
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int srcX = x * original.width / width;
                    int srcY = y * original.height / height;
                    resizedPixels[y * width + x] = pixels[srcY * original.width + srcX];
                }
            }
        
            newTexture.SetPixels(resizedPixels);
            newTexture.Apply();

            return newTexture;
        }
        
        public Texture2D ResizeTexture(Texture2D old)
        {
            Texture2D copy = DuplicateTexture(old);
            Color[] oldColor = copy.GetPixels(); // 읽기 가능한 파일의 픽셀값 저장
            copy.Reinitialize(old.width, old.height, TextureFormat.RGBA32, false); // RGBA32로 변환 (사이즈 유지)
            copy.SetPixels(oldColor); // 이전에 저장해둔 픽셀값 적용 (안할경우 이미지 단색)
            copy.Compress(true); // 추가로 압축 진행 (DXT5로 적용됨 확인)
            copy.Apply(); // 저장

            return copy;
        }

        private Texture2D DuplicateTexture(Texture source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableTexture = new Texture2D(source.width, source.height);
            readableTexture.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableTexture.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);

            return readableTexture;
        }
    }
}