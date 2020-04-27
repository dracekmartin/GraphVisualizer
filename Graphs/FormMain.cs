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
        Vertex startingVertex = null;
        Queue<Step> stepQueue = new Queue<Step>();
        //Globální proměnné barev
        Color vertexBaseColor = Color.CadetBlue;
        Color edgeBaseColor = Color.LightGray;
        Color textBaseColor = Color.Black;
        Color smallHiglightColor = Color.FromArgb(220, 220, 0);
        Color mediumHiglightColor = Color.Orange;
        Color bigHiglightColor = Color.Red;
        


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
                        Edge newEdge = new Edge(vertexes[i], vertexes[k], 0, edgeBaseColor, textBaseColor, defaultMatrix[i, k], defaultMatrix[i, k]);
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
                        int fn = Int32.Parse(edgeValueInput.Text);
                        edge.value = fn;
                        edge.text = fn;
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
                    if (vertex.Clicked(e.Location))
                    {
                        startingVertex.color = vertexBaseColor;
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

                startAlgButton.Text = "Reset";
                clickFunctionClick.Enabled = false;
                clickFunctionVertex.Enabled = false;
                clickFunctionEdge.Enabled = false;
                edgeValueInput.Enabled = false;
                algorithmSelection.Enabled = false;
                clickFunctionStartingVertex.Enabled = false;

                foreach (Vertex v in vertexes)
                {
                    v.value = int.MaxValue;
                    v.text = int.MaxValue;
                    v.color = vertexBaseColor;
                    v.visited = false;
                    v.subtreeParent = v;
                }
                foreach (Edge edge in edges)
                {
                    edge.color = edgeBaseColor;
                }

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
                    v.value = int.MaxValue;
                    v.text = int.MaxValue;
                    v.color = vertexBaseColor;
                    v.visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.color = edgeBaseColor;
                }

                refreshCanvas();
                startAlgButton.Text = "Start";
                clickFunctionClick.Enabled = true;
                clickFunctionVertex.Enabled = true;
                clickFunctionEdge.Enabled = true;
                edgeValueInput.Enabled = true;
                algorithmSelection.Enabled = true;
                clickFunctionStartingVertex.Enabled = true;
            }
        }



        //Dijkstrův algoritmus
        private void Dijkstra()
        {
            startingVertex.value = 0;
            stepQueue.Enqueue(new Step(startingVertex, startingVertex.color, 0));
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
                stepQueue.Enqueue(new Step(current, mediumHiglightColor, current.value));
                foreach (Edge e in current.vertexEdges)
                {
                    if (e.start.Equals(current) && e.end.visited == false)
                    {
                        stepQueue.Enqueue(new Step(e, smallHiglightColor, e.value));
                        stepQueue.Enqueue(new Step(e.end, smallHiglightColor, e.end.value));
                        if (current.value != int.MaxValue && e.end.value > e.value + current.value)
                        {
                            e.end.value = e.value + current.value;
                            stepQueue.Enqueue(new Step(e.end, e.end.color, e.end.value));
                        }
                        stepQueue.Enqueue(new Step(e, edgeBaseColor, e.value));
                        stepQueue.Enqueue(new Step(e.end, vertexBaseColor, e.end.value));
                    }
                    else if (e.end.Equals(current) && e.start.visited == false)
                    {
                        stepQueue.Enqueue(new Step(e, smallHiglightColor, e.value));
                        stepQueue.Enqueue(new Step(e.start, smallHiglightColor, e.start.value));
                        if (current.value != int.MaxValue && e.start.value > e.value + current.value)
                        {
                            e.start.value = e.value + current.value;
                            stepQueue.Enqueue(new Step(e.start, e.start.color, e.start.value));
                        }
                        stepQueue.Enqueue(new Step(e, edgeBaseColor, e.value));
                        stepQueue.Enqueue(new Step(e.start, vertexBaseColor, e.start.value));
                    }
                }
                stepQueue.Enqueue(new Step(current, bigHiglightColor, current.value));
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
                    stepQueue.Enqueue(new Step(currentEdge, bigHiglightColor, currentEdge.value));
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
                    stepQueue.Enqueue(new Step(currentEdge, bigHiglightColor, currentEdge.value));
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
                    stepQueue.Enqueue(new Step(currentEdge, smallHiglightColor, currentEdge.value));
                }
            }
        }



        private void Boruvka()
        {
            bool changed = false;
            while (!changed)
            {
                changed = true;
                foreach (Edge e in edges)
                {
                    if (e.start.getSubtreeParent() != e.end.getSubtreeParent())
                    {
                        changed = false;
                        if (e.start.getSubtreeParent().smallestSubtreeEdge == null || e.value < e.start.getSubtreeParent().smallestSubtreeEdge.value)
                        {
                            e.start.getSubtreeParent().smallestSubtreeEdge = e;
                        }
                        if (e.end.getSubtreeParent().smallestSubtreeEdge == null || e.value < e.end.getSubtreeParent().smallestSubtreeEdge.value)
                        {
                            e.end.getSubtreeParent().smallestSubtreeEdge = e;
                        }
                    }
                }
                foreach(Vertex v in vertexes)
                {
                    
                    if (v.getSubtreeParent().smallestSubtreeEdge != null)
                    {
                        stepQueue.Enqueue(new Step(v.getSubtreeParent().smallestSubtreeEdge.start, bigHiglightColor, v.getSubtreeParent().smallestSubtreeEdge.start.text));
                        stepQueue.Enqueue(new Step(v.getSubtreeParent().smallestSubtreeEdge.end, bigHiglightColor, v.getSubtreeParent().smallestSubtreeEdge.end.text));
                        stepQueue.Enqueue(new Step(v.getSubtreeParent().smallestSubtreeEdge, bigHiglightColor, v.getSubtreeParent().smallestSubtreeEdge.text));
                        if (v.getSubtreeParent().smallestSubtreeEdge.start.getSubtreeParent().Equals(v.getSubtreeParent()))
                        {
                            v.getSubtreeParent().smallestSubtreeEdge.end.getSubtreeParent().subtreeParent = v.getSubtreeParent();
                        }
                        else
                        {
                            v.getSubtreeParent().smallestSubtreeEdge.start.getSubtreeParent().subtreeParent = v.getSubtreeParent();
                        }
                        v.getSubtreeParent().smallestSubtreeEdge = null;
                    }
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
                startAlgButton.Enabled = true;
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
