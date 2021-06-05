using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardOnClick : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        
        Vector3 mousePosition = Input.mousePosition;
        GameObject boardHexPanel = GameObject.Find("/MainGameUI/Panel");
        GameObject boardButtonBuild = GameObject.Find("/MainGameUI/Panel/Button Build");
        GameObject boardButtonBuildHouse= GameObject.Find("/MainGameUI/Panel/Button BuildHouse");
        GameObject boardButtonBuildMine = GameObject.Find("/MainGameUI/Panel/Button BuildMine");
        GameObject boardButtonUpgradeHouse = GameObject.Find("/MainGameUI/Panel/Button UpgradeHouse");
        GameObject boardButtonCut = GameObject.Find("/MainGameUI/Panel/Button Cut");
        GameObject boardButtonCreate = GameObject.Find("/MainGameUI/Panel/Button Create");

        Transform board = GameObject.Find("Board").transform;

        boardButtonCreate.SetActive(false);
        boardButtonBuild.SetActive(false);
        boardButtonBuildHouse.SetActive(false);
        boardButtonBuildMine.SetActive(false);
        boardButtonUpgradeHouse.SetActive(false);
        boardButtonCut.SetActive(false);


        if (IsPointerOverUIElement())
        {
            var tmpBoardHex = name.Split('[', ']')[1].Split('.');
            int hexPosition = int.Parse(tmpBoardHex[0]);

            GameManagerAction availableAction = gameManager.GetAvailableAction(hexPosition);

            Field tmpHex = gameManager.fieldsList.Where(f => f.fieldId == hexPosition).FirstOrDefault();

            gameManager.onClickField = tmpHex;

            if (tmpHex.isActive == true) { 
                if (tmpHex.fieldId == 64 && gameManager.currentPlayerId == 0)
                {
                    if(gameManager.amountOfCitizens >= 200 && gameManager.amountOfFood >= 200 && gameManager.amountOfGold >= 200 && gameManager.amountOfStone >= 200 && gameManager.amountOfWood >= 200)
                    boardButtonCreate.SetActive(true);
                }
                if ((tmpHex.fieldType == FieldType.grass || tmpHex.fieldType == FieldType.forestcut) && gameManager.currentPlayerId == 1)
                {
                    boardButtonBuildHouse.SetActive(true);
                }
                if (tmpHex.fieldType == FieldType.mountain && gameManager.currentPlayerId == 1)
                {
                    boardButtonBuildMine.SetActive(true);
                }
                if (tmpHex.fieldType == FieldType.house && gameManager.currentPlayerId == 1)
                {
                    boardButtonUpgradeHouse.SetActive(true);
                }
                if ((tmpHex.fieldType == FieldType.grass || tmpHex.fieldType == FieldType.forestcut) && gameManager.currentPlayerId == 2)
                {
                    boardButtonBuild.SetActive(true);
                }
                if (tmpHex.fieldType == FieldType.forest && gameManager.currentPlayerId == 2)
                {
                    boardButtonCut.SetActive(true);
                }
            }
        }
    }

    private static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }

        return false;
    }

    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
