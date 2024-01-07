using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    void LoadData(GameDataCharacter _data);
    void SaveData(ref GameData _data);


}
