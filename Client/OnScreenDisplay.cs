using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace Jukebox.NET
{
	class OnScreenDisplay : Window
	{
		private const double max_opacity = 0.8;

		private bool persist, visible;
		private string buffer = string.Empty, text;
		private DispatcherTimer dt_in, dt_out;
		private Storyboard sb_fadeIn, sb_fadeOut;
		private TextBlock tb;

		public OnScreenDisplay()
		{
			#region Display initialization

			this.tb = new TextBlock();
			this.tb.FontFamily = new FontFamily(App.Config.Font.Face);
			this.tb.FontSize = App.Config.Font.Size;
			this.tb.Foreground = Brushes.White;
			this.tb.Margin = new Thickness(8);
			this.tb.Text = App.NameAndVersion;
			this.AddChild(this.tb);

			this.Background = Brushes.Black;
			this.Left = Monitor.BoundStart;
			this.Opacity = max_opacity;
			this.ResizeMode = ResizeMode.NoResize;
			this.ShowInTaskbar = false;
			this.SizeToContent = SizeToContent.Height;
			this.Top = 0;
			this.Topmost = true;
			this.Width = Monitor.Width;
			this.WindowStyle = WindowStyle.None;

			#endregion

			#region Storyboards

			string name = "OSD";
			NameScope.SetNameScope(this, new NameScope());
			this.RegisterName(name, this);

			if (Monitor.AdapterRAM < 128)
			{
				Duration duration = new Duration(TimeSpan.FromSeconds(0.2));
				DoubleAnimation da = new DoubleAnimation(0, duration);
				Storyboard.SetTargetName(da, name);
				PropertyPath prop = new PropertyPath("Top");
				Storyboard.SetTargetProperty(da, prop);
				sb_fadeIn = new Storyboard();
				sb_fadeIn.Children.Add(da);

				FormattedText ft = new FormattedText("hcgs",
					System.Globalization.CultureInfo.CurrentCulture,
					FlowDirection.LeftToRight,
					new Typeface(App.Config.Font.Face),
					App.Config.Font.Size,
					Brushes.White);
				da = new DoubleAnimation(0 - ft.Height - this.tb.Margin.Bottom, duration);
				Storyboard.SetTargetName(da, name);
				Storyboard.SetTargetProperty(da, prop);
				sb_fadeOut = new Storyboard();
				sb_fadeOut.Children.Add(da);
			}
			else
			{
				this.AllowsTransparency = true;

				Duration duration = new Duration(TimeSpan.FromSeconds(1));
				DoubleAnimation da = new DoubleAnimation(max_opacity, duration);
				Storyboard.SetTargetName(da, name);
				PropertyPath prop = new PropertyPath(Control.OpacityProperty);
				Storyboard.SetTargetProperty(da, prop);
				sb_fadeIn = new Storyboard();
				sb_fadeIn.Children.Add(da);

				da = new DoubleAnimation(0, duration);
				Storyboard.SetTargetName(da, name);
				Storyboard.SetTargetProperty(da, prop);
				sb_fadeOut = new Storyboard();
				sb_fadeOut.Children.Add(da);
			}

			#endregion

			#region Dispatcher timers

			this.dt_in = new DispatcherTimer();
			this.dt_in.Interval = TimeSpan.Zero;
			this.dt_in.Tick += new EventHandler(FadeIn);
			this.dt_out = new DispatcherTimer();
			this.dt_out.Interval = TimeSpan.FromSeconds(App.Config.OSD.Media);
			this.dt_out.Tick += new EventHandler(FadeOut);

			#endregion

			this.Show();
		}

		public bool Persist
		{
			private get { return this.persist; }

			set
			{
				this.persist = value;
				if (this.persist)
					this.dt_out.Interval = TimeSpan.FromSeconds(App.Config.OSD.Requested);
			}
		}

		public string Text
		{
			set
			{
				if (this.visible && this.text[0] == 'N' && value[0] != 'N')
					this.buffer = this.text;
				else if (this.buffer != string.Empty)
					this.buffer = string.Empty;
				this.text = value;
				this.dt_in.Start();
			}
		}

		#region Display animation

		private void FadeIn(object sender, EventArgs e)
		{
			((DispatcherTimer)sender).Stop();
			this.visible = true;
			this.tb.Text = this.text;
			this.sb_fadeIn.Begin(this);
			if (!this.persist)
				this.dt_out.Start();
		}

		private void FadeOut(object sender, EventArgs e)
		{
			DispatcherTimer dt = (DispatcherTimer)sender;
			dt.Interval = TimeSpan.FromSeconds(App.Config.OSD.Media);
			if (this.buffer != string.Empty)
			{
				this.tb.Text = this.buffer;
				this.buffer = string.Empty;
			}
			else
			{
				dt.Stop();
				this.visible = false;
				this.sb_fadeOut.Begin(this);
			}
		}

		#endregion

		public void Refresh()
		{
			this.tb.FontSize = App.Config.Font.Size;
		}
	}
}
