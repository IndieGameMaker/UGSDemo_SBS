using TMPro;
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
    }
}
