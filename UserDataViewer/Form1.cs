using System.ComponentModel;
using UserDataViewerCore;

namespace UserDataViewer
{
    public partial class Form1 : Form
    {
        private List<User> allUsers = new List<User>();
        private List<User> currentUsers = new List<User>();
        private int currentPage = 1;
        private const int pageSize = 50;
        private List<string> validationErrors = new List<string>();

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.DataSource = new BindingList<User>(new List<User>());
            dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Выберите файл с данными пользователей"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var result = User.LoadUsersFromFile(openFileDialog.FileName);
                allUsers = result.allUsers;
                currentUsers = allUsers;
                validationErrors = result.validationErrors;
                currentPage = 1;
                ShowCurrentPage();
                ShowValidationErrors();
            }
        }


        private void ShowValidationErrors()
        {
            richTextBoxErrors.Clear();

            if (validationErrors.Count == 0)
            {
                richTextBoxErrors.Text = "Ошибок не найдено.";
                return;
            }

            richTextBoxErrors.SelectionColor = Color.Red;

            richTextBoxErrors.AppendText($"Найдено ошибок: {validationErrors.Count}\n\n");

            for (int i = 0; i < validationErrors.Count; i++)
            {
                richTextBoxErrors.AppendText($"• {validationErrors[i]}\n");
            }

        }

        private void ShowCurrentPage()
        {
            if (currentUsers.Count == 0)
            {
                lblPageInfo.Text = "Нет данных";
                dataGridView.DataSource = new BindingList<User>(new List<User>());
                return;
            }

            try
            {
                int totalPages = (int)Math.Ceiling((double)currentUsers.Count / pageSize);
                var pageData = currentUsers
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                dataGridView.DataSource = new BindingList<User>(pageData);
                lblPageInfo.Text = $"Страница {currentPage} из {totalPages}";

                btnPrevPage.Enabled = currentPage > 1;
                btnNextPage.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                lblPageInfo.Text = "Ошибка пагинации";
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentUsers = DataViewer.SerchUser(txtSearch.Text.ToLower(), currentUsers, allUsers);

            currentPage = 1;
            ShowCurrentPage();
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dataGridView.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(columnName)) return;

            var direction = dataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending
                ? ListSortDirection.Descending
                : ListSortDirection.Ascending;
            
            var resultSort = DataViewer.SortUser(columnName, direction, currentUsers);

            currentUsers = resultSort.currentUsers;
            var errorSort = resultSort.ErrorSort;

            if (errorSort != null)
            {
                MessageBox.Show($"{errorSort}");
            }

            ShowCurrentPage();

            dataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Index != e.ColumnIndex)
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            currentPage--;
            ShowCurrentPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            currentPage++;
            ShowCurrentPage();
        }
    }
}