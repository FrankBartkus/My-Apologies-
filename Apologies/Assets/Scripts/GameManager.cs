using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn = 0;
    public GameObject[,] board_ = new GameObject[60,7];
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
                    if(board.GetComponent<Square>().safeZone == ' ')
                    {
                        board_[i, 0] = board;
                    }
                    else
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            if (board.GetComponent<Square>().safeZoneID == j)
                                board_[i, j] = board;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
