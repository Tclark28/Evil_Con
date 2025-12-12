using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GlobalBattleHandler : MonoBehaviour
{
    public static GlobalBattleHandler instance;

    public AllyStateMachine allyHandler;
    public EnemyStateMachine enemyHandler;
    public UIHandler UI_Handler;

    public List<GenBattleObjects> battleList = new List<GenBattleObjects>();
    public Queue<GenBattleObjects> battleQueue = new Queue<GenBattleObjects>();

    public GenBattleObjects currentUnit = null;

    

    //Generic functions for game loop
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        allyHandler.localInit(this);
        enemyHandler.localInit(this);
        UI_Handler.uiInit(allyHandler.ally.baseHP, enemyHandler.enemy.baseHP);

        battleList = battleList.OrderByDescending(obj => obj.unitSpeed).ToList();

        foreach (GenBattleObjects obj in battleList)
        {
            battleQueue.Enqueue(obj);
        }

    }

    void Update()
    {
        if (battleQueue.Count > 0)
        {
            if(currentUnit == null)
            {
                currentUnit = battleQueue.Dequeue();
            }
            currentUnit.localUpdate();
        }
    }

    //Logic handling functions
    public void damageEnemy(float weaponDamage, float allyDamage)
    {
        float damage = (float)((1.2 * weaponDamage) + (1.5 * allyDamage)) * 5;
        damage -= (float)((1.5 * enemyHandler.enemy.currDefense) * 0.3);

        enemyHandler.enemy.currHP -= damage;

        if(enemyHandler.enemy.currHP <= 0){
            EnemyStateMachine.currentState = State.DEAD;
            currentUnit = enemyHandler;
            return;
        }
        
        UI_Handler.updateHealthEnemy(enemyHandler.enemy.currHP);
    }
}
