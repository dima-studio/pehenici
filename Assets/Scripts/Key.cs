using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{ 
  public string m_type = "default";
  bool m_isAttached = false;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Hero hero = collision.GetComponent<Hero>();
    if (hero == null)
      return;

    if (hero.m_inventory.childCount > 0)
      return;

    transform.SetParent(hero.m_inventory);
    transform.localPosition = Vector3.zero;

    gameObject.layer = 6;
    m_isAttached = true;
  }

  private void Update()
  {
    if (transform.parent != null && m_isAttached)
    {
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
    }
  }
}
