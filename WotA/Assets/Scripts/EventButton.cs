using UnityEngine;
using UnityEngine.EventSystems;

public class EventButton: MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Button clicked!");
        // Здесь вы можете вызвать ивент или выполнить действие
    }
}
