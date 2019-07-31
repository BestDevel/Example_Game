using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoblinComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    //KICK_1,
    //KICK_2
}

public class GoblinPlayerAttack : MonoBehaviour
{
    private CharacterAnimation player_Anim;

    private bool activateTimerToReset;

    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;

    private GoblinComboState current_Combo_State;

    void Awake()
    {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if(current_Combo_State == GoblinComboState.PUNCH_1)
            {
                player_Anim.Punch_1();
            }

            if (current_Combo_State == GoblinComboState.PUNCH_2)
            {
                player_Anim.Punch_2();
            }

            if (current_Combo_State == GoblinComboState.PUNCH_3)
            {
                player_Anim.Punch_3();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            player_Anim.Kick_1();
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
