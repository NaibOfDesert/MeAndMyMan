using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUiSection : MonoBehaviour, IMenuUi
{
    GameController gameController;
    MenuUiSectionController menuUiSectionController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        menuUiSectionController = gameController.MenuUiSectionController;

    }

    public void Start()
    {
        Debug.Log(menuUiSectionController == null);

        menuUiSectionController.AddToUiList(this);

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void SetSectionAble()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

}
