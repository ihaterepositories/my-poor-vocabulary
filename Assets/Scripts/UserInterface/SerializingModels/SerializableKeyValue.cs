namespace UserInterface.SerializingModels
{
    [System.Serializable]
    public class SerializableKeyValue<T> where T : class
    {
        public string key;
        public T value;
    }
}