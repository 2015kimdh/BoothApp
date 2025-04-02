using System;
using System.Collections.Generic;
using System.Linq;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class ItemTagDropdown : MonoBehaviour
    {
        #region Property

        public List<string> SelectedTags => GetSelectedTags();

        #endregion

        #region Serialize Field

        [SerializeField] private AddNewItemView view;

        [SerializeField] private GameObject selectionPanel;
        [SerializeField] private GameObject itemTagPrefab;
        [SerializeField] private GameObject parentObject;
        [SerializeField] private UIButton dropDownButton;

        #endregion

        #region Private Field

        private List<ItemTagLabel> _itemTagLabels = new();

        private List<string> OriginalTagList => view.ItemTags;

        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            dropDownButton.onClickEvent.AddListener(ShowPanel);
        }
        
        #endregion
        
        #region Public Method

        public void Refresh()
        {
            MakeOptions();
        }

        public void RefreshOnFalse()
        {
            Refresh();
            SetAllToggleFalse();
        }
        
        #endregion

        #region Private Methods

        private void ShowPanel()
        {
            selectionPanel.SetActive(!selectionPanel.activeInHierarchy);
        }

        private List<string> GetSelectedTags()
        {
            var result = _itemTagLabels.Where(x => x.toggle.isOn == true).ToList();

            List<string> resultList = new();
            foreach (var item in result)
                resultList.Add(item.label.text);
            return resultList;
        }

        private void MakeOptions()
        {
            List<string> instantiatedList = new();
            foreach (var item in _itemTagLabels)
                instantiatedList.Add(item.label.text);

            var except = instantiatedList.Except(OriginalTagList).ToList();

            for (int i = 0; i < except.Count; i++)
            {
                var target = _itemTagLabels.Find(x => x.label.text == except[i]);
                _itemTagLabels.Remove(target);
                Destroy(target);
            }

            var needToMake = OriginalTagList.Except(instantiatedList).ToList();

            for (int i = 0; i < needToMake.Count; i++)
            {
                var newItem = Instantiate(itemTagPrefab, parentObject.transform).GetComponent<ItemTagLabel>();
                newItem.label.text = needToMake[i];
                newItem.toggle.isOn = false;
                newItem.toggle.onValueChanged.AddListener(delegate { SetTagData(); });
                _itemTagLabels.Add(newItem);
            }
        }

        private void SetAllToggleFalse()
        {
            foreach (var item in _itemTagLabels)
                item.toggle.isOn = false;
        }

        private void SetTagData()
        {
            List<string> selectedTags = new();
            foreach (var item in _itemTagLabels)
                selectedTags.Add(item.label.text);

            view.selectedTags = selectedTags;
        }
        
        #endregion
    }
}