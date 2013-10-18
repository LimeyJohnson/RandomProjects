using System;

namespace FriendsService
{
    public class FriendsEventArgs
    {
        public bool Success { get; protected set; }
        public Exception Error { get; protected set; }
    }
}