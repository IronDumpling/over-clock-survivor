using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private GameObject[] _healthBar = new GameObject[2];
    private GameObject _healthTxt;
    private GameObject _enemy;

    void Awake()
    {
        _enemy = transform.parent.gameObject;
        _enemy.GetComponent<Enemy>().onHealthChange.AddListener(OnHealthChange);

        _healthTxt = transform.Find("HealthTxt")?.gameObject;
        _healthBar[0] = transform.Find("HealthBar/BarFill")?.gameObject;
        _healthBar[1] = transform.Find("HealthBar/BarBGD")?.gameObject;
    }

    private void OnHealthChange(float currHealth, float fullHealth)
    {
        if (fullHealth <= 0 || currHealth < 0) return;
        _healthBar[0].GetComponent<Image>().fillAmount = Mathf.Min(currHealth /fullHealth, 1f);
    }
}
