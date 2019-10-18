using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tictaktoemanager : MonoBehaviour
{
    
    public bool xturn = true;
    public int[,] grid = new int[3, 3]{
                                       {-1,-1,-1 },
                                       {-1,-1,-1 },
                                       {-1,-1,-1 }
                                      };
    public int relayX;
    public int relayY;

    public GameObject TextObjext;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("kjld;jlkj;j");
            Resetgrid();
        }
    }
    void relay()
    {
        if(relayX > 3)
        {

        }
    }
    public void selected()
    {
       
      
    }
    
    /// <summary>
    /// checks if a row/collum or cross of x's are taged or y's
    /// </summary>
    /// <param name="id"></param>
    public void Wincheck(Vector2Int id)
    {
        if (grid[0, 2] == 1 && grid[1, 1] == 1 && grid[2, 0] == 1|| grid[0, 0] == 1 && grid[1, 1] == 1 && grid[2, 2] == 1)
        {
            Debug.Log("x wins!!!");
        }
        
        if (grid[0, 2] == 0 && grid[1, 1] == 0 && grid[2, 0] == 0|| grid[0, 0] == 0 && grid[1, 1] == 0 && grid[2, 2] == 0)
        {
            Debug.Log("y wins!!!");
        }
        
        if (grid[0, 2] == 1 && grid[0, 1] == 1 && grid[0, 0] == 1|| grid[1, 2] == 1 && grid[1, 1] == 1 && grid[1, 0] == 1|| grid[2, 2] == 1 && grid[2, 1] == 1 && grid[2, 0] == 1)
        {
            Debug.Log("x wins!!!");
        }
        
        if (grid[0, 2] == 0 && grid[0, 1] == 0 && grid[0, 0] == 0|| grid[1, 2] == 0 && grid[1, 1] == 0 && grid[1, 0] == 0|| grid[2, 2] == 0 && grid[2, 1] == 0 && grid[2, 0] == 0)
        {
            Debug.Log("y wins!!!");
        }

        if (grid[0, 0] == 1 && grid[1, 0] == 1 && grid[2, 0] == 1 || grid[0, 1] == 1 && grid[1, 1] == 1 && grid[2, 1] == 1 || grid[0, 2] == 1 && grid[1, 2] == 1 && grid[2, 2] == 1)
        {
            Debug.Log("x wins!!!");
        }

        if (grid[0, 0] == 0 && grid[1, 0] == 0 && grid[2, 0] == 0 || grid[0, 1] == 0 && grid[1, 1] == 0 && grid[2, 1] == 0 || grid[0, 2] == 0 && grid[1, 2] == 0 && grid[2, 2] == 0)
        {
            Debug.Log("y wins!!!");
        }

    }
    /// <summary>
    /// goes back to previous level
    /// </summary>
    void Resetgrid()
    {
        var textComponents = Component.FindObjectsOfType<Text>();
        foreach (var component in textComponents)
        {
            component.text = (" ");
        }
        grid = new int[3, 3]{
                                       {-1,-1,-1 },
                                       {-1,-1,-1 },
                                       {-1,-1,-1 }
                                      };

        xturn = true;
    }
    /// <summary>
    /// checks and sets the id's of the tic tac toe buttons
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int setSpace(Vector2Int id)
    {
        if (grid[id.x, id.y] == -1)
        {
            grid[id.x, id.y] = (xturn ? 1 : 0);
            xturn = !xturn;
            Wincheck(id);
            return grid[id.x, id.y];

        }
        else return -1;
    }
    
}
