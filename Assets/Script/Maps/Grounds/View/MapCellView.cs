using Maps.Grounds.Model.Enums;
using TMPro;
using UnityEngine;

namespace AStartPathfinding.Grounds.View
{
    public class MapCellView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro m_txtName;
        
        [SerializeField]
        private TextMeshPro m_txtType;

        [SerializeField]
        private Material m_matGround;

        [SerializeField]
        private Material m_matWall;

        public void SetColor(CellType type)
        {
            GetComponent<Renderer>().material = type == CellType.Ground ? m_matGround : m_matWall;
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
