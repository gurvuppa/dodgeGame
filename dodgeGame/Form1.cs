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

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);

        Random randGen = new Random();
        int randValue = 0;

        public Form1()
        {
            InitializeComponent();

            //spwan the first obsticle on each side
            leftObsticle.Add(new Rectangle(150, 5, 10, 60));
            rightObsticle.Add(new Rectangle(450, 350, 10, 60));
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

            // move the left obsticles
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

            //create more obsticles
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

            //remove obsticles when hit the ground
            for (int i = 0; i < leftObsticle.Count(); i++)
            {
                if (leftObsticle[i].Y > this.Height - 60 )
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

            //if hero collids with obsticles
            for (int i = 0; i < leftObsticle.Count(); i++)
            {
                if (hero.IntersectsWith(leftObsticle[i]))
                {
                    gameTimer.Enabled = false;
                }
            }
            for (int i = 0; i < rightObsticle.Count(); i++)
            {
                if (hero.IntersectsWith(rightObsticle[i]))
                {
                    gameTimer.Enabled = false;
                }
            }

            //if hero gets to the right wall
            if (hero.X > this.Width - 25)
            {
                gameTimer.Enabled = false;
            }

            leftCounter++;
            rightCounter++;

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, hero);
            
            for (int i = 0; i < leftObsticle.Count(); i++)
            { 
                e.Graphics.FillRectangle(whiteBrush, leftObsticle[i]);
            }

            for (int i = 0; i < rightObsticle.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, rightObsticle[i]);
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
    }
}


