using System.Collections.Generic;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class AddItemTagInputField : AddOptionWithInputField
    {
        #region Property
        protected override List<string> OriginalList 
            => selectedBoothViewModel.selectedBooth.boothInformationInfo.itemTags;
        
        #endregion
    }
}