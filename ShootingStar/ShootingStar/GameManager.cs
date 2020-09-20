using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        void Shooting_Star()
        {
            objectManager.AimLine();

            Basic_Progression();
        }

        void Basic_Progression()
        {
            int hp;

            objectManager.Gravity();

            objectManager.Move();

            objectManager.CollisionCheck();

            Score += objectManager.ExtinctionCheck();

            objectManager.Resist();

            form1.HpUpdate(hp = objectManager.HpCheck());

            form1.ScoreUpdate(Score);
            if (hp <= 0)
                GameOver();
        }

        void Game_Setting()
        {
            Score = 0;
            form1.ScoreUpdate(Score);
        }

        public void Dodge_Star_Setting()
        {
            Game = Dodge_Star;
            objectManager.ChangeAttackable(false);
            Game_Setting();
        }

        public void Shooting_Star_Setting()
        {
            Game = Shooting_Star;
            objectManager.ChangeAttackable(true);
            Game_Setting();
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
