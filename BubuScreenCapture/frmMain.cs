using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

using RAD.Win32;
using RAD.Windows;
using System.Drawing.Imaging;

namespace RAD.BubuScrCapture
{
	/// <summary>
	/// Clipboard Monitor Example 
	/// Copyright (c) Ross Donald 2003
	/// ross@radsoftware.com.au
	/// http://www.radsoftware.com.au
	/// <br/>
	/// Demonstrates how to create a clipboard monitor in C#. Whenever an item is copied
	/// to the clipboard by any application this form will be notified by a call to 
	/// the WindowProc method with the WM_DRAWCLIPBOARD message allowing this form to
	/// read the contents of the clipboard and perform some processing.
	/// </summary>
	/// <remarks>
	/// This application has some functionality beyond a simple example. When an item is copied
	/// to the clipboard this application deals only with images. It will capture the image
    /// and then offers to save the image in JPG format. Then the application terminates.
	/// <br/>
	/// This source code is a work in progress and comes without warranty expressed or implied.
	/// It is an attempt to demonstrate a concept, not to be a finished application.
	/// </remarks>
	public class frmMain : System.Windows.Forms.Form
	{
		#region Clipboard Formats

		string[] formatsAll = new string[] 
		{
			DataFormats.Bitmap,
			DataFormats.CommaSeparatedValue,
			DataFormats.Dib,
			DataFormats.Dif,
			DataFormats.EnhancedMetafile,
			DataFormats.FileDrop,
			DataFormats.Html,
			DataFormats.Locale,
			DataFormats.MetafilePict,
			DataFormats.OemText,
			DataFormats.Palette,
			DataFormats.PenData,
			DataFormats.Riff,
			DataFormats.Rtf,
			DataFormats.Serializable,
			DataFormats.StringFormat,
			DataFormats.SymbolicLink,
			DataFormats.Text,
			DataFormats.Tiff,
			DataFormats.UnicodeText,
			DataFormats.WaveAudio
		};

		string[] formatsAllDesc = new String[] 
		{
			"Bitmap",
			"CommaSeparatedValue",
			"Dib",
			"Dif",
			"EnhancedMetafile",
			"FileDrop",
			"Html",
			"Locale",
			"MetafilePict",
			"OemText",
			"Palette",
			"PenData",
			"Riff",
			"Rtf",
			"Serializable",
			"StringFormat",
			"SymbolicLink",
			"Text",
			"Tiff",
			"UnicodeText",
			"WaveAudio"
		};

		#endregion


		#region Constants



		#endregion


		#region Fields

		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.MenuItem mnuSaveAs;
        

		IntPtr _ClipboardViewerNext;
        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private Bitmap myBitmapCaptured;
        private bool _isShown = false;
		 		

		#endregion


		#region Constructors

		public frmMain()
		{
            InitializeComponent();
		}

		#endregion


		#region Properties - Public



		#endregion


		#region Methods - Private

		/// <summary>
		/// Register this form as a Clipboard Viewer application
		/// </summary>
		private void RegisterClipboardViewer()
		{
			_ClipboardViewerNext = Win32.User32.SetClipboardViewer(this.Handle);
		}

		/// <summary>
		/// Remove this form from the Clipboard Viewer list
		/// </summary>
		private void UnregisterClipboardViewer()
		{
			Win32.User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
		}

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


