using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    [SerializeField]
    private GameObject currentInteractiveObj;

    [SerializeField]
    public InteractionObject currentInterObjScript;

    public enum Quests
    {
        NotAQuest,
        GetShield,
        GetAnvil,
        CalmSparky,
        GetPotion,
        GetPumpkin,
        GetCoins,
    }


    public Animator anim;
    public Collider2D col;
    public Rigidbody2D rb;

    public float speedMin;
    public float speed;

    Vector2 Dir;
    float mag;

    float lastX;
    float lastY;
    float X;
    float Y;

    public List<Quests> activeQuests = new List<Quests>();
    public List<Quests> completedQuests = new List<Quests>();

    [Header("Inventory")]
    public int Coins = 0;
    public int Keys = 0;
    public int Pumpkins = 0;
    public int Potatos = 0;
    public int Carrots = 0;
    public int Corns = 0;
    public int Potions = 0;
    public int Letters = 0;
    public int Anvils = 0;
    public int Shields = 0;

    [Header("Not Inventory")]
    public int endSceneIndex;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionObject"))
        {
            currentInteractiveObj = collision.gameObject;
            currentInterObjScript = collision.gameObject.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionObject"))
        {
            currentInteractiveObj = null;
            currentInterObjScript = null;
        }
    }

    private void Start()
    {
        activeQuests.Add(Quests.NotAQuest);
        completedQuests.Add(Quests.NotAQuest);
        activeQuests.Add(Quests.GetShield);
    }

    // Update is called once per frame
    void Update()
    {

        Dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mag = Mathf.Clamp(Dir.magnitude, 0.0f, 1.0f);
        Dir.Normalize();
        rb.velocity = Dir * mag * speed * Time.deltaTime;
        anim.SetFloat("X", Dir.x);
        anim.SetFloat("Y", Dir.y);
        anim.SetFloat("Speed", mag);

        if (Input.GetButtonDown("Fire1"))
        {
            if(currentInteractiveObj != null)
            {
                switch (currentInterObjScript.interactiveType)
                {
                    case InteractionObject.InteractiveType.nothing:
                        currentInterObjScript.Nothing();
                        break;
                    case InteractionObject.InteractiveType.pickup:
                        currentInterObjScript.PickUp();
                        break;
                    case InteractionObject.InteractiveType.info:
                        currentInterObjScript.Info();
                        break;
                    case InteractionObject.InteractiveType.dialogue:
                        currentInterObjScript.Dialogue();
                        break;
                }
            }
            else
            {
                anim.SetTrigger("Attack");
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if(Mathf.Abs(rb.velocity.x) >= speedMin || Mathf.Abs(rb.velocity.y) >= speedMin)
        {
            anim.SetFloat("LastX", anim.GetFloat("X"));
            anim.SetFloat("LastY", anim.GetFloat("Y"));
        }

        if(Shields > 0)
        {
            completedQuests.Add(Quests.GetShield);
            SceneManager.LoadScene(endSceneIndex);
        }
    }

    public string writeArray(List<Quests> list)
    {
        string sentence = "";
        foreach (Quests quest in list)
        {
            sentence += quest + ", ";
        }
        return sentence;
    }


}
