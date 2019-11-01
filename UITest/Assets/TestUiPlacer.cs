using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUiPlacer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Canvas = null;
    public GameObject ButtonRef = null;

    class bar
    {
        Vector2Int id;
        public void OnClick()
        {
            print(id);
        }
    }

    bar barData = new bar();

    void Start()
    {
        for(int x = 0; x < 10; ++x)
        {
            for(int y = 0; y < 10; ++y )
            {
                var newButton = GameObject.Instantiate(ButtonRef, Canvas.transform);
                var foo = newButton.GetComponent<RectTransform>();
                var buttonCmpt = newButton.GetComponent<Button>();
                
                newButton.transform.localPosition += new Vector3(foo.sizeDelta.x * newButton.transform.localScale.x * ((float) x), foo.sizeDelta.y * newButton.transform.localScale.y * ((float)y), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