		/// <summary>
		/// Show the clipboard image in the window 
		/// </summary>
		private void GetClipboardData()
		{
			//
			// Data on the clipboard uses the 
			// IDataObject interface
			//
			IDataObject iData = new DataObject();  
			
			try
			{
				iData = Clipboard.GetDataObject();
			}
			catch (System.Runtime.InteropServices.ExternalException externEx)
			{
				// Copying a field definition in Access 2002 causes this sometimes?
				Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
				return;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}
					
			if (iData.GetDataPresent(DataFormats.Bitmap))
			{                
                //pictureBox1.Image = (Image)iData.GetData(DataFormats.Bitmap);
                myBitmapCaptured = (Bitmap)(iData.GetData(DataFormats.Bitmap));
                //myBitmapCaptured.
                pictureBox1.Image = myBitmapCaptured;

                // Get the path that stores user documents.
                //string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Test.bmp";

                //pictureBox1.Image.Save(FilePath);

                //FilePath = "\"" + FilePath + "\"";
                //Process.Start("mspaint.exe", FilePath);

                _isShown = true;
                
                
                Debug.WriteLine(DataFormats.Bitmap);
			}
		}

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");

                Encoder myEncoder = Encoder.Quality;
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 95L);

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                myEncoderParameters.Param[0] = myEncoderParameter;

                //pictureBox1.Image.Save(saveFileDialog1.FileName);
                pictureBox1.Image.Save(saveFileDialog1.FileName, myImageCodecInfo, myEncoderParameters);

            }
            //this.Close();
        }

		#endregion


		#region Methods - Public

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.Run(new frmMain());
		}


		protected override void WndProc(ref Message m)
		{
			switch ((Win32.Msgs)m.Msg)
			{
					//
					// The WM_DRAWCLIPBOARD message is sent to the first window 
					// in the clipboard viewer chain when the content of the 
					// clipboard changes. This enables a clipboard viewer 
					// window to display the new content of the clipboard. 
					//
				case Win32.Msgs.WM_DRAWCLIPBOARD:
					
					Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");

                    //IntPtr hWnd = m.WParam;

                    //System.Windows.Forms.Wi
					GetClipboardData();

					//
					// Each window that receives the WM_DRAWCLIPBOARD message 
					// must call the SendMessage function to pass the message 
					// on to the next window in the clipboard viewer chain.
					//
					Win32.User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
					break;


					//
					// The WM_CHANGECBCHAIN message is sent to the first window 
					// in the clipboard viewer chain when a window is being 
					// removed from the chain. 
					//
				case Win32.Msgs.WM_CHANGECBCHAIN:
					Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");

					// When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
					// it should call the SendMessage function to pass the message to the 
					// next window in the chain, unless the next window is the window 
					// being removed. In this case, the clipboard viewer should save 
					// the handle specified by the lParam parameter as the next window in the chain. 

					//
					// wParam is the Handle to the window being removed from 
					// the clipboard viewer chain 
					// lParam is the Handle to the next window in the chain 
					// following the window being removed. 
					if (m.WParam == _ClipboardViewerNext)
					{
						//
						// If wParam is the next clipboard viewer then it
						// is being removed so update pointer to the next
						// window in the clipboard chain
						//
						_ClipboardViewerNext = m.LParam;
					}
					else
					{
						Win32.User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
					}
					break;

                //case Win32.Msgs.WM_WINDOWPOSCHANGING:
                    //this.Visible = false;
                //    break;

                //{msg=0x46 (WM_WINDOWPOSCHANGING) hwnd=0x22060c wparam=0x0 lparam=0x39edf64 result=0x0}
                    //{msg=0x18 (WM_SHOWWINDOW) hwnd=0x605cc wparam=0x1 lparam=0x0 result=0x0}

				default:
					//
					// Let the form process the messages that we are
					// not interested in
					//
                    base.WndProc(ref m);
                    if (_isShown)
                    {
                        this.Visible = true;
                    }
                    else
                    {
                        this.Visible = false;
                    }
					break;

			}

		}

		#endregion


		#region Event Handlers - Internal

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			RegisterClipboardViewer();
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UnregisterClipboardViewer();
		}

		#endregion


		#region IDisposable Implementation

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

		#endregion


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(517, 390);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPG files|*.JPG";
            //this.saveFileDialog1.Filter = "BMP files|*.bmp";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(517, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(517, 414);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 100);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BubuScreenCapture";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	}
}
