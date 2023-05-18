using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Player{
  Red,Blue,Green,Yellow
}
public class Board
{
    Dictionary<Player,int> PlayerPos;
    int[] Ladders;
public Board(Dictionary<int,int> joints)
{
PlayerPos = new Dictionary<Player, int>();
Ladders = new int[100];

for(int i = 0; i < 4; i++)
{
    
    PlayerPos[(Player)i] = 0;
 
}

for(int i = 0; i < 100; i++)
{
Ladders[i] = -1;
}

foreach(KeyValuePair<int,int> joint in joints)
{
    Ladders[joint.Key] = joint.Value;
}
}


public List<int> UpdateBoard(Player player,int roll)
{
List<int> result = new List<int>();
for(int i = 0; i < roll;i++)
{
     PlayerPos[player] += 1;
     result.Add(PlayerPos[player]);

}
if(result[result.Count - 1] > 99)
{
    PlayerPos[player] -= roll;
    return new List<int>();
}

if(Ladders[result[result.Count - 1]] != -1)
{
  PlayerPos[player] = Ladders[result[result.Count - 1]];
  result.Add(PlayerPos[player]);
}

return result;
}


}
