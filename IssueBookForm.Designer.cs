using System;
using System.Windows.Forms;

namespace LibraryManagement
{
    partial class IssueBookForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblBorrower;
        private ComboBox cmbBorrowers;
        private Label lblBook;
        private ComboBox cmbBooks;
        private Label lblDueDate;
        private DateTimePicker dtpDueDate;
        private Button btnIssue;
        private Button btnCancel;
        private Label lblError;
        private Label lblAvailableCopies;
        private Label lblAvailableCount;
        private GroupBox groupBoxBookInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Form Setup
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Issue Book";
            this.BackColor = System.Drawing.Color.White;

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "ISSUE BOOK TO BORROWER";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.Size = new System.Drawing.Size(250, 25);
            this.Controls.Add(lblTitle);

            // Borrower Label
            lblBorrower = new Label();
            lblBorrower.Text = "Select Borrower:";
            lblBorrower.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblBorrower.Location = new System.Drawing.Point(30, 70);
            lblBorrower.Size = new System.Drawing.Size(120, 25);
            this.Controls.Add(lblBorrower);

            // Borrower ComboBox
            cmbBorrowers = new ComboBox();
            cmbBorrowers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBorrowers.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbBorrowers.Location = new System.Drawing.Point(160, 70);
            cmbBorrowers.Size = new System.Drawing.Size(300, 28);
            cmbBorrowers.TabIndex = 0;
            this.Controls.Add(cmbBorrowers);

            // Book Label
            lblBook = new Label();
            lblBook.Text = "Select Book:";
            lblBook.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblBook.Location = new System.Drawing.Point(30, 110);
            lblBook.Size = new System.Drawing.Size(120, 25);
            this.Controls.Add(lblBook);

            // Book ComboBox
            cmbBooks = new ComboBox();
            cmbBooks.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBooks.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbBooks.Location = new System.Drawing.Point(160, 110);
            cmbBooks.Size = new System.Drawing.Size(300, 28);
            cmbBooks.TabIndex = 1;
            this.Controls.Add(cmbBooks);

            // Book Info GroupBox
            groupBoxBookInfo = new GroupBox();
            groupBoxBookInfo.Text = "Book Information";
            groupBoxBookInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            groupBoxBookInfo.Location = new System.Drawing.Point(30, 150);
            groupBoxBookInfo.Size = new System.Drawing.Size(430, 80);

            lblAvailableCopies = new Label();
            lblAvailableCopies.Text = "Available Copies:";
            lblAvailableCopies.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblAvailableCopies.Location = new System.Drawing.Point(20, 25);
            lblAvailableCopies.Size = new System.Drawing.Size(120, 20);
            groupBoxBookInfo.Controls.Add(lblAvailableCopies);

            lblAvailableCount = new Label();
            lblAvailableCount.Text = "0";
            lblAvailableCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblAvailableCount.Location = new System.Drawing.Point(150, 25);
            lblAvailableCount.Size = new System.Drawing.Size(50, 20);
            groupBoxBookInfo.Controls.Add(lblAvailableCount);

            this.Controls.Add(groupBoxBookInfo);

            // Due Date Label
            lblDueDate = new Label();
            lblDueDate.Text = "Due Date:";
            lblDueDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDueDate.Location = new System.Drawing.Point(30, 240);
            lblDueDate.Size = new System.Drawing.Size(120, 25);
            this.Controls.Add(lblDueDate);

            // Due Date Picker
            dtpDueDate = new DateTimePicker();
            dtpDueDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpDueDate.Location = new System.Drawing.Point(160, 240);
            dtpDueDate.Size = new System.Drawing.Size(200, 27);
            dtpDueDate.TabIndex = 2;
            dtpDueDate.MinDate = DateTime.Today;
            dtpDueDate.Value = DateTime.Today.AddDays(14); // Default 2 weeks
            this.Controls.Add(dtpDueDate);

            // Issue Button
            btnIssue = new Button();
            btnIssue.Text = "Issue Book";
            btnIssue.BackColor = System.Drawing.Color.SteelBlue;
            btnIssue.ForeColor = System.Drawing.Color.White;
            btnIssue.FlatStyle = FlatStyle.Flat;
            btnIssue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnIssue.Location = new System.Drawing.Point(160, 280);
            btnIssue.Size = new System.Drawing.Size(120, 40);
            btnIssue.TabIndex = 3;
            this.Controls.Add(btnIssue);

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.BackColor = System.Drawing.Color.LightGray;
            btnCancel.ForeColor = System.Drawing.Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnCancel.Location = new System.Drawing.Point(290, 280);
            btnCancel.Size = new System.Drawing.Size(100, 40);
            btnCancel.TabIndex = 4;
            this.Controls.Add(btnCancel);

            // Error Label
            lblError = new Label();
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblError.Location = new System.Drawing.Point(30, 320);
            lblError.Size = new System.Drawing.Size(430, 20);
            lblError.Visible = false;
            this.Controls.Add(lblError);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}