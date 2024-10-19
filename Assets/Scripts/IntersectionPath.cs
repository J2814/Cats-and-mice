using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPath : MovementPath
{
    public void ForceConnectionToSelf()
    {
        foreach (Connection conn in Connections)
        {
            switch (conn.ConnectionType)
            {
                case ConnectionTypeEnum.EndToStart:
                    foreach (Connection c in conn.path.Connections)
                    {
                        if (c.ConnectionType == ConnectionTypeEnum.StartToEnd)
                        {
                            c.path = this; break;
                        }
                    }

                    break;
                case ConnectionTypeEnum.StartToEnd:
                    foreach (Connection c in conn.path.Connections)
                    {
                        if (c.ConnectionType == ConnectionTypeEnum.EndToStart)
                        {
                            c.path = this; break;
                        }
                    }
                    break;
                case ConnectionTypeEnum.EndToEnd:
                    foreach (Connection c in conn.path.Connections)
                    {
                        if (c.ConnectionType == ConnectionTypeEnum.EndToEnd)
                        {
                            c.path = this; break;
                        }
                    }
                    break;
                case ConnectionTypeEnum.StartToStart:
                    foreach (Connection c in conn.path.Connections)
                    {
                        if (c.ConnectionType == ConnectionTypeEnum.StartToStart)
                        {
                            c.path = this; break;
                        }
                    }
                    break;
            }
        }
    }
}
