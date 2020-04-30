using System;
using System.Collections.Generic;   
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphs
{
    public partial class FormMain : Form
    {
        //Defaultní graf
        readonly Point[] defaultVertexPositions = new Point[]{
            new Point(150*3,100*3),
            new Point(140*3,140*3),
            new Point(160*3,140*3),
            new Point(100*3,150*3),
            new Point(200*3,150*3),
            new Point(140*3,160*3),
            new Point(160*3,160*3),
            new Point(150*3,200*3)
        };
        readonly int[,] defaultMatrix = new int[,] {
            {0,4,3,5,0,0,0,0},
            {4,0,0,7,0,0,4,0},
            {3,0,0,0,7,0,9,0},
            {5,7,0,0,0,0,0,7},
            {0,0,7,0,0,0,4,0},
            {0,0,0,0,0,0,6,0},
            {0,4,9,0,4,9,0,7},
            {0,0,0,7,0,0,7,0}
        };
        //Globální proměnné
        List<Vertex> vertexes = new List<Vertex>();
        List<Edge> edges = new List<Edge>();
        Vertex startingVertex = null;
        Queue<Step> stepQueue = new Queue<Step>();
        //Globální proměnné barev
        public Color vertexBaseColor = Color.CadetBlue;
        public Color edgeBaseColor = Color.LightGray;
        public Color textBaseColor = Color.Black;
        public Color smallHiglightColor = Color.FromArgb(220, 220, 0);
        public Color mediumHiglightColor = Color.Orange;
        public Color bigHiglightColor = Color.Red;
        //Časovač kroků
        Timer t;



        //Inicializace formy a defaultního grafu
        public FormMain()
        {
            InitializeComponent();
            //Načtení defaultního grafu
            foreach (Point iterPoint in defaultVertexPositions)
            {
                vertexes.Add(new Vertex(iterPoint, vertexBaseColor));
            }
            for (int i = 0; i < vertexes.Count(); i++)
            {
                for (int k = vertexes.Count() - 1; k > i; k--)
                {
                    if (defaultMatrix[i, k] > 0)
                    {
                        Edge newEdge = new Edge(vertexes[i], vertexes[k], 0, edgeBaseColor, textBaseColor, defaultMatrix[i, k]);
                        vertexes[i].VertexEdges.Add(newEdge);
                        vertexes[k].VertexEdges.Add(newEdge);
                        edges.Add(newEdge);
                    }
                }
            }
            edgeValueInput.Enabled = false;
            algorithmSelection.SelectedIndex = 0;
            startingVertex = vertexes[0];
            pauseButton.Enabled = false;
            stepButton.Enabled = false;
            backstepButton.Enabled = false;
            RefreshCanvas();

            //Spousta objektů

            //Random r = new Random();
            //for (int i = 0; i < 128; i++)
            //{
            //    Vertex newVertex = new Vertex(new Point(r.Next(0, 960), r.Next(0, 540)), vertexBaseColor);
            //    foreach (Vertex v in vertexes)
            //    {
            //        if (r.Next(0, 11) < 4)
            //        {
            //            Edge newEdge = new Edge(newVertex, v, 0, edgeBaseColor, textBaseColor, r.Next(0, 100));
            //            newVertex.vertexEdges.Add(newEdge);
            //            v.vertexEdges.Add(newEdge);
            //            edges.Add(newEdge);
                        
            //        }
            //    }
            //    vertexes.Add(newVertex);
            //}
            //Console.WriteLine(edges.Count());
            //Refresh();


        }



        //Vykreslí všechny hrany a vrcholy z vertexes a edges
        private void Graph_Paint(object sender, PaintEventArgs e)
        {
            foreach (Edge edge in edges)
            {
                edge.DrawEdge(e.Graphics);
                edge.DrawText(e.Graphics);
            }

            foreach (Vertex vertex in vertexes)
            {
                vertex.DrawVertex(e.Graphics);
                vertex.DrawText(e.Graphics);
            }
        }



        //Řeší kliknutí na graf
        Vertex firstVertexOfNewEdge = null;
        Vertex movingVertex = null;
        private void Graph_MouseClick(object sender, MouseEventArgs e)
        {
            //Zvýrazní vrchol nebo hranu
            if (clickFunctionClick.SelectedIndex == 0)
            {
                bool found = false;
                foreach (Vertex vertex in vertexes)
                {
                    if (found == false && vertex.Clicked(e.Location))
                    {
                        vertex.Color = mediumHiglightColor;
                        found = true;
                    }
                    else
                    {
                        vertex.Color = vertexBaseColor;
                    }
                }
                foreach (Edge edge in edges)
                {
                    if (found == false && edge.Clicked(e.Location))
                    {
                        edge.Color = mediumHiglightColor;
                        found = true;
                    }
                    else
                    {
                        edge.Color = edgeBaseColor;
                    }
                }
            }

            //Přidá vrchol
            else if (clickFunctionVertex.SelectedIndex == 0)
            {
                vertexes.Add(new Vertex(e.Location, vertexBaseColor));
            }

            //Přesune vrchol
            else if (clickFunctionVertex.SelectedIndex == 1)
            {
                if (movingVertex == null)
                {
                    foreach (Vertex vertex in vertexes)
                    {
                        if (vertex.Clicked(e.Location))
                        {
                            vertex.Color = mediumHiglightColor;
                            movingVertex = vertex;
                            break;
                        }
                    }
                }
                else
                {
                    movingVertex.Position = e.Location;
                    movingVertex.Color = vertexBaseColor;
                    movingVertex = null;
                }
            }

            //Smaže vrchol
            else if (clickFunctionVertex.SelectedIndex == 2)
            {
                foreach (Vertex vertex in vertexes)
                {
                    if (vertex.Clicked(e.Location))
                    {
                        foreach (Edge edge in vertex.VertexEdges.ToArray())
                        {
                            edge.Start.VertexEdges.Remove(edge);
                            edge.End.VertexEdges.Remove(edge);
                            edges.Remove(edge);
                        }
                        vertexes.Remove(vertex);
                        if (startingVertex == vertex)
                        {
                            foreach (Vertex newStartingVertex in vertexes)
                            {
                                startingVertex = newStartingVertex;
                            }
                        }
                        break;
                    }
                }
            }

            //Přidá hranu
            else if (clickFunctionEdge.SelectedIndex == 0)
            {
                foreach (Vertex vertex in vertexes)
                {
                    if (vertex.Clicked(e.Location))
                    {
                        if (firstVertexOfNewEdge == null)
                        {
                            firstVertexOfNewEdge = vertex;
                            firstVertexOfNewEdge.Color = mediumHiglightColor;
                            break;
                        }
                        else if (vertex != firstVertexOfNewEdge)
                        {
                            Edge newEdge = new Edge(firstVertexOfNewEdge, vertex, 0, edgeBaseColor, textBaseColor, Int32.Parse(edgeValueInput.Text));
                            firstVertexOfNewEdge.VertexEdges.Add(newEdge);
                            vertex.VertexEdges.Add(newEdge);
                            edges.Add(newEdge);
                            firstVertexOfNewEdge.Color = vertexBaseColor;
                            firstVertexOfNewEdge = null;
                        }
                        break;
                    }
                }
            }

            //Změní hodnotu hrany
            else if (clickFunctionEdge.SelectedIndex == 1)
            {
                foreach (Edge edge in edges)
                {
                    if (edge.Clicked(e.Location))
                    {
                        int fn = Int32.Parse(edgeValueInput.Text);
                        edge.Value = fn;
                        edge.Text = fn + "";
                        break;
                    }
                }
                edgeValueInput.Focus();
                edgeValueInput.SelectAll();
            }

            //Smaže hranu
            else if (clickFunctionEdge.SelectedIndex == 2)
            {
                foreach (Edge edge in edges)
                {
                    if (edge.Clicked(e.Location))
                    {
                        edge.Start.VertexEdges.Remove(edge);
                        edge.End.VertexEdges.Remove(edge);
                        edges.Remove(edge);
                        break;
                    }
                }
            }

            //Určí směr hrany
            else if (clickFunctionEdge.SelectedIndex == 3)
            {
                foreach (Edge edge in edges)
                {
                    if (edge.Clicked(e.Location))
                    {
                        if ((Math.Pow(e.Location.X - edge.End.Position.X, 2) + Math.Pow(e.Location.Y - edge.End.Position.Y, 2)) < (Math.Pow(e.Location.X - edge.Start.Position.X, 2) + Math.Pow(e.Location.Y - edge.Start.Position.Y, 2)))
                        {
                            edge.End.VertexEdges.Remove(edge);
                            edge.Direction = 1;
                        }
                        else
                        {
                            edge.Start.VertexEdges.Remove(edge);
                            edge.Direction = -1;
                        }
                        break;
                    }
                }
            }

            //Vybere začáteční vrchol
            else if (clickFunctionStartingVertex.SelectedIndex == 0)
            {
                foreach (Vertex vertex in vertexes)
                {
                    if (vertex.Clicked(e.Location))
                    {
                        startingVertex.Color = vertexBaseColor;
                        startingVertex = vertex;
                        vertex.Color = mediumHiglightColor;
                        break;
                    }
                }
            }

            RefreshCanvas();
        }



        //Začne algoritmus a resetuje graf
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (startAlgButton.Text == "Start")
            {

                startAlgButton.Text = "Reset";
                clickFunctionClick.Enabled = false;
                clickFunctionVertex.Enabled = false;
                clickFunctionEdge.Enabled = false;
                edgeValueInput.Enabled = false;
                algorithmSelection.Enabled = false;
                clickFunctionStartingVertex.Enabled = false;
                startAlgButton.Enabled = false;
                settingsButton.Enabled = false;
                pauseButton.Enabled = true;

                foreach (Vertex v in vertexes)
                {
                    v.Value = int.MaxValue;
                    v.Text = "∞";
                    v.Color = vertexBaseColor;
                    v.Visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.Color = edgeBaseColor;
                }

                t = new Timer();
                t.Interval = waitTimeInput.Value;
                t.Tick += new EventHandler(StepWait);
                t.Enabled = true;

                if (algorithmSelection.SelectedIndex == 0)
                {
                    Dijkstra();
                }
                else if (algorithmSelection.SelectedIndex == 1)
                {
                    Jarnik();
                }
                else if (algorithmSelection.SelectedIndex == 2)
                {
                    Boruvka();
                }
            }
            else
            {
                foreach (Vertex v in vertexes)
                {
                    v.Value = int.MaxValue;
                    v.Text = "∞";
                    v.Color = vertexBaseColor;
                    v.Visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.Color = edgeBaseColor;
                }

                RefreshCanvas();
                startAlgButton.Text = "Start";
                clickFunctionClick.Enabled = true;
                clickFunctionVertex.Enabled = true;
                clickFunctionEdge.Enabled = true;
                edgeValueInput.Enabled = true;
                algorithmSelection.Enabled = true;
                clickFunctionStartingVertex.Enabled = true;
                settingsButton.Enabled = true;
                pauseButton.Enabled = false;
            }
        }



        //Dijkstrův algoritmus
        private void Dijkstra()
        {
            startingVertex.Value = 0;
            stepQueue.Enqueue(new Step(startingVertex, "0"));
            for (int count = 0; count < vertexes.Count(); count++)
            {
                Vertex current = null;
                foreach (Vertex v in vertexes)
                {
                    if (v.Visited == false && current == null)
                    {
                        current = v;
                    }
                    else if (v.Visited == false && v.Value < current.Value)
                    {
                        current = v;
                    }
                }
                current.Visited = true;
                stepQueue.Enqueue(new Step(current, mediumHiglightColor, current.Value + ""));
                foreach (Edge e in current.VertexEdges)
                {
                    if (e.Start.Equals(current) && e.End.Visited == false)
                    {
                        stepQueue.Enqueue(new Step(e, smallHiglightColor));
                        stepQueue.Enqueue(new Step(e.End, smallHiglightColor));
                        if (current.Value != int.MaxValue && e.End.Value > e.Value + current.Value)
                        {
                            e.End.Value = e.Value + current.Value;
                            stepQueue.Enqueue(new Step(e.End, e.End.Value + ""));
                        }
                        stepQueue.Enqueue(new Step(e.End, vertexBaseColor, e.End.Value + ""));
                        stepQueue.Enqueue(new Step(e, edgeBaseColor));
                    }
                    else if (e.End.Equals(current) && e.Start.Visited == false)
                    {
                        stepQueue.Enqueue(new Step(e, smallHiglightColor));
                        stepQueue.Enqueue(new Step(e.Start, smallHiglightColor));
                        if (current.Value != int.MaxValue && e.Start.Value > e.Value + current.Value)
                        {
                            e.Start.Value = e.Value + current.Value;
                            stepQueue.Enqueue(new Step(e.Start, e.Start.Value + ""));
                        }
                        stepQueue.Enqueue(new Step(e.Start, vertexBaseColor, e.Start.Value +""));
                        stepQueue.Enqueue(new Step(e, edgeBaseColor));
                    }
                }
                stepQueue.Enqueue(new Step(current, bigHiglightColor));
            }
        }



        //Jarníkův algoritmus
        private void Jarnik()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge e in startingVertex.VertexEdges)
            {
                edgeHeap.Add(e);
            }
            startingVertex.Visited = true;
            while (!(edgeHeap.IsEmpty()))
            {
                Edge currentEdge = edgeHeap.Pop();
                if (currentEdge.End.Visited == false)
                {
                    stepQueue.Enqueue(new Step(currentEdge, bigHiglightColor, currentEdge.Value + ""));
                    currentEdge.End.Visited = true;
                    foreach (Edge e in currentEdge.End.VertexEdges)
                    {
                        if (e.Start.Visited == false || e.End.Visited == false)
                        {
                            edgeHeap.Add(e);
                        }
                    }
                }
                else if (currentEdge.Start.Visited == false)
                {
                    stepQueue.Enqueue(new Step(currentEdge, bigHiglightColor, currentEdge.Value + ""));
                    currentEdge.Start.Visited = true;
                    foreach (Edge e in currentEdge.Start.VertexEdges)
                    {
                        if (e.Start.Visited == false || e.End.Visited == false)
                        {
                            edgeHeap.Add(e);
                        }
                    }
                }
                else
                {
                    stepQueue.Enqueue(new Step(currentEdge, smallHiglightColor, currentEdge.Value + ""));
                }
            }
        }



        //Borůvkův algoritmus WIP
        private void Boruvka()
        {
            
        }



        //Refresh
        private void RefreshCanvas()
        {
            graph.Refresh();
        }

        //Recolor
        public void Recolor()
        {
            foreach(Edge e in edges)
            {
                e.Color = edgeBaseColor;
                e.TextColor = textBaseColor;
            }
            foreach(Vertex v in vertexes)
            {
                v.Color = vertexBaseColor;
            }
            RefreshCanvas();
        }

        //Krokovač algoritmů
        private void StepWait(object sender, EventArgs e)
        {
            if (stepQueue.Count() > 0)
            {
                (stepQueue.Dequeue()).Complete();
                RefreshCanvas();
                t.Interval = waitTimeInput.Value;
            }
            else
            {
                t.Enabled = false;
                startAlgButton.Enabled = true;
            }
        }

        //Řeší focusy a výběry ve formě
        private void ClickFunctionChange(object sender, EventArgs e)
        {
            firstVertexOfNewEdge = null;
            movingVertex = null;
            foreach (Edge edge in edges)
            {
                edge.Color = edgeBaseColor;
                edge.TextColor = textBaseColor;
            }
            foreach (Vertex vertex in vertexes)
            {
                vertex.Color = vertexBaseColor;
            }
            if (clickFunctionStartingVertex.SelectedIndex == 0)
            {
                startingVertex.Color = mediumHiglightColor;
            }
            if (clickFunctionEdge.SelectedIndex == 0 || clickFunctionEdge.SelectedIndex == 1)
            {
                edgeValueInput.Enabled = true;
                edgeValueInput.Focus();
                edgeValueInput.SelectAll();
            }
            else
            {
                edgeValueInput.Enabled = false;
            }
            graph.Refresh();
        }

        private void ClickFunction_Leave(object sender, EventArgs e)
        {
            if (edgeValueInput.Focused == false)
            {
                ((ListBox)sender).ClearSelected();
            }
        }

        private void EdgeValueInput_Leave(object sender, EventArgs e)
        {
            clickFunctionEdge.ClearSelected();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormSettings fs = new FormSettings(this);
            fs.ShowDialog();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if(((Button)sender).Text == "Pause")
            {
                t.Enabled = false;
                ((Button)sender).Text = "Go";
                stepButton.Enabled = true;
                backstepButton.Enabled = true;
            }
            else
            {
                t.Enabled = true;
                ((Button)sender).Text = "Pause";
            }
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            if (stepQueue.Count() > 0)
            {
                (stepQueue.Dequeue()).Complete();
                RefreshCanvas();
            }
            else
            {
                startAlgButton.Enabled = true;
            }
        }
    }
}
