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
    [SerializeField] private Button _multiDataSaveButton;
    
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
        _multiDataSaveButton.onClick.AddListener(async () => await SaveMultiData<PlayerData>("PLAYER_DATA", _playerData));
    }

    #region 복수데이터 저장

    private async Task SaveMultiData<T>(string key, T saveData)
    {
        // 딕셔너리 자료형에 저장
        var data = new Dictionary<string, object>
        {
            {key, saveData}
        };
        
        // 저장 메서드
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }
    #endregion

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

    #region 싱글데이터 로드
    

    #endregion
    
}

/*
 * HashSet<T> 자료형
 * - 중복된 값을 허용하지 않는 컬렉션(배열, 리스트)
 * - 해시를 기반으로 값을 관리하기 때문에, 속도가 빠르다(List 보다)
 * - 인덱스 방식으로 접근할 수 없다. TryGetValue를 사용한다.
 *
 * HashSet<int> playerId = new HashSet<int>();
 * playerId.Add(1)
 * playerId.Add(2)
 * playerId.Add(1) // X (중복)
 */



