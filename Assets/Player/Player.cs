using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    public int Id { get; }
    public string Nick { get; }
    public PlayerType PlayerType { get; }

    public Player(int __id, string __nick, PlayerType __playerType)
    {
        Id = __id;
        Nick = __nick;
        PlayerType = __playerType;
    }

}
