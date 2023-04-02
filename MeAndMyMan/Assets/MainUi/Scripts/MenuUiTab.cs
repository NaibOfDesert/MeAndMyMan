    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

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
        
    }
    void Start()
    {
        menuUiTabController.AddToUiList(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        menuUiTabController.OnTabSelected(this); 

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuUiTabController.OnTabEnter(this);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuUiTabController.OnTabExit(this);
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
