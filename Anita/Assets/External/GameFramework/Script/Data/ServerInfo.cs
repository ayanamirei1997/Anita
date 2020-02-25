using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anita
{
    [System.Serializable]
    public class ServerInfo
    {
        public string uri;
        public string name;
    }

    [System.Serializable]
    public class ServerConf
    {
        public string res_file;
        public List<ServerInfo> server_list;
    }
}
