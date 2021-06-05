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
        GameObject boardHexPanel = GameObject.Find("/MainGameUI/Panel/BoardHexPanel");
        GameObject boardButtonBuild = GameObject.Find("/MainGameUI/Panel/BoardHexPanel/Button Build");
        GameObject boardButtonBuildHouse= GameObject.Find("/MainGameUI/Panel/BoardHexPanel/Button BuildHouse");
        GameObject boardButtonCreate = GameObject.Find("/MainGameUI/Panel/BoardHexPanel/Button Create");
        Transform board = GameObject.Find("Board").transform;

        boardButtonCreate.SetActive(false);
        boardButtonBuild.SetActive(false);
        boardButtonBuildHouse.SetActive(false);

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
                    boardButtonCreate.SetActive(true);
                }
                if (tmpHex.fieldType == FieldType.grass && gameManager.currentPlayerId == 2)
                {
                    boardButtonBuild.SetActive(true);
                }
                if (tmpHex.fieldType == FieldType.grass && gameManager.currentPlayerId == 1)
                {
                    boardButtonBuildHouse.SetActive(true);
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
