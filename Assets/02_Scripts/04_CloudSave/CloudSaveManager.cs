using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.CloudSave;

[Serializable]
public struct PlayerData
{
    public string name;
    public int level;
    public int xp;
    public int gold;

    public List<ItemData> items;
}

[Serializable]
public struct ItemData
{
    public string name;
    public int value;
    public string icon;
}


public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private Button _singleDataSaveButton;
    [SerializeField] private Dictionary<string, object> data;

    [Header("Player Data")]
    [SerializeField] private PlayerData _playerData;
    
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
        data = new Dictionary<string, object>
        {
            {"player_name", "Zack"},
            {"level", 30},
            {"xp", 2500},
            {"gold", 100}
        };
        
        // Cloud Save로 저장하는 메서드
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("단일 데이터 저장 성공");
    }
    #endregion
}
