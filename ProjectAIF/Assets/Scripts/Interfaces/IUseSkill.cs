using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseSkill
{
    // TODO : 스킬 데미지인 int SkillWaight는 인터페이스에서 강제 할 수 없음으로 스텟부분에서 추가해주셔야 합니다.
    public void Skill(int damage);
}
