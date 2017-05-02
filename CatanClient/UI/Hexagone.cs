﻿
using System.Collections.Generic;
using System.Drawing;

namespace CatanClient.UI
{
    class Hexagone:Texture
    {
        public List<Edge> Edges { private set; get; }
        public Point[] Points { get { return new Point[] {Edges[0].Point1, Edges[1].Point1, Edges[2].Point1, Edges[3].Point1, Edges[4].Point1, Edges[5].Point1 }; } }

        public Hexagone(int x, int y, int height, int width,int drawPenWidth) : base(x, y, height, width)
        {
            this.Edges = new List<Edge>();
            int top = (int)(height * 0.25f);
            y += drawPenWidth / 2;
            x += drawPenWidth / 2;

            Point a = new Point((x + width / 2), y);
            Point b = new Point(a.X + (width / 2), a.Y + top);
            Point c = new Point(b.X, b.Y + (int)(height * 0.5f));
            Point d = new Point(a.X, c.Y + (top));
            Point e = new Point(x, c.Y);
            Point f = new Point(e.X, b.Y);

            Edges.Add(new Edge(a,b));
            Edges.Add(new Edge(b, c));
            Edges.Add(new Edge(c, d));
            Edges.Add(new Edge(d, e));
            Edges.Add(new Edge(e, f));
            Edges.Add(new Edge(f, a));
        }
    }
}