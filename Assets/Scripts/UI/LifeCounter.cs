using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
  public class LifeCounter : MonoBehaviour
  {
    public Text m_counter;

    void Update()
    {
      m_counter.text = Hero.Instance.lives.ToString();
    }
  }

}

