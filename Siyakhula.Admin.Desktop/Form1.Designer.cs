namespace Siyakhula.Admin.Desktop
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.txtTotalAttendance = new System.Windows.Forms.TextBox();
            this.btnCalculateFunding = new System.Windows.Forms.Button();
            this.lblSubsidyResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(420, 37);
            this.lblTitle.Text = "Siyakhula ECD - Admin Portal";
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction.Location = new System.Drawing.Point(25, 80);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(250, 23);
            this.lblInstruction.Text = "Enter Verified Total Attendance:";
            // 
            // txtTotalAttendance
            // 
            this.txtTotalAttendance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTotalAttendance.Location = new System.Drawing.Point(280, 77);
            this.txtTotalAttendance.Name = "txtTotalAttendance";
            this.txtTotalAttendance.Size = new System.Drawing.Size(120, 30);
            // 
            // btnCalculateFunding
            // 
            this.btnCalculateFunding.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCalculateFunding.Location = new System.Drawing.Point(25, 130);
            this.btnCalculateFunding.Name = "btnCalculateFunding";
            this.btnCalculateFunding.Size = new System.Drawing.Size(375, 40);
            this.btnCalculateFunding.Text = "Generate Subsidy Report (R24/Child)";
            this.btnCalculateFunding.UseVisualStyleBackColor = true;
            this.btnCalculateFunding.Click += new System.EventHandler(this.btnCalculateFunding_Click);
            // 
            // lblSubsidyResult
            // 
            this.lblSubsidyResult.AutoSize = true;
            this.lblSubsidyResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSubsidyResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSubsidyResult.Location = new System.Drawing.Point(25, 200);
            this.lblSubsidyResult.Name = "lblSubsidyResult";
            this.lblSubsidyResult.Size = new System.Drawing.Size(262, 28);
            this.lblSubsidyResult.Text = "Total Funding Payout: R 0.00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 280);
            this.Controls.Add(this.lblSubsidyResult);
            this.Controls.Add(this.btnCalculateFunding);
            this.Controls.Add(this.txtTotalAttendance);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lblTitle);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Siyakhula Funding Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TextBox txtTotalAttendance;
        private System.Windows.Forms.Button btnCalculateFunding;
        private System.Windows.Forms.Label lblSubsidyResult;
    }
}

