using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Damon.ListView
{
    public class Rect
    {
        public float x;
        public float y;
        public float w;
        public float h;

        public Rect(float x1, float y1, float w1, float h1)
        {
            this.x = x1;
            this.y = y1;
            this.w = w1;
            this.h = h1;
        }

        public override string ToString()
        {
            return string.Format("x={0},y={1},w={2},h={3}", x, y, w, h);
        }
    }

    public class ListViewItem
    {
        private string m_name;
        public string name { get { return m_name; } }

        private int m_id;
        public int id { get { return m_id; } }

        private Rect m_rect = new Rect(0, 0, 0.5f, 0.2f);
        public Rect rect
        {
            get { return m_rect; }
            set { m_rect = value; }
        }

        private List<ListViewItem> m_childs;
        public List<ListViewItem> chilids { get { return m_childs; } }

        private ListViewItem m_parent;
        public ListViewItem parent { get { return m_parent; } }

        public ListViewItem(string name)
        {
            m_childs = new List<ListViewItem>();
            this.m_name = name;
        }

        public ListViewItem AddItem(ListViewItem item)
        {
            if (chilids != null)
                chilids.Add(item);
            return item;
        }

        public void OnDrawGizmos(float x, float y)
        {
            Rect temp = new Rect(x, y, m_rect.w, m_rect.h);


            Vector3 topLeft = new Vector3(x,y);
            Vector3 topRight = new Vector3(x+rect.w,topLeft.y);
            Vector3 bottomLeft = new Vector3(x,y+rect.h);
            Vector3 bottomRight = new Vector3(topRight.x,bottomLeft.y);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft,topRight);
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(topRight,bottomRight);

            Vector3 pos = new Vector3(rect.x,rect.y+rect.h/2);
            GUIStyle style = EditorStyles.boldLabel;
            style.alignment = TextAnchor.MiddleCenter|TextAnchor.UpperCenter;
           
            style.normal.textColor = Color.blue;
            Handles.Label(pos,name,style);
            
        }
    }


}
