using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFarm : MonoBehaviour
{
    private bool highWallUpgrade = false;
    private bool airNetUpgrade = false;
    private bool musicBoxUpgrade = false;
    private bool autoGemUpgrade = false;
    private bool autoFeedUpgrade = false;
    [SerializeField]
    private GameObject insideObject;
    public GameObject InsideObject { get { return insideObject; } set { insideObject = value; } }
    [SerializeField]
    private GameObject upgradeUI;
    public GameObject UpgradeUI { get { return upgradeUI; } set { upgradeUI = value; } }

    private void Start()
    {
        
        //������ �츮�� �����Լ� �߰�
    }

    private void Update()
    {
        //���׷��̵� Ȯ�� �Լ� �߰�
    }
    //���׷��̵� ����Ʈ ���̾�α� ���� �߰�
}
