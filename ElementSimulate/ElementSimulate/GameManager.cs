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

        public Games Play;

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
            if (Play != default)
            {
                Playing = true;
                form1.ToggleVisible(false);
            }

            Difficulty = _Difficulty;
        }

        void GameOver()
        {
            Playing = false;
        }

        void Dodge_Star()
        {
            int hp;
            objectManager.Rainism(Difficulty);

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

        void Shooting_Star()
        {

        }

        void Game_Setting()
        {
            Score = 0;
            form1.ScoreUpdate(Score);
        }

        public void Dodge_Star_Setting()
        {
            Play = Dodge_Star;
            form1.HpBarToggle(true);
            Game_Setting();
        }

        public void Shooting_Star_Setting()
        {
            
            form1.HpBarToggle(false);
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
