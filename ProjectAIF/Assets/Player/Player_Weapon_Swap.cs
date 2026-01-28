using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon_Swap : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Transform AR_Slot;
    float Change_Speed = 2f;
    Vector3 basePos;
    Vector3 downPos;
    Vector3 velocity;
    bool isLowering = false;
    float smoothTime = 0.15f;

    int Player_Current_Weapon;

    private void Awake()
    {
        foreach (Transform AR in Player.GetComponentsInChildren<Transform>())
        {
            if (AR.name == "AR_Slot")
            {
                AR_Slot = AR;
                break;
            }
        }

        basePos = AR_Slot.localPosition;
        downPos = basePos + Vector3.down * 0.7f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isLowering = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isLowering = false;
        }

        Vector3 target = isLowering ? downPos : basePos;

        AR_Slot.localPosition = Vector3.SmoothDamp(
            AR_Slot.localPosition,
            target,
            ref velocity,
            smoothTime
        );

        Check_Cur_Weapon();
    }

    void Check_Cur_Weapon()
    {
        
    }
}
