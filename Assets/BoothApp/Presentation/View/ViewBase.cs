using UnityEngine;

namespace BoothApp.Presentation.View
{
    public abstract class ViewBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ViewHub.Views.Add(this);
        }
    }
}