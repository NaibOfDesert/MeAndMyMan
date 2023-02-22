using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(Image))]   
public class MenuUiTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler 
{
    MenuUiController menuUiController; 

    private Image tabBackgroundImage;
    public Image TabBackgroundImage { get { return tabBackgroundImage; } set { tabBackgroundImage = value; } }


    void Awake()
    {
        menuUiController = FindObjectOfType<MenuUiController>(); // TODO: fix
        tabBackgroundImage = GetComponent<Image>();
        menuUiController.AddToTabList(this); 
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        menuUiController.OnTabSelected(this); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuUiController.OnTabEnter(this); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuUiController.OnTabExit(this);
    }
}
