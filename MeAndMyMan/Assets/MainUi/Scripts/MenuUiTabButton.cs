using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Linq;

[RequireComponent(typeof(Image))]   
public class MenuUiTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite tabBlockSprite;
    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;
    [SerializeField] private Sprite tabActiveSprite;

    [Header("Values")]
    // [SerializeField] private MenuUiTabState menuUiTabState;
    // [SerializeField] private MenuUiSectionState menuUiSectionState;
    // [SerializeField] private List<string> menuUiStatesList; 
    // public List<string> MenuUiStatesList { get {return menuUiStatesList;} }
    // private ObjectType? dependenceObjectType; 
    [SerializeField] bool isAble; 
    // [SerializeField] bool isMouseOver;
    private Image tabBackgroundImage;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    GameController gameController; 
    GameManager gameManager; 
    GameUiController gameUiController; 
    GameUiMenuController gameUiMenuController; 
    MenuUiTabController menuUiTabController;

    void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        gameManager  = gameController.GameManager; 
        gameUiController = gameController.GameUiController; 
        gameUiMenuController = gameController.GameUiMenuController; 
        menuUiTabController = gameController.MenuUiTabController; 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

        tabBackgroundImage = GetComponent<Image>();

    }
    void Start()
    {
        // menuUiTabController.AddToUiList(this);

        isAble = false;
        // menuUiStatesList = new List<string>();

        // GetStatesList(menuUiTabState);
        // GetStatesList(menuUiSectionState);
        // GetDependenceObjectType(); 
    }
    
    void Update()
    {
        // if(dependenceObjectType != null) IsAbleCheck();
        // Debug.Log(gameObject.name + isAble + " " + dependenceObjectType);

    }

    #region Pointers
    public void OnPointerClick(PointerEventData eventData)
    {
        if(isAble) {
            // isMouseOver = false;
            menuUiTabController.OnTabSelected(this); 
        }
    }
    private void IsAbleCheck()
    {
        // if(gameManager.CheckBuildInfrastructure(dependenceObjectType, ObjectLevel.Level1))
        // {
        //     isAble = false;
        //     SetTabBlockSprite();
        // }
        // else             
        // {
        //     if(isMouseOver) SetTabHoverSprite(); else SetTabIdleSprite();
        //     isAble = true; 
        // }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isAble) {
            // isMouseOver = true;
            menuUiTabController.OnTabEnter(this);
        }
        // TODO: implement view of resorces to get
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isAble) {
            // isMouseOver = false;
            menuUiTabController.OnTabExit(this);

        }
        // TODO: implement view of resorces to set as default
    }
    #endregion


    #region SpriteControl
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
        public void SetTabBlockSprite()
    {
        tabBackgroundImage.sprite = tabBlockSprite;
    }
    #endregion

    #region Selection
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
    #endregion

    // #region TabStateType
    // private void GetStatesList <T>(T menuUiState)
    // {
    //     var menuUiStateString = menuUiState.ToString(); 
    //     var menuUiStateList = String.Concat(menuUiStateString.Where(l => !char.IsWhiteSpace(l))).Split(",").ToList();
        
    //     foreach(var s in menuUiStateList)
    //     {
    //         menuUiStatesList.Add(s);
    //     }

    //     menuUiStatesList.RemoveAll(s => s == MenuUiSectionState.noneState.ToString() || s == MenuUiTabState.noneState.ToString()); // ?: to monit, can be unusefull
    // }

    // private void GetDependenceObjectType()
    // {
    //     foreach(var p in gameUiMenuController.MenuUiObjectTypeDictionary)
    //     {
    //         dependenceObjectType = menuUiStatesList.Any(s => s == p.Key.ToString()) ? p.Value : null; 
    //     }
    // }
    // #endregion
}
