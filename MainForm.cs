using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagement // Changed from MyLibrary
{
    public partial class MainForm : Form
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;Connect Timeout=30";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadBorrowers();
        }

        private void LoadBooks()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvBooks.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}");
            }
        }

        private void LoadBorrowers()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var adapter = new SqlDataAdapter("SELECT * FROM Borrowers", connection);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvBorrowers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowers: {ex.Message}");
            }
        }

        // Books Management
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            // Ensure BookForm's namespace is also LibraryManagement or add a using directive
            using (var form = new BookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit");
                return;
            }

            var bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
            // Ensure BookForm's namespace is also LibraryManagement or add a using directive
            using (var form = new BookForm())
            {
                form.BookId = bookId;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        var command = new SqlCommand("DELETE FROM Books WHERE BookID = @ID", connection);
                        command.Parameters.AddWithValue("@ID", bookId);
                        command.ExecuteNonQuery();
                    }
                    LoadBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}");
                }
            }
        }

        // Borrowers Management
        private void btnAddBorrower_Click(object sender, EventArgs e)
        {
            // Ensure BorrowerForm's namespace is also LibraryManagement or add a using directive
            using (var form = new BorrowerForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBorrowers();
                }
            }
        }

        private void btnEditBorrower_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to edit");
                return;
            }

            var borrowerId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value);
            // Ensure BorrowerForm's namespace is also LibraryManagement or add a using directive
            using (var form = new BorrowerForm())
            {
                form.BorrowerId = borrowerId;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBorrowers();
                }
            }
        }

        private void btnDeleteBorrower_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to delete");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this borrower?", "Confirm Delete",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var borrowerId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value);
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        var command = new SqlCommand("DELETE FROM Borrowers WHERE BorrowerID = @ID", connection);
                        command.Parameters.AddWithValue("@ID", borrowerId);
                        command.ExecuteNonQuery();
                    }
                    LoadBorrowers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting borrower: {ex.Message}");
                }
            }
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This event handler is currently empty. You can add logic here if needed,
            // for example, to display details of the clicked book.
        }
    }
}