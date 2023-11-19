using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TowerDataManager
{
    public List<Tower> TowerList { get; private set; }
    public int Score { get; set; } = 0;

    public TowerDataManager()
    {
        TowerList = new List<Tower>();
    }
}