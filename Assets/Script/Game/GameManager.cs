using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private GameObject m_player;

    public void CreatePlayer(Vector3 position)
    {
        Instantiate(m_player, position, Quaternion.identity, transform);
    }
}
