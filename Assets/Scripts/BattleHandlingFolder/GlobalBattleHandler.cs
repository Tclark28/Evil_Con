using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GlobalBattleHandler : MonoBehaviour
{
    public static GlobalBattleHandler instance;

    public AllyStateMachine allyHandler;
    public EnemyStateMachine enemyHandler;

    public List<GenBattleObjects> battleList = new List<GenBattleObjects>();
    public Queue<GenBattleObjects> battleQueue = new Queue<GenBattleObjects>();

    public GenBattleObjects currentUnit = null;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        allyHandler.localInit(this);
        enemyHandler.localInit(this);

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
}
