using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour, IObjectPool
{
    [SerializeField] private SpriteRenderer m_Renderer;
    [SerializeField] private Vector2 m_ScreenBounds;
    [SerializeField] private List<Sprite> m_CloudSprite;
    [SerializeField] private float m_MoveSpeed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_ScreenBounds = GameSetting.Instance.screenBounds;
    }

    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;

        if (transform.position.x < -(GameSetting.Instance.screenBounds.x + 1.0f))
        {
            //Debug.Log("Move out of screen");
            gameObject.SetActive(false);
        }
    }

    public void OnReuse()
    {
        int spriteIndex = Random.Range(0, m_CloudSprite.Count);
        float y = Random.Range(2.0f, m_ScreenBounds.y - 1.0f);

        m_Renderer.sprite = m_CloudSprite[spriteIndex];

        this.transform.Translate(0.0f, y, 0.0f, Space.World);
    }
}
