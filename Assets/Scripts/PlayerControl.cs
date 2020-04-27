using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D rg;
    Animator anim;

    Vector2 movement;

    [Header("状态")]
    public float speed;
    public float xVelocity;
    public float yVelocity;
  

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        SwictchAnima();
    }

    void Move()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        rg.MovePosition(rg.position + movement * speed * Time.fixedDeltaTime);
        
     }
    void SwictchAnima() {
        if (movement != Vector2.zero) {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }
        anim.SetFloat("Speed", movement.magnitude);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Grass":
                SceneManager.LoadScene(2,LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
}
