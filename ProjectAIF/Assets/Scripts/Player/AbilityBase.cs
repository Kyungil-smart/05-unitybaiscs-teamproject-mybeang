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
            //_description = "Maximum HP may change.";
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
           // _description = "Move speed may change.";
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
            //_description = "Defense may change.";
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
        private PistolStatus _pistolStatus;
        
        public PistolDamage(PistolStatus pistolStatusStatus)
        {
            _pistolStatus = pistolStatusStatus;
           // _description = "Pistol Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolDamageRange.min,
                AbilityManager.Instance.PistolDamageRange.max);

            value += _pistolStatus.Damage;
            if (value < _pistolStatus.MinDamage || value > _pistolStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatus.Damage = value;
            return true;
        }
    }
    
    public class PistolCriticalChance : AbilityBase
    {
        private PistolStatus _pistolStatus;
        
        public PistolCriticalChance(PistolStatus pistolStatusStatus)
        {
            _pistolStatus = pistolStatusStatus;
            //_description = "Pistol Critical Chance may change.";
        }
        
        public override bool ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.PistolCriticalChanceRange.min,
                AbilityManager.Instance.PistolCriticalChanceRange.max);

            value *= _pistolStatus.CriticalChance;
            if (value < _pistolStatus.MinCritChance || value > _pistolStatus.MaxCritChance)
            {
                return true;
            }

            if (value < 1)
            {
                return false;
            }
            _pistolStatus.CriticalChance = value;
            return true;
        }
    }
    
    public class PistolCriticalDamage : AbilityBase
    {
        private PistolStatus _pistolStatus;
        
        public PistolCriticalDamage(PistolStatus pistolStatusStatus)
        {
            _pistolStatus = pistolStatusStatus;
           // _description = "Pistol Critical Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolCriticalDamageRange.min,
                AbilityManager.Instance.PistolCriticalDamageRange.max);

            value += _pistolStatus.CriticalDamage;
            if (value < _pistolStatus.MinCritDamage || value > _pistolStatus.MaxCritDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatus.CriticalDamage = value;
            return true;
        }
    }
    
    public class PistolMagazine : AbilityBase
    {
        private PistolStatus _pistolStatus;
        
        public PistolMagazine(PistolStatus pistolStatusStatus)
        {
            _pistolStatus = pistolStatusStatus;
            //_description = "Pistol Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolMagazineRange.min,
                AbilityManager.Instance.PistolMagazineRange.max);

            value += _pistolStatus.TotalMagazine;
            if (value < _pistolStatus.MinMagazine || value > _pistolStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _pistolStatus.TotalMagazine = value;
            return true;
        }
    }
    
    public class RifleDamage : AbilityBase
    {
        private RifleStatus _rifleStatus;
        
        public RifleDamage(RifleStatus rifleStatusStatus)
        {
            _rifleStatus = rifleStatusStatus;
           // _description = "Rifle Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleDamageRange.min,
                AbilityManager.Instance.RifleDamageRange.max);
            
            value += _rifleStatus.Damage;
            if (value < _rifleStatus.MinDamage || value > _rifleStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatus.Damage = value;
            return true;
        }
    }
    
    public class RifleCriticalChance : AbilityBase
    {
        private RifleStatus _rifleStatus;
        
        public RifleCriticalChance(RifleStatus rifleStatusStatus)
        {
            _rifleStatus = rifleStatusStatus;
            //_description = "Rifle Critical Chance may change.";
        }
        
        public override bool ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.RifleCriticalChanceRange.min,
                AbilityManager.Instance.RifleCriticalChanceRange.max);
            
            value *= _rifleStatus.CriticalChance;
            if (value < _rifleStatus.MinCritChance || value > _rifleStatus.MaxCritChance)
            {
                return true;
            }

            if (value < 1)
            {
                return false;
            }
            _rifleStatus.CriticalChance = value;
            return true;
        }
    }
    
    public class RifleCriticalDamage : AbilityBase
    {
        private RifleStatus _rifleStatus;
        
        public RifleCriticalDamage(RifleStatus rifleStatusStatus)
        {
            _rifleStatus = rifleStatusStatus;
           // _description = "Rifle Critical Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleCriticalDamageRange.min,
                AbilityManager.Instance.RifleCriticalDamageRange.max);
            
            value += _rifleStatus.CriticalDamage;
            if (value < _rifleStatus.MinCritDamage || value > _rifleStatus.MaxCritDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatus.CriticalDamage = value;
            return true;
        }
    }
    
    public class RifleMagazine : AbilityBase
    {
        private RifleStatus _rifleStatus;
        
        public RifleMagazine(RifleStatus rifleStatusStatus)
        {
            _rifleStatus = rifleStatusStatus;
           // _description = "Rifle Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleMagazineRange.min,
                AbilityManager.Instance.RifleMagazineRange.max);
            
            value += _rifleStatus.TotalMagazine;
            if (value < _rifleStatus.MinMagazine || value > _rifleStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _rifleStatus.TotalMagazine = value;
            return true;
        }
    }
    
    public class GrenadeDamage : AbilityBase
    {
        private GrenadeStatus _grenadeStatus;
        
        public GrenadeDamage(GrenadeStatus grenadeStatusStatus)
        {
            _grenadeStatus = grenadeStatusStatus;
            //_description = "Grenade Damage may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeDamageRange.min,
                AbilityManager.Instance.GrenadeDamageRange.max);
            
            value += _grenadeStatus.Damage;
            if (value < _grenadeStatus.MinDamage || value > _grenadeStatus.MaxDamage)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _grenadeStatus.Damage += value;
            return true;
        }
    }
    public class GrenadeMagazine : AbilityBase
    {
        private GrenadeStatus _grenadeStatus;
        
        public GrenadeMagazine(GrenadeStatus grenadeStatusStatus)
        {
            _grenadeStatus = grenadeStatusStatus;
            //_description = "Grenade Magazine may change.";
        }
        
        public override bool ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeMagazineRange.min,
                AbilityManager.Instance.GrenadeMagazineRange.max);
            
            value += _grenadeStatus.TotalMagazine;
            if (value < _grenadeStatus.MinMagazine || value > _grenadeStatus.MaxMagazine)
            {
                return true;
            }

            if (value < 0)
            {
                return false;
            }
            _grenadeStatus.TotalMagazine += value;
            return true;
        }
    }
}
