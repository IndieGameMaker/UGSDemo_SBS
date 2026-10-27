using System;
using TMPro;
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
    }
}
