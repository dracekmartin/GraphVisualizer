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
        readonly Point[] defaultNodePositions = new Point[]{
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
            {0,17,3,5,0,0,0,0},
            {17,0,0,8,0,0,4,0},
            {3,0,0,0,11,0,9,0},
            {5,8,0,0,0,0,0,7},
            {0,0,11,0,0,0,2,0},
            {0,0,0,0,0,0,6,0},
            {0,4,9,0,2,9,0,23},
            {0,0,0,7,0,0,23,0}
        };
        //Globální proměnné
        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();
        Step currentStep = null;
        Node startingNode = null;
        Node sinkNode = null;
        public bool euclidean = false;
        public bool directed = false;
        public int canvasStartX = 0;
        public int canvasStartY = 0;
        //Globální proměnné barev
        public Color nodeBaseColor = Color.CadetBlue;
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
            OnStart();
        }



        private void OnStart()
        {
            //Načtení defaultního grafu
            foreach (Point iterPoint in defaultNodePositions)
            {
                nodes.Add(new Node(this, iterPoint, nodeBaseColor));
            }
            for (int i = 0; i < nodes.Count(); i++)
            {
                for (int k = nodes.Count() - 1; k > i; k--)
                {
                    if (defaultMatrix[i, k] > 0)
                    {
                        Edge newEdge = new Edge(this, nodes[i], nodes[k], edgeBaseColor, textBaseColor, defaultMatrix[i, k]);
                        nodes[i].NodeEdges.Add(newEdge);
                        nodes[k].NodeEdges.Add(newEdge);
                        edges.Add(newEdge);
                    }
                }
            }
            edgeValueInput.Enabled = false;
            algorithmSelection.SelectedIndex = 0;
            startingNode = nodes[0];
            sinkNode = nodes[7];
            pauseButton.Enabled = false;
            stepButton.Enabled = false;
            backstepButton.Enabled = false;
            RefreshCanvas();
        }

        //Vykreslí všechny hrany a vrcholy z nodes a edges
        private void Graph_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Edge edge in edges)
            {
                edge.DrawEdge(e.Graphics);
                edge.DrawText(e.Graphics);
            }

            foreach (Node nodes in nodes)
            {
                nodes.DrawNode(e.Graphics);
                nodes.DrawText(e.Graphics);
            }
        }



        //Řeší kliknutí na graf
        Node firstNodeOfNewEdge = null;
        Node movingNode = null;
        private void Graph_MouseClick(object sender, MouseEventArgs e)
        {
            //Zvýrazní vrchol nebo hranu
            if (clickFunctionClick.SelectedIndex == 0)
            {
                bool found = false;
                foreach (Node node in nodes)
                {
                    if (found == false && node.Clicked(e.Location))
                    {
                        node.Color = mediumHiglightColor;
                        found = true;
                    }
                    else
                    {
                        node.Color = nodeBaseColor;
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

            //Přidá nebo smaže vrchol 
            else if (clickFunctionNode.SelectedIndex == 0)
            {
                if(e.Button == MouseButtons.Left)
                {
                    Node newNode = new Node(this, new Point(e.Location.X - canvasStartX, e.Location.Y - canvasStartY), nodeBaseColor);
                    nodes.Add(newNode);
                }
                else if(e.Button == MouseButtons.Right)
                {
                    foreach (Node node in nodes)
                    {
                        if (node.Clicked(e.Location))
                        {
                            foreach (Edge edge in node.NodeEdges.ToArray())
                            {
                                edge.Start.NodeEdges.Remove(edge);
                                edge.End.NodeEdges.Remove(edge);
                                edges.Remove(edge);
                            }
                            nodes.Remove(node);
                            if (startingNode == node)
                            {
                                startingNode = null;
                            }
                            else if (sinkNode == node)
                            {
                                sinkNode = null;
                            }
                            break;
                        }
                    }
                }
            }

            //Přesune vrchol
            else if (clickFunctionNode.SelectedIndex == 1)
            {
                if (movingNode == null)
                {
                    foreach (Node node in nodes)
                    {
                        if (node.Clicked(e.Location))
                        {
                            node.Color = mediumHiglightColor;
                            movingNode = node;
                            break;
                        }
                    }
                }
                else
                {
                    movingNode.Position.X = e.Location.X - canvasStartX;
                    movingNode.Position.Y = e.Location.Y - canvasStartY;
                    movingNode.Color = nodeBaseColor;
                    if (euclidean)
                    {
                        foreach (Edge edge in movingNode.NodeEdges)
                        {
                            edge.Text = edge.Value + "";
                        }
                    }
                    movingNode = null;
                }
            }

            //Přidá nebo smaže hranu
            else if (clickFunctionEdge.SelectedIndex == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (Node node in nodes)
                    {
                        if (node.Clicked(e.Location))
                        {
                            if (firstNodeOfNewEdge == null)
                            {
                                firstNodeOfNewEdge = node;
                                firstNodeOfNewEdge.Color = mediumHiglightColor;
                                break;
                            }
                            else if (node != firstNodeOfNewEdge)
                            {
                                Edge newEdge = new Edge(this, firstNodeOfNewEdge, node, edgeBaseColor, textBaseColor, Int32.Parse(edgeValueInput.Text));
                                firstNodeOfNewEdge.NodeEdges.Add(newEdge);
                                node.NodeEdges.Add(newEdge);
                                edges.Add(newEdge);
                                firstNodeOfNewEdge.Color = nodeBaseColor;
                                firstNodeOfNewEdge = null;
                            }
                            break;
                        }
                    }
                    edgeValueInput.Focus();
                    edgeValueInput.SelectAll();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    foreach (Edge edge in edges)
                    {
                        if (edge.Clicked(e.Location))
                        {
                            edge.Start.NodeEdges.Remove(edge);
                            edge.End.NodeEdges.Remove(edge);
                            edges.Remove(edge);
                            break;
                        }
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

            //Určí směr hrany
            else if (clickFunctionEdge.SelectedIndex == 2)
            {
                foreach (Edge edge in edges)
                {
                    if (edge.Clicked(e.Location))
                    {
                        if ((Math.Pow(e.Location.X - edge.End.Position.X, 2) + Math.Pow(e.Location.Y - edge.End.Position.Y, 2)) > (Math.Pow(e.Location.X - edge.Start.Position.X, 2) + Math.Pow(e.Location.Y - edge.Start.Position.Y, 2)))
                        {
                            Node fn = edge.Start;
                            edge.Start = edge.End;
                            edge.End = fn;
                        }
                        break;
                    }
                }
            }

            //Vybere začáteční vrchol
            else if (clickFunctionStartingNode.SelectedIndex == 0)
            {
                foreach (Node node in nodes)
                {
                    if (node.Clicked(e.Location) && node != sinkNode)
                    {
                        startingNode.Color = nodeBaseColor;
                        startingNode = node;
                        node.Color = mediumHiglightColor;
                        break;
                    }
                }
            }

            //Vybere stok
            else if (clickFunctionStartingNode.SelectedIndex == 1)
            {
                foreach (Node node in nodes)
                {
                    if (node.Clicked(e.Location) && node != startingNode)
                    {
                        sinkNode.Color = nodeBaseColor;
                        sinkNode = node;
                        node.Color = bigHiglightColor;
                        break;
                    }
                }
            }

            RefreshCanvas();
        }

        Point dragStart;
        Point dragEnd;
        private void graph_MouseDown(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                dragStart = e.Location;
                dragEnd = e.Location;
            }
        }

        private void graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                dragEnd = e.Location;
            }
        }

        private void graph_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                canvasStartX += dragEnd.X - dragStart.X;
                canvasStartY += dragEnd.Y - dragStart.Y;
                RefreshCanvas();
            }
        }


        //Začne algoritmus a resetuje graf
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (startAlgButton.Text == "Start")
            {

                startAlgButton.Text = "Reset";
                clickFunctionClick.Enabled = false;
                clickFunctionNode.Enabled = false;
                clickFunctionEdge.Enabled = false;
                edgeValueInput.Enabled = false;
                algorithmSelection.Enabled = false;
                clickFunctionStartingNode.Enabled = false;
                settingsButton.Enabled = false;
                pauseButton.Enabled = true;

                foreach (Node node in nodes)
                {
                    node.Value = int.MaxValue;
                    node.Text = "∞";
                    node.Color = nodeBaseColor;
                    node.Visited = false;
                }
                foreach (Edge edge in edges)
                {
                    edge.Color = edgeBaseColor;
                }

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
                else if (algorithmSelection.SelectedIndex == 3)
                {
                    Kruskal();
                }
                else if (algorithmSelection.SelectedIndex == 4)
                {
                    EdmondsKarp();
                }
                else if(algorithmSelection.SelectedIndex == 5)
                {
                    Dinic();
                }

                while (currentStep.StepBefore != null)
                {
                    currentStep = currentStep.StepBefore;
                }

                t = new Timer();
                t.Interval = waitTimeInput.Value;
                t.Tick += new EventHandler(StepWait);

                pauseButton.Text = "Go";
                stepButton.Enabled = true;
                backstepButton.Enabled = true;
            }
            else
            {
                t.Enabled = false;
                currentStep = null;
                foreach (Node node in nodes)
                {
                    node.Value = int.MaxValue;
                    node.Text = "∞";
                    node.Color = nodeBaseColor;
                    node.Visited = false;
                    node.PartOfSubset = node;
                    node.ImportantEdge = null;
                }
                foreach (Edge edge in edges)
                {
                    edge.Text = edge.Value + "";
                    edge.Color = edgeBaseColor;
                }

                RefreshCanvas();
                startAlgButton.Text = "Start";
                pauseButton.Text = "Pause";
                clickFunctionClick.Enabled = true;
                clickFunctionNode.Enabled = true;
                clickFunctionEdge.Enabled = true;
                edgeValueInput.Enabled = true;
                algorithmSelection.Enabled = true;
                clickFunctionStartingNode.Enabled = true;
                settingsButton.Enabled = true;
                pauseButton.Enabled = false;
            }
        }



        //Dijkstrův algoritmus
        private void Dijkstra()
        {
            startingNode.Value = 0;
            NewStep(new Step(startingNode, false, "0"));
            for (int count = 0; count < nodes.Count(); count++)
            {
                Node current = null;
                foreach (Node node in nodes)
                {
                    if (node.Visited == false && current == null)
                    {
                        current = node;
                    }
                    else if (node.Visited == false && node.Value < current.Value)
                    {
                        current = node;
                    }
                }
                current.Visited = true;
                NewStep(new Step(current, false, mediumHiglightColor, current.Value + ""));
                foreach (Edge edge in current.NodeEdges)
                {
                    if (edge.Start.Equals(current) && edge.End.Visited == false)
                    {
                        NewStep(new Step(edge, false, smallHiglightColor));
                        NewStep(new Step(edge.End, false, smallHiglightColor));
                        if (current.Value != int.MaxValue && edge.End.Value > edge.Value + current.Value)
                        {
                            edge.End.Value = edge.Value + current.Value;
                            NewStep(new Step(edge.End, false, edge.End.Value + ""));
                        }
                        NewStep(new Step(edge.End, false, nodeBaseColor, edge.End.Value + ""));
                        NewStep(new Step(edge, false, edgeBaseColor));
                    }
                    else if (edge.End.Equals(current) && edge.Start.Visited == false)
                    {
                        NewStep(new Step(edge, false, smallHiglightColor));
                        NewStep(new Step(edge.Start, false, smallHiglightColor));
                        if (current.Value != int.MaxValue && edge.Start.Value > edge.Value + current.Value)
                        {
                            edge.Start.Value = edge.Value + current.Value;
                            NewStep(new Step(edge.Start, false, edge.Start.Value + ""));
                        }
                        NewStep(new Step(edge.Start, false, nodeBaseColor, edge.Start.Value +""));
                        NewStep(new Step(edge, false, edgeBaseColor));
                    }
                }
                NewStep(new Step(current, false, bigHiglightColor));
            }
        }



        //Jarníkův algoritmus MST
        private void Jarnik()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge edge in startingNode.NodeEdges)
            {
                edgeHeap.Add(edge);
            }
            startingNode.Visited = true;
            while (!(edgeHeap.IsEmpty()))
            {
                Edge currentEdge = edgeHeap.Pop();
                if (currentEdge.End.Visited == false)
                {
                    NewStep(new Step(currentEdge, false, bigHiglightColor, currentEdge.Value + ""));
                    currentEdge.End.Visited = true;
                    foreach (Edge edge in currentEdge.End.NodeEdges)
                    {
                        if (edge.Start.Visited == false || edge.End.Visited == false)
                        {
                            edgeHeap.Add(edge);
                        }
                    }
                }
                else if (currentEdge.Start.Visited == false)
                {
                    NewStep(new Step(currentEdge, false, bigHiglightColor, currentEdge.Value + ""));
                    currentEdge.Start.Visited = true;
                    foreach (Edge edge in currentEdge.Start.NodeEdges)
                    {
                        if (edge.Start.Visited == false || edge.End.Visited == false)
                        {
                            edgeHeap.Add(edge);
                        }
                    }
                }
                else
                {
                    NewStep(new Step(currentEdge, false, smallHiglightColor, currentEdge.Value + ""));
                }
            }
        }



        //Borůvkův algoritmus MST
        private void Boruvka()
        {
            int treeCount = nodes.Count();
            while(treeCount > 1)
            {
                foreach(Edge edge in edges)
                {
                    if (!(edge.Start.PartOfSubset.Equals(edge.End.PartOfSubset)))
                    {
                        if (edge.Start.PartOfSubset.ImportantEdge == null || edge.Value < edge.Start.PartOfSubset.ImportantEdge.Value)
                        {
                            edge.Start.PartOfSubset.ImportantEdge = edge;
                        }
                        if (edge.End.PartOfSubset.ImportantEdge == null || edge.Value < edge.End.PartOfSubset.ImportantEdge.Value)
                        {
                            edge.End.PartOfSubset.ImportantEdge = edge;
                        }
                    }
                }
                foreach (Node node in nodes)
                {
                    if (node.ImportantEdge != null)
                    {
                        Console.WriteLine(node.ImportantEdge.Value);
                    }
                }
                Console.WriteLine("");
                foreach (Node node in nodes)
                {
                    if (node.ImportantEdge != null)
                    {
                        if (!(node.ImportantEdge.Start.PartOfSubset.Equals(node.ImportantEdge.End.PartOfSubset)))
                        {
                            NewStep(new Step(node.ImportantEdge, false, mediumHiglightColor));
                            if (node.ImportantEdge.Start.PartOfSubset.Equals(node))
                            {
                                node.ImportantEdge.Start.PartOfSubset.PartOfSubset = node.ImportantEdge.End.PartOfSubset;
                            }
                            else
                            {
                                node.ImportantEdge.End.PartOfSubset.PartOfSubset = node.ImportantEdge.Start.PartOfSubset;
                            }
                            treeCount--;
                        }
                        node.ImportantEdge = null;
                    }
                }
            }
            foreach (Node node in nodes)
            {
                node.PartOfSubset = node;
            }
        }



        //Kruskalův algoritmus MST
        private void Kruskal()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge edge in edges)
            {
                edgeHeap.Add(edge);
            }
            int treeCount = nodes.Count();
            while(treeCount > 1)
            {
                Edge currentEdge = edgeHeap.Pop();
                if (!(currentEdge.Start.PartOfSubset.Equals(currentEdge.End.PartOfSubset)))
                {
                    currentEdge.Start.PartOfSubset.PartOfSubset = currentEdge.End.PartOfSubset;
                    NewStep(new Step(currentEdge, false, mediumHiglightColor));
                    treeCount--;
                }
            }
        }



        //Edmondsův-Karpův algoritmus MFP
        private void EdmondsKarp()
        {
            foreach (Edge edge in edges)
            {
                edge.Text = "0/" + edge.Value;
                edge.ResidualValue = edge.Value;
            }
            Stack<Edge> improvementPath = EKBFS();
            while(improvementPath.Count() > 0)
            {
                
                int improvement = int.MaxValue;
                foreach(Edge edge in improvementPath)
                {
                    Console.WriteLine(edge.Value);
                    NewStep(new Step(edge, false, mediumHiglightColor));
                    if(edge.Reverse == false)
                    {
                        if (edge.ResidualValue < improvement)
                        {
                            improvement = edge.ResidualValue;
                        }
                    }
                    else
                    {
                        if(edge.Value - edge.ResidualValue < improvement)
                        {
                            improvement = edge.Value - edge.ResidualValue;
                        }
                    }
                }
                
                foreach (Edge edge in improvementPath)
                {
                    if (edge.Reverse == false)
                    {
                        edge.ResidualValue = edge.ResidualValue - improvement;
                        NewStep(new Step(edge, false, (edge.Value - edge.ResidualValue) + "/" + edge.Value));
                    }
                    else
                    {
                        edge.ResidualValue = edge.ResidualValue + improvement;
                        NewStep(new Step(edge, false, (edge.Value - edge.ResidualValue) + "/" + edge.Value));
                    }
                }
                foreach (Edge edge in improvementPath)
                {
                    NewStep(new Step(edge, false, edgeBaseColor));
                }
                improvementPath = EKBFS();
            }
        }


        //WIP
        private Stack<Edge> EKBFS()
        {
            Stack<Edge> path = new Stack<Edge>();
            foreach (Node node in nodes)
            {
                node.Visited = false;
                node.ImportantEdge = null;
            }
            foreach (Edge edge in edges)
            {
                edge.Reverse = false;
            }
            Queue<Node> nodeQueue = new Queue<Node>();

            nodeQueue.Enqueue(startingNode);
            startingNode.Visited = true;

            while(nodeQueue.Count() != 0)
            {
                Node currentNode = nodeQueue.Dequeue();

                foreach(Edge edge in currentNode.NodeEdges)
                {
                    if(edge.Start.Equals(currentNode) && edge.End.Visited == false && edge.ResidualValue > 0)
                    {
                        nodeQueue.Enqueue(edge.End);
                        edge.End.ImportantEdge = edge;
                        edge.End.Visited = true;
                        if (edge.End.Equals(sinkNode))
                        {
                            Node currentPathNode = sinkNode;

                            while (currentPathNode.ImportantEdge != null)
                            {
                                path.Push(currentPathNode.ImportantEdge);
                                if (currentPathNode.ImportantEdge.End.Equals(currentPathNode))
                                {
                                    currentPathNode = currentPathNode.ImportantEdge.Start;
                                }
                                else
                                {
                                    currentPathNode = currentPathNode.ImportantEdge.End;
                                }
                            }
                            return path;
                        }
                    }
                    else if(edge.End.Equals(currentNode) && edge.Start.Visited == false && edge.ResidualValue < edge.Value)
                    {
                        nodeQueue.Enqueue(edge.Start);
                        edge.Start.ImportantEdge = edge;
                        edge.Start.Visited = true;
                        edge.Reverse = true;
                        if (edge.Start.Equals(sinkNode))
                        {
                            Node currentPathNode = sinkNode;

                            while (currentPathNode.ImportantEdge != null)
                            {
                                path.Push(currentPathNode.ImportantEdge);
                                if (currentPathNode.ImportantEdge.End.Equals(currentPathNode))
                                {
                                    currentPathNode = currentPathNode.ImportantEdge.Start;
                                }
                                else
                                {
                                    currentPathNode = currentPathNode.ImportantEdge.End;
                                }
                            }
                            return path;
                        }
                    }
                }
            }
            return path;
        }



        private void Dinic()
        {
            foreach (Edge edge in edges)
            {
                edge.Text = "0/" + edge.Value;
                edge.ResidualValue = edge.Value;
            }
            RefreshCanvas();
            while(DBFS())
            {
                
                while(DDFS(startingNode, int.MaxValue) != 0);
            }
        }



        private bool DBFS()
        {
            
            foreach (Node node in nodes)
            {
                node.Value = -1;
            }
            Queue<Node> nodeQueue = new Queue<Node>();
            nodeQueue.Enqueue(startingNode);
            startingNode.Value = 0;
            NewStep(new Step(startingNode, false, startingNode.Value + ""));
            while (nodeQueue.Count() != 0)
            {
                Node currentNode = nodeQueue.Dequeue();
                foreach (Edge edge in currentNode.NodeEdges)
                {
                    if (edge.Start.Equals(currentNode) && edge.End.Value == -1 && edge.ResidualValue > 0)
                    {
                        nodeQueue.Enqueue(edge.End);
                        edge.End.Value = edge.Start.Value + 1;
                        NewStep(new Step(edge.End, true, edge.End.Value + ""));

                    }
                    else if (edge.End.Equals(currentNode) && edge.Start.Value == -1 && edge.ResidualValue < edge.Value)
                    {
                        nodeQueue.Enqueue(edge.Start);
                        edge.Start.Value = edge.End.Value + 1;
                        NewStep(new Step(edge.Start, true, edge.Start.Value + ""));
                    }
                }
            }
            return !(sinkNode.Value == -1);
        }



        private int DDFS(Node currentNode, int currentFlow)
        {
            if (currentNode.Equals(sinkNode))
            {
                return currentFlow;
            }
            foreach(Edge edge in currentNode.NodeEdges)
            {
                if (edge.Start.Equals(currentNode) && edge.End.Value == edge.Start.Value + 1 && edge.ResidualValue > 0)
                {
                    NewStep(new Step(edge, false, mediumHiglightColor));
                    int newFlow = edge.ResidualValue;
                    if(newFlow > currentFlow)
                    {
                        newFlow = currentFlow;
                    }
                    int afterFlow = DDFS(edge.End, newFlow);
                    if(afterFlow > 0)
                    {
                        edge.ResidualValue -= afterFlow;
                        NewStep(new Step(edge, false, (edge.Value - edge.ResidualValue) + "/" + edge.Value));
                        NewStep(new Step(edge, false, edgeBaseColor));
                        return afterFlow;
                    }
                    NewStep(new Step(edge, false, edgeBaseColor));
                }
                else if(edge.End.Equals(currentNode) && edge.Start.Value == edge.End.Value + 1 && edge.ResidualValue < edge.Value)
                {
                    NewStep(new Step(edge, false, mediumHiglightColor));
                    int newFlow = edge.Value - edge.ResidualValue;
                    if (newFlow > currentFlow)
                    {
                        newFlow = currentFlow;
                    }
                    int afterFlow = DDFS(edge.Start, newFlow);
                    if (afterFlow > 0)
                    {
                        edge.ResidualValue += afterFlow;
                        NewStep(new Step(edge, false, (edge.Value - edge.ResidualValue) + "/" + edge.Value));
                        NewStep(new Step(edge, false, edgeBaseColor));
                        return afterFlow;
                    }
                    NewStep(new Step(edge, false, edgeBaseColor));
                }
            }
            return 0;
        }

        //Refresh
        private void RefreshCanvas()
        {
            graph.Refresh();
        }

        //Recolor
        public void RecolorToBase()
        {
            foreach(Edge edge in edges)
            {
                edge.Color = edgeBaseColor;
                edge.TextColor = textBaseColor;
            }
            foreach(Node node in nodes)
            {
                node.Color = nodeBaseColor;
            }
            RefreshCanvas();
        }

        //Takes care of creating the first step
        public void NewStep(Step newStep)
        {
            
            if (currentStep == null)
            {
                currentStep = newStep;
            }
            else
            {
                currentStep.StepAfter = newStep;
                currentStep.StepAfter.StepBefore = currentStep;
                currentStep = currentStep.StepAfter;
            }
        }

        //Performs steps of algorithms
        private void StepWait(object sender, EventArgs e)
        {
            if (currentStep.StepAfter != null)
            {
                currentStep.Complete();
                currentStep = currentStep.StepAfter;
                if(currentStep.massStep == true)
                {
                    Console.WriteLine("a");
                    StepWait(sender, e);
                }
                RefreshCanvas();
                t.Interval = waitTimeInput.Value;
            }
            else
            {
                pauseButton.Text = "Go";
                backstepButton.Enabled = true;
                stepButton.Enabled = true;

                currentStep.Complete();
                RefreshCanvas();
                t.Enabled = false;
            }
        }

        //On click functions of algorithm buttons
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Pause")
            {
                t.Enabled = false;
                ((Button)sender).Text = "Go";
                stepButton.Enabled = true;
                backstepButton.Enabled = true;
            }
            else
            {
                stepButton.Enabled = false;
                backstepButton.Enabled = false;
                t.Enabled = true;
                ((Button)sender).Text = "Pause";
            }
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            if (currentStep.StepAfter != null)
            {
                currentStep.Complete();
                currentStep = currentStep.StepAfter;
                if (currentStep.massStep == true)
                {
                    stepButton_Click(sender, e);
                }
                RefreshCanvas();
            }
            else
            {
                if (!currentStep.Reversed)
                {
                    currentStep.Complete();
                    RefreshCanvas();
                }
            }
        }

        private void backstepButton_Click(object sender, EventArgs e)
        {
            if (currentStep.StepBefore != null)
            {
                if (currentStep.StepAfter == null && currentStep.Reversed == true)
                {
                    currentStep.Complete();
                }
                else
                {
                    currentStep = currentStep.StepBefore;
                    currentStep.Complete();
                    if (currentStep.massStep == true)
                    {
                        backstepButton_Click(sender, e);
                    }
                }
                RefreshCanvas();
            }
        }

        //Makes sure only one click function is selected at a time and enables edge value input when appropriate
        private void ClickFunctionChange(object sender, EventArgs e)
        {
            firstNodeOfNewEdge = null;
            movingNode = null;
            RecolorToBase();
            if (clickFunctionStartingNode.SelectedIndex == 0 || clickFunctionStartingNode.SelectedIndex == 1)
            {
                startingNode.Color = mediumHiglightColor;
                sinkNode.Color = bigHiglightColor;
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

        private void ClickFunctionLeaveFocus(object sender, EventArgs e)
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

        //Creates a settings form
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormSettings fs = new FormSettings(this);
            fs.ShowDialog();
        }

        //Sets length of all edges to their real length
        private void euclideanSpaceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            euclidean = !euclidean;
            foreach(Edge edge in edges)
            {
                edge.Text = edge.Value + "";
            }
            RefreshCanvas();
        }

        //Makes the graph directed
        private void directedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            directed = !directed;
            RefreshCanvas();
        }
    }
}
