using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NavConnection
{
    public enum ConnectionType
    {
        Walk,
        Fall,
        Jump,
        Fly
    }

    public NavNode startNode;
    public NavNode endNode;
    public float Cost;
    public ConnectionType connectionType;
    
}
