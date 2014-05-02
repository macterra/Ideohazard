using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Ideohazard
{
	public class Ideohazard
	{
		public Point location;
		public int size = 100;
		public Brush fgBrush = Brushes.Black;
		public Brush bgBrush = Brushes.White;

		public Ideohazard()
		{
		}

		private void DrawCircle(Graphics g, PointF center, float rad, Brush brush)
		{
			g.FillEllipse(brush, center.X-rad, center.Y-rad, 2*rad, 2*rad);
		}

		private void AddCircleClip(GraphicsPath path, PointF center, float rad)
		{
			path.AddEllipse(center.X-rad, center.Y-rad, 2*rad, 2*rad);
		}

		public void Draw(Graphics g)
		{
			float outcent = size*22/100;
			float incent = outcent * 11/8;
			float inrad = outcent * 9/10;
			float cliprad = inrad * 9/10;
			float outrad = outcent * 5/4;
			float ringrad = outcent * 9/10;
			float ringwidth = outcent/8;
			float ringcutwidth = outcent/10;
			float centcutwidth = outcent/10;
			float centcutlen = incent-inrad;
			float centrad = outcent * 3/8;

			PointF center = new PointF(location.X+size/2, location.Y+size/2);
			PointF pt = center;
			pt.Y += outcent;
			PointF[] pts = { pt };

			Matrix m = new Matrix();
			m.RotateAt(120, center);

			DrawCircle(g, pts[0], outrad, fgBrush);
			m.TransformPoints(pts);
			DrawCircle(g, pts[0], outrad, fgBrush);
			m.TransformPoints(pts);
			DrawCircle(g, pts[0], outrad, fgBrush);

			pts[0] = center;
			pts[0].Y -= incent;
			
			GraphicsPath path = new GraphicsPath();

			DrawCircle(g, pts[0], inrad, bgBrush);
			AddCircleClip(path, pts[0], cliprad);
			m.TransformPoints(pts);
			DrawCircle(g, pts[0], inrad, bgBrush);
			AddCircleClip(path, pts[0], cliprad);
			m.TransformPoints(pts);
			DrawCircle(g, pts[0], inrad, bgBrush);
			AddCircleClip(path, pts[0], cliprad);

			g.SetClip(path);
			DrawCircle(g, center, ringrad+ringwidth, fgBrush);
			DrawCircle(g, center, ringrad-ringwidth, bgBrush);

			g.ResetClip();
			DrawCircle(g, center, centrad, bgBrush);

			GraphicsPath cut = new GraphicsPath();
			Rectangle rect = new Rectangle(0, 0, (int)centcutwidth, (int)centcutlen);
			rect.Offset((int)(center.X-centcutwidth/2), (int)(center.Y-centcutwidth/2));
			rect.Offset(0, -(int)centcutlen);
			cut.AddRectangle(rect);

			g.FillPath(bgBrush, cut);
			cut.Transform(m);
			g.FillPath(bgBrush, cut);
			cut.Transform(m);
			g.FillPath(bgBrush, cut);

			/*
			rect = new Rectangle(this.location, new Size(size, size));
			g.DrawRectangle(Pens.Black, rect);
			g.DrawLine(Pens.Black, rect.Left, rect.Top, rect.Right, rect.Bottom);
			g.DrawLine(Pens.Black, rect.Right, rect.Top, rect.Left, rect.Bottom);

			Console.WriteLine(rect);
			*/
		}
	}

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Ideohazard ideo;

		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ideo = new Ideohazard();
			this.ideo.fgBrush = Brushes.HotPink;
			this.ideo.bgBrush = Brushes.Black;
			this.pictureBox1.BackColor = Color.Black;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(292, 273);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Name = "Form1";
			this.Text = "Ideohazard";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			this.ideo.Draw(e.Graphics);
		}

		private void pictureBox1_Resize(object sender, System.EventArgs e)
		{
			this.ideo.size = (int)Math.Min(this.pictureBox1.Width, this.pictureBox1.Height);
			this.ideo.location.X = (this.pictureBox1.Width-this.ideo.size)/2;
			this.ideo.location.Y = (this.pictureBox1.Height-this.ideo.size)/2;
			this.pictureBox1.Invalidate();
		}
	}
}
