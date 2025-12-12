using UnityEngine;
using System.Collections.Generic;

public class GlobalBattleHandler : MonoBehaviour
{
    public static GlobalBattleHandler instance;
    public AllyStateMachine allyHandler;
    //public EnemyStateMachine enemyHandler;

    public Queue<GenBattleObjects> battleQueue = new Queue<GenBattleObjects>();
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        allyHandler.allyInit();
        //enemyHandler.enemyInit();
    }

    void Update()
    {
        allyHandler.localUpdate();
        //enemyHandler.updateEnemy();
    }
}
