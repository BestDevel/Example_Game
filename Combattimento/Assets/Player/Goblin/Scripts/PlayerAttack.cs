using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public enum GoblinComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3
}

public class PlayerAttack : MonoBehaviour
{
    private Animator player_Anim;

    private bool activateTimerToReset;

    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;

    private GoblinComboState current_Combo_State;

    private bool statoTasto = true;

    void Awake()
    {
        player_Anim = GetComponent<Animator>();
    }

    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = GoblinComboState.NONE;
    }


    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }

    void ComboAttacks()
    {
        if(Input.GetKeyUp(KeyCode.W))
        {
            if (statoTasto == true)
            {
                player_Anim.SetTrigger("Movement");
                statoTasto = false;
            }
            else
            {
                player_Anim.ResetTrigger("Movement");
                statoTasto = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            player_Anim.SetBool("Punch2", false);
            player_Anim.SetBool("Punch3", false);
            player_Anim.SetBool("Punch1", true);
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            player_Anim.SetBool("Punch1", false);
            player_Anim.SetBool("Punch3", false);
            player_Anim.SetBool("Punch2", true);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            player_Anim.SetBool("Punch1", false);
            player_Anim.SetBool("Punch2", false);
            player_Anim.SetBool("Punch3", true);
        }

    }// combo attacks

    void ResetComboState()
    {
        if(activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;

            if(current_Combo_Timer <= 0f)
            {
                current_Combo_State = GoblinComboState.NONE;
                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
}
