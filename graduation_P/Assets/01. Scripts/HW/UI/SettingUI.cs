using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingUI : BaseUI
{
    private VisualElement _root;

    private VisualElement _settingRoot;

    private VisualElement _fade;

    private List<(Button, Action)> _btnActions = new List<(Button, Action)>();
    private List<(Slider, Action<ChangeEvent<float>>)> _sliderActions = new List<(Slider, Action<ChangeEvent<float>>)>();

    private Button _exitBtn;
    private Button _initBtn;

    private Slider _master;
    private Slider _bgm;
    private Slider _sfx;


    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.AddUI(this);
    }

    private void OnEnable()
    {

    }

    public void InitUI()
    {


        InitElements();

        RegisterBtnEvents();

        RegisterSliderEvents();
    }

    private void InitElements()
    {
        _settingRoot = _root.Q("setting-container");

        _fade = _settingRoot.Q("fade-pannel");

        // buttons
        _exitBtn = _settingRoot.Q<Button>("exit-btn");
        _initBtn = _settingRoot.Q<Button>("init-btn");

        // sliders
        _master = _settingRoot.Q<Slider>("master-slider");
        _bgm    = _settingRoot.Q<Slider>(   "bgm-slider");
        _sfx    = _settingRoot.Q<Slider>(   "sfx-slider");

    }

    #region AddActions
    private void AddSliderValueChanged(Slider master, Action<ChangeEvent<float>> action)
        => _sliderActions.Add((master, action));
    private void AddButtonAction(Button btn, Action action)
       => _btnActions.Add((btn, action));
    #endregion

    #region InitEvents

    private void RegisterBtnEvents()
    {
        AddButtonAction(_exitBtn, OnExitBtnClickEvent);
        //AddButtonAction(_initBtn, OnInitBtnClickEvent);

        foreach ((Button, Action) item in _btnActions)
        {
            item.Item1.clicked += item.Item2;
        }
    }
    private void RegisterSliderEvents()
    {
        AddSliderValueChanged(_master,OnMasterSliderValueChangeEvent);
        AddSliderValueChanged(_bgm   ,OnBgmSliderValueChangeEvent);
        AddSliderValueChanged(_sfx   ,OnSfxSliderValueChangeEvent);

        foreach ((Slider, Action<ChangeEvent<float>>) item in _sliderActions)
        {
            item.Item1.RegisterValueChangedCallback(evt =>
            {
                item.Item2(evt);
            });
        }
    }

    #endregion

    #region SliderValueChangedEvents
    private void OnMasterSliderValueChangeEvent(ChangeEvent<float> evt)
    {
        Debug.Log("Master Slider의 값이 변경 되었습니다.");
    }
    private void OnSfxSliderValueChangeEvent(ChangeEvent<float> evt)
    {
        Debug.Log("Sfx Slider의 값이 변경 되었습니다.");
    }
    private void OnBgmSliderValueChangeEvent(ChangeEvent<float> evt)
    {
        Debug.Log("Bgm Slider의 값이 변경 되었습니다.");
    }
    #endregion

    #region ButtonClickedEvents

    private void OnExitBtnClickEvent()
    {
        Close();
        UIManager.Instance.ShowUI<MainUI>();
    }

    private void OnInitBtnClickEvent()
    {
        Debug.Log("초기화 버튼이 눌렸습니다.");
    }
    #endregion

    public override void Close()
    {
        _settingRoot.Q("window").RemoveFromClassList("appear");
        _fade.RemoveFromClassList("fade");
    }

    public override void Open()
    {
        _root = visualTreeAsset.CloneTree();

        root.Q("container").Add(_root);

        _root.style.flexGrow = 1;

        _root.style.position = Position.Absolute;

        _root.style.width = new Length(100, LengthUnit.Percent);
        _root.style.height = new Length(100, LengthUnit.Percent);

        InitUI();

        StartCoroutine(AddClass());
    }

    private IEnumerator AddClass()
    {
        yield return new WaitForSeconds(.1f);
        _settingRoot.Q("window").AddToClassList("appear");
        _fade.AddToClassList("fade");
    }

}
