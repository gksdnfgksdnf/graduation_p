using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : BaseUI
{
    private VisualElement _root;

    private Label _title;
    private Label _start;
    private Label _setting;
    private Label _exit;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.AddUI(this);

        if (visualTreeAsset != null)
        {
            _root = visualTreeAsset.CloneTree();
            _root.style.flexGrow = 1;
        }
        else
            Debug.LogError("Main Asset이 설정되지 않았습니다.");

    }

    private void Start()
    {
    }
    private void OnEnable()
    {
        UIManager.Instance.ShowUI<MainUI>();
    }



    public override void Close()
    {
        _title.AddToClassList("disappear");
        _start.AddToClassList("disappear");
        _setting.AddToClassList("disappear");
        _exit.AddToClassList("disappear");
    }

    public override void Open()
    {
        if (_root != null)
        {
            root.Q("container").Add(_root);

            // VisualElements를 가져오기
            _title = _root.Q<Label>("title");
            _start = _root.Q<Label>("start");
            _setting = _root.Q<Label>("setting");
            _exit = _root.Q<Label>("exit");

            // 클릭 이벤트 등록
            _start?.RegisterCallback<ClickEvent>(evt =>
            {
                //Fade
                Close();


            });

            _setting?.RegisterCallback<ClickEvent>(evt =>
            {
                Close();
                UIManager.Instance.ShowUI<SettingUI>();
            });

            _exit?.RegisterCallback<ClickEvent>(evt =>
            {
                Debug.Log("Exit 버튼 클릭됨!");
            });

        }
        else
            Debug.LogError("Root VisualElement가 설정되지 않았습니다.");

        StartCoroutine(AddClass());

    }

    private IEnumerator AddClass()
    {
        yield return new WaitForSeconds(.1f);

        _title.RemoveFromClassList("disappear");
        _start.RemoveFromClassList("disappear");
        _setting.RemoveFromClassList("disappear");
        _exit.RemoveFromClassList("disappear");
    }
}
