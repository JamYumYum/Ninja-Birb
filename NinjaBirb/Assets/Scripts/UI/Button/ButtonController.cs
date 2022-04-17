using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonAction action = () => Debug.Log("clicked");
    
    private Button button;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.image.alphaHitTestMinimumThreshold = 0.5f;
        //button.onClick.AddListener(() =>{ action(); });
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        action();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public delegate void ButtonAction();
    
}
