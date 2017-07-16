using System;
using System.Drawing;
using SharpCanvas;

namespace ChatBotWithLib
{
    internal class Graph
    {
        Canvas canvas;
        public void DrawGraph()
        {
            if(canvas != null)
            {
                CloseGraph();
            }
            canvas = new Canvas(1200, 500);
            canvas.ImageCanvasMode();
            canvas.title = "Graph";
            Line l = new Line(0, 0, 100, 100);
            canvas.SetPenColor(Colorb.White);
            canvas.SetPenSize(1);
            canvas.PenCanvasDraw(l);
        }
        public void CloseGraph()
        {
            canvas.window.Exit();
        }
    }
}