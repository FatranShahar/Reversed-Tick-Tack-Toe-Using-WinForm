using System;

namespace gameLogic
{
    public class Player
    {
        public enum ePlayerType
        {
            NORMAL = 1,
            COMPUTER
        }

        private string m_PlayerName;
        private int m_Score;
        private ePlayerType m_PlayerType;
        private Board.CellValue m_PlayerSymbol;

        public Player(int i_PlayerType,string i_Name, Board.CellValue i_Symbol)
        {
            m_PlayerName = i_Name;
            m_PlayerType = (ePlayerType)i_PlayerType;
            m_Score = 0;
            m_PlayerSymbol = i_Symbol;
        }
        
        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public  ePlayerType PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                if(PlayerType == ePlayerType.NORMAL)
                {
                    m_PlayerType = ePlayerType.NORMAL;
                }
                else
                {
                    m_PlayerType = ePlayerType.COMPUTER;
                }

            }
        }

        public Board.CellValue PlayerSymbol
        {
            get
            {
                return m_PlayerSymbol;
            }

        }
        
        public int RandCoordForCPU(int i_BoardSize,out int i_Col)
        {
            Random random = new Random();
            int randRow = random.Next(0, i_BoardSize);
            int randCol = random.Next(0, i_BoardSize);
            i_Col = randCol;
            return randRow;
        }
    }
}