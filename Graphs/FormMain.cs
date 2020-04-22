using System;
using System.Collections.Generic;
using System.Data;
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
        Color vertexBaseColor = Color.CadetBlue;
        Color edgeBaseColor = Color.LightGray;
        Color textBaseColor = Color.Black;
        Color smallHiglightColor = Color.FromArgb(220, 220, 0);
        Color mediumHiglightColor = Color.Orange;
        Color bigHiglightColor = Color.Red;
        Vertex startingVertex = null;
        Queue<ColorChangingStep> stepQueue = new Queue<ColorChangingStep>();



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
                        vertexes[i].vertexEdges.Add(newEdge);
                        vertexes[k].vertexEdges.Add(newEdge);
                        edges.Add(newEdge);
                    }
                }
            }
            edgeValueInput.Enabled = false;
            algorithmSelection.SelectedIndex = 0;
            startingVertex = vertexes[0];
            refreshCanvas();

            //Spousta objektů

            Random r = new Random();
            for (int i = 0; i < 32; i++)
            {
                Vertex newVertex = new Vertex(new Point(r.Next(0, 960), r.Next(0, 540)), vertexBaseColor);
                foreach (Vertex v in vertexes)
                {
                    if (r.Next(0, 11) < 4)
                    {
                        Edge newEdge = new Edge(newVertex, v, 0, edgeBaseColor, textBaseColor, r.Next(0, 100));
                        newVertex.vertexEdges.Add(newEdge);
                        v.vertexEdges.Add(newEdge);
                        edges.Add(newEdge);
                        
                    }
                }
                vertexes.Add(newVertex);
            }
            Console.WriteLine(edges.Count());
            Refresh();


        }

        //Vykreslí všechny hrany a vrcholy z vertexes a edges
        private void graph_Paint(object sender, PaintEventArgs e)
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
        private void graph_MouseClick(object sender, MouseEventArgs e)
        {
            //Zvýrazní vrchol nebo hranu
            if (clickFunctionClick.SelectedIndex == 0)
            {
                bool found = false;
                foreach (Vertex vertex in vertexes)
                {
                    if (found == false && vertex.Clicked(e.Location))
                    {
                        vertex.color = mediumHiglightColor;
                        found = true;
                    }
                    else
                    {
                        vertex.color = vertexBaseColor;
                    }
                }
                foreach (Edge edge in edges)
                {
                    if (found == false && edge.Clicked(e.Location))
                    {
                        edge.color = mediumHiglightColor;
                        found = true;
                    }
                    else
                    {
                        edge.color = edgeBaseColor;
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
                            vertex.color = mediumHiglightColor;
                            movingVertex = vertex;
                            break;
                        }
                    }
                }
                else
                {
                    movingVertex.position = e.Location;
                    movingVertex.color = vertexBaseColor;
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
                        foreach (Edge edge in vertex.vertexEdges.ToArray())
                        {
                            edge.start.vertexEdges.Remove(edge);
                            edge.end.vertexEdges.Remove(edge);
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
                            firstVertexOfNewEdge.color = mediumHiglightColor;
                            break;
                        }
                        else if (vertex != firstVertexOfNewEdge)
                        {
                            Edge newEdge = new Edge(firstVertexOfNewEdge, vertex, 0, edgeBaseColor, textBaseColor, Int32.Parse(edgeValueInput.Text));
                            firstVertexOfNewEdge.vertexEdges.Add(newEdge);
                            vertex.vertexEdges.Add(newEdge);
                            edges.Add(newEdge);
                            firstVertexOfNewEdge.color = vertexBaseColor;
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
                        edge.value = Int32.Parse(edgeValueInput.Text);
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
                        edge.start.vertexEdges.Remove(edge);
                        edge.end.vertexEdges.Remove(edge);
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
                        if ((Math.Pow(e.Location.X - edge.end.position.X, 2) + Math.Pow(e.Location.Y - edge.end.position.Y, 2)) < (Math.Pow(e.Location.X - edge.start.position.X, 2) + Math.Pow(e.Location.Y - edge.start.position.Y, 2)))
                        {
                            edge.end.vertexEdges.Remove(edge);
                            edge.direction = 1;
                        }
                        else
                        {
                            edge.start.vertexEdges.Remove(edge);
                            edge.direction = -1;
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
                    vertex.color = vertexBaseColor;
                }
                foreach (Vertex vertex in vertexes)
                {
                    if (vertex.Clicked(e.Location))
                    {
                        startingVertex = vertex;
                        vertex.color = mediumHiglightColor;
                        break;
                    }
                }
            }
            refreshCanvas();
        }



        //Začne algoritmus a resetuje graf
        private void startButton_Click(object sender, EventArgs e)
        {
            if (startAlgButton.Text == "Start")
            {
                foreach (Vertex v in vertexes)
                {
                    v.value = int.MaxValue;
                    v.color = vertexBaseColor;
                    v.visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.color = edgeBaseColor;
                }
                startAlgButton.Text = "Reset";
                startAlgButton.Enabled = false;
                startAlgButton.Refresh();
                Timer t = new Timer();
                t.Interval = waitTimeInput.Value;
                t.Tick += new EventHandler(stepWait);
                t.Enabled = true;
                if (algorithmSelection.SelectedIndex == 0)
                {
                    Dijkstra();
                }
                else if (algorithmSelection.SelectedIndex == 1)
                {
                    Kruskal();
                }
                else if (algorithmSelection.SelectedIndex == 2)
                {
                    Jarnik();
                }
                startAlgButton.Enabled = true;
                startAlgButton.Refresh();
            }
            else
            {
                foreach (Vertex v in vertexes)
                {
                    v.value = int.MaxValue;
                    v.color = vertexBaseColor;
                    v.visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.color = edgeBaseColor;
                }
                refreshCanvas();
                startAlgButton.Text = "Start";
                startAlgButton.Refresh();

            }
        }



        //Dijkstrův algoritmus
        private void Dijkstra()
        {
            startingVertex.value = 0;
            for (int count = 0; count < vertexes.Count(); count++)
            {
                Vertex current = null;
                foreach (Vertex v in vertexes)
                {
                    if (v.visited == false && current == null)
                    {
                        current = v;
                    }
                    else if (v.visited == false && v.value < current.value)
                    {
                        current = v;
                    }
                }
                current.visited = true;
                //
                current.color = mediumHiglightColor;
                refreshCanvas();
                //stepWait();
                //
                foreach (Edge e in current.vertexEdges)
                {
                    if (e.start.Equals(current) && e.end.color == vertexBaseColor)
                    {
                        //
                        e.end.color = smallHiglightColor;
                        e.color = smallHiglightColor;
                        refreshCanvas();
                        //stepWait();
                        //
                        if (current.value != int.MaxValue && e.end.value > e.value + current.value)
                        {
                            e.end.value = e.value + current.value;
                        }
                        //
                        e.end.color = vertexBaseColor;
                        e.color = edgeBaseColor;
                        refreshCanvas();
                        //stepWait();
                        //
                    }
                    else if (e.end.Equals(current) && e.start.color == vertexBaseColor)
                    {
                        //
                        e.start.color = smallHiglightColor;
                        e.color = smallHiglightColor;
                        refreshCanvas();
                        //stepWait();
                        //
                        if (current.value != int.MaxValue && e.start.value > e.value + current.value)
                        {
                            e.start.value = e.value + current.value;
                        }
                        //
                        e.start.color = vertexBaseColor;
                        e.color = edgeBaseColor;
                        refreshCanvas();
                        //stepWait();
                        //
                    }
                }
                //
                current.color = bigHiglightColor;
                refreshCanvas();
                //stepWait();
                //
            }
        }

        //ŠPATNĚ
        private void Kruskal()
        {
            List<Edge> sortedEdges = edges.OrderBy(v => v.value).ToList();
            foreach (Edge e in sortedEdges)
            {
                if (e.start.visited == false || e.end.visited == false)
                {
                    e.start.visited = true;
                    e.end.visited = true;
                    stepQueue.Enqueue(new ColorChangingStep(e, bigHiglightColor));
                }
                else
                {
                    stepQueue.Enqueue(new ColorChangingStep(e, smallHiglightColor));
                }

            }
        }

        //Jarníkův algoritmus
        private void Jarnik()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge e in startingVertex.vertexEdges)
            {
                edgeHeap.Add(e);
            }
            startingVertex.visited = true;
            while (!(edgeHeap.IsEmpty()))
            {
                Edge currentEdge = edgeHeap.Pop();
                if (currentEdge.end.visited == false)
                {
                    stepQueue.Enqueue(new ColorChangingStep(currentEdge, bigHiglightColor));
                    currentEdge.end.visited = true;
                    foreach (Edge e in currentEdge.end.vertexEdges)
                    {
                        if (e.start.visited == false || e.end.visited == false)
                        {
                            edgeHeap.Add(e);
                        }
                    }
                }
                else if (currentEdge.start.visited == false)
                {
                    stepQueue.Enqueue(new ColorChangingStep(currentEdge, bigHiglightColor));
                    currentEdge.start.visited = true;
                    foreach (Edge e in currentEdge.start.vertexEdges)
                    {
                        if (e.start.visited == false || e.end.visited == false)
                        {
                            edgeHeap.Add(e);
                        }
                    }
                }
                else
                {
                    stepQueue.Enqueue(new ColorChangingStep(currentEdge, smallHiglightColor));
                }
            }
        }

        //Refresh
        private void refreshCanvas()
        {
            graph.Refresh();
        }

        //Krokovač algoritmů
        private void stepWait(object sender, EventArgs e)
        {
            if (stepQueue.Count() > 0)
            {
                (stepQueue.Dequeue()).Complete();
                refreshCanvas();
                ((Timer)sender).Interval = waitTimeInput.Value;
            }
            else
            {
                ((Timer)sender).Enabled = false;
            }
        }

        //Řeší focusy a výběry ve formě
        private void clickFunctionChange(object sender, EventArgs e)
        {
            firstVertexOfNewEdge = null;
            movingVertex = null;
            foreach (Edge edge in edges)
            {
                edge.color = edgeBaseColor;
                edge.textColor = textBaseColor;
            }
            foreach (Vertex vertex in vertexes)
            {
                vertex.color = vertexBaseColor;
            }
            if (clickFunctionStartingVertex.SelectedIndex == 0)
            {
                startingVertex.color = mediumHiglightColor;
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

        private void clickFunction_Leave(object sender, EventArgs e)
        {
            if (edgeValueInput.Focused == false)
            {
                ((ListBox)sender).ClearSelected();
            }
        }

        private void edgeValueInput_Leave(object sender, EventArgs e)
        {
            clickFunctionEdge.ClearSelected();
        }
    }
}
