using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    GameController gameController;
    BoardController boardController;
    InfrastructureController infrastructureController;

    int experience;

    public GameManager(GameController gameController)
    {
        this.gameController = gameController;
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;

        experience = 0;
    }

    public void CountExpValue()
    {
        foreach(var house in infrastructureController.HouseList)
        {
          //  experience += house.gameObject.ObjectLevel; 
        }

    }


}

