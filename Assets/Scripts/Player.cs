using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IBeingSick
{
    public float moveSpeed = 5f;
    public Text deseaseLabel;

    Vector2 movement;
    Vector2 mousePos;
    bool isSick = false;
    public float deseaseLevel = 0f;
    float timerForNewVirus = 0f;
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


        RaycastHit2D hit;
        hit = Physics2D.Raycast(rb.position, lookDir, 3f, LayerMask.GetMask("interactables"));
        Debug.DrawLine(transform.position, hit.point, Color.green);
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

    public void gettingSick()
    {
        if (!isSick)
        {
            isSick = true;
            StartCoroutine("beingSick");
        }
    }

    public void Cured()
    {
        isSick = false;
        StopCoroutine("beingSick");
        deseaseLevel = 0f;
        deseaseLabel.text = deseaseLevel.ToString() + "%";
    }

    public void spawnVirus()
    {
        GameManager.instance.SpawnVirus(transform.position);
    }

    public void Quarantine()
    {
        Debug.Log("You are sick. You are not able to play!");
    }

    IEnumerator beingSick()
    {
        while (deseaseLevel <= 95)
        {
            deseaseLevel += 5f;
            deseaseLabel.text = deseaseLevel.ToString() + "%";
            if (Random.value < 0.5)
                spawnVirus();
            yield return new WaitForSeconds(2);
        }
        if (isSick)
            Quarantine();
    }
}

