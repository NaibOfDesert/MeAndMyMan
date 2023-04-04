using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Image))]   
public class MenuUiTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IMenuUi
{
    [SerializeField] private Sprite tabBlockSprite;
    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;
    [SerializeField] private Sprite tabActiveSprite;

    private Image tabBackgroundImage;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    bool isAble; 

    GameController gameController; 
    MenuUiTabController menuUiTabController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        menuUiTabController = gameController.MenuUiTabController; 
        tabBackgroundImage = GetComponent<Image>();
    }


    void Update()
    {
        // to check is able to click
    }
    void Start()
    {
        isAble = true;
        menuUiTabController.AddToUiList(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isAble) menuUiTabController.OnTabSelected(this); 

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuUiTabController.OnTabEnter(this);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuUiTabController.OnTabExit(this);
    }

    private void IsAbleCheck()
    {
        // if(isAble) ? isAble == true : isAble == false;
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
