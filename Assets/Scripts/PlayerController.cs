using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    [SerializeField] private float moveSpeed = 5f;
    private float inverseMoveSpeed;
    private Vector3 newposition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        inverseMoveSpeed = 1 / moveSpeed;
        newposition = transform.position;
    }

    // Move character to position pointed by Mouse
    private void Move(Vector3 end)
    {
        StartCoroutine(SmoothMove(end));

    }

    private IEnumerator SmoothMove(Vector3 end)
    {
        //Debug.Log("move to x: " + end.x + " y: " + end.y);
        Vector3 newpos = transform.position;
        while((end - newpos).sqrMagnitude > float.Epsilon)
        {
            newpos = Vector3.MoveTowards(rigidbody.position, end,
            moveSpeed * Time.deltaTime);
            //Debug.Log("new position: x: " + newpos.x + " y: " + newpos.y);
            rigidbody.position = newpos;
            yield return null;
        }
        
    }
    // Update is called once per frame
    // in update, we detect user's key input (move left and right, interact with objects) and call
    // corresponding functions
    void Update()
    {
        // for moving
        if(Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if(Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log(hit.point.x + " " + hit.point.y);
            //    Move(hit.point);
            //}
            Vector3 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (end != transform.position)
            {
                end.z = transform.position.z;
                Move(end);
            }

        }
    }
}
