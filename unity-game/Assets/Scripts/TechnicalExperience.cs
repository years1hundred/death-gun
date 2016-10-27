using UnityEngine;
using System.Collections;

public class TechnicalExperience : MonoBehaviour {

    public int currentTechnicalLevel;
    public int currentTechnicalExperience;
    public int[] technicalToLevelUp;

    public float technicalSpeed;



    void Start()
    {

    }



    void Update()
    {
        //If the experience that the player has accumulated surpases the requirement established by the array, then the repair levels up
        if (currentTechnicalExperience >= technicalToLevelUp[currentTechnicalLevel])
        {
            currentTechnicalLevel = currentTechnicalLevel + 1;
        }
    }

    //This adds repair experience to the player's current experience values
    public void AddRepairExperience(int repairExperienceToAdd)
    {
        currentTechnicalExperience += repairExperienceToAdd;
    }
}
