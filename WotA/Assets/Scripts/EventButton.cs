using UnityEngine;
using UnityEngine.EventSystems;

public class EventButton: MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked!");
        // ����� �� ������ ������� ����� ��� ��������� ��������
    }
}
