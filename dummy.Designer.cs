
namespace CarWashManagementSystem
{
    partial class dummy
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtComAddr = new System.Windows.Forms.TextBox();
            this.lblComAddr = new System.Windows.Forms.Label();
            this.txtComName = new System.Windows.Forms.TextBox();
            this.lblComName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(542, 282);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 42);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(369, 282);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 42);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // txtComAddr
            // 
            this.txtComAddr.Location = new System.Drawing.Point(263, 195);
            this.txtComAddr.Name = "txtComAddr";
            this.txtComAddr.Size = new System.Drawing.Size(247, 22);
            this.txtComAddr.TabIndex = 35;
            // 
            // lblComAddr
            // 
            this.lblComAddr.AutoSize = true;
            this.lblComAddr.Location = new System.Drawing.Point(61, 198);
            this.lblComAddr.Name = "lblComAddr";
            this.lblComAddr.Size = new System.Drawing.Size(135, 17);
            this.lblComAddr.TabIndex = 34;
            this.lblComAddr.Text = "Company Address : ";
            // 
            // txtComName
            // 
            this.txtComName.Location = new System.Drawing.Point(263, 127);
            this.txtComName.Name = "txtComName";
            this.txtComName.Size = new System.Drawing.Size(247, 22);
            this.txtComName.TabIndex = 33;
            // 
            // lblComName
            // 
            this.lblComName.AutoSize = true;
            this.lblComName.Location = new System.Drawing.Point(61, 130);
            this.lblComName.Name = "lblComName";
            this.lblComName.Size = new System.Drawing.Size(120, 17);
            this.lblComName.TabIndex = 32;
            this.lblComName.Text = "Company Name : ";
            // 
            // dummy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtComAddr);
            this.Controls.Add(this.lblComAddr);
            this.Controls.Add(this.txtComName);
            this.Controls.Add(this.lblComName);
            this.Name = "dummy";
            this.Text = "dummy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtComAddr;
        private System.Windows.Forms.Label lblComAddr;
        private System.Windows.Forms.TextBox txtComName;
        private System.Windows.Forms.Label lblComName;
    }
}