using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AllyStateMachine : GenBattleObjects
{
    public BaseAllySetup ally;

    public State currentState;

    public GlobalBattleHandler globalBattleHandler;

    public bool firstAction;
    //Initialize the current state
    public void allyInit()
    {
        globalBattleHandler = GlobalBattleHandler.instance;
        currentState = State.ADDTOLIST;
    }

    // Update is called once per frame
    public override void localUpdate()
    {
        switch (currentState)
        {
            case (State.ADDTOLIST):
                //Code for adding to the list
                addToList();
                firstAction = true;
                break;

            case (State.ACTION):
                //Code for doing the action
                TakeAction();
                break;

            case (State.DEAD):
                //Code for when dead
                Die();
                break;
        }
    }

    
    public override void TakeAction()
    {
        //Implementation of TakeAction for Ally
       if(firstAction){
              Debug.Log("Choose an action");
              firstAction = false;
       }
       //Perform action logic here
       if(Input.GetKeyDown(KeyCode.Alpha1)){
            Debug.Log("Ally used Skill 1!");
            currentState = State.ADDTOLIST;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("Ally used Skill 2!");
            currentState = State.ADDTOLIST;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            Debug.Log("Ally used Skill 3!");
            currentState = State.ADDTOLIST;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            Debug.Log("Ally used Skill 4!");
            currentState = State.ADDTOLIST;
        }
    }

    public override void addToList()
    {
        //Implementation of addToList for Ally
        Debug.Log("Added to the battle queue!");
        globalBattleHandler.battleQueue.Enqueue(this);
        currentState = State.ACTION;
    }

    public override void Die()
    {
        Debug.Log("Ally has died.");
        //Implementation of Die for Ally
    }
    
}
