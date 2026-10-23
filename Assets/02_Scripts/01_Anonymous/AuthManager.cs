using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button _signInButton; // 로그인 버튼
    [SerializeField] private Button _signOutButton; // 로그아웃 버튼
    [SerializeField] private TextMeshProUGUI _messageText;

    [SerializeField] private Button _playerNameSaveButton;
    [SerializeField] private TMP_InputField _playerNameIF;
    

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
        _signOutButton.onClick.AddListener(() =>
        {
            // 로그 아웃
            AuthenticationService.Instance.SignOut();
        });
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
            _messageText.text = $"Player Id: {AuthenticationService.Instance.PlayerId}";
        };
        
        // 로그아웃 이벤트
        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("익명 사용자 로그아웃 완료");
            _messageText.text = "";
        };
        
        // 로그인 실패
        AuthenticationService.Instance.SignInFailed += (e) =>
        {
            Debug.Log($"로그인 실패: {e.Message}");
        };
        
        // 세션 종료
        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("세션 종료(Timeout)");
        };
    }
    
    // 사용자 이름 (NickName 설정)
    // 50자까지 허용
    // 중간 공백 불가
    // 4자리 해시값 자동으로 붙는다. Zack => Zack#1234

    private async Task SetPlayerName(string playerName)
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            Debug.Log($"{playerName} is Updated");
            _messageText.text = $"{playerName} is Updated";
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }
}
