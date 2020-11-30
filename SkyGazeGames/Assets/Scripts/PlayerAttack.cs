using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region variables

    //PUBLIC VARIABLES
    //public variable, used to manage time between attacks
    public float startTimeBetweenAttacks;

    //This is the key we use to attack
    public KeyCode AttackKey;

    //Attacking Position for the HitBox
    public Transform AttackPos;

    //float for the hitbox radius
    public float radius;

    //Layer of the enemies
    public LayerMask damageable;

    //damage we deal to all of the enemies (min)
    public int minDamage;

    //damage we deal to all of the enemies (max)
    public int maxDamage;


    //PRIVATE VARIABLES
    //private var, timer
    private float timeBtwAttack;

    //animation for sword
    private Animator swordAnimator;

    #endregion

    private void Start()
    {
        timeBtwAttack = startTimeBetweenAttacks;
        swordAnimator = this.transform.Find("Sword").GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //If we can attack
        if(timeBtwAttack <= 0)
        {
            //If input
            if (Input.GetKey(AttackKey))
            {
                //Animations
                swordAnimator.Play("SwordStab");
                swordAnimator.Play("Default");
                //Get an array of colliders within a certain radius
                Collider2D[] CollidersToDamage = Physics2D.OverlapCircleAll(AttackPos.position, radius, damageable);
                for (int i = 0; i < CollidersToDamage.Length; i++)
                {
                    //Damage all eligible enemies
                    int damage = Random.Range(minDamage, maxDamage);
                    CollidersToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            //able to attack
            timeBtwAttack = startTimeBetweenAttacks;
        }
        else
        {
            //Subtract the timer
            timeBtwAttack -= Time.deltaTime;
        }
    }

    //Draw gizmos, shows the circle
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, radius);
    }
}