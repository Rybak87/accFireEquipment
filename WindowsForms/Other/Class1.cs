//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using BL;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    public class SortOfSign : IComparer
//    {
//        Dictionary<Type, int> dictPriorityType = new Dictionary<Type, int>()
//        {
//            [typeof(Location)] = 4,
//            [typeof(FireCabinet)] = 3,
//            [typeof(Extinguisher)] = 2,
//            [typeof(Hose)] = 1,
//            [typeof(Hydrant)] = 0
//        };




//        public int Compare(object _left, object _right)
//        {
//            TreeNode left = _left as TreeNode;
//            TreeNode right = _right as TreeNode;

//            var leftSign = left.Tag as EntitySign;
//            var rightSign = left.Tag as EntitySign;

//            if (leftSign == null)
//                return 1;
//            if (rightSign == null)
//                return -1;
//            int leftPriority = dictPriorityType[leftSign.Type];
//            int rightPriority = dictPriorityType[rightSign.Type];
//            if (leftPriority > rightPriority)
//                return 1;
//            if (leftPriority < rightPriority)
//                return -1;
//            if(leftSign.)
//        }
//    }
//}
