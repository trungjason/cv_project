using System;

namespace _51900286_51900712_51900718_51900699
{
    public class DatabaseIDSupport
    {
        private const int Max_DIGIT = 3;
        public static string CreateNewMaxID( string maxID )
        {
            if ( maxID.Equals("-1"))
            {
                return null;
            }

            // Lấy số id hiện tại parse về int rồi tăng 1 sau đó thêm số 0 vào
            int temp_MaxID = Int32.Parse(maxID.Substring(maxID.Length - Max_DIGIT));
            temp_MaxID += 1;
            string newMaxID = temp_MaxID.ToString();
            newMaxID = newMaxID.PadLeft(Max_DIGIT, '0');

            return maxID.Substring(0, maxID.Length - Max_DIGIT) + newMaxID;
        }
    }
}
