using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 lastPos;
    private FixedJoint2D anchor;
    private bool rotateActive = false;
    private Vector3 dir;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * GameManager.Instance.bulletSpeed);
        anchor = this.GetComponent<FixedJoint2D>();
        AddLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, GameObject.Find("Player").transform.position);
        if (GameManager.Instance.hookAnchored == false)
        {
            rotateActive = true;
        }
        else if (GameManager.Instance.hookAnchored == true)
        {
            rotateActive = false;
        }
        RotateBullet(rotateActive);
        DrawLine();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && GameManager.Instance.hookAnchored == false)
        {
            Destroy(this.gameObject);
            GameManager.Instance.isBulletExist = false;
        }
        else if (col.gameObject.tag != "Nonstick")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.velocity = new Vector3(0, 0, 0);
            if (GameManager.Instance.hookAnchored == false)
            {
                anchor.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
                anchor.enabled = true;
                GameManager.Instance.hookAnchored = true;
            }
        }
    }

    void RotateBullet(bool active)
    {
        if (active == true)
        {
            dir = this.GetComponent<Rigidbody2D>().GetPointVelocity(dir);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
        else if (active == false)
        {

        }
    }

    void AddLine()
    {
        line = this.GetComponent<LineRenderer>();
        line.positionCount = 2;
    }
    void DrawLine()
    {
        line.SetPosition(0, this.gameObject.transform.position);
        line.SetPosition(1, GameObject.Find("Player").transform.position);
    }
}
