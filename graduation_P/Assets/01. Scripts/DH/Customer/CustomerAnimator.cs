using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer;

    private void Awake()
    {
        if (renderer == null)
            renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Enter()
    {
        renderer.enabled = true;
    }

    public void Exit()
    {
        renderer.enabled = false;
    }
}
