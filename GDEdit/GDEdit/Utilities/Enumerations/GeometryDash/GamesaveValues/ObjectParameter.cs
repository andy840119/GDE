using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Objects.GeometryDash;

namespace GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues
{
    /// <summary>This enumeration provides values for the parameters of a <see cref="LevelObject"/>.</summary>
    public enum ObjectParameter
    {
        /// <summary>Represents the ID of the <see cref="LevelObject"/>.</summary>
        ID = 1,
        /// <summary>Represents the X location of the <see cref="LevelObject"/> in units.</summary>
        X = 2,
        /// <summary>Represents the Y location of the <see cref="LevelObject"/> in units.</summary>
        Y = 3,
        /// <summary>Represents a value determining whether the <see cref="LevelObject"/> is flipped horizontally.</summary>
        FlippedHorizontally = 4,
        /// <summary>Represents a value determining whether the <see cref="LevelObject"/> is flipped vertically.</summary>
        FlippedVertically = 5,
        /// <summary>Represents the rotation of the <see cref="LevelObject"/> in degrees (positive is clockwise).</summary>
        Rotation = 6,
        /// <summary>Represents the Red value of the color in the trigger.</summary>
        Red = 7,
        /// <summary>Represents the Green value of the color in the trigger.</summary>
        Green = 8,
        /// <summary>Represents the Blue value of the color in the trigger.</summary>
        Blue = 9,
        /// <summary>Represents the duration of the effect of the trigger.</summary>
        Duration = 10,
        /// <summary>Represents the Touch Triggered value of the trigger.</summary>
        TouchTriggered = 11,
        /// <summary>Represents the ID value of the Secret Coin.</summary>
        SecretCoinID = 12,
        /// <summary>Represents the checked property of the portal.</summary>
        PortalChecked = 13,
        /// <summary>Unknown feature with ID 14.</summary>
        UnknownFeature14 = 14,
        /// <summary>Represents the Player Color 1 property of the Color trigger.</summary>
        SetColorToPlayerColor1 = 15,
        /// <summary>Represents the Player Color 2 property of the Color trigger.</summary>
        SetColorToPlayerColor2 = 16,
        /// <summary>Represents the Blending property of the Color trigger.</summary>
        Blending = 17,
        /// <summary>Unknown feature with ID 18.</summary>
        UnknownFeature18 = 18,
        /// <summary>Unknown feature with ID 19.</summary>
        UnknownFeature19 = 19,
        /// <summary>Represents the Editor Layer 1 value of the <see cref="LevelObject"/>.</summary>
        EL1 = 20,
        /// <summary>Represents the Main Color Channel value of the <see cref="LevelObject"/>.</summary>
        Color1 = 21,
        /// <summary>Represents the Detail Color Channel value of the <see cref="LevelObject"/>.</summary>
        Color2 = 22,
        /// <summary>Represents the Target Color ID property of the Color trigger.</summary>
        TargetColorID = 23,
        /// <summary>Represents the Z Layer value of the <see cref="LevelObject"/>.</summary>
        ZLayer = 24,
        /// <summary>Represents the Z Order value of the <see cref="LevelObject"/>.</summary>
        ZOrder = 25,
        /// <summary>Unknown feature with ID 26.</summary>
        UnknownFeature26 = 26,
        /// <summary>Unknown feature with ID 27.</summary>
        UnknownFeature27 = 27,
        /// <summary>Represents the Move X value of the Move trigger in units.</summary>
        MoveX = 28,
        /// <summary>Represents the Move Y value of the Move trigger in units.</summary>
        MoveY = 29,
        /// <summary>Represents the Easing mode value of the trigger.</summary>
        Easing = 30,
        /// <summary>Represents the text of the text object encrypted in Base 64.</summary>
        TextObjectText = 31,
        /// <summary>Represents the scaling of the <see cref="LevelObject"/>.</summary>
        Scaling = 32,
        /// <summary>Unknown feature with ID 33.</summary>
        UnknownFeature33 = 33,
        /// <summary>Represents the Group Parent property of the <see cref="LevelObject"/>.</summary>
        GroupParent = 34,
        /// <summary>Represents the opacity value of the trigger.</summary>
        Opacity = 35,
        /// <summary>Represents the value for whether the <see cref="LevelObject"/> is a trigger or not. This is not confirmed.</summary>
        IsTrigger = 36,
        /// <summary>Unknown feature with ID 37.</summary>
        UnknownFeature37 = 37,
        /// <summary>Unknown feature with ID 38.</summary>
        UnknownFeature38 = 38,
        /// <summary>Unknown feature with ID 39.</summary>
        UnknownFeature39 = 39,
        /// <summary>Unknown feature with ID 40.</summary>
        UnknownFeature40 = 40,
        /// <summary>Represents the Color 1 HSV enabled value of the object.</summary>
        Color1HSVEnabled = 41,
        /// <summary>Represents the Color 2 HSV enabled value of the object.</summary>
        Color2HSVEnabled = 42,
        /// <summary>Represents the Color 1 HSV values of the object.</summary>
        Color1HSVValues = 43,
        /// <summary>Represents the Color 2 HSV values of the object.</summary>
        Color2HSVValues = 44,
        /// <summary>Represents the Fade In value of the Pulse trigger.</summary>
        FadeIn = 45,
        /// <summary>Represents the Hold value of the Pulse trigger.</summary>
        Hold = 46,
        /// <summary>Represents the Fade Out value of the Pulse trigger.</summary>
        FadeOut = 47,
        /// <summary>Represents the Pulse Mode value of the Pulse trigger.</summary>
        PulseMode = 48,
        /// <summary>Represents the Copied Color HSV values of the trigger.</summary>
        CopiedColorHSVValues = 49,
        /// <summary>Represents the Copied Color ID value of the trigger.</summary>
        CopiedColorID = 50,
        /// <summary>Represents the Target Group ID value of the trigger.</summary>
        TargetGroupID = 51,
        /// <summary>Represents the Target Type value of the Pulse trigger.</summary>
        TargetType = 52,
        /// <summary>Unknown feature with ID 53.</summary>
        UnknownFeature53 = 53,
        /// <summary>Represents the value for the distance of the yellow teleportation portal from the blue teleportation portal.</summary>
        YellowTeleportationPortalDistance = 54,
        /// <summary>Unknown feature with ID 55.</summary>
        UnknownFeature55 = 55,
        /// <summary>Represents the Activate Group property of the trigger.</summary>
        ActivateGroup = 56,
        /// <summary>Represents the assigned Group IDs of the <see cref="LevelObject"/>.</summary>
        GroupIDs = 57,
        /// <summary>Represents the Lock To Player X property of the Move trigger.</summary>
        LockToPlayerX = 58,
        /// <summary>Represents the Lock To Player Y property of the Move trigger.</summary>
        LockToPlayerY = 59,
        /// <summary>Represents the Copy Opacity property of the trigger.</summary>
        CopyOpacity = 60,
        /// <summary>Represents the Editor Layer 2 value of the <see cref="LevelObject"/>.</summary>
        EL2 = 61,
        /// <summary>Represents the Spawn Triggered value of the trigger.</summary>
        SpawnTriggered = 62,
        /// <summary>Represents the Delay in the Spawn Trigger.</summary>
        SpawnDelay = 63,
        /// <summary>Represents the Don't Fade property of the <see cref="LevelObject"/>.</summary>
        DontFade = 64,
        /// <summary>Represents the Main Only property of the Pulse trigger.</summary>
        MainOnly = 65,
        /// <summary>Represents the Detail Only property of the Pulse trigger.</summary>
        DetailOnly = 66,
        /// <summary>Represents the Don't Enter property of the <see cref="LevelObject"/>.</summary>
        DontEnter = 67,
        /// <summary>Represents the Degrees value of the Rotate trigger.</summary>
        Degrees = 68,
        /// <summary>Represents the Times 360 value of the Rotate trigger.</summary>
        Times360 = 69,
        /// <summary>Represents the Lock Object Rotation property of the Rotate trigger.</summary>
        LockObjectRotation = 70,
        /// <summary>Represents the Follow Group ID value of the Follow Trigger.</summary>
        FollowGroupID = 71,
        /// <summary>Represents the Target Pos Group ID value of the Move Trigger.</summary>
        TargetPosGroupID = 71,
        /// <summary>Represents the Center Group ID value of the Rotate Trigger.</summary>
        CenterGroupID = 71,
        /// <summary>Represents the secondary Group ID value of some triggers.</summary>
        SecondaryGroupID = 71,
        /// <summary>Represents the X Mod of value the Follow Trigger.</summary>
        XMod = 72,
        /// <summary>Represents the Y Mod of value the Follow Trigger.</summary>
        YMod = 73,
        /// <summary>Represents a value in the Follow trigger whose use is unknown.</summary>
        UnknownFollowTriggerFeature = 74,
        /// <summary>Represents the Strength value of the Shake trigger.</summary>
        Strength = 75,
        /// <summary>Represents the Animation ID value of the Animate trigger.</summary>
        AnimationID = 76,
        /// <summary>Represents the Count value of the Pickup trigger or Pickup Item.</summary>
        Count = 77,
        /// <summary>Represents the Subtract Count property of the Pickup trigger or Pickup Item.</summary>
        SubtractCount = 78,
        /// <summary>Represents the Pickup Mode value of the Pickup Item.</summary>
        PickupMode = 79,
        /// <summary>Represents the Target Item ID value, or the assigned Item ID value of the Pickup trigger or Pickup Item respectively.</summary>
        ItemID = 80,
        /// <summary>Represents the Block ID value of the Collision block <see cref="LevelObject"/>.</summary>
        BlockID = 80,
        /// <summary>Represents the Block A ID value of the Collision trigger.</summary>
        BlockAID = 80,
        /// <summary>Represents the Hold Mode value of the Touch trigger.</summary>
        HoldMode = 81,
        /// <summary>Represents the Toggle Mode value of the Touch trigger.</summary>
        ToggleMode = 82,
        /// <summary>Unknown feature with ID 83.</summary>
        UnknownFeature83 = 83,
        /// <summary>Represents the Interval value of the Shake trigger.</summary>
        Interval = 84,
        /// <summary>Represents the Easing Rate value of the trigger.</summary>
        EasingRate = 85,
        /// <summary>Represents the Exclusive property of the Pulse trigger.</summary>
        Exclusive = 86,
        /// <summary>Represents the Multi Trigger property of the trigger.</summary>
        MultiTrigger = 87,
        /// <summary>Represents the Comparison value of the Instant Count trigger.</summary>
        Comparison = 88,
        /// <summary>Represents the Dual Mode property of the Touch trigger.</summary>
        DualMode = 89,
        /// <summary>Represents the Speed value of the Follow Player Y trigger.</summary>
        Speed = 90,
        /// <summary>Represents the Delay of the Follow Player Y Trigger.</summary>
        FollowDelay = 91,
        /// <summary>Represents the Offset value of the Follow Player Y trigger.</summary>
        YOffset = 92,
        /// <summary>Represents the Trigger On Exit property of the Collision trigger.</summary>
        TriggerOnExit = 93,
        /// <summary>Represents the Dynamic Block property of the Collision block.</summary>
        DynamicBlock = 94,
        /// <summary>Represents the Block B ID of the Collision trigger.</summary>
        BlockBID = 95,
        /// <summary>Determines whether the glow of the <see cref="LevelObject"/> is disabled or not.</summary>
        DisableGlow = 96,
        /// <summary>Represents the custom rotation speed of the rotating <see cref="LevelObject"/> in degrees per second.</summary>
        CustomRotationSpeed = 97,
        /// <summary>Determines whether the rotation of the rotating <see cref="LevelObject"/> is disabled or not.</summary>
        DisableRotation = 98,
        /// <summary>Represents the Multi Activate property of the Count trigger.</summary>
        MultiActivate = 99,
        /// <summary>Determines whether the Use Target of the Move trigger is enabled or not.</summary>
        EnableUseTarget = 100,
        /// <summary>Represents the coordinates that the <see cref="LevelObject"/> will follow the <see cref="LevelObject"/> in the Target Pos Group ID.</summary>
        TargetPosCoordinates = 101,
        /// <summary>Determines whether the Spawn trigger will be disabled while playtesting in the editor. (Currently dysfunctional as of 2.103)</summary>
        EditorDisable = 102,
        /// <summary>Determines whether the <see cref="LevelObject"/> is only enabled in High Detail Mode.</summary>
        HighDetail = 103,
        /// <summary>Unknown feature with ID 104.</summary>
        UnknownFeature104 = 104,
        /// <summary>Represents the coordinates that the <see cref="LevelObject"/> will follow the <see cref="LevelObject"/> in the Target Pos Group ID.</summary>
        MaxSpeed = 105,
        /// <summary>Determines whether the animated <see cref="LevelObject"/> will randomly start.</summary>
        RandomizeStart = 106,
        /// <summary>Represents the animation speed of the animated <see cref="LevelObject"/>.</summary>
        AnimationSpeed = 107,
        /// <summary>Represents the linked Group ID of the <see cref="LevelObject"/>.</summary>
        LinkedGroupID = 108,
        /// <summary>Represents whether the player switches direction of the orb <see cref="LevelObject"/>.</summary>
        OrbSwitchPlayerDirection = -1
    }
}
