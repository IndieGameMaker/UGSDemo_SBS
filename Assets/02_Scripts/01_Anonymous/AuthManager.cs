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
        _signInButton.interactable = false;
        
        UnityServices.Instance.Initialized += () =>
        {
            Debug.Log("UGS 초기화 완료");
            BindingEvents();
            _signInButton.interactable = true;
        };
        
        // UGS 초기화
        await UnityServices.InitializeAsync();
    }

    private void OnEnable()
    {
        _signInButton.onClick.AddListener(async () => await SignInAsync());
    }

    private async Task SignInAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }

    // 인증 관련 이벤트 연결
    private void BindingEvents()
    {
        // 로그인 성공 이벤트
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("익명 사용자 로그인 완료");
        };
    }
}
