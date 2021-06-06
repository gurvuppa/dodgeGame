/*Gurvir Uppal
 * Dodge Game
 * Mr.T
 *ICS3U
 *June 4, 2021
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodgeGame
{
    public partial class Form1 : Form
    {

        Rectangle hero = new Rectangle(50, 200, 20, 20);

        List<Rectangle> leftObsticle = new List<Rectangle>();
        List<Rectangle> rightObsticle = new List<Rectangle>();

        int HERO_SPEED = 8;
        int LEFT_SPEED = 8;
        int RIGHT_SPEED = -8;
        int leftCounter;
        int rightCounter;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;

        string gameState = "waiting";

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);

        Random randGen = new Random();
        int randValue = 0;

        public Form1()
        {
            InitializeComponent();

        }

        public void GameInitialize()
        {
            //spwan the first obsticle on each side and hero
            leftObsticle.Add(new Rectangle(150, 5, 10, 60));
            rightObsticle.Add(new Rectangle(450, 350, 10, 60));
            hero.X = 50;
            hero.Y = 200;

            titleLabel.Text = "";
            subTitleLabel.Text = "";

            gameTimer.Enabled = true;
            gameState = "running";
        }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over" || gameState == "win")
                    {
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "over" || gameState == "win")
                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move hero
            moveHero();

            // move the Obsticles
            moveObsticles();

            //create more obsticles
            spawnObsticles();

            //remove obsticles when hit the ground
            deleteObsticles();

            //if hero collids with obsticles
            obsticleCollision();

            //if hero gets to the right wall
            heroWins();

            leftCounter++;
            rightCounter++;

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")

            {
                titleLabel.Text = "Dodge Runner 1995";
                subTitleLabel.Text = "Press Space Bar to Start or Escape to Exit";
            }
            else if (gameState == "running")
            {
                e.Graphics.FillRectangle(goldBrush, hero);

                for (int i = 0; i < leftObsticle.Count(); i++)
                {
                    e.Graphics.FillRectangle(whiteBrush, leftObsticle[i]);
                }

                for (int i = 0; i < rightObsticle.Count(); i++)
                {
                    e.Graphics.FillRectangle(whiteBrush, rightObsticle[i]);
                }
            }
            else if (gameState == "over")
            {
                titleLabel.Text = "THEY GOT YOU";
                subTitleLabel.Text += "\nPress Space Bar to Play Again or Escape to Exit";
            }
            else if (gameState == "win")
            {
                titleLabel.Text = "MISSION COMPLETE";
                subTitleLabel.Text += "\nPress Space Bar to Play Again or Escape to Exit";
            }

        }
        
        public void moveHero()
        {
            if (wDown == true && hero.Y > 5)
            {
                hero.Y -= HERO_SPEED;
            }

            if (sDown == true && hero.Y < this.Height - hero.Height - 5)
            {
                hero.Y += HERO_SPEED;
            }

            if (aDown == true && hero.X > 4)
            {
                hero.X -= HERO_SPEED;
            }

            if (dDown == true && hero.X < this.Width - hero.Width - 5)
            {
                hero.X += HERO_SPEED;
            }
        }

        public void moveObsticles()
        {
            for (int i = 0; i < leftObsticle.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = leftObsticle[i].Y + LEFT_SPEED;

                //replace the rectangle in the list with updated one using new y 
                leftObsticle[i] = new Rectangle(leftObsticle[i].X, y, 10, 60);
            }

            // move the right obsticles
            for (int i = 0; i < rightObsticle.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = rightObsticle[i].Y + RIGHT_SPEED;

                //replace the rectangle in the list with updated one using new y 
                rightObsticle[i] = new Rectangle(rightObsticle[i].X, y, 10, 60);

            }
        }

        public void spawnObsticles()
        {
            if (leftCounter == 18)
            {
                leftObsticle.Add(new Rectangle(150, 0, 10, 60));
                leftCounter = 0;
            }
            if (rightCounter == 18)
            {
                rightObsticle.Add(new Rectangle(450, 350, 10, 60));
                rightCounter = 0;
            }
        }

        public void deleteObsticles()
        {
            for (int i = 0; i < leftObsticle.Count(); i++)
            {
                if (leftObsticle[i].Y > this.Height - 60)
                {
                    leftObsticle.RemoveAt(i);
                }
            }
            for (int i = 0; i < rightObsticle.Count(); i++)
            {
                if (rightObsticle[i].Y < 0)
                {
                    rightObsticle.RemoveAt(i);
                }
            }
        }

        public void obsticleCollision()
        {
            for (int i = 0; i < leftObsticle.Count(); i++)
            {
                if (hero.IntersectsWith(leftObsticle[i]))
                {
                    gameTimer.Enabled = false;
                    gameState = "over";
                }
            }
            for (int i = 0; i < rightObsticle.Count(); i++)
            {
                if (hero.IntersectsWith(rightObsticle[i]))
                {
                    gameTimer.Enabled = false;
                    gameState = "over";
                }
            }
        }

        public void heroWins()
        {
            if (hero.X > this.Width - 25)
            {
                gameTimer.Enabled = false;
                gameState = "win";
            }
        }








    }
}


