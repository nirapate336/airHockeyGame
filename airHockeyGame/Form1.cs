﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace airHockeyGame
{
    public partial class Form1 : Form
    {
        int paddle1X = 50;
        int paddle1Y = 180;
        int player1Score = 0;

        int paddle2X = 500;
        int paddle2Y = 180;
        int player2Score = 0;

        int paddleWidth = 60;
        int paddleHeight = 60;
        int paddleSpeed = 4;

        int ballX = 285;
        int ballY = 200;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        int ballWidth = 30;
        int ballHeight = 30;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        Pen globalpen = new Pen(Color.Gray, 5);

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);
       
        SoundPlayer hitSound1 = new SoundPlayer(Properties.Resources.puckhit);
        SoundPlayer goalSound = new SoundPlayer(Properties.Resources.cheer);

        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
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
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
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
                case Keys.A:
                    aDown = false;
                break;
                case Keys.D:
                    dDown = false;
                break;
                case Keys.Up:
                    upArrowDown = false;
                break;
                case Keys.Down:
                    downArrowDown = false;
                break;
                case Keys.Left:
                    leftArrowDown = false;
                break;
                case Keys.Right:
                    rightArrowDown = false;
                break;
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ballX += ballXSpeed;
            ballY += ballYSpeed;
            //move player 1 
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }
            if (aDown == true && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
            }
            if (dDown == true && paddle1X < this.Width - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }
            //move player 2 
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (leftArrowDown == true && paddle2X > 0)
            {

                paddle2X -= paddleSpeed;
            }
            if (rightArrowDown == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }
            //check if ball hit top or bottom wall and change direction if it does 
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle (paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle net1 = new Rectangle(579, 150, 20, 100);
            Rectangle net2 = new Rectangle(00, 150, 20, 100);

            //check if ball went into net
            if (net1.IntersectsWith(ballRec))
            {
                ballX = 285;
                ballY = 200;
                ballXSpeed = 0;
                ballYSpeed = 0;
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
                goalSound.Play();
            }
            if (net2.IntersectsWith(ballRec))
            {
                ballX = 285;
                ballY = 200;
                ballXSpeed = 0;
                ballYSpeed = 0;
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
                goalSound.Play();
            }
          
            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 
            if ( player2Rec.IntersectsWith(ballRec) && ballX > paddle2X)
            {
                if (ballXSpeed == 0)
                {
                    ballXSpeed = randGen.Next(-9, -5);
                   
                }
                else
                {
                    ballXSpeed *= -1;
                }
                ballX = paddle2X + paddleWidth + 1;
                ballYSpeed = randGen.Next(1, 5);
            
                hitSound1.Play();
            }
            else if (player2Rec.IntersectsWith(ballRec)&& ballX <paddle2X)
            {
                if (ballXSpeed == 0)
                {
                    ballXSpeed = randGen.Next (5,9); 
                }
                else
                {
                    ballXSpeed *=  -1;

                }
                ballX = paddle2X - paddleWidth - 1;
                ballYSpeed = randGen.Next(1, 5);

                hitSound1.Play();
            }

             if (player1Rec.IntersectsWith(ballRec) && ballX<paddle1X)
            {
               if( ballXSpeed ==0)
                {
                    ballXSpeed = randGen.Next (-9,-5);
                   
                }
               else
                {
                    ballXSpeed *=  -1;

                }

                ballX = paddle1X - paddleWidth - 1;
                hitSound1.Play();
                ballYSpeed = randGen.Next(1, 5);

            }
            else if (player1Rec.IntersectsWith(ballRec) && ballX>paddle1X)
            {
                
               if(ballXSpeed==0)
                {
                    ballXSpeed = randGen.Next (5,9); 
                }
               else
                {
                    ballXSpeed *= -1;
                    
                }
                ballX = paddle1X + paddleWidth + 1;
                hitSound1.Play();
                ballYSpeed = randGen.Next(1, 5);

            }

            //bounce off of side walls
            if (ballX <= 0)
            {
                ballXSpeed *= -1;
                ballX = 0;
            }
          
            else if (ballX >= 550 )
            {
                ballXSpeed *= -1;
                ballX = 550;
            }
           
            // check score and stop game if either player is at 3 
            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
                SoundPlayer overSound = new SoundPlayer(Properties.Resources.gameover);
                overSound.Play();

                if(player1Score==3)
                {
                    p1ScoreLabel.Text = "Player 1 wins!";
                    p2ScoreLabel.Text = "";
                }
                else if (player2Score==3)
                {
                    p1ScoreLabel.Text = "Player 2 wins!";
                    p2ScoreLabel.Text = "";
                }
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(globalpen, 579, 150, 20, 100);
            e.Graphics.DrawRectangle(globalpen, 00, 150, 20, 100);
            e.Graphics.DrawLine(globalpen, 300, 00, 300,400);
            e.Graphics.DrawEllipse(globalpen, 250, 165, 100,100 );
            e.Graphics.FillEllipse(blackBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillEllipse(redBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);

        }
    }
}