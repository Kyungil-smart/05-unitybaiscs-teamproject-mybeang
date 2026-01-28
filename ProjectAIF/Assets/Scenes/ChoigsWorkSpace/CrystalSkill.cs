using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : MonoBehaviour
{
    // 전멸시킬 적 태그 이름
    public string enemyTag = "Enemy";

    // 웨이브당 딱 한번 제한
    private bool used = false;

    // 마우스 클릭시 실행
    private void OnMouseDown()
    {
        // 5초 후 스킬 발동, 전멸 코루틴 시작 
        StartCoroutine(KillAllAfterDelay());
    }
    // 시간지연 가능한 함수 
    IEnumerator KillAllAfterDelay()
    {
        Debug.Log("전방위 스킬 발동");


        // 발동 5초 딜레이
        yield return new WaitForSeconds(5f);

        // Enemy 태그를 가진 몬스터 전부 찾기
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Enemy");
        
        // 전부 제거하기
        foreach (GameObject monster in Monsters)
        {
            Destroy(monster);
        }

        Debug.Log("강대한 힘으로 몬스터 전멸");
    }
}
