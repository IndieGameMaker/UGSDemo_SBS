using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TestThread : MonoBehaviour
{
    // 비동기 (Async)
    // 동기 (Sync)
    
    private async void Start()
    {
        await GameDataInit();
    }

    private async Task GameDataInit()
    {
        // Thread.Sleep(3000); // 3초동안 멈춤, Blocking 방식 (동기)
        await Task.Delay(3000); // Non-Blocking 방식 (프리징 현상이 없어)
        Debug.Log("게임 데이터 로드 완료");
    }

    private void Update()
    {
        Debug.Log("다른 작업 실행 중...");
    }
}
