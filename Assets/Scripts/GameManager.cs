using System.Collections;
using System.Collections;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool HasGameFinished, CanClick;

    public static GameManager instance;

    int roll;

    public GameObject gamePiece;
    public Vector3 startPos;
    public int Number_Player;

    public Board MyBoard;
    List<Player> Players;
    int CurrentPlayer;

    public Vector3[] Position;

    Dictionary<int, int> joints;

    Dictionary<Player, GameObject> pieces;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CanClick = true;
        HasGameFinished = false;
        CurrentPlayer = 0;


        SetUpPositions();
        SetUpLadders();


        MyBoard = new Board(joints);
        Players = new List<Player>();
        pieces = new Dictionary<Player, GameObject>();

        for (int i = 0; i < Number_Player; i++)
        {
            Players.Add((Player)i);
            GameObject temp = Instantiate(gamePiece);
            pieces[(Player)i] = temp;
            temp.transform.position = startPos;
            temp.GetComponent<Piece>().SetColors((Player)i);
        }
    }



    void SetUpPositions()
    {
        Position = new Vector3[100];
        float diff = 0.45f;
        Position[0] = startPos;
        int index = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Position[index] = new Vector3(Position[index - 1].x + diff, Position[index - 1].y, Position[index - 1].z);
                index++;
            }

            Position[index] = new Vector3(Position[index - 1].x, Position[index - 1].y + diff, Position[index - 1].z);
            index++;

            for (int j = 0; j < 9; j++)
            {
                Position[index] = new Vector3(Position[index - 1].x - diff, Position[index - 1].y, Position[index - 1].z);
                index++;
            }
            if (index == 100) return;
            Position[index] = new Vector3(Position[index - 1].x, Position[index - 1].y + diff, Position[index - 1].z);
            index++;

        }
    }
    void SetUpLadders()
    {
        joints = new Dictionary<int, int> {
{1,22},
{7,33},
{19,76},
{28,8},
{31,67},
{37,14},
{40,78},
{46,4},
{52,32},
{61,36},
{73,97},
{81,9},
{84,94},
{85,53},
{91,69},
{96,24}
    };
    }


    // Update is called once per frame
    void Update()
    {
        if (HasGameFinished || CanClick)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector3(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider) return;

            if (hit.collider.CompareTag("Die"))
            {
                print("Click");
                roll = 1 + Random.Range(0, 6);
                hit.collider.gameObject.GetComponent<Die>().Roll(roll);
                CanClick = false;
                print("Click");
            }
        }
    }

    public void MovePiece(int temp)
    {
        roll = temp;
        List<int> result = MyBoard.UpdateBoard(Players[CurrentPlayer], roll);

        if (result.Count == 0)
        {
            CanClick = true;
            CurrentPlayer = (CurrentPlayer + 1) % Players.Count;
            return;
        }

        pieces[Players[CurrentPlayer]].GetComponent<Piece>().SetMovement(result);
        CanClick = true;
        if (result[result.Count - 1] == 99)
        {
            Players.RemoveAt(CurrentPlayer);
            CurrentPlayer %= CurrentPlayer;
            if (Players.Count == 1) HasGameFinished = true;
            return;
        }

        CurrentPlayer = roll == 6 ? CurrentPlayer : (CurrentPlayer + 1) % Players.Count;
    }
}
