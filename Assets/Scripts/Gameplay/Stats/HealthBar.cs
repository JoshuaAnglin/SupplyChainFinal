using SCG.Stats;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent;
    [SerializeField] RectTransform foreground;
    [SerializeField] Text healthValue;

    void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        foreground.localScale = Vector3.Lerp(foreground.localScale, new Vector3(healthComponent.GetFraction(), 1, 1), 1);
        healthValue.text = (foreground.localScale.x * 100).ToString("0") + "%";

        //foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        //healthValue.text = (foreground.localScale.x * 100).ToString("0") + "%";
    }
}
