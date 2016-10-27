using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

    public GameObject Player;

    [SerializeField]
    private FireStat fireStat;

    private TechnicalExperience repairExperience;

    public int repairExperienceToGive;

    public bool specificFire;



    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        repairExperience = FindObjectOfType<TechnicalExperience>();
    }



    void Update()
    {
        if (specificFire == true)
        {
            //While the player is putting out the fire, the fire's health loses value
            //The rate at which the fire is put out is calculated by the base repair speed of the player character (modifiable via the player character itself) plus 10% of the current repair level 
            if (Player.GetComponent<PlayerController>().puttingOutFire)
            {
                fireStat.CurrentVal -= Player.GetComponent<TechnicalExperience>().technicalSpeed + (Player.GetComponent<TechnicalExperience>().currentTechnicalLevel * .1f);
            }
        }

        //Once the fire's health equals zero, the fire destroys itself
        if (fireStat.CurrentVal == 0)
        {
            Destroy(gameObject);

            repairExperience.AddRepairExperience(repairExperienceToGive);

            //Makes it so that if the player puts out the fire while standing in the fire, the player's health won't continue to degenerate infinitely
            Player.GetComponent<PlayerController>().touchingFire = false;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        //This makes it so that only the specific emergency fire that the player is interacting with is put out when the player interacts with it
        if (other.gameObject.CompareTag("Player"))
        {
            specificFire = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        //This makes it so that only the specific emergency fire that the player is interacting with is put out when the player interacts with it
        if (other.gameObject.CompareTag("Player"))
        {
            specificFire = false;
        }
    }
}
