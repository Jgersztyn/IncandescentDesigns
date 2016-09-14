using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncandescentDesigns
{
    public static class ExtensionMethods
    {
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static void Swap2List(List<byte> firstList, List<byte> secondList, int index)
        {
            // TODO : Error handling not done.  
            for (int i = index; i < firstList.Count; i++)
            {
                var temp = firstList[i];
                firstList[index] = secondList[i];
                secondList[i] = temp;
            }
        }
    }
}