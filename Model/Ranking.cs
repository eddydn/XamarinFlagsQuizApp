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

namespace XamarinFlagsQuizApp.Model
{
    public class Ranking
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public Ranking(int Id,double Score)
        {
            this.Id = Id;
            this.Score = Score;
        }
    }
}