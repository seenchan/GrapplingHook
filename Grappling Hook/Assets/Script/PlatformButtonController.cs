using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonController : MonoBehaviour
{

    public GameObject connectedObj;

    [Header("3 = Rotate Counter Clockwise")]
    [Header("2 = Rotate Clockwise")]
    [Header("1 = appear/disappear")]
    [Header("Behaviour Options List")]
    public int behaviourOption;


    // Start is called before the first frame update
    void Start()
    {
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D col)
    {
        TurnOn();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        TurnOff();
    }

    void TurnOn()
    {
        this.GetComponent<SpriteRenderer>().color = Color.green;
        BehaviourOptions(behaviourOption, true);
    }
    void TurnOff()
    {
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        BehaviourOptions(behaviourOption, false);
    }

    void BehaviourOptions(int choice, bool Active)
    {
        if (choice == 1)
        {
            if (Active == true)
            {
                connectedObj.SetActive(true);
            }
            else if (Active == false)
            {
                connectedObj.SetActive(false);
            }
        }
        else if (choice == 2)
        {
            if (Active == true)
            {
                connectedObj.transform.Rotate(Vector3.back);
            }
            else if (Active == false)
            {

            }
        }
        else if (choice == 3)
        {
            if (Active == true)
            {
                connectedObj.transform.Rotate(Vector3.forward);
            }
            else if (Active == false)
            {

            }
        }
    }
}
