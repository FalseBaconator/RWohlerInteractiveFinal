using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject Inventory;
    public int Coins = 0;
    public TMP_Text coinText;
    public int Keys = 0;
    public TMP_Text keyText;
    public int Pumpkins = 0;
    public TMP_Text pumpkinText;
    public int Potatoes = 0;
    public TMP_Text potatoText;
    public int Carrots = 0;
    public TMP_Text carrotText;
    public int Corns = 0;
    public TMP_Text cornText;
    public int Potions = 0;
    public TMP_Text potionText;
    public int Letters = 0;
    public TMP_Text letterText;
    public int Anvils = 0;
    public TMP_Text anvilText;
    public int Shields = 0;
    public TMP_Text shieldText;

    [Header("Not Inventory")]
    public int endSceneIndex;

    bool menuOpen = false;

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

        if (Input.GetButtonDown("Pause")) ToggleMenu();

        if (Input.GetButtonDown("Fire1") && menuOpen == false)
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

    private void ToggleMenu()
    {
        if (menuOpen)
        {
            menuOpen = false;
            Time.timeScale = 1;
            Inventory.SetActive(false);
        }
        else
        {
            menuOpen = true;
            Time.timeScale = 0;
            Inventory.SetActive(true);
            coinText.text = Coins.ToString("00");
            keyText.text = Keys.ToString("00");
            pumpkinText.text = Pumpkins.ToString("00");
            potatoText.text = Potatoes.ToString("00");
            carrotText.text = Carrots.ToString("00");
            cornText.text = Corns.ToString("00");
            potionText.text = Potions.ToString("00");
            letterText.text = Letters.ToString("00");
            anvilText.text = Anvils.ToString("00");
            shieldText.text = Shields.ToString("00");
        }
    }

}
