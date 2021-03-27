using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Vector2 movement;
    Vector2 mousePos;
    bool isInteracting = false;
    GameObject interactable = null;
    Animator anim;

    public Rigidbody2D rb;
    public Camera cam;
    public LayerMask layerMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement.magnitude != 0f)
        {
            anim.SetBool("moving", true);
            anim.SetFloat("x_velocity", movement.x);
            anim.SetFloat("y_velocity", movement.y);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;


        RaycastHit2D hit;
        hit = Physics2D.Raycast(rb.position, lookDir, 3f, LayerMask.GetMask("interactables"));
        if (hit.collider != null)
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this.gameObject);
            }
        }

    }
}
