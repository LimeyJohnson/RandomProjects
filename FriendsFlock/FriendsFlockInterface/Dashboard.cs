
namespace FriendsFlockInterface
{
    public static class Dashboard
    {
        public static bool overrideToken = false;
        private static string testToken = "157050984313106|a54bea6aa5b2709d7cf4af22.1-1778641336|0cCD59Jh2szmVW2pHOJnTXCwY0E";
        private static long testUid = 1778641336;

        public static int TestVertexes = 1000;
        public static int TestEdges = 60000;

        public static string UpdateToken(string fbToken)
        {
            if (overrideToken)
                return testToken;
            else
                return fbToken;
        }

        public static long UpdateUid(long userUid)
        {
            if (overrideToken)
                return testUid;
            else
                return userUid;
        }
    }
}