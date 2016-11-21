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

namespace XamarinFlagsQuizApp.Acitivty
{
    [Activity(Label = "Done",Theme ="@style/AppTheme")]
    public class Done : Activity
    {
        Button btnTryAgain;
        TextView txtTotalScore, txtTotalQuestion;
        ProgressBar progressBarResult;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Done);

            DbHelper.DbHelper db = new DbHelper.DbHelper(this);

            btnTryAgain = FindViewById<Button>(Resource.Id.btnTryAgain);
            txtTotalQuestion = FindViewById<TextView>(Resource.Id.txtTotalQuestion);
            txtTotalScore = FindViewById<TextView>(Resource.Id.txtTotalScore);
            progressBarResult = FindViewById<ProgressBar>(Resource.Id.doneProgressBar);

            btnTryAgain.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };

            //Get Data
            Bundle bundle = Intent.Extras;
            if(bundle != null)
            {
                int score = bundle.GetInt("SCORE");
                int totalQuestion = bundle.GetInt("TOTAL");
                int correctAnswer = bundle.GetInt("CORRECT");

                txtTotalScore.Text = $"SCORE : {score}";
                txtTotalQuestion.Text = $"PASSED : {correctAnswer}/{totalQuestion}";

                progressBarResult.Max = totalQuestion;
                progressBarResult.Progress = correctAnswer;

                //Save score
                db.InsertScore(score);
            }
        }
    }
}