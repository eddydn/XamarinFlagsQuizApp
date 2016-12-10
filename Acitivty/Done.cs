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

                //Update 2.0
                int playCount = 0;
                if(totalQuestion == 30) // EASY MODE
                {
                    playCount = db.GetPlayCount(0);
                    playCount++;
                    db.UpdatePlayCount(0, playCount);
                }
                else if (totalQuestion == 50) // MEDIUM MODE
                {
                    playCount = db.GetPlayCount(1);
                    playCount++;
                    db.UpdatePlayCount(1, playCount);
                }
                else if (totalQuestion == 100) // HARD MODE
                {
                    playCount = db.GetPlayCount(2);
                    playCount++;
                    db.UpdatePlayCount(2, playCount);
                }
                else if (totalQuestion == 200) // HARDEST MODE
                {
                    playCount = db.GetPlayCount(3);
                    playCount++;
                    db.UpdatePlayCount(3, playCount);
                }

                double minus = ((5.0 / (float)score) * 100) * (playCount - 1);
                double finalScore = score - minus;

                txtTotalScore.Text = $"SCORE : {finalScore.ToString("0.00")} (-{5*(playCount-1)}%";
                txtTotalQuestion.Text = $"PASSED : {correctAnswer}/{totalQuestion}";

                progressBarResult.Max = totalQuestion;
                progressBarResult.Progress = correctAnswer;

                //Save score
                db.InsertScore(finalScore);
            }
        }
    }
}