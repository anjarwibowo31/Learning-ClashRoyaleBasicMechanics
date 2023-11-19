using System.Collections;
using UnityEngine;

public class Test3 : Test2
{
    public override void TestMethod()
    {
        print("From Last Test");
    }

    private void Awake()
    {
        TestMethod();
    }
}