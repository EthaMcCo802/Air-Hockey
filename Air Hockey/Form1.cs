using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        //The global variables
        int paddle1X = 60;
        int paddle1Y = 240;
        int player1Score = 0;

        int net1X = 19;

        int net2X = 610;

        int paddle2X = 570;
        int paddle2Y = 240;
        int player2Score = 0;

        int paddleWidth = 25;
        int paddleHeight = 25;
        int paddleSpeed = 4;

        int ballX = 315;
        int ballY = 240;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        int ballWidth = 20;
        int ballHeight = 20;

        bool wDown = false;
        bool sDown = false;
        bool dDown = false;
        bool aDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush limeBrush = new SolidBrush(Color.Lime);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen blackPen = new Pen(Color.Black, 5);
        Pen bluePen = new Pen(Color.Blue, 5);
        Pen limePen = new Pen(Color.Lime, 5);

        SoundPlayer hit = new SoundPlayer(Properties.Resources.Hit);
        SoundPlayer applause = new SoundPlayer(Properties.Resources.Applause);
        SoundPlayer score = new SoundPlayer(Properties.Resources.Score);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //The designs seen on the game screen
            e.Graphics.FillRectangle(redBrush, 50, 100, 5, 90);
            e.Graphics.FillRectangle(redBrush, 50, 100, 550, 5);
            e.Graphics.FillRectangle(redBrush, 600, 100, 5, 90);
            e.Graphics.FillRectangle(redBrush, 50, 305, 5, 85);
            e.Graphics.FillRectangle(redBrush, 50, 390, 555, 5);
            e.Graphics.FillRectangle(redBrush, 600, 305, 5, 85);
            e.Graphics.FillRectangle(blackBrush, 20, 190, 35, 120);
            e.Graphics.FillRectangle(blackBrush, 600, 190, 35, 120);
            e.Graphics.FillRectangle(blackBrush, 320, 100, 10, 295);

            e.Graphics.DrawArc(bluePen, 10, 190, 100, 120, 264, 192);
            e.Graphics.DrawArc(limePen, 544, 190, 100, 120, 84, 192);
            e.Graphics.DrawEllipse(blackPen, 225, 150, 200, 200);

            e.Graphics.FillEllipse(blackBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(limeBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        { 
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
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
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //moves the ball 
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //moves player 1 
            if (wDown == true && paddle1Y > 0 && paddle1Y > 100)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight && paddle1Y < 370)
            {
                paddle1Y += paddleSpeed;
            }

            if (dDown == true && paddle1X < this.Width / 2 - 10 - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }

            if (aDown == true && paddle1X > 0 && paddle1X > 50)
            {
                paddle1X -= paddleSpeed;
            }

            //moves player 2 
            if (upArrowDown == true && paddle2Y > 0 && paddle2Y > 100)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight && paddle2Y < 370)
            {
                paddle2Y += paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X > 0 && paddle2X < 580)
            {
                paddle2X += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > this.Width / 2 + 5 - paddleWidth)
            {
                paddle2X -= paddleSpeed;
            }

            //creates Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle side1Rec = new Rectangle(50, 100, 5, 90);
            Rectangle side2Rec = new Rectangle(50, 100, 550, 5);
            Rectangle side3Rec = new Rectangle(600, 100, 5, 90);
            Rectangle side4Rec = new Rectangle(50, 300, 5, 90);
            Rectangle side5Rec = new Rectangle(50, 390, 555, 5);
            Rectangle side6Rec = new Rectangle(600, 300, 5, 90);
            Rectangle net1Rec = new Rectangle(20, 190, 35, 110);
            Rectangle net2Rec = new Rectangle(600, 190, 35, 115);


            //The collision of objects
            if (player1Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                if (ballX <= paddle1X)
                {
                    ballX = paddle1X - paddleWidth - 1;
                    hit.Play();
                }
                else
                {
                    ballX = paddle1X + paddleWidth + 1;
                    ballXSpeed = 5;
                    ballYSpeed = 5;
                    hit.Play();
                }
            }

            else if (player2Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                if (ballX <= paddle2X)
                {
                    ballX = paddle2X - paddleWidth - 1;
                    ballXSpeed = -5;
                    ballYSpeed = 5;
                    hit.Play();
                }
                else
                {
                    ballX = paddle2X + paddleWidth + 1;
                    hit.Play();
                }

            }
            if (side1Rec.IntersectsWith(ballRec))
            {

                ballXSpeed *= -1;
                if (ballX <= 50)
                {
                    ballX = 20 - 5 - 1;
                }

            }

            if (side2Rec.IntersectsWith(ballRec))
            {

                ballYSpeed *= -1;
                if (ballY <= 50)
                {
                    ballY = 50 - 5 - 1;
                }


            }

            if (side3Rec.IntersectsWith(ballRec))
            {

                ballXSpeed *= -1;
                if (ballX >= 600)
                {
                    ballX = 600 + 5 + 1;
                }

            }

            if (side4Rec.IntersectsWith(ballRec))
            {

                ballXSpeed *= -1;
                if (ballX <= 50)
                {
                    ballX = 50 - 5 - 1;
                }

            }

            if (side5Rec.IntersectsWith(ballRec))
            {

                ballYSpeed *= -1;
                if (ballY <= 50)
                {
                    ballY = 50 - 5 - 1;
                }

            }

            if (side6Rec.IntersectsWith(ballRec))
            {

                ballXSpeed *= -1;
                if (ballX >= 600)
                {
                    ballX = 600 + 5 + 1;
                }

            }

            //Adds to player score when a player scores a goal
            if (ballX <= net1X)
            {
                player2Score++;

                p2ScoreLabel.Text = $"{player2Score}";

                score.Play();

                ballX = 315;
                ballY = 200;

                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1Y = 240;
                paddle1X = 60;
                paddle2Y = 240;
                paddle2X = 570;
            }
            else if (ballX >= net2X)
            {
                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";

                score.Play();

                ballX = 315;
                ballY = 200;

                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1Y = 240;
                paddle1X = 60;
                paddle2Y = 240;
                paddle2X = 570;
            }

            //Tells which player has won
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;

                applause.Play();

                winLabel.Text = "Player 1 wins!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;

                applause.Play();

                winLabel.Text = "Player 2 wins!";
            }

            Refresh();

        }
    }
}


