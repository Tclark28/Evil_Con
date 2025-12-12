using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AllyStateMachine : GenBattleObjects
{
    public BaseAllySetup ally;

    public State currentState;

    public GlobalBattleHandler globalBattleHandler;
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
       //bool actionComplete = false;
        /*
        while (!actionComplete)
        {
            //Perform action logic here
            if(Keyboard.current){
                Debug.Log("Ally used Skill 1!");
                actionComplete = true;
            }
            if(Keyboard.current[Key.2].wasPressedThisFrame){
                Debug.Log("Ally used Skill 2!");
                actionComplete = true;
            }
            if(Keyboard.current[Key.3].wasPressedThisFrame){
                Debug.Log("Ally used Skill 3!");
                actionComplete = true;
            }
            if(Keyboard.current[Key.4].wasPressedThisFrame){
                Debug.Log("Ally used Skill 4!");
                actionComplete = true;
            }
        }
        */
        Debug.Log("Taking action!");
        currentState = State.ADDTOLIST;
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
