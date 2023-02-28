    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]   
public class MenuUiTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler 
{
    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;
    [SerializeField] private Sprite tabActiveSprite;

    private Image tabBackgroundImage;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected; 

    MenuUiController menuUiController;

    void Awake()
    {
        menuUiController = FindObjectOfType<MenuUiController>(); 
        tabBackgroundImage = GetComponent<Image>();
    }

    void Update()
    {
        
    }
    void Start()
    {
        menuUiController.AddToTabList(this);

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

    public void SetTabIdleSprite()
    {
        tabBackgroundImage.sprite = tabIdleSprite; 
    }
    public void SetTabHoverSprite()
    {
        tabBackgroundImage.sprite = tabHoverSprite;

    }
    public void SetTabActiveSprite()
    {
        tabBackgroundImage.sprite = tabActiveSprite;

    }

    public void Select()
    {
        if(onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
