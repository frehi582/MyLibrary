namespace LibraryManagement // Changed from MyLibrary
{
    partial class ReturnBooksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvBorrowers = new System.Windows.Forms.DataGridView();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpBorrowerActions = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).BeginInit();
            this.grpBorrowerActions.SuspendLayout();
            this.SuspendLayout();
            //
            // dgvBorrowers
            //
            this.dgvBorrowers.AllowUserToAddRows = false;
            this.dgvBorrowers.AllowUserToDeleteRows = false;
            this.dgvBorrowers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowers.BackgroundColor = System.Drawing.Color.LightCoral;
            this.dgvBorrowers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowers.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvBorrowers.Location = new System.Drawing.Point(0, 0);
            this.dgvBorrowers.Name = "dgvBorrowers";
            this.dgvBorrowers.ReadOnly = true;
            this.dgvBorrowers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowers.Size = new System.Drawing.Size(600, 300);
            this.dgvBorrowers.TabIndex = 0;
            this.dgvBorrowers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBorrowers_CellContentClick);
            //
            // btnReturn
            //
            this.btnReturn.Location = new System.Drawing.Point(20, 20);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(100, 30);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            //
            // btnRemove
            //
            this.btnRemove.Location = new System.Drawing.Point(140, 20);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 30);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            //
            // grpBorrowerActions
            //
            this.grpBorrowerActions.BackColor = System.Drawing.Color.IndianRed;
            this.grpBorrowerActions.Controls.Add(this.btnRemove);
            this.grpBorrowerActions.Controls.Add(this.btnReturn);
            this.grpBorrowerActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBorrowerActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBorrowerActions.Location = new System.Drawing.Point(0, 310);
            this.grpBorrowerActions.Name = "grpBorrowerActions";
            this.grpBorrowerActions.Size = new System.Drawing.Size(600, 70);
            this.grpBorrowerActions.TabIndex = 3;
            this.grpBorrowerActions.TabStop = false;
            this.grpBorrowerActions.Text = "Borrower Actions";
            //
            // ReturnBooksForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 380);
            this.Controls.Add(this.grpBorrowerActions);
            this.Controls.Add(this.dgvBorrowers);
            this.Name = "ReturnBooksForm";
            this.Text = "Borrowers Management"; // This text will be updated in code based on context
            // Event handler changed from BorrowersManagementForm_Load to ReturnBooksForm_Load
            // this.Load += new System.EventHandler(this.BorrowersManagementForm_Load); // Removed
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).EndInit();
            this.grpBorrowerActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBorrowers;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox grpBorrowerActions;
    }
}