using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button _signInButton; // 로그인 버튼

    private async void Awake()
    {
        // UGS 초기화
        await UnityServices.InitializeAsync();
    }

    private void OnEnable()
    {
        _signInButton.onClick.AddListener(async () => await SignInAsync());
    }

    private async Task SignInAsync()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("익명 사용자 로그인 완료");
    }
}
