using UnityEngine;



public class CameraController : MonoBehaviour
{
  public Transform player;
  public float     m_yoffset;
  public float     m_speed;
  public float     m_clampDistance;

  private Vector3 pos;

  private void Aweke()
  {
    if (!player)
      player = FindObjectOfType<Hero>().transform;

  }

  private void LateUpdate()
  {
    pos = player.position + new Vector3(0f,m_yoffset,0f);
    float distance = Vector3.Distance(transform.position, pos);
    Vector3 newPos = Vector3.Lerp(transform.position, pos, Time.deltaTime * m_speed * distance);
    if (distance > m_clampDistance)
    {
      float distFix = Mathf.Clamp(Mathf.Abs(distance - m_clampDistance), 0f, m_clampDistance);
      newPos = pos + (transform.position - pos).normalized * (m_clampDistance - distFix);
    }

    newPos.z = transform.position.z;
    transform.position = newPos;
  }

}