using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public List<Color> Colors;

    bool CanMove;
    int MoveIndex;
    List<int> MovePos;
    float Speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        CanMove = false;
        MoveIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!CanMove) return;
        float step = Speed * Time.deltaTime;
        Vector3 targetPos = GameManager.instance.Position[MovePos[MoveIndex]];
        transform.position = Vector3.MoveTowards(transform.position,targetPos,step);
        if(Vector3.Distance(transform.position,targetPos) < 0.001f)
        {
            MoveIndex ++;
            if(MoveIndex == MovePos.Count)
            {
                MoveIndex = 0;
                CanMove = false;
            }
        } 
    }

    public void SetColors(Player player)
    {

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Colors[(int)player];
    }

    public void SetMovement(List<int> result)
    {
MovePos = result;
CanMove = true;

    }
}
