using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GiantBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.2f;
    private void Awake()
    {
        Cannon.Instance.OnProgressChange += HandleBarChange;
    }
    private void OnDestroy()
    {
        Cannon.Instance.OnProgressChange -= HandleBarChange;
    }

    private void HandleBarChange(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;
        while (elapsed<updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }
}
