using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionObject : MonoBehaviour
{

    bool questCompleted = false;

    public enum InteractiveType
    {
        nothing,
        pickup,
        info,
        dialogue
    }

    [Header("Type of Interactable")]
    public InteractiveType interactiveType;

    [Header("PickUp Object")]
    public string name;

    [Header("Info Object")]
    public string infoMessage;
    public float infoTime;
    TMP_Text infoText;

    [Header("Dialogue Object")]
    //[TextArea]
    public DialogueClass[] dialogues;
    //public string[] dialogue;


    GameObject player;

    private void Start()
    {
        System.Array.Reverse(dialogues);
        player = GameObject.FindGameObjectWithTag("Player");
        infoText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<TMP_Text>();
    }

    public void Nothing()
    {
        Debug.Log("This is Unassigned");
    }

    public void PickUp()
    {
        switch (name)
        {
            case "Coin":
                player.GetComponent<Character>().Coins++;
                break;
            case "Pumpkin":
                player.GetComponent<Character>().Pumpkins++;
                break;
        }
        gameObject.SetActive(false);
    }

    public void Info()
    {
        StartCoroutine(ShowInfo(infoMessage, infoTime));
    }

    public void Dialogue()
    {
        DialogueClass dialogue = new DialogueClass();
        foreach (DialogueClass dia in dialogues)
        {
            if ((dia.happensOnce && dia.happened == false) || dia.happensOnce == false)
            {
                if (dia.requirement == DialogueClass.Objects.None && (dia.afterQuest == false && questCompleted == false)&& DialogueRequirementCheck(dia) == false) dialogue = dia; //default
                else if (DialogueRequirementCheck(dia)) dialogue = dia; //with reward
                else if (dia.afterQuest && questCompleted) dialogue = dia; //completed quest
            }
        }
        if (dialogue.endQuest) questCompleted = true;
        if (dialogue.reward != DialogueClass.Objects.None)
        {

        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogue.happened = true;
    }

    IEnumerator ShowInfo(string message, float delay)
    {
        infoText.text = message;
        yield return new WaitForSeconds(delay);
        infoText.text = null;
    }

    public bool DialogueRequirementCheck(DialogueClass dia)
    {
        switch (dia.requirement)
        {
            case DialogueClass.Objects.Coin:
                if (player.GetComponent<Character>().Coins >= dia.requiredAmount) return true;
                else return false;
            case DialogueClass.Objects.Key:
                if (player.GetComponent<Character>().Keys >= dia.requiredAmount) return true;
                else return false;
            case DialogueClass.Objects.Pumpkin:
                if (player.GetComponent<Character>().Pumpkins >= dia.requiredAmount) return true;
                else return false;
            case DialogueClass.Objects.Potion:
                if (player.GetComponent<Character>().Potions >= dia.requiredAmount) return true;
                else return false;
            case DialogueClass.Objects.Letter:
                if (player.GetComponent<Character>().Letters >= dia.requiredAmount) return true;
                else return false;
            case DialogueClass.Objects.Anvil:
                if (player.GetComponent<Character>().Anvils >= dia.requiredAmount) return true;
                else return false;
            default:
                return false;
        }
    }

}
