using System.Collections.Generic;
using UnityEngine;

// TODO: 실제 Player 나 무기 Status 에 맞는 것으로 변경해야함.
namespace Ability
{
    public abstract class AbilityBase
    {
        public bool IsUnlucky = false;
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
        private PlayerStatusDummy _playerStatus;
        
        public Hp(PlayerStatusDummy playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.HpRange.min,
                AbilityManager.Instance.HpRange.max);
            
            if (value < 0) PlaySound();
            _playerStatus.MaxHp += value;
        }
    }

    public class MoveSpeed : AbilityBase
    {
        private PlayerStatusDummy _playerStatus;
        
        public MoveSpeed(PlayerStatusDummy playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.SpeedRange.min,
                AbilityManager.Instance.SpeedRange.max);
            
            if (value < 1) PlaySound();
            _playerStatus.MoveSpeed *= value;
        }
    }

    public class Defense : AbilityBase
    {
        private PlayerStatusDummy _playerStatus;
        
        public Defense(PlayerStatusDummy playerStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _playerStatus = playerStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.DefenseRange.min,
                AbilityManager.Instance.DefenseRange.max);
            
            if (value < 0) PlaySound();
            _playerStatus.Defense += value;
        }
    }
    
    public class PistolDamage : AbilityBase
    {
        private PistolDummy _pistolStatus;
        
        public PistolDamage(PistolDummy pistolStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatus = pistolStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolDamageRange.min,
                AbilityManager.Instance.PistolDamageRange.max);
            
            if (value < 0) PlaySound();
            _pistolStatus.Damage += value;
        }
    }
    
    public class PistolCriticalChance : AbilityBase
    {
        private PistolDummy _pistolStatus;
        
        public PistolCriticalChance(PistolDummy pistolStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatus = pistolStatus;
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.PistolCriticalChanceRange.min,
                AbilityManager.Instance.PistolCriticalChanceRange.max);
            
            if (value < 1) PlaySound();
            _pistolStatus.CriticalChance *= value;
        }
    }
    
    public class PistolCriticalDamage : AbilityBase
    {
        private PistolDummy _pistolStatus;
        
        public PistolCriticalDamage(PistolDummy pistolStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatus = pistolStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolCriticalDamageRange.min,
                AbilityManager.Instance.PistolCriticalDamageRange.max);
            
            if (value < 0) PlaySound();
            _pistolStatus.Damage += value;
        }
    }
    
    public class PistolMagazine : AbilityBase
    {
        private PistolDummy _pistolStatus;
        
        public PistolMagazine(PistolDummy pistolStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _pistolStatus = pistolStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.PistolMagazineRange.min,
                AbilityManager.Instance.PistolMagazineRange.max);
            
            if (value < 0) PlaySound();
            _pistolStatus.Damage += value;
        }
    }
    
    public class RifleDamage : AbilityBase
    {
        private RifleDummy _rifleStatus;
        
        public RifleDamage(RifleDummy rifleStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatus = rifleStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleDamageRange.min,
                AbilityManager.Instance.RifleDamageRange.max);
            
            if (value < 0) PlaySound();
            _rifleStatus.Damage += value;
        }
    }
    
    public class RifleCriticalChance : AbilityBase
    {
        private RifleDummy _rifleStatus;
        
        public RifleCriticalChance(RifleDummy rifleStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatus = rifleStatus;
        }
        
        public override void ApplyAbility()
        {
            float value = 1 + Random.Range(
                AbilityManager.Instance.RifleCriticalChanceRange.min,
                AbilityManager.Instance.RifleCriticalChanceRange.max);
            
            if (value < 1) PlaySound();
            _rifleStatus.CriticalChance *= value;
        }
    }
    
    public class RifleCriticalDamage : AbilityBase
    {
        private RifleDummy _rifleStatus;
        
        public RifleCriticalDamage(RifleDummy rifleStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatus = rifleStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleCriticalDamageRange.min,
                AbilityManager.Instance.RifleCriticalDamageRange.max);
            
            if (value < 0) PlaySound();
            _rifleStatus.Damage += value;
        }
    }
    
    public class RifleMagazine : AbilityBase
    {
        private RifleDummy _rifleStatus;
        
        public RifleMagazine(RifleDummy rifleStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _rifleStatus = rifleStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.RifleMagazineRange.min,
                AbilityManager.Instance.RifleMagazineRange.max);
            
            if (value < 0) PlaySound();
            _rifleStatus.Damage += value;
        }
    }
    
    public class GrenadeDamage : AbilityBase
    {
        private GrenadeDummy _grenadeStatus;
        
        public GrenadeDamage(GrenadeDummy grenadeStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _grenadeStatus = grenadeStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeDamageRange.min,
                AbilityManager.Instance.GrenadeDamageRange.max);
            
            if (value < 0) PlaySound();
            _grenadeStatus.Damage += value;
        }
    }
    public class GrenadeMagazine : AbilityBase
    {
        private GrenadeDummy _grenadeStatus;
        
        public GrenadeMagazine(GrenadeDummy grenadeStatus, AudioSource aSrc, AudioClip aClip) : base(aSrc, aClip)
        {
            _grenadeStatus = grenadeStatus;
        }
        
        public override void ApplyAbility()
        {
            int value = Random.Range(
                AbilityManager.Instance.GrenadeMagazineRange.min,
                AbilityManager.Instance.GrenadeMagazineRange.max);
            
            if (value < 0) PlaySound();
            _grenadeStatus.Magazine += value;
        }
    }
}
