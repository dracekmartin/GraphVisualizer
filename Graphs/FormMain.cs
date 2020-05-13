using System;
using System.Collections.Generic;   
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Graphs
{
    public partial class FormMain : Form
    {
        //Default graph
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
        //Global properties
        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();
        Node startingNode = null;
        Node sinkNode = null;
        Step currentStep = null;
        public bool euclidean = false;
        public bool directed = false;
        public int canvasStartX = 0;
        public int canvasStartY = 0;
        //Global color properites
        public Color nodeBaseColor = Color.CadetBlue;
        public Color edgeBaseColor = Color.LightGray;
        public Color textBaseColor = Color.Black;
        public Color smallHiglightColor = Color.FromArgb(220, 220, 0);
        public Color mediumHiglightColor = Color.Orange;
        public Color bigHiglightColor = Color.Red;
        //Timer of algotihm steps
        Timer t;



        public FormMain()
        {
            InitializeComponent();
            OnStart();
        }



        //Loads a default graph
        private void OnStart()
        {
            foreach (Point iterPoint in defaultNodePositions)
            {
                nodes.Add(new Node(iterPoint, nodeBaseColor));
            }
            for (int i = 0; i < nodes.Count(); i++)
            {
                for (int k = nodes.Count() - 1; k > i; k--)
                {
                    if (defaultMatrix[i, k] > 0)
                    {
                        Edge newEdge = new Edge(nodes[i], nodes[k], edgeBaseColor, textBaseColor, defaultMatrix[i, k]);
                        nodes[i].NodeEdges.Add(newEdge);
                        nodes[k].NodeEdges.Add(newEdge);
                        edges.Add(newEdge);
                    }
                }
            }
            startingNode = nodes[0];
            sinkNode = nodes[7];
            edgeValueInput.Enabled = false;
            pauseButton.Enabled = false;
            stepButton.Enabled = false;
            backstepButton.Enabled = false;
            RefreshCanvas();
        }

        //Draws everything from nodes and edges
        private void Graph_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Edge edge in edges)
            {
                edge.DrawEdge(e.Graphics, directed, canvasStartX, canvasStartY);
                edge.DrawText(e.Graphics, canvasStartX, canvasStartY);
            }

            foreach (Node nodes in nodes)
            {
                nodes.DrawNode(e.Graphics, canvasStartX, canvasStartY);
                nodes.DrawText(e.Graphics, canvasStartX, canvasStartY);
            }
        }

        //Solves clicks on the graph
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
                    if (found == false && node.Clicked(e.Location, canvasStartX, canvasStartY))
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
                    if (found == false && edge.Clicked(e.Location, canvasStartX, canvasStartY))
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
                    Node newNode = new Node(new Point(e.Location.X - canvasStartX, e.Location.Y - canvasStartY), nodeBaseColor);
                    nodes.Add(newNode);
                }
                else if(e.Button == MouseButtons.Right)
                {
                    foreach (Node node in nodes)
                    {
                        if (node.Clicked(e.Location, canvasStartX, canvasStartY))
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
                        if (node.Clicked(e.Location, canvasStartX, canvasStartY))
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
                        if (node.Clicked(e.Location, canvasStartX, canvasStartY))
                        {
                            if (firstNodeOfNewEdge == null)
                            {
                                firstNodeOfNewEdge = node;
                                firstNodeOfNewEdge.Color = mediumHiglightColor;
                                break;
                            }
                            else if (node != firstNodeOfNewEdge)
                            {
                                Edge newEdge = new Edge(firstNodeOfNewEdge, node, edgeBaseColor, textBaseColor, Int32.Parse(edgeValueInput.Text));
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
                        if (edge.Clicked(e.Location, canvasStartX, canvasStartY))
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
                    if (edge.Clicked(e.Location, canvasStartX, canvasStartY))
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
                    if (edge.Clicked(e.Location, canvasStartX, canvasStartY))
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
                    if (node.Clicked(e.Location, canvasStartX, canvasStartY) && node != sinkNode)
                    {
                        if(startingNode != null) startingNode.Color = nodeBaseColor;
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
                    if (node.Clicked(e.Location, canvasStartX, canvasStartY) && node != startingNode)
                    {
                        if(sinkNode != null) sinkNode.Color = nodeBaseColor;
                        sinkNode = node;
                        node.Color = bigHiglightColor;
                        break;
                    }
                }
            }

            RefreshCanvas();
        }

        //Performs the drag operation
        Point dragStart;
        Point dragEnd;
        private void Graph_MouseDown(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                dragStart = e.Location;
                dragEnd = e.Location;
            }
        }

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                dragEnd = e.Location;
            }
        }

        private void Graph_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickFunctionClick.SelectedIndex == 1 && e.Button == MouseButtons.Left)
            {
                canvasStartX += dragEnd.X - dragStart.X;
                canvasStartY += dragEnd.Y - dragStart.Y;
                RefreshCanvas();
            }
        }

        //Disables appropriate controls, starts the algorithm and resets the form after the algorithm is done
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (startAlgButton.Text == "Start")
            {
                ResetNodes();
                ResetEdges();
                Console.WriteLine("a");
                if (algorithmSelectionSP.SelectedIndex == 0)
                {
                    Dijkstra();
                }
                else if (algorithmSelectionMST.SelectedIndex == 0)
                {
                    Jarnik();
                }
                else if (algorithmSelectionMST.SelectedIndex == 1)
                {
                    Boruvka();
                }
                else if (algorithmSelectionMST.SelectedIndex == 2)
                {
                    Kruskal();
                }
                else if (algorithmSelectionMF.SelectedIndex == 0)
                {
                    EdmondsKarp();
                }
                else if(algorithmSelectionMF.SelectedIndex == 1)
                {
                    Dinic();
                }

                if(currentStep != null)
                {
                    while (currentStep.StepBefore != null)
                    {
                        currentStep = currentStep.StepBefore;
                    }
                }
                else
                {
                    return;
                }

                startAlgButton.Text = "Reset";

                clickFunctionClick.Enabled = false;
                clickFunctionNode.Enabled = false;
                clickFunctionEdge.Enabled = false;
                edgeValueInput.Enabled = false;
                algorithmSelectionSP.Enabled = false;
                algorithmSelectionMST.Enabled = false;
                algorithmSelectionMF.Enabled = false;
                clickFunctionStartingNode.Enabled = false;
                settingsButton.Enabled = false;
                euclideanSpaceCheckBox.Enabled = false;
                directedCheckBox.Enabled = false;
                pauseButton.Enabled = true;

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
                ResetNodes();
                ResetEdges();

                RefreshCanvas();
                startAlgButton.Text = "Start";
                pauseButton.Text = "Pause";
                clickFunctionClick.Enabled = true;
                clickFunctionNode.Enabled = true;
                clickFunctionEdge.Enabled = true;
                algorithmSelectionSP.Enabled = true;
                algorithmSelectionMST.Enabled = true;
                algorithmSelectionMF.Enabled = true;
                clickFunctionStartingNode.Enabled = true;
                settingsButton.Enabled = true;
                euclideanSpaceCheckBox.Enabled = true;
                directedCheckBox.Enabled = true;
                pauseButton.Enabled = false;
                stepButton.Enabled = false;
                backstepButton.Enabled = false;
            }
        }

        //Resets all nodes to default state
        private void ResetNodes()
        {
            foreach (Node node in nodes)
            {
                node.Color = nodeBaseColor;
                node.Text = "";
                node.TextColor = textBaseColor;

                node.Value = int.MaxValue;
                node.Visited = false;
                node.PartOfSubset = node;
                node.ImportantEdge = null;
            }
        }

        //Resets all edgesto default state
        private void ResetEdges()
        {
            foreach (Edge edge in edges)
            {
                edge.Text = edge.Value + "";
                edge.Color = edgeBaseColor;
            }
        }


        //Dijkstra's algorithm
        private void Dijkstra()
        {
            foreach (Node node in nodes)
            {
                NewStep(new Step(node, true, "∞"));
            }
            RefreshCanvas();
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
                        if(edge.End.Value == int.MaxValue)
                        {
                            NewStep(new Step(edge.End, false, edge.Value + "+" + current.Value));
                            edge.End.Value = edge.Value + current.Value;
                        }
                        else
                        {
                            NewStep(new Step(edge.End, false, edge.End.Value + "?" + edge.Value + "+" + current.Value));
                            if (edge.End.Value > edge.Value + current.Value)
                            {
                                NewStep(new Step(edge.End, false, edge.End.Value + ">" + (edge.Value + current.Value)));
                                edge.End.Value = edge.Value + current.Value;
                            }
                            else
                            {
                                NewStep(new Step(edge.End, false, edge.End.Value + "<=" + (edge.Value + current.Value)));
                            }
                        }
                        
                        NewStep(new Step(edge.End, false, edge.End.Value + ""));
                        NewStep(new Step(edge.End, false, nodeBaseColor, edge.End.Value + ""));
                        NewStep(new Step(edge, false, edgeBaseColor));
                    }
                    else if (edge.End.Equals(current) && edge.Start.Visited == false)
                    {
                        NewStep(new Step(edge, false, smallHiglightColor));
                        NewStep(new Step(edge.Start, false, smallHiglightColor));
                        if(edge.Start.Value == int.MaxValue)
                        {
                            NewStep(new Step(edge.Start, false, edge.Value + "+" + current.Value));
                            edge.Start.Value = edge.Value + current.Value;
                        }
                        else
                        {
                            NewStep(new Step(edge.Start, false, edge.Start.Value + "?" + edge.Value + "+" + current.Value));
                            if (edge.Start.Value > edge.Value + current.Value)
                            {
                                NewStep(new Step(edge.Start, false, edge.Start.Value + ">" + (edge.Value + current.Value)));
                                edge.Start.Value = edge.Value + current.Value;
                            }
                            else
                            {
                                NewStep(new Step(edge.Start, false, edge.Start.Value + "<=" + (edge.Value + current.Value)));
                            }
                        }
                        NewStep(new Step(edge.Start, false, edge.Start.Value + ""));
                        NewStep(new Step(edge.Start, false, nodeBaseColor, edge.Start.Value +""));
                        NewStep(new Step(edge, false, edgeBaseColor));
                    }
                }
                NewStep(new Step(current, false, bigHiglightColor));
            }
        }



        //Jarník's algorithm
        private void Jarnik()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge edge in startingNode.NodeEdges)
            {
                edgeHeap.Add(edge);
            }
            startingNode.Visited = true;
            NewStep(new Step(startingNode, false, mediumHiglightColor));
            while (!(edgeHeap.IsEmpty()))
            {
                Edge currentEdge = edgeHeap.Pop();
                if (currentEdge.End.Visited == false)
                {
                    NewStep(new Step(currentEdge, false, mediumHiglightColor));
                    NewStep(new Step(currentEdge.End, false, mediumHiglightColor));
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
                    NewStep(new Step(currentEdge, false, mediumHiglightColor));
                    NewStep(new Step(currentEdge.Start, false, mediumHiglightColor));
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
                    NewStep(new Step(currentEdge, false, smallHiglightColor));
                }
            }
        }



        //Borůvka's algorithm
        private void Boruvka()
        {
            int treeCount = nodes.Count();
            foreach(Node node in nodes)
            {
                NewStep(new Step(node, true, mediumHiglightColor));
            }
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
                List<Edge> addedEdges = new List<Edge>();
                foreach (Node node in nodes)
                {
                    if (node.ImportantEdge != null)
                    {
                        NewStep(new Step(node, false, bigHiglightColor));
                        NewStep(new Step(node.ImportantEdge, false, bigHiglightColor));
                        if (!(node.ImportantEdge.Start.PartOfSubset.Equals(node.ImportantEdge.End.PartOfSubset)))
                        {
                            addedEdges.Add(node.ImportantEdge);
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
                        NewStep(new Step(node.ImportantEdge, false, smallHiglightColor));
                        NewStep(new Step(node, false, mediumHiglightColor));
                        node.ImportantEdge = null;
                    }
                }
                foreach (Edge edge in addedEdges)
                {
                    NewStep(new Step(edge, true, mediumHiglightColor));
                }
            }
        }



        //Kruskal algorithm
        private void Kruskal()
        {
            EdgeMinHeap edgeHeap = new EdgeMinHeap(edges.Count());
            foreach(Edge edge in edges)
            {
                edgeHeap.Add(edge);
            }
            int treeCount = nodes.Count();
            foreach (Node node in nodes)
            {
                NewStep(new Step(node, true, mediumHiglightColor));
            }
            while (treeCount > 1 || edgeHeap.IsEmpty())
            {
                Edge currentEdge = edgeHeap.Pop();
                if (!(currentEdge.Start.PartOfSubset.Equals(currentEdge.End.PartOfSubset)))
                {
                    currentEdge.Start.PartOfSubset.PartOfSubset = currentEdge.End.PartOfSubset;
                    NewStep(new Step(currentEdge, false, mediumHiglightColor));
                    treeCount--;
                }
                else
                {
                    NewStep(new Step(currentEdge, false, smallHiglightColor));
                }
            }
        }



        //Edmonds-Karp's algorithm
        private void EdmondsKarp()
        {
            foreach (Edge edge in edges)
            {
                NewStep(new Step(edge, true, "0/" + edge.Value));
                edge.ResidualValue = edge.Value;
            }
            Stack<Edge> improvementPath = EKBFS();
            while(improvementPath.Count() > 0)
            {
                int improvement = int.MaxValue;
                foreach(Edge edge in improvementPath)
                {
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



        //Dinic's algorithm WIP
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
                while(DDFS(startingNode, int.MaxValue) != 0)
                {
                    CleanUp();
                }
            }
        }
        List<Node> sortedNodes = new List<Node>();
        private bool DBFS()
        {
            foreach (Node node in nodes)
            {
                node.Visited = false;
                node.Value = -1;
            }
            foreach (Edge edge in edges)
            {
                edge.Usable = false;
            }
            Queue<Node> nodeQueue = new Queue<Node>();
            nodeQueue.Enqueue(startingNode);
            startingNode.Value = 0;
            NewStep(new Step(startingNode, false, startingNode.Value + ""));
            sortedNodes = new List<Node>();
            while (nodeQueue.Count() != 0)
            {
                Node currentNode = nodeQueue.Dequeue();
                foreach (Edge edge in currentNode.NodeEdges)
                {
                    if (edge.Start.Equals(currentNode) && (edge.End.Value == -1 || edge.End.Value == edge.Start.Value + 1) && edge.ResidualValue > 0)
                    {
                        edge.Usable = true;
                        nodeQueue.Enqueue(edge.End);
                        edge.End.Value = edge.Start.Value + 1;
                        sortedNodes.Add(edge.End);
                        NewStep(new Step(edge.End, true, edge.End.Value + ""));
                    }
                    else if (edge.End.Equals(currentNode) && (edge.Start.Value == -1 || edge.Start.Value == edge.End.Value + 1) && edge.ResidualValue < edge.Value)
                    {
                        edge.Usable = true;
                        nodeQueue.Enqueue(edge.Start);
                        edge.Start.Value = edge.End.Value + 1;
                        sortedNodes.Add(edge.Start);
                        NewStep(new Step(edge.Start, true, edge.Start.Value + ""));
                    }
                }
            }
            foreach(Edge edge in edges)
            {
                if (edge.Usable)
                {
                    NewStep(new Step(edge, true, smallHiglightColor));
                }
            }
            CleanUp();
            return !(sinkNode.Value == -1);
        }
        private void CleanUp()
        {
            foreach (Node node in nodes)
            {
                node.Visited = false;
            }
            sinkNode.Visited = true;
            for (int i = sortedNodes.Count() - 1; i >= 0; i--)
            {
                if (sortedNodes[i].Visited == true)
                {
                    foreach (Edge edge in sortedNodes[i].NodeEdges)
                    {
                        if (edge.Usable && sortedNodes[i].Equals(edge.End))
                        {
                            edge.Start.Visited = true;
                        }
                        else if (edge.Usable && sortedNodes[i].Equals(edge.Start))
                        {
                            edge.End.Visited = true;
                        }
                    }
                }
                else
                {
                    foreach (Edge edge in sortedNodes[i].NodeEdges)
                    {
                        edge.Usable = false;
                    }
                }
            }
            foreach (Node node in nodes)
            {
                node.Visited = false;
            }
            foreach (Edge edge in edges)
            {
                if (edge.Usable == false)
                {
                    NewStep(new Step(edge, true, edgeBaseColor));
                }
            }
        }
        private int DDFS(Node currentNode, int currentFlow)
        {
            if (currentNode.Equals(sinkNode))
            {
                return currentFlow;
            }
            foreach(Edge edge in currentNode.NodeEdges)
            {
                if (edge.Usable && edge.Start.Equals(currentNode) && edge.End.Value == edge.Start.Value + 1 && edge.ResidualValue > 0)
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
                        NewStep(new Step(edge, false, smallHiglightColor));
                        return afterFlow;
                    }
                    NewStep(new Step(edge, false, smallHiglightColor));
                }
                else if(edge.Usable && edge.End.Equals(currentNode) && edge.Start.Value == edge.End.Value + 1 && edge.ResidualValue < edge.Value)
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
                        NewStep(new Step(edge, false, smallHiglightColor));
                        return afterFlow;
                    }
                    NewStep(new Step(edge, false, smallHiglightColor));
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

        //Performs steps of an algorithm
        private void StepWait(object sender, EventArgs e)
        {
            if (currentStep.StepAfter != null)
            {
                currentStep.Complete();
                currentStep = currentStep.StepAfter;
                if (currentStep.massStep == true)
                {
                    StepWait(sender, e);
                    return;
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
        private void PauseButton_Click(object sender, EventArgs e)
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

        //Performs steps of the algorithm manually
        private void StepButton_Click(object sender, EventArgs e)
        {
            if (currentStep.StepAfter != null)
            {
                currentStep.Complete();
                currentStep = currentStep.StepAfter;
                if (currentStep.massStep == true)
                {
                    StepButton_Click(sender, e);
                    return;
                }
            }
            else if(currentStep.StepAfter == null && currentStep.Reversed == false)
            {
                currentStep.Complete();
            }
            RefreshCanvas();
        }

        private void BackstepButton_Click(object sender, EventArgs e)
        {
            if (currentStep.StepAfter == null && currentStep.Reversed == true)
            {
                currentStep.Complete();
                if (currentStep.massStep)
                {
                    BackstepButton_Click(sender, e);
                    return;
                }
            }
            else if(currentStep.StepBefore != null)
            {
                currentStep = currentStep.StepBefore;
                currentStep.Complete();
                if (currentStep.massStep)
                {
                    BackstepButton_Click(sender, e);
                    return;
                }
            }
            RefreshCanvas();
        }

        //Makes sure only one click function is selected at a time and enables edge value input when appropriate
        private void ClickFunctionChange(object sender, EventArgs e)
        {
            firstNodeOfNewEdge = null;
            movingNode = null;
            RecolorToBase();
            if (clickFunctionStartingNode.SelectedIndex == 0 || clickFunctionStartingNode.SelectedIndex == 1)
            {
                if(startingNode != null) startingNode.Color = mediumHiglightColor;
                if(sinkNode != null) sinkNode.Color = bigHiglightColor;
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

        //Makes sure only one alghoritm is selected at a time

        private void AlgorithmSelection_Enter(object sender, EventArgs e)
        {
            List<ListBox> algorithms = new List<ListBox>();
            algorithms.Add(algorithmSelectionSP);
            algorithms.Add(algorithmSelectionMST);
            algorithms.Add(algorithmSelectionMF);
            foreach(ListBox lb in algorithms) if (((ListBox)sender) != lb) lb.ClearSelected();
        }

        //Creates a settings form
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FormSettings fs = new FormSettings(this);
            fs.ShowDialog();
        }

        //Sets length of all edges to their real length
        private void EuclideanSpaceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            euclidean = !euclidean;
            foreach(Edge edge in edges)
            {
                edge.Euclidean = !edge.Euclidean;
                edge.Text = edge.Value + "";
            }
            RefreshCanvas();
        }

        //Makes the graph directed
        private void DirectedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            directed = !directed;
            RefreshCanvas();
        }

        //Saves the graph in .graphml format
        private void SaveGraphButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?><graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\">");
                writer.WriteLine("<key attr.name=\"x\" attr.type=\"float\" for=\"node\" id=\"x\"/>");
                writer.WriteLine("<key attr.name=\"y\" attr.type=\"float\" for=\"node\" id=\"y\"/>");
                writer.WriteLine("<key attr.name=\"weight\" attr.type=\"double\" for=\"edge\" id=\"weight\"/>");
                if (directed == true)
                {
                    writer.WriteLine("<graph edgedefault=\"directed\">");
                }
                else
                {
                    writer.WriteLine("<graph edgedefault=\"undirected\">");
                }
                int idCount = 0;
                foreach (Node node in nodes)
                {
                    node.id = idCount;
                    idCount++;
                    writer.WriteLine("<node id=\"" + node.id + "\">");
                    writer.WriteLine("<data key=\"x\">" + node.Position.X + "</data>");
                    writer.WriteLine("<data key=\"y\">" + (-node.Position.Y) + "</data>");
                    writer.WriteLine("</node>");
                }
                idCount = 0;
                foreach (Edge edge in edges)
                {
                    writer.WriteLine("<edge id=\"" + idCount + "\" source=\"" + edge.Start.id + "\" target=\"" + edge.End.id + "\">");
                    idCount++;
                    writer.WriteLine("<data key=\"weight\">" + edge.Value + "</data>");
                    writer.WriteLine("</edge>");
                }
                writer.WriteLine("</graph>");
                writer.WriteLine("</graphml>");
                writer.Close();
            }
        }

        //Saves the graph from a .graphml file
        private void LoadGraphButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nodes = new List<Node>();
                edges = new List<Edge>();
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);
                Console.WriteLine(doc.DocumentElement.Name);
                XmlNode graph = doc.DocumentElement.GetElementsByTagName("graph")[0];
                if(graph.Attributes["edgedefault"].Value == "directed")
                {
                    directed = true;
                }
                else
                {
                    directed = false;
                }
                XmlNodeList nodesAndEdges = graph.ChildNodes;
                foreach (XmlNode node in nodesAndEdges)
                {
                    if(node.Name == "node")
                    {
                        nodes.Add(new Node(new Point(int.Parse(node.ChildNodes[0].InnerText), -int.Parse(node.ChildNodes[1].InnerText)), nodeBaseColor));
                    }
                    else if(node.Name == "edge")
                    {
                        Edge newEdge = new Edge(nodes[int.Parse(node.Attributes["source"].Value)], nodes[int.Parse(node.Attributes["target"].Value)], edgeBaseColor, textBaseColor, int.Parse(node.ChildNodes[0].InnerText));
                        edges.Add(newEdge);
                        newEdge.Start.NodeEdges.Add(newEdge);
                        newEdge.End.NodeEdges.Add(newEdge);
                    }
                }
                RefreshCanvas();
            }
        }

        //Deletes all nodes and edges
        private void ClearGraphButton_Click(object sender, EventArgs e)
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
            startingNode = null;
            sinkNode = null;
            RefreshCanvas();
        }
    }
}
