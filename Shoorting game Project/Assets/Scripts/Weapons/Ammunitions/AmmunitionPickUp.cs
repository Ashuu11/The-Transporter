using UnityEngine;

public class AmmunitionPickUp : MonoBehaviour, ILootable
{
    [SerializeField] private int ammunitionCount;
    [SerializeField] private AmmunitionTypes ammunitionType;

    public void OnStartLook()
    {
        //Show tooltip ui
        Debug.Log($"Started looking at {ammunitionType}!");
    }

    public void OnInteract()
    {
        AmmunitionManager.instance.AddAmmunition(ammunitionCount, ammunitionType);
        Destroy(gameObject);
    }

    public void OnEndLook()
    {
        //Hide tooltip ui
        Debug.Log($"Stopped looking at {ammunitionType}!");
    }   

   

}
