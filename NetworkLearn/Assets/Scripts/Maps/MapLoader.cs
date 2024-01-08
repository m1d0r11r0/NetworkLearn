using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private const float BLOCK_SIZE = 1f;        // �e�I�u�W�F�N�g�̃T�C�Y

    [SerializeField] MapData[] _Maps;           // �}�b�v�pScriptableObject�z��
    [SerializeField] GameObject _WallPref;      // �Ǘp�v���n�u
    [SerializeField] GameObject _TreePref;      // ���؃v���n�u
    [SerializeField] GameObject _FloorPref;     // ���p�v���n�u
    [SerializeField] Vector3 _MapCenter;        // �}�b�v�̒��S�_

    private Transform _Transform;       // ������Transform�L���b�V��
    private Transform _FloorRoot;       // ���I�u�W�F�N�g�̃��[�g
    private Transform _WallRoot;        // �ǃI�u�W�F�N�g�̃��[�g
    private Transform _TreeRoot;        // ���؃I�u�W�F�N�g�̃��[�g

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// �������֐�
    /// </summary>
    private void Init()
    {
        // �e���[�g�p�I�u�W�F�N�g�̐���
        _Transform = transform;
        _FloorRoot = new GameObject("FloorRoot").transform;
        _FloorRoot.transform.parent = _Transform;
        _WallRoot = new GameObject("WallRoot").transform;
        _WallRoot.transform.parent = _Transform;
        _TreeRoot = new GameObject("TreeRoot").transform;
        _TreeRoot.transform.parent = _Transform;

#if false
        // NOTE:���Ń}�b�v���[�h�����̔z�u
        LoadMap(0);
#endif
    }

    /// <summary>
    /// �}�b�v�̓ǂݍ��ݏ���
    /// </summary>
    /// <param name="stageIdx">�X�e�[�W�̃C���f�b�N�X</param>
    public void LoadMap(int stageIdx)
    {
        if (stageIdx >= _Maps.Length)
        {
            Debug.LogError($"�s���ȃC���f�b�N�X�l�ł�\nstageIdx : {stageIdx}");
            return;
        }

        var loadMap = _Maps[stageIdx];

        // �}�b�v�̊���W(����)�̌v�Z
        var mapRootPos = new Vector3();
        mapRootPos.x = _MapCenter.x - loadMap.MapSize.x * BLOCK_SIZE / 2f;
        mapRootPos.y = _MapCenter.y;
        mapRootPos.z = _MapCenter.z + loadMap.MapSize.y * BLOCK_SIZE / 2f;

        // ���ݐ������Ă���I�u�W�F�N�g�̍��W
        var currentMapPos = mapRootPos;

        for (int y = 0; y < loadMap.MapSize.y; y++)
        {
            currentMapPos.z = mapRootPos.z - y * BLOCK_SIZE;
            for (int x = 0; x < loadMap.MapSize.x; x++)
            {
                currentMapPos.x = mapRootPos.x + x * BLOCK_SIZE;
                InstantiateMapBlock(loadMap.LoadData[y][x], currentMapPos);
            }
        }
    }

    /// <summary>
    /// �}�b�v�u���b�N�̔z�u
    /// </summary>
    /// <param name="type">�}�b�v���</param>
    /// <param name="mapPos">�z�u���W</param>
    private void InstantiateMapBlock(MapData.LoadType type, Vector3 mapPos)
    {
        // ��U���̐���
        GameObject.Instantiate(_FloorPref, mapPos, Quaternion.identity, _FloorRoot);

        switch (type)
        {
            case MapData.LoadType.Wall:
                GameObject.Instantiate(_WallPref, mapPos, Quaternion.identity, _WallRoot);
                break;
            case MapData.LoadType.TreeDef:
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.identity, _TreeRoot);
                break;
            case MapData.LoadType.TreeTop:
                // X+
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(90f, 0f, 0f), _TreeRoot);
                break;
            case MapData.LoadType.TreeDown:
                // x-
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(-90f, 0f, 0f), _TreeRoot);
                break;
            case MapData.LoadType.TreeRight:
                // z-
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(0f, 0f, -90f), _TreeRoot);
                break;
            case MapData.LoadType.TreeLeft:
                // z+
                GameObject.Instantiate(_TreePref, mapPos, Quaternion.Euler(0f, 0f, 90f), _TreeRoot);
                break;

            case MapData.LoadType.Player1:
                // TODO:�v���C���[1���W�̎w��
                //      ���̃N���X����w�肷��\��������
                break;
            case MapData.LoadType.Player2:
                // TODO:�v���C���[2���W�̎w��
                //      ���̃N���X����w�肷��\��������
                break;

            case MapData.LoadType.Item01:
            case MapData.LoadType.Item02:
            case MapData.LoadType.Item03:
            case MapData.LoadType.Item04:
            case MapData.LoadType.Item05:
            case MapData.LoadType.Item06:
            case MapData.LoadType.Item07:
            case MapData.LoadType.Item08:
                // TODO:�A�C�e���̔z�u����
                //      �A�C�e���̎�����������Ȃ̂ł܂��z�u���������Ȃ�
                break;
        }
    }
}
