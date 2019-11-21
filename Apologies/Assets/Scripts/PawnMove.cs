using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    public Color selected;
    public Color unSelected;
    public int pawnNumber;
    static int movedPawnNumber = -1;
    static bool selectionMade = false;
    GameObject manager;
    int moveBy;
    GameObject moveTo;
    GameObject selectBoard;
    bool start = true;
    public char color;
    float timer = 0.0f;
    const int yellow =  0;
    const int green =   1;
    const int red =     2;
    const int blue =    3;
    // Start is called before the first frame update
    void Start()
    {
        currentID = -1;
        manager = GameObject.FindWithTag("Manager");
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<PawnMove>() != null)
            {
                if (manager != null)
                {
                    if (manager.GetComponent<GameManager>().turn == yellow && color == 'y' || manager.GetComponent<GameManager>().turn == green && color == 'g' || manager.GetComponent<GameManager>().turn == red && color == 'r' || manager.GetComponent<GameManager>().turn == blue && color == 'b')
                    {
                        if (moveBy != 0 && !selectionMade)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                movedPawnNumber = pawnNumber;
                                selectBoard = manager.GetComponent<GameManager>().board_[findId(moveBy)];
                                lightBoard();
                            }
                        }
                    }
                }
            }
        }
    }
    int findId(int i)
    {
        int id = currentID;
        if (start)
        {
            switch (color)
            {
                case 'y':
                    id = 15 * yellow + 2 + 1;
                    break;
                case 'g':
                    id = 15 * green + 2 + 1;
                    break;
                case 'r':
                    id = 15 * red + 2 + 1;
                    break;
                case 'b':
                    id = 15 * blue + 2 + 1;
                    break;
            }
        }
            int use = -1;
        switch (color)
        {
            case 'y':
                use = yellow;
                break;
            case 'g':
                use = green;
                break;
            case 'r':
                use = red;
                break;
            case 'b':
                use = blue;
                break;
        }
        if (id > 60 - 1 + 6 * use)
            return (id + i < 60 + 6 * (use + 1)) ? id + i : id;
        else if ((id + i + 60 - (15 * use - 2) - 1) % 60 < (id + 60 - (15 * use - 2) - 1) % 60)
            return ((id + i) % 60 + 60 - (15 * use + 2) + 6 * use - 1);
        return (id + i) % 60;
    }
    void lightBoard()
    {
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if(manager.GetComponent<GameManager>().board_[j] != null)
                manager.GetComponent<GameManager>().board_[j].GetComponent<SpriteRenderer>().color = unSelected;
        }
        if (selectBoard.GetComponent<SpriteRenderer>() == null)
            Debug.Log("):");
        else
            selectBoard.GetComponent<SpriteRenderer>().color = selected;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movedPawnNumber == pawnNumber)
            {
                //  if((moveBy + currentID) % 60 < moveBy % 60)
                //      Debug.Log(moveBy + currentID);
                moveTo = selectBoard;
                selectBoard = null;
                timer = 1.5f;
                if (start)  start = false;
                selectionMade = true;
            }
            else
            {
                moveBy = 0;
            }
        }
        if(moveBy == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                moveBy = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                moveBy = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                moveBy = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                moveBy = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                moveBy = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                moveBy = 9;
            }
        }
        if (moveTo != null && movedPawnNumber > -1 && selectionMade)
        {
            moveTo.GetComponent<SpriteRenderer>().color = unSelected;
            if (timer > 1.2f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.25f, 0.3f);
            }
            else if (timer > 0.9f)
            {
                float tempId = moveTo.GetComponent<Square>().squareID;
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z + (Random.value - 0.5f) / ((tempId % 15 != 5 || tempId < 65) ? 4 : 1), 0.3f);
                LeanTween.moveLocalX(gameObject, moveTo.transform.position.x + (Random.value - 0.5f) / ((tempId % 15 != 5 || tempId < 65) ? 4 : 1), 0.3f);
            }
            else if (timer > 0.6f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 0.25f, 0.3f);
            }
            else
            {
                timer = 0.0f;
                currentID = moveTo.GetComponent<Square>().squareID;
                moveBy = 0;
                moveTo = null;
                movedPawnNumber = -1;
                selectionMade = false;
                manager.GetComponent<GameManager>().turn = ++manager.GetComponent<GameManager>().turn % 4;
            }
            timer -= Time.deltaTime;
        }
    }
}
