using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AllyStateMachine : GenBattleObjects
{
    public BaseAllySetup ally;
    
    public override float unitSpeed {get {return ally.currSpeed;}}
    public override string unitName {get {return ally.allyName;}}

    public State currentState;

    public GlobalBattleHandler globalBattleHandler;

    bool printOnce = true;
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
       if(Input.GetKeyDown(KeyCode.Alpha1)){
            allyAttack();
            currentState = State.ADDTOLIST;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(!ally.isBlocking){
                allyBlock();
                currentState = State.ADDTOLIST;
            }else{
                Debug.Log("You cannot block twice without getting hit");
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            allyItem();
            currentState = State.ADDTOLIST;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            Debug.Log("I am now going to run away!");
            SceneManager.LoadScene("Scene 1");
        }
    }

    
    
    public void allyAttack()
    {
        //Implementation of allyAttack
        Debug.Log("Attacking enemy!");
        //Attack logic here
        globalBattleHandler.damageEnemy(0, ally.currDamage);
    }

    public void allyBlock(){
        //Implementation of allyBlock
        Debug.Log("You have been blocked!");
        ally.isBlocking = true;
    }

    public void allyItem(){
        //Implementation of allyItem
        Debug.Log("Prepare for an item!");
    }

    public override void Die()
    {
        Debug.Log("Ally has died.");
        SceneManager.LoadScene("Scene 1");
    }
}
