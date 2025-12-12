using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnemyStateMachine : GenBattleObjects
{
    public BaseEnemySetup enemy; // Reference to the enemy setup

    public override float unitSpeed {get {return enemy.currSpeed;}}
    public override string unitName {get {return enemy.enemyName;}}

    public State currentState; // Current state of the enemy

    public GlobalBattleHandler globalBattleHandler; // Reference to the global battle handler

    public bool printOnce = true;

    //Initialize the current state
    public override void localInit(GlobalBattleHandler instance)
    {
        globalBattleHandler = instance;
        globalBattleHandler.battleList.Add(this);
        currentState = State.ACTION;
    }

    // Update is called once per frame
    public override void localUpdate()
    {
        switch (currentState)
        {
            case (State.ADDTOLIST):
                //Code for adding to the list
                addToList();
                printOnce = true;
                break;

            case (State.ACTION):
                //Code for doing the action
                if(printOnce){
                    Debug.Log(unitName + ": Take an action!");
                    printOnce = false;
                }
                TakeAction();
                break;

            case (State.DEAD):
                //Code for when dead
                Die();
                break;
        }
    }

    public override void addToList()
    {
        //Implementation of addToList for Ally
        globalBattleHandler.battleQueue.Enqueue(this);
        currentState = State.ACTION;
        globalBattleHandler.currentUnit = null;
    }
    
    public override void TakeAction()
    {
       //Perform action logic here
       int choice = Random.Range(1, 11);

       if(choice <= 6) //Attack on 0 - 6
       {
            enemyAttack();
       }else{ //Block when above a 6
            //Don't block if already blocking
            if(!enemy.isBlocking){
                enemyBlock();
            }
       }
       
       
    }

    public void enemyAttack()
    {
        //Implementation of enemyAttack
        Debug.Log("Attacking an ally!");
        //Attack logic here
        globalBattleHandler.damageAlly(enemy.currDamage);
        currentState = State.ADDTOLIST;
    }

    public void enemyBlock(){
        //Implementation of enemy Block
        Debug.Log("You have been blocked! Evily!");
        enemy.isBlocking = true;
        currentState = State.ADDTOLIST;
    }

    public void enemyItem(){
        //Implementation of enemyItem
        Debug.Log("Prepare for an evil item!");
    }

    public override void Die()
    {
        Debug.Log("Ally has died.");
        SceneManager.LoadScene("Scene 1");
    }
}
