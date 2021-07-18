using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private GameObject m_player;

    private void Start()
    {
        Instantiate(m_player, Vector3.zero, Quaternion.identity, transform);
    }
}
