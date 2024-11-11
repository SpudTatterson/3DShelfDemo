using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour, IPointerHandler
{
    [SerializeField] UnityEvent OnClick;
    public void OnPointerClick()
    {
        OnClick.Invoke();
    }
}