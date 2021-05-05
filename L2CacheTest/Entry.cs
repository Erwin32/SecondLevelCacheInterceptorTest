using System;

namespace L2CacheTest
{
    [Serializable]
    public class Entry
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Data { get; set; }
    }
}