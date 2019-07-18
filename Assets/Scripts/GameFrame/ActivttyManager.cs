using System;
using System.Collections.Generic;
public class ActivttyManager
{
    private static List<Activity> que = new List<Activity>();
    public static int count { get { return que.Count; }}
    public static void Register(Activity activity)
    {
        if (que != null)
            que.Add(activity);
    }
    public static Activity Get(int index)
    {
        if (index < 0 || index >= que.Count) return null;

        return que[index];
    }
}
