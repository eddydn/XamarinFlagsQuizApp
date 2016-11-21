using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinFlagsQuizApp.Common
{
    public class Common
    {
        public const int EASY_MODE_NUM = 30;
        public const int MEDIUM_MODE_NUM = 50;
        public const int HARD_MODE_NUM = 100;
        public const int HARDEST_MODE_NUM = 200;

        public enum MODE
        {
            EASY,
            MEDIUM,
            HARD,
            HARDEST
        }

    }
}