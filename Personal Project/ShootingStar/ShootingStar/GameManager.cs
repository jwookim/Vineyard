using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    class GameManager
    {
        Form1 form1;
        ObjectManager objectManager;

        int Score;
        public int Difficulty;

        public bool Playing { get; private set; }

        public delegate void Games();

        public Games Game;

        public GameManager(Form1 _form1)
        {
            form1 = _form1;

            form1.BackColor = Color.MidnightBlue;

            objectManager = new ObjectManager(form1);

            Playing = false;

            Score = 0;
            Difficulty = 0;
        }

        public void Start(int _Difficulty)
        {
            if (Game != default)
            {
                Playing = true;
                form1.ToggleVisible(false);
            }

            Difficulty = _Difficulty;
        }

        void GameOver()
        {
            Score = 0;
            Playing = false;
            form1.ToggleVisible(true);
            objectManager.Init();
        }

        void Dodge_Star()
        {
            objectManager.Rainism(Difficulty);

            Basic_Progression();

            if (objectManager.HpCheck() <= 0)
            {
                Dodge_Star_Rank();
                GameOver();
            }
        }

        void Shooting_Star()
        {
            objectManager.Discus_Throw(Difficulty);

            objectManager.AimLine();

            Basic_Progression();


            if (objectManager.HpCheck() <= 0)
            {
                Shooting_Star_Rank();
                GameOver();
            }
        }

        void Basic_Progression()
        {

            objectManager.Gravity();

            objectManager.Move();

            objectManager.CollisionCheck();

            Score += objectManager.ExtinctionCheck();

            objectManager.Resist();

            form1.HpUpdate(objectManager.HpCheck());

            form1.ScoreUpdate(Score);
        }

        void Game_Setting()
        {
            Score = 0;
            form1.ScoreUpdate(Score);
        }

        public void Dodge_Star_Setting()
        {
            form1.Height = 480;
            Game = Dodge_Star;
            objectManager.ChangeAttackable(false);
            Game_Setting();
        }

        public void Shooting_Star_Setting()
        {
            form1.Height = 1000;
            Game = Shooting_Star;
            objectManager.ChangeAttackable(true);
            Game_Setting();
        }

        public void Dodge_Star_Rank()
        {
            List<int> rank = new List<int>();
            try
            {
                StreamReader reader = new StreamReader("Dodge_Ranking.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        rank.Add(int.Parse(line));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                reader.Close();
            }
            catch
            {

            }
            rank.Add(Score);

            rank.Sort();
            rank.Reverse();

            StreamWriter writer = new StreamWriter("Dodge_Ranking.txt");
            for(int i=0; i<5; i++)
            {
                if (i < rank.Count && rank[i] > 0)
                {
                    if (rank[i] == Score)
                        form1.Rank_Update(i, rank[i], true);
                    else
                        form1.Rank_Update(i, rank[i]);
                    writer.WriteLine(rank[i]);
                }
                else
                    form1.Rank_Update(i, 0);
            }

            writer.Close();

            form1.Rank_Swap(false);
            form1.ToggleRank(true);
        }
        public void Shooting_Star_Rank()
        {
            List<int> rank = new List<int>();
            try
            {
                StreamReader reader = new StreamReader("Shooting_Ranking.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        rank.Add(int.Parse(line));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                reader.Close();
            }
            catch
            {

            }
            rank.Add(Score);

            rank.Sort();
            rank.Reverse();

            StreamWriter writer = new StreamWriter("Shooting_Ranking.txt");
            for (int i = 0; i < 5; i++)
            {
                if (i < rank.Count && rank[i] > 0)
                {
                    if (rank[i] == Score)
                        form1.Rank_Update(i, rank[i], true);
                    else
                        form1.Rank_Update(i, rank[i]);
                    writer.WriteLine(rank[i]);
                }
                else
                    form1.Rank_Update(i, 0);
            }

            writer.Close();

            form1.Rank_Swap(true);
            form1.ToggleRank(true);
        }

        public void KeyUp(KeyEventArgs e)
        {
            objectManager.KeyUp(e);
        }

        public void KeyDown(KeyEventArgs e)
        {
            objectManager.KeyDown(e);
        }


        
    }
}
