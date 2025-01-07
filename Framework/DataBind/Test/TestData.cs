using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestData:BindableData
{
    [BindableProperty("name",typeof(Text),"text")]
    public BindableProperty<string> name = new BindableProperty<string>();

    [BindableProperty("input",typeof(InputField),"text")]
    public BindableProperty<string> input = new BindableProperty<string>();

    [BindableProperty("dropDown",typeof(Dropdown),"options",BindDirection.Data2View)]
    public BindableProperty<string[]> userNames = new BindableProperty<string[]>(new []{"1","2","3"});

    [BindableProperty("dropDown", typeof(Dropdown), "value", BindDirection.Both)]
    public BindableProperty<int> selectUser = new BindableProperty<int>();
}
