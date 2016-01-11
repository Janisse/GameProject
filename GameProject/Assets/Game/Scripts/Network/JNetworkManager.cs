using UnityEngine;
using System.Collections;

public class JNetworkManager : MonoBehaviour
{
	#region Properties
	internal int port = 8000;
	internal int nbPlayerMax = 12;

	private bool isInit = false;
	#endregion

	#region Network Methods
	internal void Init(bool isServer = false)
	{
		if(isServer)
		{
			Network.InitializeServer(nbPlayerMax, port, true);
		}
		isInit = true;
	}

	internal string GetIP()
	{
		if(isInit == true)
			return Network.player.ipAddress;
		return "";
	}

	internal void ConnectTo(string a_IPAddress)
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			Network.Connect(a_IPAddress, port);
		}
	}
	#endregion
}
