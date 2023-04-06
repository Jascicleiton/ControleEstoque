using TMPro;
using UnityEngine;

public class LocationDropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
   
    /// <summary>
    /// Handles all locations that an item can go to or come from
    /// </summary>
    void Start()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        dropdown.ClearOptions();
        
        if (InternalDatabase.locations.Count > 0)
        {
            dropdown.AddOptions(InternalDatabase.locations);
        }
        dropdown.value = HelperMethods.GetLocationDPValue("Estoque");
    }

    private void OnEnable()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        if(dropdown.options.Count == 0)
        {
            dropdown.AddOptions(InternalDatabase.locations);
        }
    }

    public void ResetDropDown()
    {
        dropdown.value = HelperMethods.GetLocationDPValue("Estoque");
    }
}
