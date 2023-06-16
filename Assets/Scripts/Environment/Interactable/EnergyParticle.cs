using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticle : MonoBehaviour
{
    private float _energyAmount;
    public float m_amount
    {
        get => _energyAmount;
        set => _energyAmount = value;
    }

    public Sprite m_sprite;
    public Color m_color;

    private SpriteRenderer render;

    public void Awake()
    {
        render = gameObject?.GetComponent<SpriteRenderer>();
    }

    public void Render()
    {
        render.color = m_color;
        render.sprite = m_sprite;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
