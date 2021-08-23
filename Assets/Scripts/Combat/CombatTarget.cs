using UnityEngine;

namespace RPG.Combat 
{
    //adds health component automatically whenever we have a CombatTarget component
    //see lec 41 at the end
    [RequireComponent(typeof(Health))] 
    public class CombatTarget : MonoBehaviour
    {

    }

}
