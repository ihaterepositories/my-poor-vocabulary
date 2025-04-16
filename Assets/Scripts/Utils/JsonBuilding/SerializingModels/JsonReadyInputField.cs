using System;
using UnityEngine.UI;

namespace Utils.JsonBuilding.SerializingModels
{
    [Serializable]
    public class JsonReadyInputField
    {
        public string KeyNameForJson;
        public InputField InputField;
    }
}