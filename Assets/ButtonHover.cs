using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TMP_Text texto;

    [SerializeField] private Color colorNormal = Color.yellow;
    [SerializeField] private Color colorHover = new Color(1f, 0.5f, 0f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.color = colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.color = colorNormal;
    }

    public void OnSelect(BaseEventData eventData)
    {
        texto.color = colorHover;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        texto.color = colorNormal;
    }
}