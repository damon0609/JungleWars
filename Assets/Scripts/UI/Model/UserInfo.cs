using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

[System.Serializable]
[ProtoContract]
public class UserInfo
{
    [ProtoMember(1)]
    public string name;
    [ProtoMember(2)]
    public string password;
    [ProtoMember(3)]
    public string email;

    public override string ToString()
    {
        return string.Format("name={0},password={1}",name,password);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(UserInfo)||obj==null) return false;

        UserInfo info = obj as UserInfo;
        if (info.name == name && info.password == password)
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
