using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.ListView;
public class ZTest01 : MonoBehaviour {

    ListViewGroup  group = new ListViewGroup(0.2f, 10, new Damon.ListView.Rect(0, 0, 4, 5));
    void Start () {

        ListViewItem item01 = new ListViewItem("1");
        group.AddItem(item01);
        item01.AddItem(new ListViewItem("1_1"));
        item01.AddItem(new ListViewItem("1_2"));
        item01.AddItem(new ListViewItem("1_3"));
        item01.AddItem(new ListViewItem("1_4"));

        ListViewItem item02 = new ListViewItem("2");
        ListViewItem item0201 = new ListViewItem("2_1");
        item02.AddItem(item0201);
        item0201.AddItem(new ListViewItem("2_1_1"));
        item0201.AddItem(new ListViewItem("2_1_2"));
        ListViewItem item2_1_3=new ListViewItem("2_1_3");
        item0201.AddItem(item2_1_3);

        ListViewItem item2_1_3_1=new ListViewItem("2_1_3_1");
        item2_1_3.AddItem(item2_1_3_1);
        group.AddItem(item02);


        ListViewItem item03 = new ListViewItem("3");
        item03.AddItem(new ListViewItem("3_1"));
        item03.AddItem(new ListViewItem("3_2"));
        group.AddItem(item03);

        ListViewItem item04 = new ListViewItem("4");
        item04.AddItem(new ListViewItem("4_1"));
        item04.AddItem(new ListViewItem("4_2"));

        group.AddItem(item04);

        Debug.Log(group.totalItem(group));

    }
    void Update () {
		
	}

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            group.OnDrawView();
        }
    }
}
