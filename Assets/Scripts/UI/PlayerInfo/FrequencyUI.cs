using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyUI : MonoBehaviour
{
    private GameObject[] _freqBar = new GameObject[4];
    private GameObject _freqTxt;

    void Awake()
    {
        Player.Instance.onFreqChange.AddListener(OnFreqChange);
        _freqTxt = transform.Find("FreqTxt")?.gameObject;
        _freqBar[0] = transform.Find("FreqBar/FreqBarLowFill")?.gameObject;
        _freqBar[1] = transform.Find("FreqBar/FreqBarUpFill")?.gameObject;
        _freqBar[2] = transform.Find("FreqBar/FreqBarCurrFill")?.gameObject;
        _freqBar[3] = transform.Find("FreqBar/FreqBarBgd")?.gameObject;
    }

    private void OnFreqChange(float freq, float minFreq, float maxFreq, float fullFreq)
    {
        if (maxFreq <= 0 || freq < 0) return;
        _freqBar[0].GetComponent<Image>().fillAmount = Mathf.Min(minFreq / fullFreq, 1f);
        _freqBar[1].GetComponent<Image>().fillAmount = Mathf.Min(maxFreq / fullFreq, 1f);
        _freqBar[2].GetComponent<Image>().fillAmount = Mathf.Min(freq / fullFreq, 1f);
        _freqTxt.GetComponent<TMPro.TMP_Text>().text = $"{freq} Frequency";
    }
}
