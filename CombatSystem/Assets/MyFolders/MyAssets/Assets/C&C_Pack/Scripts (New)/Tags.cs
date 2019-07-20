using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis
{
    public const string VERTICAL_AXIS = "Vertical";
    public const string HORIZONTAL_AXIS = "Horizontal";
}

public class Tags
{
    public const string PLAYER_TAG = "Player";
}

public class AnimationTags
{
    public const string WALK_PARAMETER = "Walk";
    public const string DEFEND_PARAMETER = "Defend";
    public const string ATTACK_TRIGGER_1 = "Attack1";
    public const string ATTACK_TRIGGER_2 = "Attack2";
    internal static string MOVEMENT;
    internal static string PUNCH_1_TRIGGER;
    internal static string PUNCH_2_TRIGGER;
    internal static string KICK_2_TRIGGER;

    public static string PUNCH_3_TRIGGER { get; internal set; }
    public static string KICK_1_TRIGGER { get; internal set; }
}
