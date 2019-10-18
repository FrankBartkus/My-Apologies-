using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tictactoe : MonoBehaviour
{
    [SerializeField] Vector2Int ID = Vector2Int.zero;

    [SerializeField] GameObject GameManager = null;
    
    [SerializeField] GameObject TextObject = null;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        var tictactoe = FindObjectOfType<tictaktoemanager>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /// <summary>
    /// when the button is pressed execute this code
    /// </summary>
    public void ButtonPress()
    {
        var relaycheck = FindObjectOfType<tictaktoemanager>();

        if (GameManager)
        {
            bool Xturn = FindObjectOfType<tictaktoemanager>().xturn;
            value = GameManager.GetComponent<tictaktoemanager>().setSpace(ID);
            print(value);
            
            if (value == 0)
            {
                TextObject.GetComponent<Text>().text = "O";
                relaycheck.relayY++;
                Debug.Log(relaycheck.relayY);
            }
            if (value == 1)
            {
                TextObject.GetComponent<Text>().text = "X";
                relaycheck.relayX++;
                Debug.Log(relaycheck.relayX);
            }


        }
        else print("Game Manager not set on object \"" + gameObject.name + "\"");
    }
}
