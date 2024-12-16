public class IntersectionPath : MovementPath
{

    public void ForceConnectionToSelf()
    {
        foreach (Connection conn in Connections)
        {
            switch (conn.ConnectionType)
            {
                case ConnectionTypeEnum.EndToStart:
                    HandleConnection(conn, ConnectionTypeEnum.StartToEnd);
                    break;

                case ConnectionTypeEnum.StartToEnd:
                    HandleConnection(conn, ConnectionTypeEnum.EndToStart);
                    break;

                case ConnectionTypeEnum.EndToEnd:
                    HandleConnection(conn, ConnectionTypeEnum.EndToEnd);
                    break;

                case ConnectionTypeEnum.StartToStart:
                    HandleConnection(conn, ConnectionTypeEnum.StartToStart);
                    break;
            }
        }
    }

    public void DisconnectFromSelf()
    {
        foreach (Connection conn in Connections)
        {
            foreach (Connection pathconn in conn.path.Connections)
            {
                if (pathconn.path == this)
                {
                    pathconn.path = null;
                }
            }

        }
    }
    private void HandleConnection(Connection conn, ConnectionTypeEnum targetType)
    {
        foreach (Connection c in conn.path.Connections)
        {
            if (c.ConnectionType == targetType)
            {
                c.path = this;
                break;
            }
        }
    }
}
