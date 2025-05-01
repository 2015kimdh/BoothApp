using System;
using System.Collections.Generic;
using System.IO;
using BoothApp.Data;
using BoothApp.Utility;
using Newtonsoft.Json;
using UnityEngine;

namespace BoothApp.Presentation.LoadedImageHub
{
    /// <summary>
    /// Image 폴더 내의 이미지를 싹 긁어모아 메모리에 올려두는 과정
    /// 앱 기동 시 딱 한 번 작동
    /// </summary>
    public class LoadAllImageInStorage : MonoBehaviour
    {
        public List<Sprite> test = new();
        private void Awake()
        {
            Application.targetFrameRate = 60;
            GetImagesInStorage();
        }

        /// <summary>
        /// 이미지 가져오는 함수
        /// </summary>
        private void GetImagesInStorage()
        {
            if(!CheckImageFolder())
                return;
            GetImage();
        }
        
        /// <summary>
        /// Image 폴더가 생성이 되어있는지 확인한 후
        /// 없으면 생성하고 종료
        /// </summary>
        private bool CheckImageFolder()
        {
            DirectoryInfo directory = new DirectoryInfo(DataPath.ImagePath);
            Debug.Log(DataPath.ImagePath);
            if (!directory.Exists)
            {
                directory.Create();
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 실제로 이미지를 가져오는 함수
        /// 모든 png 확장자를 가진 파일의 목록을 가져오고
        /// 해당 파일에 접근해 이미지 가져오기
        /// </summary>
        private void GetImage()
        {
            DirectoryInfo directory = new DirectoryInfo(DataPath.ImagePath);
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension.ToLower().CompareTo(FileExtension.Png) == 0)
                {
                    var imageData = File.ReadAllBytes(DataPath.ImagePath + "/" + file.Name);
                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(imageData);
                    var spriteData = ImageLoader.TextureToSprite(tex);
                    
                    // 스프라이트 이름 지정
                    string spriteName = Path.GetFileName(file.Name).Split('.')[0];
                    
                    spriteData.name = spriteName;
                    ImageHub.LoadedImage.Add(spriteData);
                    
                    test.Add(spriteData);
                    
                    // 디버깅용 스프라이트 이름 출력
                    Debug.Log(spriteData.name);
                }
            }
        }
    }
}