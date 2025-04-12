using System;
using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BoothApp.Presentation
{
    public class BoothColumnGroup : MonoBehaviour
    {
        private BoothDataPresenter _presenter;
        public List<BoothColumn> boothColumns = new();
        [SerializeField] private BoothColumnMaker columnMaker;
        public UnityEvent onRefresh;

        private void Awake()
        {
            _presenter = FindObjectOfType<BoothDataPresenter>();
        }

        private void Start()
        {
            RefreshBoothColumnObject();
        }

        public void RefreshBoothColumnObject()
        {
            var needToMake = new List<BoothInfo>();

            foreach (var item in _presenter.boothInfo)
            {
                if (boothColumns.Find(x
                        => x.boothName.text == item.boothInformationInfo.boothName) == null)
                    needToMake.Add(item);
            }

            foreach (var make in needToMake)
            {
                var newColumn = columnMaker.MakeBoothColumn();
                
                // 부스 데이터 입력
                SetBoothColumnData(newColumn, make);

                boothColumns.Add(newColumn);
            }

            if (_presenter.boothInfo.Count < boothColumns.Count)
            {
                List<BoothColumn> deleteBooth = new();
                foreach (var booth in boothColumns)
                {
                    var answer =
                        _presenter.boothInfo.Find(x => x.boothInformationInfo.boothName == booth.boothName.text);
                    if (answer == null)
                        deleteBooth.Add(booth);
                }

                foreach (var item in deleteBooth)
                {
                    boothColumns.Remove(item);
                    Destroy(item.gameObject);
                }
            }

            RefreshOldData();
            ArrayBoothItemByCreatedAt();
            onRefresh.Invoke();
        }

        private void RefreshOldData()
        {
            foreach (var presenter in _presenter.boothInfo)
            {
                var booth = boothColumns.Find(x => x.boothName.text == presenter.boothInformationInfo.boothName);
                if (booth == null)
                    continue;

                // 변경된 사항이 있을 경우
                if (DateTimeUtil.DateTimeStringToDateTime(booth.updatedAt.text)
                    < DateTimeUtil.DateTimeStringToDateTime(presenter.boothInformationInfo.modifyAt))
                {
                    SetBoothColumnData(booth, presenter);
                }
            }
        }

        private void ArrayBoothItemByCreatedAt()
        {
            boothColumns.Sort(((infoA, infoB) =>
            {
                return DateTimeUtil.DateTimeStringToDateTime(infoA.createdAt.text)
                    .CompareTo(DateTimeUtil.DateTimeStringToDateTime(infoB.createdAt.text));
            }));

            for (int i = 0; i < boothColumns.Count; i++)
                boothColumns[i].gameObject.transform.SetAsFirstSibling();
        }
        
        private void SetBoothColumnData(BoothColumn targetColumn, BoothInfo boothInfo)
        {
            targetColumn.boothName.text = boothInfo.boothInformationInfo.boothName;
            targetColumn.createdAt.text = boothInfo.boothInformationInfo.createdAt;
            targetColumn.updatedAt.text = boothInfo.boothInformationInfo.modifyAt;

            if (ImageHub.CheckImageExist(boothInfo.boothInformationInfo.imageName))
                targetColumn.image.sprite = ImageHub.GetImageWithName(boothInfo.boothInformationInfo.imageName);
        }

        
        public void DeleteBoothColumnObject(string boothName)
        {
        }
    }
}