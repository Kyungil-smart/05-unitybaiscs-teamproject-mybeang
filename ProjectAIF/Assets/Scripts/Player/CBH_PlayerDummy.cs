using UnityEngine;


public class PlayerStatusDummy : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHp;
    public float MoveSpeed;
    public int Defense;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AbilityManager.Instance.ReadyToThreeAbilities();
        }
    }
}
