namespace Generator3
{
    public class CoroutineHandlerBase
    {
        // classic Generator

        public static void StartUpdate()
        {
            while (true)
            {
                Update();
            }
        }
    }
}