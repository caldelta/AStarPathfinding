using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AStartPathfinding.Grounds
{
    public class GroundView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro m_txtNumber;

        public void SetNumber(int number)
        {
            m_txtNumber.text = number.ToString();
        }
    }

}
