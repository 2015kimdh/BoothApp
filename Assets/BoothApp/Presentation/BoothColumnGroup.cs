using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoothApp.Presentation
{
    public class BoothColumnGroup : MonoBehaviour
    {
        private BoothDataPresenter _presenter;
        public List<BoothColumn> boothColumns = new();
        [SerializeField] private BoothColumnMaker columnMaker;

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
            if (_presenter.boothInfo.Count > boothColumns.Count)
            {
                int gap = _presenter.boothInfo.Count - boothColumns.Count;
                for (int i = gap; i > 0; i--)
                    boothColumns.Add(columnMaker.MakeBoothColumn());
            }

            if (_presenter.boothInfo.Count < boothColumns.Count)
            {
                int gap = boothColumns.Count - _presenter.boothInfo.Count;
                for (int i = gap; i > 0; i -=1)
                {
                    var item = boothColumns.Last();
                    boothColumns.Remove(item);
                    Destroy(item.gameObject);
                }
            }

            for (int i = 0; i < _presenter.boothInfo.Count(); i++)
            {
                var presenterInfo = _presenter.boothInfo[i].boothInformationInfo;
                boothColumns[i].boothName.text = presenterInfo.boothName;
                boothColumns[i].createdAt.text = presenterInfo.createdAt;
                boothColumns[i].updatedAt.text = presenterInfo.modifyAt;
            }
        }

        public void DeleteBoothColumnObject(string boothName)
        {
        }
    }
}