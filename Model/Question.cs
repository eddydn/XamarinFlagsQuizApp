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
    public class Question
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }

        public Question(int Id,string Image,string AnswerA,string AnswerB,string AnswerC,string AnswerD,string CorrectAnswer)
        {
            this.Id = Id;
            this.Image = Image;
            this.AnswerA = AnswerA;
            this.AnswerB = AnswerB;
            this.AnswerC = AnswerC;
            this.AnswerD = AnswerD;
            this.CorrectAnswer = CorrectAnswer;
        }
    }
}