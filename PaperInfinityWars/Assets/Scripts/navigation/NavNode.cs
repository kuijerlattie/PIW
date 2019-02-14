using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class NavNode
{
    public enum NodeType {
        Center,
        Edge
    }

    List<NavConnection> navConnections = new List<NavConnection>();

    public void SetConnection(NavNode connectedNode, NavConnection.ConnectionType connectionType)
    {
        NavConnection newconnection = new NavConnection();
        newconnection.startNode = this;
        newconnection.endNode = connectedNode;
        newconnection.connectionType = connectionType;
        navConnections.Add(newconnection);
    }
}
