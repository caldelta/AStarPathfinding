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
        private Material m_matOpen;

        [SerializeField]
        private Material m_matGround;

        [SerializeField]
        private Material m_matWall;

        private Material m_material;

        public CellType CellType { get; set; }

        private void Start()
        {
            m_material = GetComponent<Renderer>().materials[0];
        }
        public void SetColor(CellType type)
        {
            GetComponent<Renderer>().material = ToColor(type);
        }
        public Material ToColor(CellType type) => type switch
        {
            CellType.Open => m_matOpen,
            CellType.Ground => m_matGround,
            CellType.Wall => m_matWall,
            _ => throw new System.NotImplementedException()
        };
        public void SetName(int name)
        {
            m_txtName.text = name.ToString();
        }

        public void SetType(CellType type)
        {
            m_txtType.text = type.ToString();
        }
    }

}
