using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingStar
{
    enum Direct
    {
        Left,
        Right
    }



    enum STATE
    {
        IDLE,
        RUN,
        DASH,
        JUMP,
        REST,
        SHOT_IDLE,
        SHOT_RUN,
        SHOT_JUMP
    }

    class Character : GameObject
    {
        public Direct direct { get; private set; }
        public STATE state { get; private set; }

        bool goLeft;
        bool goRight;
        bool aimUp;
        bool aimDown;

        int aniNum = 0;
        int aniTime = 0;
        int aniTerm = 0;

        public int Angle { get; private set; }

        bool Land;

        bool Attackable;
        float AtkCooldown;

        Image[,] act;
        Image[,] Idle;
        Image[,] Run;
        Image[,] Jump;
        Image[,] Dash;
        Image[,] Rest;
        Image[,] Shot_Idle;
        Image[,] Shot_Run;
        Image[,] Shot_Jump;
        public float Health { get; private set; }

        const float AtkDelay = 1f;
        public Character(Form1 form, bool _atkable = false):base(form.Width/2, form.Height - 100, form)
        {
            Init();

            direct = Direct.Right;
            state = STATE.IDLE;

            Elasticity = 0.1f;
            Mass = 15f;
            Attackable = _atkable;
            Land = false;
            

            string name;
            Idle = new Image[2, 2];
            Run = new Image[2, 4];
            Jump = new Image[2, 1];
            Dash = new Image[2, 2];
            Rest = new Image[2, 5];
            Shot_Idle = new Image[2, 1];
            Shot_Run = new Image[2, 3];
            Shot_Jump = new Image[2, 1];
            act = Idle;


            for (int i = 0; i < Idle.GetLength(1); i++)
            {
                name = "RockMan_Idle_L_0" + (i + 1);
                Idle[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Idle.GetLength(1); i++)
            {
                name = "RockMan_Idle_R_0" + (i + 1);
                Idle[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Run.GetLength(1); i++)
            {
                name = "RockMan_Run_L_0" + (i + 1);
                Run[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Run.GetLength(1); i++)
            {
                name = "RockMan_Run_R_0" + (i + 1);
                Run[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Dash.GetLength(1); i++)
            {
                name = "RockMan_Dash_L_0" + (i + 1);
                Dash[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Dash.GetLength(1); i++)
            {
                name = "RockMan_Dash_R_0" + (i + 1);
                Dash[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Jump.GetLength(1); i++)
            {
                name = "RockMan_Jump_L_0" + (i + 1);
                Jump[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Jump.GetLength(1); i++)
            {
                name = "RockMan_Jump_R_0" + (i + 1);
                Jump[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Rest.GetLength(1); i++)
            {
                name = "RockMan_Rest_L_0" + (i + 1);
                Rest[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Rest.GetLength(1); i++)
            {
                name = "RockMan_Rest_R_0" + (i + 1);
                Rest[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Idle.GetLength(1); i++)
            {
                name = "RockMan_Idle_Shot_L_0" + (i + 1);
                Shot_Idle[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Idle.GetLength(1); i++)
            {
                name = "RockMan_Idle_Shot_R_0" + (i + 1);
                Shot_Idle[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Run.GetLength(1); i++)
            {
                name = "RockMan_Run_Shot_L_0" + (i + 1);
                Shot_Run[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Run.GetLength(1); i++)
            {
                name = "RockMan_Run_Shot_R_0" + (i + 1);
                Shot_Run[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Jump.GetLength(1); i++)
            {
                name = "RockMan_Jump_Shot_L_0" + (i + 1);
                Shot_Jump[0, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            for (int i = 0; i < Shot_Jump.GetLength(1); i++)
            {
                name = "RockMan_Jump_Shot_R_0" + (i + 1);
                Shot_Jump[1, i] = (Image)Properties.Resources.ResourceManager.GetObject(name);
            }

            myPicturebox.SizeMode = PictureBoxSizeMode.AutoSize;
            myPicturebox.Image = Properties.Resources.RockMan_Idle_R_01;
        }

        public void ChangeSTATE(STATE _state)
        {
            if (state != _state)
            {
                state = _state;
                aniNum = 0;

                switch (state)
                {
                    case STATE.IDLE:
                        act = Idle;
                        aniTerm = 7;
                        break;
                    case STATE.RUN:
                        act = Run;
                        aniTerm = 4;
                        break;
                    case STATE.DASH:
                        act = Dash;
                        aniTerm = 3;
                        break;
                    case STATE.JUMP:
                        act = Jump;
                        break;
                    case STATE.REST:
                        act = Rest;
                        aniTerm = 5;
                        break;
                    case STATE.SHOT_IDLE:
                        act = Shot_Idle;
                        break;
                    case STATE.SHOT_RUN:
                        act = Shot_Run;
                        aniTerm = 4;
                        break;
                    case STATE.SHOT_JUMP:
                        act = Shot_Jump;
                        break;
                }

                myPicturebox.Image = act[(int)direct, aniNum];
            }
        }

        public void Animate()
        {
            aniTime++;

            if (aniTime >= aniTerm)
            {
                if (act != null)
                {
                    switch (state)
                    {
                        case STATE.IDLE:
                            if (AtkCooldown <= 0.6f)
                            {
                                ChangeSTATE(STATE.SHOT_IDLE);
                                break;
                            }
                            aniNum++;
                            if (aniNum >= Idle.GetLength(1))
                                aniNum = 0;

                            if (goLeft || goRight)
                                ChangeSTATE(STATE.RUN);
                            if (!Land)
                                ChangeSTATE(STATE.JUMP);
                            break;
                        case STATE.RUN:
                            if (AtkCooldown <= 0.6f)
                            {
                                ChangeSTATE(STATE.SHOT_RUN);
                                break;
                            }
                            aniNum++;
                            if (aniNum >= Run.GetLength(1))
                                aniNum = 1;

                            if (!goLeft && !goRight)
                                ChangeSTATE(STATE.IDLE);
                            if (!Land)
                                ChangeSTATE(STATE.JUMP);
                            break;
                        case STATE.DASH:
                            if (aniNum < Dash.GetLength(1) - 1)
                                aniNum++;
                            if (!Land)
                                ChangeSTATE(STATE.JUMP);
                            break;
                        case STATE.JUMP:
                            if (AtkCooldown <= 0.6f)
                                ChangeSTATE(STATE.SHOT_JUMP);
                            /*if (Land)
                                ChangeSTATE(STATE.IDLE);*/
                            break;
                        case STATE.REST:
                            aniNum++;
                            if (aniNum >= Rest.GetLength(1))
                                ChangeSTATE(STATE.IDLE);
                            break;
                        case STATE.SHOT_IDLE:
                            if (AtkCooldown > 0.6f)
                            {
                                ChangeSTATE(STATE.IDLE);
                                break;
                            }

                            if (goLeft || goRight)
                                ChangeSTATE(STATE.SHOT_RUN);

                            if (!Land)
                                ChangeSTATE(STATE.SHOT_JUMP);
                            break;
                        case STATE.SHOT_RUN:
                            if (AtkCooldown > 0.6f)
                            {
                                ChangeSTATE(STATE.RUN);
                                break;
                            }
                            aniNum++;
                            if (aniNum >= Run.GetLength(1))
                                aniNum = 1;

                            if (!goLeft && !goRight)
                                ChangeSTATE(STATE.SHOT_IDLE);

                            if (!Land)
                                ChangeSTATE(STATE.SHOT_JUMP);
                            break;
                        case STATE.SHOT_JUMP:
                            if (AtkCooldown > 0.6f)
                                ChangeSTATE(STATE.JUMP);
                            break;
                    }


                    aniTime = 0;
                }
            }
            myPicturebox.Image = act[(int)direct, aniNum];
        }

        public void Init()
        {
            goLeft = false;
            goRight = false;

            Health = 100f;

            vector.Vertical = 0f;
            vector.Horizontal = 0f;

            Angle = 0;
        }

        public override void Move()
        {
            if (goLeft)
            {
                if (vector.Horizontal > -10f)
                    vector.Horizontal -= 2f;
            }

            if (goRight)
            {
                if (vector.Horizontal < 10f)
                    vector.Horizontal += 2f;
            }

            if(aimUp)
            {
                if (Angle < 90)
                    Angle += 3;
            }

            if (aimDown)
            {
                if (Angle > -90)
                    Angle -= 3;
            }

            if (AtkCooldown <= AtkDelay)
                AtkCooldown += Interval;

            base.Move();

            Animate();
        }

        public void Control_Up(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (goLeft)
                {
                    Braking();
                    goLeft = false;
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                if (goRight)
                {
                    Braking();
                    goRight = false;
                }
            }

            if (Attackable)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (aimUp)
                        aimUp = false;
                }

                if (e.KeyCode == Keys.Down)
                {
                    if (aimDown)
                        aimDown = false;
                }
            }
        }
        public void Control_Down(KeyEventArgs e)
        {
            if (state != STATE.REST)
            {
                if (e.KeyCode == Keys.Left)
                {
                    if (goRight)
                        goRight = false;
                    goLeft = true;
                    direct = Direct.Left;

                }

                if (e.KeyCode == Keys.Right)
                {
                    if (goLeft)
                        goLeft = false;
                    goRight = true;
                    direct = Direct.Right;
                }

                if (e.KeyCode == Keys.X && Land)
                {
                    vector.Vertical -= 25f;
                    Land = false;
                }

                if (e.KeyCode == Keys.Z)
                {
                    Guard();
                }

                if (AtkCooldown > AtkDelay)
                {
                    if (e.KeyCode == Keys.C)
                    {
                        AtkCooldown = 0f;
                    }
                }

                if (Attackable)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (aimDown)
                            aimUp = false;
                        aimUp = true;

                    }

                    if (e.KeyCode == Keys.Down)
                    {
                        if (aimUp)
                            aimUp = false;
                        aimDown = true;
                    }

                }
            }
        }


        void Guard()
        {
            if (state != STATE.JUMP && AtkCooldown <= 0.4f)
            {
                ChangeSTATE(STATE.REST);

                goLeft = false;
                goRight = false;
                aimUp = false;
                aimDown = false;

                Braking();
            }
        }


        void Braking()
        {
            vector.Horizontal *= 0.01f;
        }

        public override void Gravity()
        {
            if (!Land)
                base.Gravity();
        }

        public override void Overlap(int left, int top)
        {
            if (state != STATE.REST)
            {
                Land = false;
                base.Overlap(left, top);
            }
        }

        public void Landing()
        {
            Land = true;
            vector.Vertical = 0f;
            ChangeSTATE(STATE.IDLE);
        }

        public override void Collision(Vector v, float _mass)
        {
            if (state != STATE.REST)
            {
                Damage((float)(Math.Sqrt(Math.Pow(v.Vertical, 2) + Math.Pow(v.Horizontal, 2)) * _mass) / 20f);
                base.Collision(v, _mass);
            }
        }

        public void ChangeAttackable(bool _atk)
        {
            Attackable = _atk;
        }


        void Damage(float dmg)
        {
            Health -= dmg;

            if (Health < 0)
                Health = 0;
        }


    }
}
