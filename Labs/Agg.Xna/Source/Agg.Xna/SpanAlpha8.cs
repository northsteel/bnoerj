﻿// Copyright (C) 2009, Bjoern Graf <bjoern.graf@gmx.net>
// Copyright (C) 2002-2003 Maxim Shemanarev (McSeem)
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://bnoerj.codeplex.com/license.

namespace Agg.Xna
{
	using Microsoft.Xna.Framework.Graphics;

	/// <summary>
	/// Supports rendering/reading to/from a rendering buffer whose pixels are
	/// stored monochrome and have a size of 8 bits.
	/// </summary>
	public class SpanAlpha8 : ISpan
	{
		public void Render(RowIterator row, int x, int count, CoverageIterator covers, Color c)
		{
			byte dst = c.A;
			row.Move(x);
			for (int i = 0; i < count; i++)
			{
				int alpha = covers.Current * c.A;
				uint src = row[i];
				row[i] = (byte)((((dst - src) * alpha) + (src << 16)) >> 16);

				covers.MoveNext();
			}
		}

		public void HorizontalLine(RowIterator row, int x, int count, Color c)
		{
			byte dst = c.A;
			row.Move(x);
			for (int i = 0; i < count; i++)
			{
				row[i] = dst;
			}
		}

		public Color Get(RowIterator row, int x)
		{
			byte rgb = row[x];
			Color c = new Color();
			c.R = 0;
			c.G = 0;
			c.B = 0;
			c.A = rgb;
			return c;
		}
	}
}
