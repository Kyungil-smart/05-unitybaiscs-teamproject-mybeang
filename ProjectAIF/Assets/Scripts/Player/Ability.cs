using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    // Player 더미 클래스
    public class DummyPlayer
    {
        public int Hp = 100;
        public float Speed = 10;
        public int Defense = 0;
    }

    // Weapon 더미 클래스
    public class DummyWeapon
    {
        public int Damage = 0;
        public float CriticalChance = 0.3f;
        public int CriticalDamage = 10;
    }


    public abstract class Ability {}

    public class Hp : Ability, IAbilityPlus
    {
        public void PlusValue<DummyPlayer>(DummyPlayer player)
        {
            // player.Hp += Random.Range(
            //     AbilityManager.Instance.HpRange.min,
            //     AbilityManager.Instance.HpRange.max);
        }
    }

    public class MoveSpeed : Ability, IAbilityRate
    {
        public void MultipleValue<DummyPlayer>(DummyPlayer player)
        {
            // player.MoveSpeed *= ( 1 + Random.Range(
            //     AbilityManager.Instance.SpeedRange.min,
            //     AbilityManager.Instance.SpeedRange.max));
        }
    }

    public class Defense : Ability, IAbilityPlus
    {
        public void PlusValue<DummyPlayer>(DummyPlayer player)
        {
            // player.MoveSpeed += Random.Range(
            //     AbilityManager.Instance.DefenseRange.min,
            //     AbilityManager.Instance.DefenseRange.max);
        }
    }
    
    public class PistoalDamage : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Damage += Random.Range(
            //     AbilityManager.Instance.PistolDamageRange.min,
            //     AbilityManager.Instance.PistolDamageRange.max
            // );
        }
    }
    
    public class PistoalCriticalChance : Ability, IAbilityRate
    {
        public void MultipleValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.CriticalChance *= ( 1 + Random.Range(
            //     AbilityManager.Instance.PistolCriticalChanceRange.min,
            // AbilityManager.Instance.PistolCriticalChanceRange.max));
        }
    }
    
    public class PistoalCriticalDamage : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Damage += Random.Range(
            //     AbilityManager.Instance.PistolCriticalDamageRange.min,
            //     AbilityManager.Instance.PistolCriticalDamageRange.max
            // );
        }
    }
    
    public class PistolMagazine : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Magazine += Random.Range(
            //     AbilityManager.Instance.PistolMagazineRange.min,
            //     AbilityManager.Instance.PistolMagazineRange.max
            // );
        }
    }
    
    public class RifleDamage : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Damage += Random.Range(
            //     AbilityManager.Instance.RifleDamageRange.min,
            //     AbilityManager.Instance.RifleDamageRange.max
            // );
        }
    }
    
    public class RifleCriticalChance : Ability, IAbilityRate
    {
        public void MultipleValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.CriticalChance *= ( 1 + Random.Range(
            //     AbilityManager.Instance.PistolCriticalChanceRange.min,
            // AbilityManager.Instance.PistolCriticalChanceRange.max));
        }
    }
    
    public class RifleCriticalDamage : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Damage += Random.Range(
            //     AbilityManager.Instance.PistolCriticalDamageRange.min,
            //     AbilityManager.Instance.PistolCriticalDamageRange.max
            // );
        }
    }
    
    public class RifleMagazine : Ability, IAbilityPlus
    {
        public void PlusValue<DummyWeapon>(DummyWeapon weapon)
        {
            // weapon.Magazine += Random.Range(
            //     AbilityManager.Instance.PistolMagazineRange.min,
            //     AbilityManager.Instance.PistolMagazineRange.max
            // );
        }
    }
}
