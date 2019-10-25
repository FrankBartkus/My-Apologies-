using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    public Color selected;
    public Color unSelected;
    public int pawnNumber;
    int moveBy;
    GameObject moveTo;
    bool start = true;
    public char color;
    float timer = 0.0f;
    static GameObject selection;
    static bool selectionMade = false;
    // Start is called before the first frame update
    void Start()
    {
        currentID = -1;
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<PawnMove>() != null)
            {
                GameObject manager = GameObject.FindWithTag("Manager");
                if (manager != null)
                {
                    if (manager.GetComponent<GameManager>().turn == 0 && color == 'y' || manager.GetComponent<GameManager>().turn == 1 && color == 'g' || manager.GetComponent<GameManager>().turn == 2 && color == 'r' || manager.GetComponent<GameManager>().turn == 3 && color == 'b')
                    {
                        if (moveBy != 0)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                selection = gameObject;
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
        int id = (currentID + i) % 60;
        if (start)
        {
            start = false;
            switch (color)
            {
                case 'y':
                    id = 4 + i;
                    break;
                case 'g':
                    id = 19 + i;
                    break;
                case 'r':
                    id = 24 + i;
                    break;
                case 'b':
                    id = 49 + i;
                    break;
            }
        }
        return id;
    }
    void setMoveTo()
    {
        moveTo = GameObject.FindWithTag("Manager").GetComponent<GameManager>().board_[findId(moveBy), 0];
        timer = 1.5f;
    }
    void lightBoard()
    {
        for (int j = 0; j < 60; j++)
        {
            GameObject.FindWithTag("Manager").GetComponent<GameManager>().board_[j, 0].GetComponent<SpriteRenderer>().color = unSelected;
        }
        GameObject.FindWithTag("Manager").GetComponent<GameManager>().board_[findId(moveBy), 0].GetComponent<SpriteRenderer>().color = selected;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selection != null)
            {
                setMoveTo();
                selectionMade = true;
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
        }
        if (moveTo != null && selection == gameObject && selectionMade)
        {
            moveTo.GetComponent<SpriteRenderer>().color = unSelected;
            if (timer > 1.2f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.25f, 0.3f);
            }
            else if (timer > 0.9f)
            {
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z + (Random.value - 0.5f) / 4, 0.3f);
                LeanTween.moveLocalX(gameObject, moveTo.transform.position.x + (Random.value - 0.5f) / 4, 0.3f);
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
                selection = null;
                selectionMade = false;
                return;
            }
            timer -= Time.deltaTime;
        }
    }
}
