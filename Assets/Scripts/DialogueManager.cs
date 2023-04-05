using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator anim;

    private Queue<string> dialogue;
    private DialogueClass dialogueClass;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Queue<string>();
    }

    public void StartDialogue(DialogueClass dia)
    {
        dialogueClass = dia;
        dialogue.Clear();
        dialogueUI.SetActive(true);
        StopPlayer();
        foreach (string sentence in dia.sentences)
        {
            dialogue.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void StopPlayer()
    {
        player.GetComponent<Character>().enabled = false;
        anim.SetFloat("Speed", 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ResumePlayer()
    {
        player.GetComponent<Character>().enabled = true;
    }

    public void DisplayNextSentence()
    {
        if(dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }
        string text = dialogue.Dequeue();
        dialogueText.text = text;
    }


    public void EndDialogue()
    {
        switch (dialogueClass.consumed)
        {
            case DialogueClass.Objects.Coin:
                player.GetComponent<Character>().Coins -= dialogueClass.consumedAmount;
                break;
            case DialogueClass.Objects.Key:
                player.GetComponent<Character>().Keys -= dialogueClass.consumedAmount;
                break;
            case DialogueClass.Objects.Pumpkin:
                player.GetComponent<Character>().Pumpkins -= dialogueClass.consumedAmount;
                break;
            case DialogueClass.Objects.Potion:
                player.GetComponent<Character>().Potions -= dialogueClass.consumedAmount;
                break;
            case DialogueClass.Objects.Letter:
                player.GetComponent<Character>().Letters -= dialogueClass.consumedAmount;
                break;
            case DialogueClass.Objects.Anvil:
                player.GetComponent<Character>().Anvils -= dialogueClass.consumedAmount;
                break;
        }
        switch (dialogueClass.reward)
        {
            case DialogueClass.Objects.Coin:
                player.GetComponent<Character>().Coins += dialogueClass.rewardAmount;
                break;
            case DialogueClass.Objects.Key:
                player.GetComponent<Character>().Keys += dialogueClass.rewardAmount;
                break;
            case DialogueClass.Objects.Pumpkin:
                player.GetComponent<Character>().Pumpkins += dialogueClass.rewardAmount;
                break;
            case DialogueClass.Objects.Potion:
                player.GetComponent<Character>().Potions += dialogueClass.rewardAmount;
                break;
            case DialogueClass.Objects.Letter:
                player.GetComponent<Character>().Letters += dialogueClass.rewardAmount;
                break;
            case DialogueClass.Objects.Anvil:
                player.GetComponent<Character>().Anvils += dialogueClass.rewardAmount;
                break;
        }
        dialogueUI.SetActive(false);
        dialogue.Clear();
        ResumePlayer();
    }

}
