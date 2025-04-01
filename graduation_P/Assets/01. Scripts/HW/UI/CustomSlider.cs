using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UIElements;
using static UnityEngine.InputManagerEntry;

public class CustomSlider : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _container;
    private List<VisualElement> _newDraggerList; //OnEnable에서 초기화 X
    private List<VisualElement> _barList;

    //                       dragger        
    private List<(Slider, VisualElement)> _elementList;

    private bool _isSoundDisabled = false;
    public bool IsSoundDisabled => _isSoundDisabled; // 음소거 기능

    private Color _activeColor = new Color(255, 255, 255, 255);
    private Color _disableColor = new Color(152, 154, 175, 255);

    //[SerializeField] private AudioMixerGroup _mainSound;
    //[SerializeField] private AudioMixerGroup _bgmSound;
    //[SerializeField] private AudioMixerGroup _sfxSound;

    private Slider _master;
    private Slider _bgm;
    private Slider _sfx;

    private bool _isLoaded = false;


    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        _elementList = new List<(Slider, VisualElement)>();
        _newDraggerList = new List<VisualElement>();
        _barList = new List<VisualElement>();
    }

    private void OnEnable()
    {
        if (_isLoaded == false) return;

        _container = _uiDocument.rootVisualElement.Q<VisualElement>("setting-container");

        var sliderList = _container.Query<Slider>().ToList();

        for (int i = 0; i < sliderList.Count; i++) //슬라이더 개수에 따라 label과 dragger의 개수가 정해지기 때문
        {
            Slider slider = sliderList[i];
            VisualElement dragger = sliderList[i].Q("unity-dragger");

            _elementList.Add((slider, dragger));
        }

        foreach (var (slider, dragger) in _elementList)
        {

            AddElemet(slider, dragger);

            slider.RegisterCallback<ChangeEvent<float>>(HandleSliderValueChanged);

            slider.RegisterCallback<GeometryChangedEvent>(HandleSliderInit);
        }

        //_master = _container.Q<Slider>("master");
        //_bgm = _container.Q<Slider>("bgm");
        //Debug.Log(_bgm.name);

        //_sfx = _container.Q<Slider>("sfx");
        //Debug.Log(_sfx.name);


    }

    private void Update()
    {
        //_mainSound.audioMixer.SetFloat("Master", _master.value);

    }

    private void HandleLabelClick(ClickEvent evt)
    {
        Label label = evt.target as Label;

        Slider slider = label.parent.parent.Q<Slider>("slider");

        VisualElement bar = slider.Q("Bar");
        Debug.Log(label.style.color);

        label.style.color = new StyleColor(label.style.color == Color.white ? _disableColor : Color.white);
        Debug.Log(_disableColor);
        Debug.Log(label.style.color);

        label.style.textShadow = new StyleTextShadow(
            new TextShadow { blurRadius = 0, offset = new Vector2(3, 3), color = new Color(102, 104, 128, 255) });

        bar.style.backgroundColor = label.style.color;

    }

    private void Slider(Slider slider)
    {
        VisualElement newDragger = slider.Q("new-dragger");
        VisualElement dragger = slider.Q("unity-dragger");
        Vector2 distance = new Vector2((newDragger.layout.width - dragger.layout.width) / 2, (newDragger.layout.height - dragger.layout.height) / 2);
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - distance);
    }
    private void HandleSliderInit(GeometryChangedEvent evt)
    {
        Slider slider = evt.target as Slider;
        Slider(slider);
    }

    private void HandleSliderValueChanged(ChangeEvent<float> evt)
    {
        Slider slider = evt.target as Slider;


        if (slider.parent.name == "master-sound")
        {
            _master = slider;
            //_mainSound.audioMixer.SetFloat("Master", Mathf.Lerp(-50, 2, slider.value));
            Debug.Log(slider.value);
        }

        if (slider.parent.name == "bgm-sound")
        {
            _bgm = slider;

            //_bgmSound.audioMixer.SetFloat("BGM", Mathf.Lerp(-50, 2, slider.value));
            Debug.Log(slider.value);
        }

        if (slider.parent.name == "sfx-sound")
        {
            _sfx = slider;

            //_sfxSound.audioMixer.SetFloat("SFX", Mathf.Lerp(-50, 2, slider.value));
            Debug.Log(slider.value);
        }

        Slider(slider);
    }



    void AddElemet(Slider slider, VisualElement dragger)
    {
        VisualElement bar = new VisualElement();
        dragger.Add(bar);
        bar.name = "Bar";
        bar.AddToClassList("bar");
        _barList.Add(bar);

        VisualElement newDragger = new VisualElement();
        slider.Add(newDragger);
        newDragger.name = "new-dragger";
        newDragger.AddToClassList("new-dragger");

        newDragger.pickingMode = PickingMode.Ignore;

        _newDraggerList.Add(newDragger);
    }
}
