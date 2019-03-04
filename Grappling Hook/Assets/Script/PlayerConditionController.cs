using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Lethal")
        {
            GameManager.Instance.restartGame = true;
        }
        if (col.tag == "Finish")
        {
            int i;
            GameManager.Instance.winGame = true;
            if (int.TryParse(col.name, out i))
            {
                GameManager.Instance.chosenLevel = i;
            }
        }
    }
}
