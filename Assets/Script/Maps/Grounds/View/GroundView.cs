using Maps.Grounds.Model.Enums;
using TMPro;
using UnityEngine;

namespace AStartPathfinding.Grounds
{
    public class GroundView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro m_txtName;
        
        [SerializeField]
        private TextMeshPro m_txtType;

        [SerializeField]
        private Material m_matGround;

        [SerializeField]
        private Material m_matWall;

        public void SetColor(int type)
        {
            GetComponent<Renderer>().material = type == 0 ? m_matGround : m_matWall;
        }
        public void SetName(int name)
        {
            m_txtName.text = name.ToString();
        }

        public void SetType(int type)
        {
            m_txtType.text = type.ToString();
        }
    }

}
