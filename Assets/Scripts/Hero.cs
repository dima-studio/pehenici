using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
  public float speed = 3f;
  public int lives = 7;
  public float JumpForce = 15f;
  private bool isGrounded = false;
  private bool m_isInWater = false;
  public AudioSource audio_speed;

  private Rigidbody2D rb;
  private Restart m_lostCheckPoint = null;

  private Animator anim;
  private SpriteRenderer sprite;
  private float m_lastDamageTime = 0f;
  private float m_imortalTime = 1f;


  public Transform m_inventory;


  public static Hero Instance
  {
    get;
    private set;
  }

  private void Awake()
  {
    Instance = this;
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sprite = GetComponentInChildren<SpriteRenderer>();
  }

  private void Update()
  {
    if (isGrounded) 
      State = States.idle;

    if (m_isInWater)
    {
      rb.gravityScale = 0.17f;
    }
    else
    {
      rb.gravityScale = 3f;
    }

    if (Input.GetButton("Horizontal"))
      Run(m_isInWater);

    if ((isGrounded||m_isInWater) && Input.GetButtonDown("Jump")) 
      Jump(m_isInWater);

    if (!Input.GetButton("Horizontal"))
      audio_speed.Pause();
    else if (!audio_speed.isPlaying)
      audio_speed.Play();
  }

  

  private void Run(bool inWater)
  {
    if (isGrounded) 
      State = States.Run;

    Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    float speedMult = 1f;
    if (inWater)
      speedMult = 0.6f;

    
    transform.position = Vector3.MoveTowards(
      transform.position,
      transform.position + dir, 
      speed * Time.deltaTime * speedMult
      );

    sprite.flipX = dir.x < 0.0f;

  }

  private void Jump(bool inWater)
  {
    if (inWater)
      rb.AddForce(transform.up * JumpForce * 0.1f, ForceMode2D.Impulse);
    else 
      rb.AddForce(transform.up * JumpForce * 1f, ForceMode2D.Impulse);
  }


 

  private void CheckGround()
  {

    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position+new Vector3(0f,0.83f,0f), 0.2f, LayerMask.GetMask("Ground","Default"));
    isGrounded = colliders.Length > 0;

    if (!isGrounded) 
      State = States.Jimp;
  }

  public void OnTriggerEnter2D(Collider2D collision)
  {
    
    if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
    {
      m_isInWater = true;
    }

    if (collision.gameObject.GetComponent<Restart>() != null)
    {
      m_lostCheckPoint = collision.gameObject.GetComponent<Restart>();
    }
  }

  public void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
    {
      m_isInWater = false;
    }
  }

  private void FixedUpdate()
  {
    CheckGround();
  }

  private enum States
  {
    idle, Run, Jimp
  }

  private States State
  {
    get { return (States)anim.GetInteger("State"); }
    set { anim.SetInteger("State", (int)value); }

  }

  public void GetDamage(int damage)
  {
    if (Time.time - m_lastDamageTime < m_imortalTime)
      return;

    m_lastDamageTime = Time.time;

    StartCoroutine(AnimateDamage());

    lives = lives - damage;
    if (lives < 0f )
    {
      if (m_lostCheckPoint != null)
      {
        lives = 5;
        transform.position = m_lostCheckPoint.transform.position;
      }
    }
  }

  private IEnumerator AnimateDamage()
  {
    while (Time.time - m_lastDamageTime < m_imortalTime)
    { 
      yield return new WaitForSeconds(0.1f);
      sprite.color = new Color(1f,1f,1f,0.5f);
      yield return new WaitForSeconds(0.1f);
      sprite.color = new Color(1f,0f,0f,1f);
    }
    sprite.color = new Color(1f,1f,1f,1f);
  }

}


