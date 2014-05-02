using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Ideohazard
{
	public class Ideohazard
	{
		public Point location;
		public int size = 100;
		public Color fg = Color.Black;
		public Color bg = Color.White;

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
            g.SmoothingMode = SmoothingMode.AntiAlias;

			Brush fgBrush = new SolidBrush(this.fg);
			Brush bgBrush = new SolidBrush(this.bg);

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

		private PictureBox pictureBox1;
		private MainMenu mainMenu1;
		private MenuItem menuItem1;
		private MenuItem menuSaveAs;
		private MenuItem menuItem2;
		private MenuItem menuSetColor;
		private MenuItem menuSetBackground;
		private MenuItem menuSaveMetafile;

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
			this.ideo.fg = Color.White;
			this.ideo.bg = Color.Black;
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuSetColor = new System.Windows.Forms.MenuItem();
			this.menuSetBackground = new System.Windows.Forms.MenuItem();
			this.menuSaveMetafile = new System.Windows.Forms.MenuItem();
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
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuSaveAs,
																					  this.menuSaveMetafile});
			this.menuItem1.Text = "File";
			// 
			// menuSaveAs
			// 
			this.menuSaveAs.Index = 0;
			this.menuSaveAs.Text = "Save As...";
			this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuSetColor,
																					  this.menuSetBackground});
			this.menuItem2.Text = "View";
			// 
			// menuSetColor
			// 
			this.menuSetColor.Index = 0;
			this.menuSetColor.Text = "Set Color...";
			this.menuSetColor.Click += new System.EventHandler(this.menuSetColor_Click);
			// 
			// menuSetBackground
			// 
			this.menuSetBackground.Index = 1;
			this.menuSetBackground.Text = "Set Background Color...";
			this.menuSetBackground.Click += new System.EventHandler(this.menuSetBackground_Click);
			// 
			// menuSaveMetafile
			// 
			this.menuSaveMetafile.Index = 1;
			this.menuSaveMetafile.Text = "Save As Metafile...";
			this.menuSaveMetafile.Click += new System.EventHandler(this.menuSaveMetafile_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Menu = this.mainMenu1;
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

		private void menuSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();

			dlg.DefaultExt = ".png";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Bitmap bm = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
				Graphics g = Graphics.FromImage(bm);
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.Clear(this.ideo.bg);
				this.ideo.Draw(g);

				switch(Path.GetExtension(dlg.FileName))
				{
					case ".bmp":
						bm.Save(dlg.FileName, ImageFormat.Bmp);
						break;
					case ".gif":
						bm.Save(dlg.FileName, ImageFormat.Gif);
						break;
					case ".jpg":
						bm.Save(dlg.FileName, ImageFormat.Jpeg);
						break;
					case ".png":
						bm.Save(dlg.FileName, ImageFormat.Png);
						break;
					case ".emf":
						bm.Save(dlg.FileName, ImageFormat.Emf);
						break;
					case ".wmf":
						bm.Save(dlg.FileName, ImageFormat.Wmf);
						break;
					case ".tiff":
						bm.Save(dlg.FileName, ImageFormat.Tiff);
						break;
				}
			}
		}

		private void menuSetColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				this.ideo.fg = dlg.Color;
				this.pictureBox1.Invalidate();
			}
		}

		private void menuSetBackground_Click(object sender, System.EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				this.pictureBox1.BackColor = dlg.Color;
				this.ideo.bg = dlg.Color;
				this.pictureBox1.Invalidate();
			}
		}

		private void menuSaveMetafile_Click(object sender, System.EventArgs e)
		{
			Bitmap bm = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
			Graphics g = Graphics.FromImage(bm);
			IntPtr hDC = g.GetHdc();
			Metafile mf = new Metafile(hDC, EmfType.EmfOnly);
			Graphics g2 = Graphics.FromImage(mf);
			g2.SmoothingMode = SmoothingMode.AntiAlias;
			g2.Clear(this.ideo.bg);
			this.ideo.Draw(g2);
			mf.Save("c:/000/ideo.wmf");
			//g.ReleaseHdc(hDC);
		}
	}
}
