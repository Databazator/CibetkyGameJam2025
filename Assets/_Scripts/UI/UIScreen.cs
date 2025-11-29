using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public abstract class UIScreen : MonoBehaviour
{
    protected UIDocument _document;
    protected VisualElement _root;

    public const string VisibleClass = "screen-visible";
    public const string HiddenClass = "screen-hidden";

    public virtual void Initialize()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        // _root.RegisterCallback<TransitionEndEvent>(OnTransitionEnd);
    }

    protected void OnTransitionEnd(TransitionEndEvent evt)
    {
        if (_root.ClassListContains(HiddenClass))
        {
            _root.style.display = DisplayStyle.None;
        }
    }

    public virtual void Show()
    {
        _root.style.display = DisplayStyle.Flex;
        _root.AddToClassList(VisibleClass);
        _root.BringToFront();
        _root.RemoveFromClassList(HiddenClass);
    }

    public virtual void Hide()
    {
        _root.AddToClassList(HiddenClass);
        _root.RemoveFromClassList(VisibleClass);
    }
}
