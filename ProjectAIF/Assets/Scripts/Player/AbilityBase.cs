using System.Collections.Generic;
using UnityEngine;

// TODO: 실제 Player 나 무기 Status 에 맞는 것으로 변경해야함.
namespace Ability
{
    public abstract class AbilityBase
    {
        protected string _description;
        public string Description => _description;

        /// <summary>
        /// 플레이어 어빌리티 적용
        /// </summary>
        /// <returns>Unlucky 면 false, Lucky 면 True</returns>
        public abstract bool ApplyAbility();
    }

    public class Hp : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public Hp(PlayerStatus playerStatus)
        {
            _playerStatus = playerStatus;
            _description = "Maximum HP may change.";
        }
        
        public override bool ApplyAbility()  
        {
            int value = Random.Range(
                AbilityManager.Instance.HpRange.min,
                AbilityManager.Instance.HpRange.max);

            value += _playerStatus.TotalHp;
            if (value < _playerStatus.MinHp || value > _playerStatus.MaxHp)
            {
                return true;
            }
            if (value < 0)
            {
                return false;
            }
            _playerStatus.TotalHp = value;
            return true;
        }
    }

    public class MoveSpeed : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public MoveSpeed(PlayerStatus playerStatus) 
        {
            _playerStatus = playerStatus;
            _description = "Move speed may change.";
        }
        
        public override bool ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.MoveSpeedRange.min,
                AbilityManager.Instance.MoveSpeedRange.max);
            
            value *= _playerStatus.MoveSpeed;
            if (value < _playerStatus.MinMoveSpeed || value > _playerStatus.MaxMoveSpeed)
            {
                return true;
            }

            if (value < 1)
            {
                return false;
            }
            _playerStatus.MoveSpeed = value;
            return true;
        }
    }

    public class Defense : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public Defense(PlayerStatus playerStatus)
        {
            _playerStatus = playerStatus;
            _description = "Defense may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.DefenseRange.min,
                AbilityManager.Instance.DefenseRange.max);

            value += _playerStatus.Defense;
            if (value < _playerStatus.MinDefense || value > _playerStatus.MaxDefense)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _playerStatus.Defense = value;
            return true;
        }
    }
    
    public class PistolDamage : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolDamage(PistolStatus pistolStatusStatus)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolDamageRange.min,
                AbilityManager.Instance.PistolDamageRange.max);

            value += _pistolStatusStatus.Damage;
            if (value < _pistolStatusStatus.MinDamage || value > _pistolStatusStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatusStatus.Damage = value;
            return true;
        }
    }
    
    public class PistolCriticalChance : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolCriticalChance(PistolStatus pistolStatusStatus)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Critical Chance may change.";
        }
        
        public override bool ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.PistolCriticalChanceRange.min,
                AbilityManager.Instance.PistolCriticalChanceRange.max);

            value *= _pistolStatusStatus.CriticalChance;
            if (value < _pistolStatusStatus.MinCritChance || value > _pistolStatusStatus.MaxCritChance)
            {
                return true;
            }

            if (value < 1)
            {
                return false;
            }
            _pistolStatusStatus.CriticalChance = value;
            return true;
        }
    }
    
    public class PistolCriticalDamage : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolCriticalDamage(PistolStatus pistolStatusStatus)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Critical Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolCriticalDamageRange.min,
                AbilityManager.Instance.PistolCriticalDamageRange.max);

            value += _pistolStatusStatus.Damage;
            if (value < _pistolStatusStatus.MinCritDamage || value > _pistolStatusStatus.MaxCritDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatusStatus.Damage = value;
            return true;
        }
    }
    
    public class PistolMagazine : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolMagazine(PistolStatus pistolStatusStatus)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolMagazineRange.min,
                AbilityManager.Instance.PistolMagazineRange.max);

            value += _pistolStatusStatus.TotalMagazine;
            if (value < _pistolStatusStatus.MinMagazine || value > _pistolStatusStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatusStatus.TotalMagazine = value;
            return true;
        }
    }
    
    public class RifleDamage : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleDamage(RifleStatus rifleStatusStatus)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleDamageRange.min,
                AbilityManager.Instance.RifleDamageRange.max);
            
            value += _rifleStatusStatus.Damage;
            if (value < _rifleStatusStatus.MinDamage || value > _rifleStatusStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatusStatus.Damage = value;
            return true;
        }
    }
    
    public class RifleCriticalChance : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleCriticalChance(RifleStatus rifleStatusStatus)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Critical Chance may change.";
        }
        
        public override bool ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.RifleCriticalChanceRange.min,
                AbilityManager.Instance.RifleCriticalChanceRange.max);
            
            value *= _rifleStatusStatus.CriticalChance;
            if (value < _rifleStatusStatus.MinCritChance || value > _rifleStatusStatus.MaxCritChance)
            {
                return true;
            }

            if (value < 1)
            {
                return false;
            }
            _rifleStatusStatus.CriticalChance = value;
            return true;
        }
    }
    
    public class RifleCriticalDamage : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleCriticalDamage(RifleStatus rifleStatusStatus)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Critical Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleCriticalDamageRange.min,
                AbilityManager.Instance.RifleCriticalDamageRange.max);
            
            value += _rifleStatusStatus.CriticalDamage;
            if (value < _rifleStatusStatus.MinCritDamage || value > _rifleStatusStatus.MaxCritDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatusStatus.CriticalDamage = value;
            return true;
        }
    }
    
    public class RifleMagazine : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleMagazine(RifleStatus rifleStatusStatus)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleMagazineRange.min,
                AbilityManager.Instance.RifleMagazineRange.max);
            
            value += _rifleStatusStatus.TotalMagazine;
            if (value < _rifleStatusStatus.MinMagazine || value > _rifleStatusStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatusStatus.TotalMagazine = value;
            return true;
        }
    }
    
    public class GrenadeDamage : AbilityBase
    {
        private GrenadeStatus _grenadeStatusStatus;
        
        public GrenadeDamage(GrenadeStatus grenadeStatusStatus)
        {
            _grenadeStatusStatus = grenadeStatusStatus;
            _description = "Grenade Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeDamageRange.min,
                AbilityManager.Instance.GrenadeDamageRange.max);
            
            value += _grenadeStatusStatus.Damage;
            if (value < _grenadeStatusStatus.MinDamage || value > _grenadeStatusStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _grenadeStatusStatus.Damage += value;
            return true;
        }
    }
    public class GrenadeMagazine : AbilityBase
    {
        private GrenadeStatus _grenadeStatusStatus;
        
        public GrenadeMagazine(GrenadeStatus grenadeStatusStatus)
        {
            _grenadeStatusStatus = grenadeStatusStatus;
            _description = "Grenade Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeMagazineRange.min,
                AbilityManager.Instance.GrenadeMagazineRange.max);
            
            value += _grenadeStatusStatus.TotalMagazine;
            if (value < _grenadeStatusStatus.MinMagazine || value > _grenadeStatusStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _grenadeStatusStatus.TotalMagazine += value;
            return true;
        }
    }
}
