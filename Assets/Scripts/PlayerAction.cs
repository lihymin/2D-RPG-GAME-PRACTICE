using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameManager manager;
    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizontal;
    bool isVertical;
    Vector3 dirVec;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Check Button Down & Up
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if(hDown) {
            isHorizontal = true;
        }

        else if(vDown) {
            isHorizontal = false;
        }

        else if(hUp || vUp) {
            isHorizontal = h != 0;
        }

        //Animation
        if(anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }

        else if(anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }

        else {
            anim.SetBool("isChange", false);
        }

        //Direction
        if(vDown && v == 1) {
            dirVec = Vector3.up;
        }

        else if(vDown && v == -1) {
            dirVec = Vector3.down;
        }

        else if(hDown && h == 1) {
            dirVec = Vector3.right;
        }

        else if(hDown && h == -1) {
            dirVec = Vector3.left;
        }

        //Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null) {
            manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizontal ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        }

        else {
            scanObject = null;
        }
    }
}
