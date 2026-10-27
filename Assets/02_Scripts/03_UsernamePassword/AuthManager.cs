using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AuthUsernamePassword
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userNameIF, _passwordIF;
        [SerializeField] private Button _signupButton;
        [SerializeField] private Button _signinButton;

        private async void Awake()
        {
            try
            {
                await UnityServices.InitializeAsync();
                Debug.Log("UGS 초기화 완료");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void OnEnable()
        {
            // 회원 가입 이벤트 연결
            _signupButton.onClick.AddListener(async () =>
            {
                await SignupWithUsernamePassAsync(_userNameIF.text, _passwordIF.text);
            });
            
            // 로그인 이벤트 연결
            _signinButton.onClick.AddListener(async () =>
            {
                await SignIn(_userNameIF.text, _passwordIF.text);
            });
        }

        private async Task SignIn(string username, string password)
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                Debug.Log("로그인 성공");
            }
            catch (AuthenticationException e)
            {
                Debug.LogException(e);
            }
            catch (RequestFailedException e)
            {
                Debug.LogException(e);
            }
        }

        /*
         * 회원가입
         * - UserName : 3 ~ 20자, 대소문자 구별 없음, - @
         * - 비밀번호   : 대소문자 구별, 8 ~ 30, 영문자 대문자 1, 소문자 1, 숫자 1, 특수 문자 1
         */
        private async Task SignupWithUsernamePassAsync(string username, string password)
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                Debug.Log("회원가입 성공");
            }
            catch (AuthenticationException e)
            {
                Debug.LogException(e);
            }
            catch (RequestFailedException e)
            {
                Debug.LogException(e);
            }
        }
    }
}
