using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn = 0;
    public GameObject[] board_ = new GameObject[60+4*7];
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] boards = GameObject.FindGameObjectsWithTag("Board");
        for(int i = 0; i < 60; i++)
        {
            foreach (GameObject board in boards)
            {
                if (board.GetComponent<Square>().squareID == i)
                {
                    switch(board.GetComponent<Square>().safeZone)
                    {
                        case 'y':
                            for (int j = 0; j < 7; j++)
                            {
                                if (board.GetComponent<Square>().squareID == j + i)
                                    board_[i + j] = board;
                            }
                            break;
                        case 'g':
                            for (int j = 0; j < 7; j++)
                            {
                                if (board.GetComponent<Square>().squareID == j + i)
                                    board_[i + j + 7] = board;
                            }
                            break;
                        case 'r':
                            for (int j = 0; j < 7; j++)
                            {
                                if (board.GetComponent<Square>().squareID == j + i)
                                    board_[i + j + 14] = board;
                            }
                            break;
                        case 'b':
                            for (int j = 0; j < 7; j++)
                            {
                                if (board.GetComponent<Square>().squareID == j + i)
                                    board_[i + j + 21] = board;
                            }
                            break;
                        default:
                            board_[i] = board;
                            break;
                    }
                }
            }
        }
        int id = 0;
        GameObject[] pawns = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pawn in pawns)
        {
            pawn.GetComponent<PawnMove>().pawnNumber = id++;
        }
    }
}
