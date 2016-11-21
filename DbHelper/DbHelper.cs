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
using Android.Database.Sqlite;
using System.IO;
using XamarinFlagsQuizApp.Model;
using Android.Database;

namespace XamarinFlagsQuizApp.DbHelper
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static string DB_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string DB_NAME = "MyDB.db";
        private static int VERSION = 1;
        private Context context;

        public DbHelper(Context context) : base(context, DB_NAME, null, VERSION)
        {
            this.context = context;
        }

        private string GetSQLitePath()
        {
            return Path.Combine(DB_PATH, DB_NAME);
        }

        public override SQLiteDatabase WritableDatabase
        {
            get
            {
                return CreateSQLiteDB();
            }
        }

        private SQLiteDatabase CreateSQLiteDB()
        {
            SQLiteDatabase sqliteDB = null;
            string path = GetSQLitePath();
            Stream streamSQLite = null;
            FileStream streamWriter = null;
            Boolean isSQLiteInit = false;
            try
            {
                if (File.Exists(path))
                    isSQLiteInit = true;
                else
                {
                    streamSQLite = context.Resources.OpenRawResource(Resource.Raw.MyDB);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if(streamSQLite != null && streamWriter != null)
                    {
                        if (CopySQLiteDB(streamSQLite, streamWriter))
                            isSQLiteInit = true;
                    }
                }
                if (isSQLiteInit)
                    sqliteDB = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadwrite);
            }
            catch { }
            return sqliteDB;
        }

        private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter)
        {
            bool isSuccess = false;
            int length = 1024;
            Byte[] buffer = new Byte[length];
            try
            {
                int bytesRead = streamSQLite.Read(buffer, 0, length);
                while (bytesRead > 0) { 
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, length);
                }
                isSuccess = true;
            }
            catch
            {

            }
            finally
            {
                streamWriter.Close();
                streamSQLite.Close();
            }
            return isSuccess;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
           
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            
        }

        public void InsertScore(int score)
        {
            String query = $"INSERT INTO Ranking(Score) VALUES({score})";
            SQLiteDatabase db = this.WritableDatabase;
            db.ExecSQL(query);
        }

        public List<Ranking> GetRanking()
        {
            List<Ranking> lstRanking = new List<Ranking>();
            SQLiteDatabase db = this.WritableDatabase;
            ICursor c;
            try
            {
                c = db.RawQuery("SELECT * FROM Ranking ORDER BY Score", null);
                if (c == null) return null;
                c.MoveToNext();
                do
                {
                    int Id = c.GetInt(c.GetColumnIndex("Id"));
                    int Score = c.GetInt(c.GetColumnIndex("Score"));

                    Ranking ranking = new Model.Ranking(Id, Score);
                    lstRanking.Add(ranking);
                }
                while (c.MoveToNext());
                c.Close();
            }
            catch { }
            db.Close();
            return lstRanking;

        }

        public List<Question> GetQuestionMode(string mode)
        {
            List<Question> lstQuestion = new List<Question>();
            SQLiteDatabase db = this.WritableDatabase;
            ICursor c;
            int limit = 0;
            if (mode.Equals(Common.Common.MODE.EASY.ToString()))
                limit = Common.Common.EASY_MODE_NUM;
            else if (mode.Equals(Common.Common.MODE.MEDIUM.ToString()))
                limit = Common.Common.MEDIUM_MODE_NUM;
            else if (mode.Equals(Common.Common.MODE.HARD.ToString()))
                limit = Common.Common.HARD_MODE_NUM;
            else
                limit = Common.Common.HARDEST_MODE_NUM;
            try
            {
                c = db.RawQuery($"SELECT * FROM Question ORDER BY Random() LIMIT {limit}",null);
                if (c == null) return null;
                c.MoveToFirst();
                do
                {
                    int Id = c.GetInt(c.GetColumnIndex("ID"));
                    string Image = c.GetString(c.GetColumnIndex("Image"));
                    string AnswerA = c.GetString(c.GetColumnIndex("AnswerA"));
                    string AnswerB = c.GetString(c.GetColumnIndex("AnswerB"));
                    string AnswerC = c.GetString(c.GetColumnIndex("AnswerC"));
                    string AnswerD = c.GetString(c.GetColumnIndex("AnswerD"));
                    string CorrectAnswer = c.GetString(c.GetColumnIndex("CorrectAnswer"));

                    Question question = new Question(Id, Image, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);
                    lstQuestion.Add(question);

                }
                while (c.MoveToNext());
                c.Close();


            }
            catch { }
            return lstQuestion;
        }


    }
}