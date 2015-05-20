﻿using System;
using System.Windows.Forms;
using SQADemicApp.BL;

namespace SQADemicApp
{
    public partial class GameBoard : Form
    {
        GameBoardModels boardModel;
        CharacterPane form2 = new CharacterPane();
        PlayerPanel playerForm = new PlayerPanel();
        public enum STATE { Dispatcher, Initializing, Move, Draw, Cure, Default }
        public static STATE CurrentState;
        public GameBoard()
        {
            CurrentState = STATE.Initializing;
            string[] rolesDefault = { "Dispatcher", "Scientist" };
            boardModel = new GameBoardModels(rolesDefault);
            InitializeComponent();
            form2.Show();
            playerForm.Show();
            UpdatePlayerForm();
            //GameBoardModels.CURESTATUS.RedCure = GameBoardModels.Cures.CURESTATE.Cured;
            //GameBoardModels.cubeCount.blackCubes = 9;
            CurrentState = STATE.Default;
        }
        public GameBoard(string[] playerRoles)
        {

            boardModel = new GameBoardModels(playerRoles);

            InitializeComponent();
            form2.Show();
            playerForm.Show();
            UpdatePlayerForm();
            CurrentState = STATE.Default;
        }

        //private void DrawBtn_Click(object sender, EventArgs e)
        //{
        //    Card drawnCard = boardModel.drawCard();
        //    GameBoardModels.players[GameBoardModels.CurrentPlayerIndex].hand.Add(drawnCard);
        //    button49.Text = String.Format("Draw\n{0}", boardModel.playerDeckSize());
        //    UpdatePlayerForm();
        //}

        private void City_Click(object sender, EventArgs e)
        {
            Button pressed = sender as Button;
            switch (CurrentState)
            {
                case STATE.Dispatcher:
                    
                    break;
                case STATE.Move:
                    if (PlayerActionsBL.moveplayer(GameBoardModels.players[GameBoardModels.CurrentPlayerIndex], Create.cityDictionary[pressed.Text.Substring(1)]))
                    {
                        switch (GameBoardModels.CurrentPlayerIndex)
                        {
                            case 3:
                                form2.Player4.Text = "Player 4\n" + pressed.Text.Substring(1);
                                break;
                            case 2:
                                form2.Player3.Text = "Player 3\n" + pressed.Text.Substring(1);
                                break;
                            case 1:
                                form2.Player2.Text = "Player 2\n" + pressed.Text.Substring(1);
                                break;
                            default:
                                form2.Player1.Text = "Player 1\n" + pressed.Text.Substring(1);
                                break;
                        }
                        boardModel.incTurnCount();
                        UpdatePlayerForm();
                    }
                    else
                    {
                        MessageBox.Show("Invalid City", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                default:
                    CityPageForm CPForm = new CityPageForm(Create.cityDictionary[pressed.Text.Substring(1)]);
                    CPForm.Show();
                    break;
            }
            CurrentState = STATE.Default;
        }
        public void UpdatePlayerForm()
        {
            playerForm.progressBar1.Value = 100 * (boardModel.currentPlayerTurnCounter) / 4;
            playerForm.label1.Text = playerForm.label1.Text.Substring(0, playerForm.label1.Text.Length - 3) + (Convert.ToInt32(boardModel.currentPlayerTurnCounter)) + "/" + 4;
            playerForm.listBox1.Items.Clear();
            playerForm.listBox1.Items.AddRange(GameBoardModels.players[GameBoardModels.CurrentPlayerIndex].handStringList().ToArray());
            if(GameBoardModels.players[GameBoardModels.CurrentPlayerIndex].role==ROLE.Dispatcher)
            {
                playerForm.DispatcherMove.Show();
                playerForm.AAButton.Location = new System.Drawing.Point(159, 82);
            }
            else
            {
                playerForm.DispatcherMove.Hide();
                playerForm.AAButton.Location = new System.Drawing.Point(91, 81);
            }
            if (boardModel.currentPlayerTurnCounter == 4 )
            {
                playerForm.EndSequenceBtn.Show();
            }
            else
            {
                playerForm.EndSequenceBtn.Hide();
            }
            updateCubeCounts();
            updateCounters();
            updateCureStatus();
        }
        private void updateCubeCounts()
        {
            playerForm.RedCubes.Text = String.Format("Red Cubes Remaining:    {0,-2}/24", GameBoardModels.cubeCount.redCubes);
            playerForm.BlueCubes.Text = String.Format("Blue Cubes Remaining:   {0,-2}/24", GameBoardModels.cubeCount.blueCubes);
            playerForm.BlackCubes.Text = String.Format("Black Cubes Remaining:  {0,-2}/24", GameBoardModels.cubeCount.blackCubes);
            playerForm.YellowCubes.Text = String.Format("Yellow Cubes Remaining: {0,-2}/24", GameBoardModels.cubeCount.yellowCubes);
        }
        private void updateCounters()
        {
            playerForm.InfectionRate.Text = string.Format("Infection Rate: {0}", boardModel.InfectionRateCounter);
            playerForm.OutbreakCount.Text = string.Format("Outbreak Count: {0}", GameBoardModels.outbreakMarker);
        }
        private void updateCureStatus()
        {
            // set value of cure label to status in game board
            // if status is NotCured, change to No Cure for nicer appearance
            playerForm.RedCure.Text = String.Format("Red:  {0}", GameBoardModels.CURESTATUS.RedCure.ToString().Replace("NotCured", "No Cure"));
            playerForm.BlueCure.Text = String.Format("Blue: {0}", GameBoardModels.CURESTATUS.BlueCure.ToString().Replace("NotCured", "No Cure"));
            playerForm.BlackCure.Text = String.Format("Black:  {0}", GameBoardModels.CURESTATUS.BlackCure.ToString().Replace("NotCured", "No Cure"));
            playerForm.YellowCure.Text = String.Format("Yellow: {0}", GameBoardModels.CURESTATUS.YellowCure.ToString().Replace("NotCured", "No Cure"));
        }
    }
}
