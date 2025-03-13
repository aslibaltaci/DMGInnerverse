using UnityEngine;
using UnityEngine.UI;

public class ConsumptionBar : MonoBehaviour
{
    public Image barFill; 

    void Start()
    {
        if (barFill == null)
        {
            Debug.LogError("Consumption Bar Image is not assigned!");
            return;
        }

        barFill.fillAmount = 0f;
        Debug.Log("Bar Fill at Start: " + barFill.fillAmount);
    }

    public void UpdateBar(int collected, int max)
    {
        if (barFill == null)
        {
            Debug.LogError("Consumption Bar Image is missing!");
            return;
        }

        float fillValue = (float)collected / max;
        barFill.fillAmount = Mathf.Clamp01(fillValue);

        Debug.Log("Updated Bar: " + barFill.fillAmount);
    }
}
