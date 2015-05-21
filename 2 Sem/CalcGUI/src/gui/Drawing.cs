using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CalcLib;

namespace gui
{
	class Drawing
	{
		static private PictureBox _monitor;
		// ReSharper disable once PossibleLossOfFraction
		// ReSharper disable once PossibleLossOfFraction
		static private PointF _center;
		private static Graphics _graph;

		public static void Draw(PictureBox monitor, string tern)
		{
			_monitor = monitor;
			_center = new PointF(x: _monitor.Width/2, y: _monitor.Height/2);
			_graph = _monitor.CreateGraphics();
			_graph.Clear(Color.White);

			DrawCoordinateLines();
			try
			{
				DrawGraphFunc(tern);
			}
			catch (Exception)
			{
                JimDead.Dead(_monitor);
			}
		}

		static private void DrawCoordinateLines()
		{
			var pen = new Pen(Color.Black);

			_graph.DrawLine(pen, _center.X, 0, _center.X, _monitor.Height);
			_graph.DrawLine(pen, 0, _center.Y, _monitor.Width, _center.Y);

			var xTriangle = new PointF[3];
			xTriangle[0].X = _monitor.Width;
			xTriangle[0].Y = _center.Y;
			xTriangle[1].X = _monitor.Width - 10;
			xTriangle[1].Y = _center.Y - 4;
			xTriangle[2].X = _monitor.Width - 10;
			xTriangle[2].Y = _center.Y + 4;

			var yTriangle = new PointF[3];
			yTriangle[0].X = _center.X;
			yTriangle[0].Y = 0;
			yTriangle[1].X = _center.X - 4;
			yTriangle[1].Y = 10;
			yTriangle[2].X = _center.X + 4;
			yTriangle[2].Y = 10;

			_graph.FillPolygon(Brushes.Black, yTriangle);
			_graph.FillPolygon(Brushes.Black, xTriangle);

			for (int i = -13; i < 14; i++)
			{
				PointF aX = new PointF { X = _center.X + i * 25, Y = _monitor.Height - _center.Y - 4 };
				PointF bX = new PointF { X = _center.X + i * 25, Y = _monitor.Height - _center.Y + 4 };
				PointF cX = new PointF { X = aX.X - 7, Y = aX.Y + 8 };

				PointF aY = new PointF { X = _center.X - 4, Y = _monitor.Height - _center.Y + i * 25 };
				PointF bY = new PointF { X = _center.X + 4, Y = _monitor.Height - _center.Y + i * 25 };
				PointF cY = new PointF { X = aY.X - 15, Y = aY.Y - 5 };

				pen.Color = Color.Black;

				_graph.DrawLine(pen, aX, bX);
				_graph.DrawLine(pen, aY, bY);

				pen.Color = Color.LightGray;

				if (i != 0)
				{
					_graph.DrawLine(pen, new PointF { X = aX.X, Y = 0 }, new PointF { X = aX.X, Y = _monitor.Height });
					_graph.DrawLine(pen, new PointF { X = 0, Y = aY.Y }, new PointF { X = _monitor.Width, Y = aY.Y });
					_graph.DrawString(Convert.ToString(-i), SystemFonts.DefaultFont, Brushes.Black, cY);
				}

				_graph.DrawString(Convert.ToString(i), SystemFonts.DefaultFont, Brushes.Black, cX);
			}
		}

		static private void DrawGraphFunc(string tern)
		{
			var brush = new SolidBrush(Color.Red);
		    var calc = new CalcClass();

            calc.ParsToPolish(tern);

            var size = new PointF
            {
                X = 2F,
                Y = 2F
            };

		    for (float i = 0; i < _monitor.Width; i += 1.0F/(uint) 25)
		    {
                var foo = ((i - _center.X) / (uint)25);

                var point = new PointF
                {
                    X = ((i - _center.X) / (uint)23 * (uint)23 + _center.X),
                    Y = _monitor.Height - Convert.ToSingle(calc.GetResult(foo) * (uint)25 + _center.Y)
                };

                if ((point.X < _monitor.Width) && (point.Y < _monitor.Height) && (point.X > 0) && (point.Y > 0))
                {
                    _graph.FillRectangle(brush, point.X, point.Y, size.X, size.Y);
                }
		    }
		}

	}
}
