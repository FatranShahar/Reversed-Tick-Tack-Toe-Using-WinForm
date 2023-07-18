using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;


namespace WinFormsUI
{
    public class BoardForm : Form
    {
        private readonly int m_BoardSize;
        private Button[,] m_BoardMat;
        private const int k_ButtonSize = 50; 
        private const int k_ButtonSpacing = 5;
        private const int k_FormPadding = 20;

        private Label m_Player1;
        private Label m_Player2;


        public event Action<int, int> MarkCellOnBoard;

        private SettingForm m_SettingsForm;

        public BoardForm()
        {
            m_SettingsForm = new SettingForm();
            m_SettingsForm.ShowDialog();
            m_BoardSize = m_SettingsForm.BoardSize;
            m_BoardMat = new Button[m_BoardSize, m_BoardSize];
            InitializeComponent();
            InitBoard(m_SettingsForm.Player1Name, m_SettingsForm.Player2Name);
        }

        public void ShowMe()
        {
            this.ShowDialog();
        }

        public int FormSize
        {
            get
            {
                return BoardSize * (k_ButtonSize + k_ButtonSpacing) + k_FormPadding * 2;
            }
        }

        public Label Player1Label
        {
            get
            {
               return m_Player1;
            }
        }

        public Label Player2Label
        {
            get
            {
                return m_Player2;
            }
        }

        public int FormPadding
        {
            get
            {
                return k_FormPadding;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1.Text;
            }
            set
            {
                m_Player1.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2.Text;
            }
            set
            {
                m_Player2.Text = value;
            }
        }

        public SettingForm Settings
        {
            get
            {
                return m_SettingsForm;
            }
        }

        public void InitBoard(string i_Player1Name, string i_Player2Name)
        {
            for (int i = 0; i < BoardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < BoardMatrix.GetLength(1); j++)
                {
                    Button cell = new Button();
                    cell.Text = "";
                    cell.Size = new Size(k_ButtonSize, k_ButtonSize);
                    cell.Location = new Point(k_FormPadding + j * (k_ButtonSize + k_ButtonSpacing), k_FormPadding + i * (k_ButtonSize + k_ButtonSpacing - 1));
                    BoardMatrix[i, j] = cell;
                    BoardMatrix[i, j].Click += Button_Click;
                    this.Controls.Add(BoardMatrix[i, j]);
                }
            }

            ClientSize = new Size(this.FormSize, this.FormSize + 100);
            Padding = new Padding(this.FormPadding);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            m_Player1 = new Label();
            m_Player1.Text = $"{i_Player1Name}: {0}";
            m_Player1.Font = new Font(m_Player1.Font, FontStyle.Bold);
            int middleOfForm = this.ClientSize.Width / 2;

            m_Player1.AutoSize = true;

            m_Player2 = new Label();
            m_Player2.Text = $"{i_Player2Name}: {0}";

            m_Player1.Location = new Point(middleOfForm - (m_Player1.Width + m_Player2.Width) / 2, BoardMatrix[BoardSize - 1, BoardSize - 1].Bottom + 20);
            m_Player2.Location = new Point(m_Player1.Right, BoardMatrix[BoardSize - 1, BoardSize - 1].Bottom + 20);
            m_Player2.AutoSize = true;

            Controls.Add(m_Player1);
            Controls.Add(m_Player2);
        }

        public Button[,] BoardMatrix
        {
            get
            {
                return m_BoardMat;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(148, 98);
            this.Name = "BoardUI";
            this.ResumeLayout(false);
        }

        public void SwitchFontToBold(Label i_BoldLabel, Label i_RegularLabel)
        {
            i_BoldLabel.Font = new Font(i_BoldLabel.Font, FontStyle.Bold);
            i_RegularLabel.Font = new Font(i_RegularLabel.Font, FontStyle.Regular);
        }

        private void Button_Click(object sender, EventArgs e)
        {

            Button clickedButton = (Button)sender;
            int row = -1;
            int col = -1;

            for (int r = 0; r < BoardMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < BoardMatrix.GetLength(1); c++)
                {
                    if (BoardMatrix[r, c] == clickedButton)
                    {
                        row = r;
                        col = c;
                        break;
                    }
                }

                if (row != -1)
                {
                    break;
                }

            }

            clickedButton.Enabled = false;
            MarkCellOnClick(row, col);
        }

        private void MarkCellOnClick(int i_Row, int i_Col)
        {
            if (MarkCellOnBoard != null)
            {
                MarkCellOnBoard.Invoke(i_Row, i_Col);
            }

        }

        private void ResetBoard()
        {
            for (int i = 0; i < BoardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < BoardMatrix.GetLength(1); j++)
                {
                    BoardMatrix[i, j].Text = "";
                    BoardMatrix[i, j].Enabled = true;

                }
            }

        }
        
        public void ShowMessageEndGame(StringBuilder i_Message, string i_Title)
        {
            DialogResult result = MessageBox.Show(i_Message.ToString(), i_Title, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ResetBoard();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
        }
        static void Main() { }
      
    }
}