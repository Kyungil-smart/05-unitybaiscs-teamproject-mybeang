using System.Collections.Generic;
using UnityEngine;

// TODO: 실제 Player 나 무기 Status 에 맞는 것으로 변경해야함.
namespace Ability
{
    public abstract class AbilityBase
    {
        public bool IsUnlucky = false;
        protected string _description;
        public string Description => _description;
        protected AudioSource _audioSource;
        protected AudioClip _audioClip;

        public AbilityBase(AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            _audioClip = audioClip;
        }
        
        protected void PlaySound()
        {
            if (IsUnlucky && _audioClip != null)
            {
                _audioSource?.PlayOneShot(_audioClip);
            }
        }

        public abstract void ApplyAbility();
    }

    public class Hp : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public Hp(PlayerStatus playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
            _description = "Maximum HP may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.HpRange.min,
                AbilityManager.Instance.HpRange.max);

            value += _playerStatus.TotalHp;
            if (value < _playerStatus.MinHp || value > _playerStatus.MaxHp)
            {
                return;
            }
            if (value < 0)
            {
                PlaySound();
            }
            _playerStatus.TotalHp = value;
        }
    }

    public class MoveSpeed : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public MoveSpeed(PlayerStatus playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
            _description = "Move speed may change.";
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.MoveSpeedRange.min,
                AbilityManager.Instance.MoveSpeedRange.max);
            
            value *= _playerStatus.MoveSpeed;
            if (value < _playerStatus.MinMoveSpeed || value > _playerStatus.MaxMoveSpeed)
            {
                return;
            }

            if (value < 1)
            {
                PlaySound();
            }
            _playerStatus.MoveSpeed = value;
        }
    }

    public class Defense : AbilityBase
    {
        private PlayerStatus _playerStatus;
        
        public Defense(PlayerStatus playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
            _description = "Defense may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.DefenseRange.min,
                AbilityManager.Instance.DefenseRange.max);

            value += _playerStatus.Defense;
            if (value < _playerStatus.MinDefense || value > _playerStatus.MaxDefense)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _playerStatus.Defense = value;
        }
    }
    
    public class PistolDamage : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolDamage(PistolStatus pistolStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Damage may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolDamageRange.min,
                AbilityManager.Instance.PistolDamageRange.max);

            value += _pistolStatusStatus.Damage;
            if (value < _pistolStatusStatus.MinDamage || value > _pistolStatusStatus.MaxDamage)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _pistolStatusStatus.Damage = value;
        }
    }
    
    public class PistolCriticalChance : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolCriticalChance(PistolStatus pistolStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Critical Chance may change.";
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.PistolCriticalChanceRange.min,
                AbilityManager.Instance.PistolCriticalChanceRange.max);

            value *= _pistolStatusStatus.CriticalChance;
            if (value < _pistolStatusStatus.MinCritChance || value > _pistolStatusStatus.MaxCritChance)
            {
                return;
            }

            if (value < 1)
            {
                PlaySound();
            }
            _pistolStatusStatus.CriticalChance = value;
        }
    }
    
    public class PistolCriticalDamage : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolCriticalDamage(PistolStatus pistolStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Critical Damage may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolCriticalDamageRange.min,
                AbilityManager.Instance.PistolCriticalDamageRange.max);

            value += _pistolStatusStatus.Damage;
            if (value < _pistolStatusStatus.MinCritDamage || value > _pistolStatusStatus.MaxCritDamage)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _pistolStatusStatus.Damage = value;
        }
    }
    
    public class PistolMagazine : AbilityBase
    {
        private PistolStatus _pistolStatusStatus;
        
        public PistolMagazine(PistolStatus pistolStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatusStatus = pistolStatusStatus;
            _description = "Pistol Magazine may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolMagazineRange.min,
                AbilityManager.Instance.PistolMagazineRange.max);

            value += _pistolStatusStatus.TotalMagazine;
            if (value < _pistolStatusStatus.MinMagazine || value > _pistolStatusStatus.MaxMagazine)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _pistolStatusStatus.TotalMagazine = value;
        }
    }
    
    public class RifleDamage : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleDamage(RifleStatus rifleStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Damage may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleDamageRange.min,
                AbilityManager.Instance.RifleDamageRange.max);
            
            value += _rifleStatusStatus.Damage;
            if (value < _rifleStatusStatus.MinDamage || value > _rifleStatusStatus.MaxDamage)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _rifleStatusStatus.Damage = value;
        }
    }
    
    public class RifleCriticalChance : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleCriticalChance(RifleStatus rifleStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Critical Chance may change.";
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.RifleCriticalChanceRange.min,
                AbilityManager.Instance.RifleCriticalChanceRange.max);
            
            value *= _rifleStatusStatus.CriticalChance;
            if (value < _rifleStatusStatus.MinCritChance || value > _rifleStatusStatus.MaxCritChance)
            {
                return;
            }

            if (value < 1)
            {
                PlaySound();
            }
            _rifleStatusStatus.CriticalChance = value;
        }
    }
    
    public class RifleCriticalDamage : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleCriticalDamage(RifleStatus rifleStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Critical Damage may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleCriticalDamageRange.min,
                AbilityManager.Instance.RifleCriticalDamageRange.max);
            
            value += _rifleStatusStatus.CriticalDamage;
            if (value < _rifleStatusStatus.MinCritDamage || value > _rifleStatusStatus.MaxCritDamage)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _rifleStatusStatus.CriticalDamage = value;
        }
    }
    
    public class RifleMagazine : AbilityBase
    {
        private RifleStatus _rifleStatusStatus;
        
        public RifleMagazine(RifleStatus rifleStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatusStatus = rifleStatusStatus;
            _description = "Rifle Magazine may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleMagazineRange.min,
                AbilityManager.Instance.RifleMagazineRange.max);
            
            value += _rifleStatusStatus.TotalMagazine;
            if (value < _rifleStatusStatus.MinMagazine || value > _rifleStatusStatus.MaxMagazine)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _rifleStatusStatus.TotalMagazine = value;
        }
    }
    
    public class GrenadeDamage : AbilityBase
    {
        private GrenadeStatus _grenadeStatusStatus;
        
        public GrenadeDamage(GrenadeStatus grenadeStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _grenadeStatusStatus = grenadeStatusStatus;
            _description = "Grenade Damage may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeDamageRange.min,
                AbilityManager.Instance.GrenadeDamageRange.max);
            
            value += _grenadeStatusStatus.Damage;
            if (value < _grenadeStatusStatus.MinDamage || value > _grenadeStatusStatus.MaxDamage)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _grenadeStatusStatus.Damage += value;
        }
    }
    public class GrenadeMagazine : AbilityBase
    {
        private GrenadeStatus _grenadeStatusStatus;
        
        public GrenadeMagazine(GrenadeStatus grenadeStatusStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _grenadeStatusStatus = grenadeStatusStatus;
            _description = "Grenade Magazine may change.";
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeMagazineRange.min,
                AbilityManager.Instance.GrenadeMagazineRange.max);
            
            value += _grenadeStatusStatus.TotalMagazine;
            if (value < _grenadeStatusStatus.MinMagazine || value > _grenadeStatusStatus.MaxMagazine)
            {
                return;
            }

            if (value < 0)
            {
                PlaySound();
            }
            _grenadeStatusStatus.TotalMagazine += value;
        }
    }
}
