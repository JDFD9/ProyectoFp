using UnityEngine;

public class GameMusicTrigger : MonoBehaviour
{
    void Start()
    {
        MusicManager.Instance.PlayGameMusic();
    }
}
