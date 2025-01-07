using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyTrigger : MonoBehaviour
{
  public string       m_keyType = "default";
  public UnityEvent   m_onKeyEnter;


  private void OnTriggerEnter2D(Collider2D collision)
  {
    Key key = collision.gameObject.GetComponent<Key>();
    if (key == null)
      return;


    if (key.m_type == m_keyType)
    {
      m_onKeyEnter.Invoke();

      Destroy( key.gameObject );
    }
  }
}
