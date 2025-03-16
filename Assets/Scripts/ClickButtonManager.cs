using UnityEngine;
using UnityEngine.Events;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButtonController _clickButton;
    [SerializeField] private ClickButtonConfig _buttonConfig;

    public event UnityAction OnClicked;
    public void Initialize()
    {
        _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
    }

    private void ShowClick()
    {
        Debug.Log("Click");
    }
}
