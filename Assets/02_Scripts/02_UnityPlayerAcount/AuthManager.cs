using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AuthUnityPlayerAccount
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signOutButton;
        [SerializeField] private Button _playerNameSaveButton;

        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TMP_InputField _playerNameIF;

        private async void Awake()
        {
            // UGS 초기화
            await UnityServices.InitializeAsync();
            
            //로그인(신규가입) 후 호출될 이벤트 연결
            PlayerAccountService.Instance.SignedIn += OnSignedIn;            
        }

        private void OnEnable()
        {
            _signInButton.onClick.AddListener(async () =>
            {
                // Unity Player Account 로 로그인 시작요청 메서드
                await PlayerAccountService.Instance.StartSignInAsync();
            });
            
            _playerNameSaveButton.onClick.AddListener(async () =>
            {
                await SetPlayerName(_playerNameIF.text);
            });
        }
        
        // 로그인 프로세스 호출
        private async void OnSignedIn()
        {
            // 엑세스 토큰 할당
            string accessToken = PlayerAccountService.Instance.AccessToken;
            Debug.Log("엑세스 토큰 발행");
            _messageText.text = accessToken;
            await SignInUnityAsync(accessToken);
        }
        
        // 실제로 로그인 요청 로직
        private async Task SignInUnityAsync(string accessToken)
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("로그인 성공");
        }
        
        private async Task SetPlayerName(string playerName)
        {
            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            
                // 플레이어 이름 가져오기
                // string __name = await AuthenticationService.Instance.GetPlayerNameAsync();
                string __name = AuthenticationService.Instance.PlayerName;
            
                Debug.Log($"{__name} is Updated");
                _messageText.text = $"{__name} is Updated";
            }
            catch (AuthenticationException e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
