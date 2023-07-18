namespace gameLogic
{
    public class Board
    {
        public enum CellValue
        {
            Space,
            X,
            O
        }

        private CellValue[,] m_GameBoard;
        private int m_Size;
        private int m_AmountOfEmptyCells;

        public Board(int i_Size)
        {
            m_Size = i_Size;
            m_GameBoard = new CellValue[m_Size, m_Size];
            this.ResetBoard();
        }

        public int SizeOfBoard
        {
            get
            {
                return m_Size;
            }
            set
            {
                m_Size = SizeOfBoard;
            }
        }
        public CellValue[,] Mat
        {
            get
            {
                return m_GameBoard;
            }
        }
        public int NumberOfEmptyCells
        {
            get
            {
                return m_AmountOfEmptyCells;
            }
            set
            {
                m_AmountOfEmptyCells = value;
            }
        }

        public bool CheckValidIndex(int i_Row, int i_Column)
        {
            return (m_GameBoard[i_Row, i_Column] == CellValue.Space);
        }

        public bool CheckBoardStatusForLose()
        {
            return CheckSeconderyDiagonalForLose() || CheckPrimeryDiagonalForLose() || CheckColsForLose() || CheckRowsForLose();
        }

        public bool CheckRowsForLose()
        {
            bool looseStatus = false;
            for (int i = 0; i < m_Size; i++)
            {
                int counter = 0;
                if (!CheckValidIndex(i, 0))
                {
                    for (int j = 0; j < m_Size; j++)
                    {
                        if (m_GameBoard[i, j] == m_GameBoard[i, 0])
                        {
                            counter++;
                        }
                    }
                }
                if (counter == m_Size)
                {
                    looseStatus = true;
                    break;
                }
            }
            return looseStatus;
        }

        public bool CheckColsForLose()
        {
            bool looseStatus = false;
            for (int j = 0; j < m_Size; j++)
            {
                int counter = 0;
                                
                if(!CheckValidIndex(0, j))
                {
                    for (int i = 0; i < m_Size; i++)
                    {
                        if (m_GameBoard[i, j] == m_GameBoard[0, j] && m_GameBoard[i, j] != CellValue.Space)
                        {
                            counter++;
                        }

                    }

                }

                if (counter == m_Size)
                {
                    looseStatus = true;
                    break;
                }

            }
            return looseStatus;
        }

        public bool CheckPrimeryDiagonalForLose()
        {
            int counter = 0;
            bool looseStatus = false;

            if (!CheckValidIndex(0, 0))
            {
                for (int i = 0; i < m_Size; i++)
                {
                    if (m_GameBoard[i, i] == m_GameBoard[0, 0])
                    {
                        counter++;
                    }

                }

                if (counter == m_Size)
                {
                    looseStatus = true;
                }

            }
            return looseStatus;
        }

        public bool CheckSeconderyDiagonalForLose()
        {
            int counter = 0;
            bool looseStatus = false;
            int j = m_Size - 1;
            if (!CheckValidIndex(0, j))
            {
                for (int i = 0; i < m_Size; i++)
                {
                    if (m_GameBoard[i, j] == m_GameBoard[0, m_Size - 1])
                    {
                        counter++;
                    }

                    j--;

                }
                if (counter == m_Size)
                {
                    looseStatus = true;
                }

            }
            return looseStatus;
        }

        public bool PlaceNewSignOnBoard(int i_i, int i_j, CellValue i_Sign)
        {
            bool spotIsAvaildable = CheckValidIndex(i_i, i_j);
            if (spotIsAvaildable)
            {
                m_GameBoard[i_i, i_j] = i_Sign;
                NumberOfEmptyCells = NumberOfEmptyCells - 1; 
            }

            return spotIsAvaildable;
        }

        public CellValue GetSlot(int i_row, int i_col)
        {
            return m_GameBoard[i_row, i_col];
        }
        public void ResetBoard()
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_GameBoard[i, j] = CellValue.Space;
                }

            }

            NumberOfEmptyCells = m_Size * m_Size;
        }
        public static void Main() { }
    }
}