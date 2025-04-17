using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.AddItem;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public class ItemTagDropdownSetter : MonoBehaviour
    {
        #region Property

        public List<string> SelectedTag => SetTagData();

        #endregion

        #region Serialize Field

        [SerializeField] protected GameObject selectionPanel;
        [SerializeField] protected GameObject itemTagPrefab;
        [SerializeField] protected GameObject parentObject;
        [SerializeField] protected UIButton dropDownButton;
        [SerializeField] protected TMP_Text currentTags;

        #endregion

        #region Private Field

        protected List<ItemTagLabel> _itemTagLabels = new();
        protected bool setDataFlag = true;

        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            dropDownButton.onClickEvent.AddListener(ShowPanel);
        }

        #endregion

        #region Public Method

        public void Refresh(List<string> original, List<string> selected)
        {
            MakeOptions(original);
            SetSelectedTagBySaveInfo(selected);
            SetCurrentTagLabel();
        }

        /// <summary>
        /// 목록 생성만 하고 모두 선택 해제
        /// </summary>
        public void RefreshOnly(List<string> original)
        {
            MakeOptions(original);
            SetAllToggleFalse();
            SetCurrentTagLabel();
        }
        
        #endregion
        
        #region Private Methods

        private void ShowPanel()
        {
            selectionPanel.SetActive(!selectionPanel.activeInHierarchy);
        }

        private void MakeOptions(List<string> tagList)
        {
            List<string> instantiatedList = new();
            foreach (var item in _itemTagLabels)
                instantiatedList.Add(item.label.text);

            var except = instantiatedList.Except(tagList).ToList();

            for (int i = 0; i < except.Count; i++)
            {
                var target = _itemTagLabels.Find(x => x.label.text == except[i]);
                _itemTagLabels.Remove(target);
                Destroy(target.gameObject);
            }

            var needToMake = tagList.Except(instantiatedList).ToList();

            for (int i = 0; i < needToMake.Count; i++)
            {
                var newItem = Instantiate(itemTagPrefab, parentObject.transform).GetComponent<ItemTagLabel>();
                newItem.label.text = needToMake[i];
                newItem.toggle.isOn = false;
                newItem.toggle.onValueChanged.AddListener(delegate { SetCurrentTagLabel(); });
                _itemTagLabels.Add(newItem);
            }
        }

        public void SetAllToggleFalse()
        {
            foreach (var item in _itemTagLabels)
                item.toggle.isOn = false;
        }

        protected void SetSelectedTagBySaveInfo(List<string> saveInfo)
        {
            if (saveInfo.Count == 0)
            {
                SetAllToggleFalse();
                return;
            }

            foreach (var item in _itemTagLabels)
            {
                if (saveInfo.Contains(item.label.text))
                    item.toggle.isOn = true;
                else
                    item.toggle.isOn = false;
            }
        }
        
        private List<string> SetTagData()
        {
            List<string> selectedTags = new();
            foreach (var item in _itemTagLabels)
            {
                if (item.toggle.isOn)
                    selectedTags.Add(item.label.text);
            }
            return selectedTags;
        }

        protected void SetCurrentTagLabel()
        {
            currentTags.text = "미설정";
            var selected = SelectedTag;
            for (int i = 0; i < selected.Count; i++)
            {
                if (i == 0)
                    currentTags.text = selected[i];
                else
                    currentTags.text = currentTags.text + "/" + selected[i];
            }
        }

        #endregion
    }
}