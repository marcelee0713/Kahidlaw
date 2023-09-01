using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject meleeObj;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        meleeObj.SetActive(false);
    }

    void Update()
    {
        if (ModeChanger.mode == "Melee" && !DialogueManager.isDialogueActive)
        {
            meleeObj.SetActive(true);
        }
        else
        {
            meleeObj.SetActive(false);

        }
    }
}
