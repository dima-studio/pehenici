using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjump : MonoBehaviour
{
  public float JumpForce = 30f;
  public Rigidbody2D m_physics;
  public float m_speed = 0.1f;
  public SpriteRenderer m_view;
  public float m_swapDirectionMin = 1f;
  public float m_swapDirectionMax = 5f;
  public float m_jumpTimeMin = 5f;
  public float m_jumpTimeMax = 5f;


  // переменные для изменения направления
  float m_oldSwapDirTime = 0f; // предыдущее время, когда изменилось направление (время)
  Vector2 m_currDir = new Vector2(1f, 0f); // текущее направление
  float m_nextSwapTime = 0f; // когда в следующий раз измениться направление (время)

  // переменные для прыжка 
  float m_jumpTime = 0f;
  float m_nextJumpTime = 0f;


  private bool isGrounded = false;
  private bool m_isInWater = false;


  void Update()
  {
    if (Time.time - m_oldSwapDirTime > m_nextSwapTime)
    {
      m_nextSwapTime = UnityEngine.Random.Range(m_swapDirectionMin, m_swapDirectionMax);
      m_oldSwapDirTime = Time.time;
      m_currDir.x = -m_currDir.x;
      m_view.flipX = !m_view.flipX;
    }
    //m_physics.MovePosition(m_physics.position + m_currDir * m_speed * Time.fixedDeltaTime + Physics2D.gravity * Time.fixedDeltaTime);
    transform.position = Vector3.MoveTowards(
      transform.position,
      transform.position + new Vector3(m_currDir.x, m_currDir.y, 0f), 
      m_speed * Time.deltaTime
      );
    
    UpdateJump ();
  }

  private void Jump(bool fgfgjutyydyhdfh)
  {
    if (fgfgjutyydyhdfh)
      m_physics.AddForce(transform.up * JumpForce * 1.5f, ForceMode2D.Impulse);
    else
      m_physics.AddForce(transform.up * JumpForce * 15f, ForceMode2D.Impulse);
  }

  void UpdateJump()
  {
    if (Time.time - m_jumpTime > m_nextJumpTime)
    {
      m_nextJumpTime = UnityEngine.Random.Range(m_jumpTimeMin, m_jumpTimeMax);
      m_jumpTime = Time.time;
      Jump(false);
    }
  }
}

