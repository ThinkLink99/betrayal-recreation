namespace betrayal_recreation_client.WinForms
{
    partial class Form1
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
            this.btnMoveWest = new System.Windows.Forms.Button();
            this.btnMoveSouth = new System.Windows.Forms.Button();
            this.btnMoveEast = new System.Windows.Forms.Button();
            this.btnMoveNorth = new System.Windows.Forms.Button();
            this.btnMoveDownstairs = new System.Windows.Forms.Button();
            this.btnMoveUpstairs = new System.Windows.Forms.Button();
            this.btnEndTurn = new System.Windows.Forms.Button();
            this.lblCurrentTurn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMoveWest
            // 
            this.btnMoveWest.Location = new System.Drawing.Point(12, 261);
            this.btnMoveWest.Name = "btnMoveWest";
            this.btnMoveWest.Size = new System.Drawing.Size(75, 48);
            this.btnMoveWest.TabIndex = 0;
            this.btnMoveWest.Text = "West";
            this.btnMoveWest.UseVisualStyleBackColor = true;
            this.btnMoveWest.Click += new System.EventHandler(this.btnMoveWest_Click);
            // 
            // btnMoveSouth
            // 
            this.btnMoveSouth.Location = new System.Drawing.Point(93, 261);
            this.btnMoveSouth.Name = "btnMoveSouth";
            this.btnMoveSouth.Size = new System.Drawing.Size(75, 48);
            this.btnMoveSouth.TabIndex = 1;
            this.btnMoveSouth.Text = "South";
            this.btnMoveSouth.UseVisualStyleBackColor = true;
            this.btnMoveSouth.Click += new System.EventHandler(this.btnMoveSouth_Click);
            // 
            // btnMoveEast
            // 
            this.btnMoveEast.Location = new System.Drawing.Point(174, 261);
            this.btnMoveEast.Name = "btnMoveEast";
            this.btnMoveEast.Size = new System.Drawing.Size(75, 48);
            this.btnMoveEast.TabIndex = 2;
            this.btnMoveEast.Text = "East";
            this.btnMoveEast.UseVisualStyleBackColor = true;
            this.btnMoveEast.Click += new System.EventHandler(this.btnMoveEast_Click);
            // 
            // btnMoveNorth
            // 
            this.btnMoveNorth.Location = new System.Drawing.Point(93, 207);
            this.btnMoveNorth.Name = "btnMoveNorth";
            this.btnMoveNorth.Size = new System.Drawing.Size(75, 48);
            this.btnMoveNorth.TabIndex = 3;
            this.btnMoveNorth.Text = "North";
            this.btnMoveNorth.UseVisualStyleBackColor = true;
            this.btnMoveNorth.Click += new System.EventHandler(this.btnMoveNorth_Click);
            // 
            // btnMoveDownstairs
            // 
            this.btnMoveDownstairs.Location = new System.Drawing.Point(255, 261);
            this.btnMoveDownstairs.Name = "btnMoveDownstairs";
            this.btnMoveDownstairs.Size = new System.Drawing.Size(75, 46);
            this.btnMoveDownstairs.TabIndex = 4;
            this.btnMoveDownstairs.Text = "Downstairs";
            this.btnMoveDownstairs.UseVisualStyleBackColor = true;
            this.btnMoveDownstairs.Click += new System.EventHandler(this.btnMoveDownstairs_Click);
            // 
            // btnMoveUpstairs
            // 
            this.btnMoveUpstairs.Location = new System.Drawing.Point(255, 209);
            this.btnMoveUpstairs.Name = "btnMoveUpstairs";
            this.btnMoveUpstairs.Size = new System.Drawing.Size(75, 46);
            this.btnMoveUpstairs.TabIndex = 5;
            this.btnMoveUpstairs.Text = "Upstairs";
            this.btnMoveUpstairs.UseVisualStyleBackColor = true;
            this.btnMoveUpstairs.Click += new System.EventHandler(this.btnMoveUpstairs_Click);
            // 
            // btnEndTurn
            // 
            this.btnEndTurn.Location = new System.Drawing.Point(312, 38);
            this.btnEndTurn.Name = "btnEndTurn";
            this.btnEndTurn.Size = new System.Drawing.Size(79, 23);
            this.btnEndTurn.TabIndex = 6;
            this.btnEndTurn.Text = "End Turn";
            this.btnEndTurn.UseVisualStyleBackColor = true;
            this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
            // 
            // lblCurrentTurn
            // 
            this.lblCurrentTurn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentTurn.Location = new System.Drawing.Point(9, 12);
            this.lblCurrentTurn.Name = "lblCurrentTurn";
            this.lblCurrentTurn.Size = new System.Drawing.Size(382, 23);
            this.lblCurrentTurn.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Stats:";
            // 
            // lblStats
            // 
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.Location = new System.Drawing.Point(9, 70);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(215, 134);
            this.lblStats.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 321);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCurrentTurn);
            this.Controls.Add(this.btnEndTurn);
            this.Controls.Add(this.btnMoveUpstairs);
            this.Controls.Add(this.btnMoveDownstairs);
            this.Controls.Add(this.btnMoveNorth);
            this.Controls.Add(this.btnMoveEast);
            this.Controls.Add(this.btnMoveSouth);
            this.Controls.Add(this.btnMoveWest);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMoveWest;
        private System.Windows.Forms.Button btnMoveSouth;
        private System.Windows.Forms.Button btnMoveEast;
        private System.Windows.Forms.Button btnMoveNorth;
        private System.Windows.Forms.Button btnMoveDownstairs;
        private System.Windows.Forms.Button btnMoveUpstairs;
        private System.Windows.Forms.Button btnEndTurn;
        private System.Windows.Forms.Label lblCurrentTurn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStats;
    }
}

