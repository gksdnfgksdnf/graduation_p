using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<Type, BaseUI> _uiBaseDict;
    private List<BaseUI> _uiBaseList;

    protected override void OnCreateInstance()
    {
        base.OnCreateInstance();
        _uiBaseDict = new Dictionary<Type, BaseUI>();
        _uiBaseList = new List<BaseUI>();
    }

    public BaseUI GetUI<T>() where T : BaseUI
    {
        return _uiBaseDict[typeof(T)];
    }

    public void AddUI(BaseUI baseUi)
    {
        _uiBaseDict.Add(baseUi.GetType(), baseUi);
    }
}
