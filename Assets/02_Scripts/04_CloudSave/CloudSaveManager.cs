using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private Button _singleDataSaveButton;

    private void Start()
    {
        BindingButtonEvents();
    }
    
    private void BindingButtonEvents()
    {
        _singleDataSaveButton.onClick.AddListener(async () => await SaveSingleData());
    }

    #region 싱글데이터 저장
    private async Task SaveSingleData()
    {
        
    }
    #endregion
}
