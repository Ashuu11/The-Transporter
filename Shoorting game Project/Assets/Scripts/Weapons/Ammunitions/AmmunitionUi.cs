using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmunitionUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammunitionTypeText;
    [SerializeField] TextMeshProUGUI ammunitionCountText;
    [SerializeField] GameObject gunImage;
    [SerializeField] GameObject gunImagerifle;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UpdateAmmunitionType(Gun gun)
    {
        if(gun == null)
        {
            canvasGroup.alpha = 0;
            return;
        }

        canvasGroup.alpha = 1;

        UpdateAmmunitionCount(AmmunitionManager.instance.GetAmmunitionCount(gun.ammunitionType));        

        ammunitionTypeText.text = gun.ammunitionType.ToString();
        if( gun.ammunitionType.ToString() == "Light")
        {
            gunImage.SetActive(true);
            gunImagerifle.SetActive(false);
        }
        else if( gun.ammunitionType.ToString() == "Heavy")
        {
            gunImage.SetActive(false);
            gunImagerifle.SetActive(true);
        }

    }

    public void UpdateAmmunitionCount(int newCount)
    {
        ammunitionCountText.text = newCount.ToString();
    }
}
