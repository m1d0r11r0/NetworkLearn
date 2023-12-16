using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public class LoadTypeList
    {
        [SerializeField]
        private LoadType[] list;

        public LoadTypeList(int size)
        {
            list = new LoadType[size];
        }

        public LoadType this[int i]
        {
            get { return list[i]; }

            set { list[i] = value; }
        }
    }
    /// <summary>
    /// �ǂݍ��ݎ��ɎQ�Ƃ���C���f�b�N�X�penum
    /// </summary>
    public enum LoadType
    {
        Road = 0,
        TreeDef,
        Wall,
        Player1,
        Player2,
        TreeTop,
        TreeDown,
        TreeRight,
        TreeLeft,
        Item01,
        Item02,
        Item03,
        Item04,
        Item05,
        Item06,
        Item07,
        Item08,
    }

    /// <summary>
    /// �v���C���ɎQ�Ƃ���}�X�̎��
    /// </summary>
    public enum Type
    {
        // TODO:�C���Q�[���ł̃}�b�v�f�[�^�̎��������l����
    }

    [SerializeField] private string _MapName;       // �}�b�v��
    [SerializeField] private Vector2Int _MapSize;   // �}�b�v�̃T�C�Y
    [SerializeField] private LoadTypeList[] _LoadData;    // �������ݒ�p�}�b�v�W���O�z��

    public string MapName
    {
        get { return _MapName; }
        set { _MapName = value; }
    }

    public Vector2Int MapSize
    {
        get { return _MapSize; }
        set { _MapSize = value; }
    }

    public LoadTypeList[] LoadData
    {
        get { return _LoadData; }
        set { _LoadData = value; }
    }
}