using UnityEngine;

[CreateAssetMenu(fileName ="GameSetting", menuName ="Template/GameSetting")]
public class GameSetting : ScriptableSingleton<GameSetting>
{
    public Vector2 screenBounds;
    public bool enableAudio;
}
