using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClass : MonoBehaviour
{
    public int priority = 0;
    public Character.Quests activeQuest;
    public Character.Quests startQuest;
    public Character.Quests endQuest;
    public Character.Quests completedQuest;

    public enum Objects
    {
        None, Coin, Key, Pumpkin, Potion, Letter, Anvil, Shield
    }

    public Objects requirement;
    public int requiredAmount;
    public Objects reward;
    public int rewardAmount;
    public Objects consumed;
    public int consumedAmount;

    [TextArea]
    public string[] sentences;


}
