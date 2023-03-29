using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuUiSectionController : MonoBehaviour, IMenuUiController
{
    [SerializeField] List<IMenuUi> menuUiSectionList;

    void Start()
    {
        menuUiSectionList = new List<IMenuUi>();
    }


    void Update()
    {
        
    }

  

    public void AddToUiList(IMenuUi menuUiSection)
    {
        menuUiSectionList.Add(menuUiSection);
    }
}
