using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    public GameObject currentTowerText;
    public BuildingSystem buildingSystem;
    public GameObject cannonTooltip;
    public GameObject windmillTooltip;
    public GameObject rocksToolTip;
    public GameObject raftTooltip;
    public GameObject upgradeToolTip;

    public void CannonTowerToolTipActive()
    {
        cannonTooltip.SetActive(true);
    }
    public void CannonTowerToolTipExit()
    {
        cannonTooltip.SetActive(false);
    }


    public void WindMillTowerToolTipActive()
    {
        windmillTooltip.SetActive(true);
    }

    public void WindMillTowerToolTipExit()
    {
        windmillTooltip.SetActive(false);
    }


    public void RocksPlacementToolTipActive()
    {
        rocksToolTip.SetActive(true);
    }
    public void RocksPlacementToolTipExit()
    {
        rocksToolTip.SetActive(false);
    }


    public void RaftPlacementToolTipActive()
    {
        raftTooltip.SetActive(true);
    }

    public void RaftPlacementToolTipExit()
    {
        raftTooltip.SetActive(false);
    }
    public void UpgradeToolTipActive()
    {
        upgradeToolTip.SetActive(true);
    }
    public void UpgradeToolTipExit()
    {
        upgradeToolTip.SetActive(false);
    }
}
