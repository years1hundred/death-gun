using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBarController : MonoBehaviour
{

    public PlayerController PlayerControllerScript;

    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private bool sprintColor;

    [SerializeField]
    private Color sprintOrange;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + Mathf.Round(value / MaxValue * 100) + "%";
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        //Causes the bar to fill up via lerp instead of jarring amounts
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

        //Causes the afterburner bar to appear red when it's overheated, and orange when it's full
        if (sprintColor)
        {
            if (PlayerControllerScript.sprintExhausted == true)
            {
                content.color = Color.red;
            }

            else if (PlayerControllerScript.sprintExhausted == false)
            {
                content.color = Color.Lerp(sprintOrange, sprintOrange, fillAmount);
            }
        }
    }

    //Calculates the exact amount of stat the player has
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
