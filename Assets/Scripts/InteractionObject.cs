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
    public string infoMessage1;
    public string infoMessage2;
    public Sprite sprite2;
    public float infoTime;
    public DialogueClass.Objects infoRequirement;
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
            case "Potato":
                player.GetComponent<Character>().Potatoes++;
                break;
            case "Carrot":
                player.GetComponent<Character>().Carrots++;
                break;
            case "Corn":
                player.GetComponent<Character>().Corns++;
                break;
        }
        gameObject.SetActive(false);
    }

    public void Info()
    {
        if(infoRequirement == DialogueClass.Objects.None)
            StartCoroutine(ShowInfo(infoMessage1, infoTime));
        else
        {
            switch (infoRequirement)
            {
                case DialogueClass.Objects.Coin:
                    if (player.GetComponent<Character>().Coins > 0)
                    {
                        player.GetComponent<Character>().Coins--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Key:
                    if (player.GetComponent<Character>().Keys > 0)
                    {
                        player.GetComponent<Character>().Keys--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Pumpkin:
                    if (player.GetComponent<Character>().Pumpkins > 0)
                    {
                        player.GetComponent<Character>().Pumpkins--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Potion:
                    if (player.GetComponent<Character>().Potions > 0)
                    {
                        player.GetComponent<Character>().Potions--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Letter:
                    if (player.GetComponent<Character>().Letters > 0)
                    {
                        player.GetComponent<Character>().Letters--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Anvil:
                    if (player.GetComponent<Character>().Anvils > 0)
                    {
                        player.GetComponent<Character>().Anvils--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
                case DialogueClass.Objects.Shield:
                    if (player.GetComponent<Character>().Shields > 0)
                    {
                        player.GetComponent<Character>().Shields--;
                        StartCoroutine(ShowInfo(infoMessage2, infoTime));
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                        gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        StartCoroutine(ShowInfo(infoMessage1, infoTime));
                    }
                    break;
            }
        }
    }

    public void Dialogue()
    {
        DialogueClass dialogue = new DialogueClass();
        dialogue.priority = 0;
        dialogue.sentences = new string[] { "ERROR! NO VALID DIALOGUES!" };
        foreach (DialogueClass dia in dialogues)
        {
            if (player.GetComponent<Character>().activeQuests.Contains(dia.activeQuest) && player.GetComponent<Character>().completedQuests.Contains(dia.completedQuest)
                && dia.priority > dialogue.priority)
            {
                if (DialogueRequirementCheck(dia)) dialogue = dia;
            }
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
            case DialogueClass.Objects.None:
                return true;
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
            case DialogueClass.Objects.Shield:
                if (player.GetComponent<Character>().Shields >= dia.requiredAmount) return true;
                else return false;
            default:
                return false;
        }
    }

}
