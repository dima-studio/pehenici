using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicken : MonoBehaviour
{
  public Rigidbody2D      m_physics;
  public float            m_swapDirectionMin = 1f;
  public float            m_swapDirectionMax = 5f;
  public float            m_speed = 0.1f;
  public SpriteRenderer   m_view;


  float     m_oldSwapDirTime = 0f;
  Vector2   m_currDir = new Vector2(1f, 0f);
  float     m_nextSwapTime = 0f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Time.time - m_oldSwapDirTime > m_nextSwapTime)
    {
      m_nextSwapTime = UnityEngine.Random.Range(m_swapDirectionMin, m_swapDirectionMax);
      m_oldSwapDirTime = Time.time;
      m_currDir.x = -m_currDir.x;
      m_view.flipX = !m_view.flipX;
    }
    m_physics.MovePosition(m_physics.position + m_currDir * m_speed * Time.fixedDeltaTime + Physics2D.gravity * Time.fixedDeltaTime);
  }
}
