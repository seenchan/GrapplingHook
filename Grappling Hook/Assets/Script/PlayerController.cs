using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public GameObject gunPoint;
    public GameObject bullet;
    public float moveSpeed = 750f;
    public float speedLimit = 8f;
    public float reelingSpeed = 0.5f;
    public float rotateSpeed = 10f;

    private Rigidbody2D playerRB;
    private DistanceJoint2D rope;
    private GameObject hook;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        rope = this.GetComponent<DistanceJoint2D>();

        rope.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        AimToMouse();
        ShootControl();
        MoveControl();
        CheckHook();
        SlowMoControl();
    }

    void MoveControl()
    {
        //Move Player
        if (Input.GetKey(KeyCode.D))
        {
            if (playerRB.velocity.x <= speedLimit)
            {
                playerRB.AddForce(Vector2.right * moveSpeed);
            }
            else if (GameManager.Instance.hookAnchored == true)
            {
                playerRB.AddForce(Vector2.right * moveSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (playerRB.velocity.x >= -speedLimit)
            {
                playerRB.AddForce(Vector2.left * moveSpeed);
            }
            else if (GameManager.Instance.hookAnchored == true)
            {
                playerRB.AddForce(Vector2.left * moveSpeed);
            }
        }
        

        if (Input.GetKey(KeyCode.W) && GameManager.Instance.isBulletExist == true)
        {
            reel(-reelingSpeed / 100);
        }
        else if (Input.GetKey(KeyCode.S) && GameManager.Instance.isBulletExist == true)
        {
            reel(reelingSpeed / 100);
        }
    }
    
    void SlowMoControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Time.timeScale = GameManager.Instance.sloMoSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Time.timeScale = 1f;
        }
    }

    void AimToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        Vector3 relPos = ray.origin - this.transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, relPos);
        player.transform.rotation = rotation;
    }

    void reel(float reelSpeed)
    {
        rope.autoConfigureDistance = false;
        rope.distance = rope.distance + reelSpeed;
    }

    void ShootControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance.isBulletExist == false)
            {
                Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
                GameManager.Instance.isBulletExist = true;
                rope.autoConfigureDistance = true;
            }
        }
        if (Input.GetMouseButton(1))
        {
            GameManager.Instance.isBulletExist = false;
            Destroy(GameObject.FindGameObjectWithTag("Hook"));

            DeactivateRope();
            GameManager.Instance.hookAnchored = false;
        }
    }

    void CheckHook()
    {
        if(GameManager.Instance.hookAnchored == true)
        {
            ActivateRope();
        }
    }
    
    void ActivateRope()
    {
        hook = GameObject.FindGameObjectWithTag("Hook");
        rope.enabled = true;
        rope.connectedBody = hook.GetComponent<Rigidbody2D>();
        rope.enableCollision = true;
    }
    void DeactivateRope()
    {
        rope.enabled = false;
    }
}
