using System;
using System.Text;
using System.Collections.Generic;
using gameLogic;
using WinFormsUI;
using System.Windows.Forms;

namespace tic_tac_toe_reverse
{
    public class Controller
    {
        private BoardForm m_BoardForm;
        private Board m_GameBoard;
        private List<Player> m_Players;
        private bool m_VsComputer;
        private bool m_Player1Turn;
        public Controller()
        {
            m_BoardForm = new BoardForm();
            m_GameBoard = new Board(m_BoardForm.BoardSize);
            m_Players = new List<Player>();
            m_VsComputer = m_BoardForm.Settings.IsVsComputer;
            m_Player1Turn = true;

            m_Players.Add(new Player((int)Player.ePlayerType.NORMAL, m_BoardForm.Settings.Player1Name, Board.CellValue.X));
            Player.ePlayerType playerType;
            if (m_VsComputer)
            {
                playerType = Player.ePlayerType.COMPUTER;
            }
            else
            {
                playerType = Player.ePlayerType.NORMAL;
            }

            m_Players.Add(new Player((int)playerType, m_BoardForm.Settings.Player2Name, Board.CellValue.O));

            initUIEvents();

        }

        public BoardForm Board_Form
        {
            get
            {
                return m_BoardForm;
            }
        }

        private void initUIEvents()
        {
            m_BoardForm.MarkCellOnBoard += TransferDataToLogic;
        }

        private void TransferDataToLogic(int i_Row, int i_Col)
        {
            string title;
            StringBuilder bodyMessage;
            bool computerWinner = false, Player1Winner;
            if (m_Player1Turn)
            {
                m_GameBoard.PlaceNewSignOnBoard(i_Row, i_Col, Board.CellValue.X);
                m_BoardForm.BoardMatrix[i_Row, i_Col].Text = "X";
                computerWinner = m_GameBoard.CheckBoardStatusForLose();
                if (m_VsComputer)
                {
                    int row;
                    int col;

                    row = m_Players[1].RandCoordForCPU(m_GameBoard.SizeOfBoard, out col);
                    while (!m_GameBoard.CheckValidIndex(row, col))
                    {
                        row = m_Players[1].RandCoordForCPU(m_GameBoard.SizeOfBoard, out col);
                    }

                    m_Player1Turn = !m_Player1Turn;

                    m_GameBoard.PlaceNewSignOnBoard(row, col, Board.CellValue.O);
                    m_BoardForm.BoardMatrix[row, col].Text = "O";
                    m_BoardForm.BoardMatrix[row, col].Enabled = false;
                    Player1Winner = m_GameBoard.CheckBoardStatusForLose();
                }
                else
                {
                    m_BoardForm.SwitchFontToBold(m_BoardForm.Player2Label, m_BoardForm.Player1Label);
                }
            }
            else
            {
                m_BoardForm.BoardMatrix[i_Row, i_Col].Text = "O";
                m_GameBoard.PlaceNewSignOnBoard(i_Row, i_Col, Board.CellValue.O);
                m_BoardForm.SwitchFontToBold(m_BoardForm.Player1Label, m_BoardForm.Player2Label);
            }

            if (m_GameBoard.CheckBoardStatusForLose())
            {
                Player winner;
                StringBuilder labelMsg = new StringBuilder();
                Label winnerLabel = new Label();
                if(computerWinner)
                {
                    winner = m_Players[1];
                    winnerLabel = m_BoardForm.Player2Label;
                }
                else if (m_Player1Turn)
                {
                    winner = m_Players[1];
                    winnerLabel = m_BoardForm.Player2Label;
                }
                else
                {
                    winner = m_Players[0];
                    winnerLabel = m_BoardForm.Player1Label;

                }
                winner.PlayerScore += 1;
                labelMsg.Append($"{winner.PlayerName}: {winner.PlayerScore}");
                winnerLabel.Text = labelMsg.ToString();
                bodyMessage = MessageWin(out title, winner.PlayerName);
                m_GameBoard.ResetBoard();
                m_BoardForm.ShowMessageEndGame(bodyMessage, title);
            }
            else if (m_GameBoard.NumberOfEmptyCells == 0)
            {
                bodyMessage = MessageTie(out title);
                m_GameBoard.ResetBoard();
                m_BoardForm.ShowMessageEndGame(bodyMessage, title);
            }
            
            m_Player1Turn = !m_Player1Turn;
        }

        private StringBuilder MessageWin(out string i_Title, string i_WinnerName)
        {
            i_Title = "A Win!";
            StringBuilder WinnerMessage = new StringBuilder();
            WinnerMessage.AppendLine($"The winner is {i_WinnerName}!");
            WinnerMessage.AppendLine("Would you like to play another round?");
            return WinnerMessage;
        }
        private StringBuilder MessageTie(out string i_Title)
        {
            i_Title = "A Tie!";
            StringBuilder tieMessage = new StringBuilder();
            tieMessage.AppendLine("Tie!");
            tieMessage.AppendLine("Would you like to play another round?");
            return tieMessage;
        }

        public void Run()
        {
            Board_Form.ShowMe();
        }
    }
}
