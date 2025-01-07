using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  public int m_damage = 1;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject == Hero.Instance.gameObject)
    {
      Hero.Instance.GetDamage(m_damage);
    }
  }

  public class Entity : MonoBehaviour
  {

    public virtual void GetDamage()
    {

    }

    public virtual void Die()
    {
     Destroy(this.gameObject);

    }
  }
}
