using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private GameObject[] _healthBar = new GameObject[2];
    private GameObject _healthTxt;

    void Awake()
    {
        Player.Instance.onHealthChange.AddListener(OnHealthChange);
        _healthTxt = transform.Find("HealthTxt")?.gameObject;
        _healthBar[0] = transform.Find("HealthBar/HealthBarFill")?.gameObject;
        _healthBar[1] = transform.Find("HealthBar/HealthBarBgd")?.gameObject;
    }

    private void OnHealthChange(float currHealth, float fullHealth)
    {
        if (fullHealth <= 0 || currHealth < 0) return;
        _healthBar[0].GetComponent<Image>().fillAmount = Mathf.Min(currHealth /fullHealth, 1f);
    }
}
