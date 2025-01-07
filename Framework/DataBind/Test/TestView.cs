using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestView : BindableView
{
    public TestData data;
    void Start()
    {
        data = new TestData();
        Bind(data);
        data.name.Value = "xxx";
    }

    // Update is called once per frame
    void Update()
    {
        //data.name.Value = Time.time.ToString();

        //Debug.Log(data.input.Value + " "+data.selectUser.Value);
    }
}
