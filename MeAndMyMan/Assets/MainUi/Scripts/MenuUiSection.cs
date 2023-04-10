using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUiSection : MonoBehaviour
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
        menuUiSectionController.AddToUiList(this);

    }

    public void SetSectionAble()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

}
