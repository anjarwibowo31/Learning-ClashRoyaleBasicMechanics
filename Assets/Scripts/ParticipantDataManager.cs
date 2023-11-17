using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParticipantDataManager
{
    public List<Tower> TowerList { get; private set; }
    public int Score { get; set; } = 0;

    public ParticipantDataManager()
    {
        TowerList = new List<Tower>();
    }
}