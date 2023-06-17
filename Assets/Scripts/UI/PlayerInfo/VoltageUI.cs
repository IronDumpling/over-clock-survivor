using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoltageUI : MonoBehaviour
{
    private GameObject[] _energyBar = new GameObject[2];
    private GameObject _energyTxt;
    private GameObject _voltageTxt;

    void Awake()
    {
        Player.Instance.onEnergyChange.AddListener(OnEnergyChange);
        _energyTxt = transform.Find("EnergyTxt")?.gameObject;
        _voltageTxt = transform.Find("VoltageTxt")?.gameObject;
        _energyBar[0] = transform.Find("EnergyBar/EnergyBarFill")?.gameObject;
        _energyBar[1] = transform.Find("EnergyBar/EnergyBarBgd")?.gameObject;
    }

    private void OnEnergyChange(float energy, float maxEnergy)
    {
        if (maxEnergy <= 0 || energy < 0) return;
        _energyBar[0].GetComponent<Image>().fillAmount = energy / maxEnergy;
        _voltageTxt.GetComponent<TMPro.TMP_Text>().text = $"Voltage: {Player.Instance.m_level}";
        _energyTxt.GetComponent<TMPro.TMP_Text>().text = $"Energy: {energy}";
    }
}
