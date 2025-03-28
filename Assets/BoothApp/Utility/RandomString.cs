using System;

namespace BoothApp.Utility
{
    public static class RandomString
    {
        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값

        public static string RandomStringGenerate(int _nLength = 12)
        {
            const string strPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  //문자 생성 풀
            char[] chRandom = new char[_nLength];

            for (int i = 0; i < _nLength; i++ )
            {
                chRandom[i] = strPool[_random.Next(strPool.Length)];
            }
            string strRet = new String(chRandom);   // char to string
            return strRet;
        }
    }
}