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
using XamarinFlagsQuizApp.Model;
using XamarinFlagsQuizApp.Common;

namespace XamarinFlagsQuizApp.Acitivty
{
    [Activity(Label = "Score",Theme ="@style/AppTheme")]
    public class Score : Activity
    {
        ListView lstView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Score);

            lstView = FindViewById<ListView>(Resource.Id.lstView);

            DbHelper.DbHelper db = new DbHelper.DbHelper(this);
            List<Ranking> lstRanking = db.GetRanking();
            if(lstRanking.Count > 0)
            {
                CustomAdapter adapter = new CustomAdapter(this, lstRanking);
                lstView.Adapter = adapter;
            }
        }
    }
}