using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : BaseUI
{
    [SerializeField] private VisualTreeAsset _mainAsset;
    [SerializeField] private VisualTreeAsset _settingAsset;

    private VisualElement _mainRoot;
    private VisualElement _root;
    private VisualElement _settingRoot;

    private VisualElement _start;
    private VisualElement _setting;
    private VisualElement _exit;

    private SettingUI _settingUI;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.AddUI(this);


        _root = _uiDocument.rootVisualElement;

        _settingUI = GetComponent<SettingUI>();

        if (_mainAsset != null)
        {
            _mainRoot = _mainAsset.CloneTree();
            _mainRoot.style.flexGrow = 1;
        }
        else
            Debug.LogError("Main Asset이 설정되지 않았습니다.");


    }

    private void OnEnable()
    {
        if (_mainRoot != null)
        {
            _root.Q("container").Add(_mainRoot);

            // VisualElements를 가져오기
            _start = _mainRoot.Q<VisualElement>("start");
            _setting = _mainRoot.Q<VisualElement>("setting");
            _exit = _mainRoot.Q<VisualElement>("exit");

            // 클릭 이벤트 등록
            _start?.RegisterCallback<ClickEvent>(evt =>
            {
                //Fade
            });

            _setting?.RegisterCallback<ClickEvent>(evt =>
            {
                _mainRoot.style.display = DisplayStyle.None;
                _settingRoot = _settingAsset.CloneTree();
                _root.Q("container").Add(_settingRoot);
                _settingRoot.style.flexGrow = 1;
                _settingUI.InitUI();
            });

            _exit?.RegisterCallback<ClickEvent>(evt =>
            {
                Debug.Log("Exit 버튼 클릭됨!");
            });
        }
        else
            Debug.LogError("Root VisualElement가 설정되지 않았습니다.");
    }
}
