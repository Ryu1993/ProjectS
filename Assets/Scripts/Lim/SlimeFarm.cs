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

    public LayerMask playerLayer;

    private void Start()
    {

        //������ �츮�� �����Լ� �߰�
    }

    private void Update()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, 20f, playerLayer);
        
        if (target.Length>0)
        {
            upgradeUI.SetActive(true);
        }
        else
        {
            upgradeUI.SetActive(false);
        }
        //���׷��̵� Ȯ�� �Լ� �߰�
    }
    //���׷��̵� ����Ʈ ���̾�α� ���� �߰�
}
