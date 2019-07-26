using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Damon.ListView
{
    public class ListViewGroup : IEnumerable<ListViewItem>
    {
        public event Action<ListViewItem> addItemHandler;
        public event Action<ListViewItem> removeItemHandler;

        public List<ListViewItem> listViewItems;

        private float m_rowHeight;
        public float rowHeight
        {
            get { return m_rowHeight; }
            set { rowHeight = value; }
        }

        private float m_columnSize;
        public float columnSize
        {
            get { return m_columnSize; }
            set { m_columnSize = value; }
        }

        private float x_space = 0;
        public float xSpace
        {
            get { return x_space; }
            set { x_space = value; }
        }

        private float y_space = 0;
        public float ySpace
        {
            get { return y_space; }
            set { y_space = value; }
        }

        public Rect view;
        public Rect scrollView;
        private int totalNum = 0;

        public int totalItem(ListViewGroup group)
        {
            for (int i = 0; i < group.listViewItems.Count; i++)
            {
                ListViewItem item = group.listViewItems[i];
                Debug.Log("根对象:" + item.name);
                totalNum = totalItem(item);
            }
            return totalNum;
        }
        public int totalItem(ListViewItem items)
        {
            foreach (ListViewItem temp in items.chilids)
            {
                if (temp.chilids.Count > 0)
                {
                    for (int i = 0; i < temp.chilids.Count; i++)
                    {
                        Debug.Log("子对象:" + temp.chilids[i].name);
                        totalItem(temp.chilids[i]);
                    }
                }
                else
                {
                    Debug.Log("无对象:" + temp.name);
                    totalNum++;
                }
            }
            return totalNum;
        }


        //往根节点中添加项
        public ListViewItem AddItem(ListViewItem item)
        {
            if (listViewItems != null)
                listViewItems.Add(item);

            if (addItemHandler != null)
                addItemHandler(item);

            return item;
        }

        public ListViewItem RemoveItem(ListViewItem item)
        {
            if (listViewItems != null && listViewItems.Contains(item))
                listViewItems.Remove(item);
            if (removeItemHandler != null)
                removeItemHandler(item);
            return item;
        }

        public ListViewGroup()
        {
            listViewItems = new List<ListViewItem>();
        }

        public ListViewGroup(float rowSize, float columnSize, Rect rect)
        {
            listViewItems = new List<ListViewItem>();
            this.m_rowHeight = rowSize;
            this.m_columnSize = columnSize;
            this.view = rect;
        }

        public void OnDrawView()
        {
            Vector3 topLeft = new Vector3(view.x - view.w / 2, view.y - view.h / 2, 0);
            Vector3 topRight = new Vector3(topLeft.x + view.w, topLeft.y, 0);
            Vector3 bottomLeft = new Vector3(topLeft.x, topLeft.y + view.h, 0);
            Vector3 bottomRight = new Vector3(bottomLeft.x + view.w, bottomLeft.y, 0);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomLeft, bottomRight);

            for (int i = 0; i < listViewItems.Count; i++)
            {
                ListViewItem item = listViewItems[i];
                float xPos = view.x - view.w / 2;
                float yPos = (view.y - view.h / 2) + i * (rowHeight + ySpace);//每行的坐标
                item.OnDrawGizmos(xPos, yPos);
            }
        }

        public IEnumerator<ListViewItem> GetEnumerator()
        {
            for (int i = 0; i < listViewItems.Count; i++)
            {
                yield return listViewItems[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
