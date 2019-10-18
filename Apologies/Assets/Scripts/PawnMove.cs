using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    GameObject moveTo;
    bool start = true;
    public char color;
    float timer = 0.0f;
    bool isSelected = false;
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
            if (hit.transform.tag == "Player")
            {
                GameObject manager = GameObject.FindWithTag("Manager");
                if (manager != null)
                {
                    if (manager.GetComponent<GameManager>().turn == 0 && color == 'y' || manager.GetComponent<GameManager>().turn == 1 && color == 'g' || manager.GetComponent<GameManager>().turn == 2 && color == 'r' || manager.GetComponent<GameManager>().turn == 3 && color == 'b')
                    {
                        if (moveTo != null)
                        {
                            isSelected = true;
                            Debug.Log("Worked");
                        }
                    }
                }
            }
        }
    }
    int move(int i)
    {
        int id = (currentID + i) % 60;
        if (moveTo == null)
        {
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
            moveTo = GameObject.FindWithTag("Manager").GetComponent<GameManager>().board_[id, 0];
            timer = 1.5f;
            return id;
        }
        return currentID % 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentID = move(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentID = move(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentID = move(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentID = move(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentID = move(5);
        }
        if (moveTo != null && isSelected)
        {
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
                moveTo = null;
                isSelected = false;
                return;
            }
            timer -= Time.deltaTime;
        }
    }
}
