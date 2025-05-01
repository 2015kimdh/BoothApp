using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoothApp.Utility;
using UnityEngine;

namespace BoothApp.Presentation
{
    public class ImageGarbageCollector : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private BoothDataPresenter presenter;

        #endregion

        #region Method

        private void Awake()
        {
            GC();
        }

        /// <summary>
        /// 사용하지 않는 이미지 삭제
        /// </summary>
        private void GC()
        {
            List<string> spriteNamesInUsing = new();
            var usedImage = presenter.boothInfo.Select(x => x.boothInformationInfo.imageName).ToList();
            var originalItemImage = presenter.boothInfo
                .SelectMany(booth => booth.boothInformationInfo.originalItemStatus)
                .Select(item => item.itemInfo.imageName).ToList();
            var purchasedItemImage = presenter.boothInfo
                .SelectMany(booth => booth.boothInformationInfo.purchasedItemStatus)
                .Select(item => item.itemInfo.imageName).ToList();
            usedImage = usedImage.Union(originalItemImage)
                .Union(purchasedItemImage).Distinct().ToList();
            // 사용 중인 이미지 이름 모두 가져오기

            DirectoryInfo directory = new DirectoryInfo(DataPath.ImagePath);
            var files = directory.GetFiles();
            var fileNames = files.Select(x => Path.GetFileName(x.Name).Split('.')[0]);
            fileNames = fileNames.Except(usedImage).ToList();
            files = files.Where(x => fileNames.Contains(Path.GetFileName(x.Name).Split('.')[0])).ToArray();

            foreach (var target in files)
            {
                File.Delete(DataPath.ImagePath + "/" + target.Name);
            }
        }

        #endregion
    }
}