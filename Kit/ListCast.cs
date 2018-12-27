using System.Collections.Generic;

namespace Kit
{
    internal class ListCast<T> where T : Cast
    {
        public List<T> Cast { get; set; }
    }
}