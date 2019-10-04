using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID = 0;
    GameObject moveTo;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject[] boards = GameObject.FindGameObjectsWithTag("board");
            foreach (GameObject board in boards)
            {
                if (board.GetComponent<Square>().squareID == currentID + 1)
                {
                    moveTo = board;
                    timer = 4.0f;
                }
            }
        }
        if(moveTo != null)
        {
            if (timer > 3)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.0f, 1.0f).setEaseOutQuad();
            }
            else if (timer > 2)
            {
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z - transform.position.y, 0.5f).setEaseInOutQuad();
            }
            else if (timer > 1)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 0.25f, 1.0f).setEaseOutQuad();
            }
            else
            {
                timer = 0.0f;
                moveTo = null;
                return;
            }
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }
    }
}
