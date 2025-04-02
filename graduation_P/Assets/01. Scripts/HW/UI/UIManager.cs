using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<Type, BaseUI> _uiBaseDict;

    protected override void OnCreateInstance()
    {
        base.OnCreateInstance();
        _uiBaseDict = new Dictionary<Type, BaseUI>();
    }

    private void Awake()
    {
        
    }

    public BaseUI GetUI<T>() where T : BaseUI
    {
        if (_uiBaseDict.TryGetValue(typeof(T), out BaseUI ui))
        {
            return ui;
        }
        Debug.LogWarning($"UIManager: {typeof(T).Name} UI가 등록되지 않았습니다.");
        return null;
    }

    public T GetUIType<T>() where T : BaseUI
    {
        return GetUI<T>() as T;
    }

    public VisualTreeAsset GetVTAsset<T>() where T : BaseUI
    {
        return GetUI<T>().visualTreeAsset;
    }
    public void AddUI(BaseUI baseUi)
    {
        _uiBaseDict.Add(baseUi.GetType(), baseUi);
    }

    public void ShowUI<T>() where T : BaseUI
    {
        if (_uiBaseDict.TryGetValue(typeof(T), out BaseUI ui))
        {
            ui.Open();
        }
    }

    public void HideUI<T>() where T : BaseUI
    {
        if (_uiBaseDict.TryGetValue(typeof(T), out BaseUI ui))
        {
            ui.Close();
        }
    }
}
