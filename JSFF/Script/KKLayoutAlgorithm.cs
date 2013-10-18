﻿using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
using FreindsLibrary;
using jQueryApi;
using JSFFScript;

namespace GraphSharp.Algorithms.Layout.Simple.FDP
{
    //public class KKLayoutAlgorithm
    //{

    //    #region Variables needed for the layout
    //    /// <summary>
    //    /// Minimal distances between the vertices.
    //    /// </summary>
    //    private double[] distances;
    //    private double[] edgeLengths;
    //    private double[] springConstants;

    //    //cache for speed-up
    //    private Friend[] vertices;
    //    /// <summary>
    //    /// Positions of the vertices, stored by indices.
    //    /// </summary>
    //    private Point[] positions;

    //    private double diameter;
    //    private double idealEdgeLength;
    //    FriendsGroup VisitedGraph;

    //    //Parameters
    //    int MaxIterations = 20;
    //    int Width;
    //    int Height;
    //    int LengthFactor;
    //    bool ExchangeVertices = true;
    //    double DisconnectedMultiplier = 0.5;
    //    double K = 1;
    //    #endregion

    //    #region Contructors
    //    public KKLayoutAlgorithm(FriendsGroup visitedGraph)
    //    {
    //        this.VisitedGraph = visitedGraph;
    //    }

    //    #endregion

    //    protected void InternalCompute()
    //    {
    //        #region Initialization
    //        distances = new Array[];
    //        edgeLengths = new double[];
    //        springConstants = new double[VisitedGraph.VertexCount, VisitedGraph.VertexCount];
    //        vertices = new Friend[VisitedGraph.VertexCount];
    //        positions = new Point[VisitedGraph.VertexCount];

    //        //initializing with random positions
           
    //        //copy positions into array (speed-up)
    //        int index = 0;
    //        for (int iterator1=0; iterator1<VisitedGraph.VertexCount; iterator1++)
    //        {
    //            Friend v = (Friend)VisitedGraph.Friends[VisitedGraph.Friends.Keys[iterator1]];   
    //            vertices[index] = v;
    //            positions[index] = v.position;
    //            index++;
    //        }

    //        //calculating the diameter of the graph
    //        //TODO check the diameter algorithm
    //        //diameter = VisitedGraph.GetDiameter<Vertex, Edge, Graph>(out distances);

    //        //L0 is the length of a side of the display area
    //        //double L0 = Math.Min(Width, Height);

    //        //ideal length = L0 / max d_i,j
    //        idealEdgeLength = 2;

    //        //calculating the ideal distance between the nodes
    //        for (int i = 0; i < VisitedGraph.VertexCount - 1; i++)
    //        {
    //            for (int j = i + 1; j < VisitedGraph.VertexCount; j++)
    //            {
    //                //distance between non-adjacent vertices
    //                double dist = diameter * DisconnectedMultiplier;

    //                //calculating the minimal distance between the vertices
    //                if (distances[i, j] != 100000)
    //                    dist = Math.Min(distances[i, j], dist);
    //                if (distances[j, i] != 100000)
    //                    dist = Math.Min(distances[j, i], dist);
    //                distances[i, j] = distances[j, i] = dist;
    //                edgeLengths[i, j] = edgeLengths[j, i] = idealEdgeLength * dist;
    //                springConstants[i, j] = springConstants[j, i] = K / Math.Pow(dist, 2);
    //            }
    //        }
    //        #endregion

    //        int n = VisitedGraph.VertexCount;
    //        if (n == 0)
    //            return;

    //        //TODO check this condition
    //        for (int currentIteration = 0; currentIteration < MaxIterations; currentIteration++)
    //        {
    //            #region An iteration
    //            double maxDeltaM = -100000;
    //            int pm = -1;

    //            //get the 'p' with the max delta_m
    //            for (int i = 0; i < n; i++)
    //            {
    //                double deltaM = CalculateEnergyGradient(i);
    //                if (maxDeltaM < deltaM)
    //                {
    //                    maxDeltaM = deltaM;
    //                    pm = i;
    //                }
    //            }
    //            //TODO is needed?
    //            if (pm == -1)
    //                return;

    //            //calculating the delta_x & delta_y with the Newton-Raphson method
    //            //there is an upper-bound for the while (deltaM > epsilon) {...} cycle (100)
    //            for (int i = 0; i < 100; i++)
    //            {
    //                positions[pm].AddVector(CalcDeltaXY(pm));

    //                double deltaM = CalculateEnergyGradient(pm);
    //                //real stop condition
    //                if (deltaM < Math.E)
    //                    break;
    //            }

    //            //what if some of the vertices would be exchanged?
    //            if (ExchangeVertices && maxDeltaM < Math.E)
    //            {
    //                double energy = CalcEnergy();
    //                for (int i = 0; i < n - 1; i++)
    //                {
    //                    for (int j = i + 1; j < n; j++)
    //                    {
    //                        double xenergy = CalcEnergyIfExchanged(i, j);
    //                        if (energy > xenergy)
    //                        {
    //                            Point p = positions[i];
    //                            positions[i] = positions[j];
    //                            positions[j] = p;
    //                            return;
    //                        }
    //                    }
    //                }
    //            }
    //            #endregion

    //        }
    //        Report(MaxIterations);
    //    }

    //    protected void Report(int currentIteration)
    //    {
    //        #region Copy the calculated positions
    //        //pozíciók átmásolása a VertexPositions-ba
    //        //for (int i = 0; i < vertices.Length; i++)
    //       //     VertexPositions[vertices[i]] = positions[i];
    //        #endregion

    //        //OnIterationEnded(currentIteration, (double)currentIteration / (double)MaxIterations, "Iteration " + currentIteration + " finished.", true);
    //    }

    //    /// <returns>
    //    /// Calculates the energy of the state where 
    //    /// the positions of the vertex 'p' & 'q' are exchanged.
    //    /// </returns>
    //    private double CalcEnergyIfExchanged(int p, int q)
    //    {
    //        double energy = 0;
    //        for (int i = 0; i < vertices.Length - 1; i++)
    //        {
    //            for (int j = i + 1; j < vertices.Length; j++)
    //            {
    //                int ii = (i == p) ? q : i;
    //                int jj = (j == q) ? p : j;

    //                double l_ij = edgeLengths[i, j];
    //                double k_ij = springConstants[i, j];
    //                double dx = positions[ii].X - positions[jj].X;
    //                double dy = positions[ii].Y - positions[jj].Y;

    //                energy += k_ij / 2 * (dx * dx + dy * dy + l_ij * l_ij -
    //                                       2 * l_ij * Math.Sqrt(dx * dx + dy * dy));
    //            }
    //        }
    //        return energy;
    //    }

    //    /// <summary>
    //    /// Calculates the energy of the spring system.
    //    /// </summary>
    //    /// <returns>Returns with the energy of the spring system.</returns>
    //    private double CalcEnergy()
    //    {
    //        double energy = 0, dist, l_ij, k_ij, dx, dy;
    //        for (int i = 0; i < vertices.Length - 1; i++)
    //        {
    //            for (int j = i + 1; j < vertices.Length; j++)
    //            {
    //                dist = distances[i, j];
    //                l_ij = edgeLengths[i, j];
    //                k_ij = springConstants[i, j];

    //                dx = positions[i].X - positions[j].X;
    //                dy = positions[i].Y - positions[j].Y;

    //                energy += k_ij / 2 * (dx * dx + dy * dy + l_ij * l_ij -
    //                                       2 * l_ij * Math.Sqrt(dx * dx + dy * dy));
    //            }
    //        }
    //        return energy;
    //    }

    //    /// <summary>
    //    /// Determines a step to new position of the vertex m.
    //    /// </summary>
    //    /// <returns></returns>
    //    private Vector CalcDeltaXY(int m)
    //    {
    //        double dxm = 0, dym = 0, d2xm = 0, dxmdym = 0, dymdxm = 0, d2ym = 0;
    //        double l, k, dx, dy, d, ddd;

    //        for (int i = 0; i < vertices.Length; i++)
    //        {
    //            if (i != m)
    //            {
    //                //common things
    //                l = edgeLengths[m, i];
    //                k = springConstants[m, i];
    //                dx = positions[m].X - positions[i].X;
    //                dy = positions[m].Y - positions[i].Y;

    //                //distance between the points
    //                d = Math.Sqrt(dx * dx + dy * dy);
    //                ddd = Math.Pow(d, 3);

    //                dxm += k * (1 - l / d) * dx;
    //                dym += k * (1 - l / d) * dy;
    //                //TODO isn't it wrong?
    //                d2xm += k * (1 - l * Math.Pow(dy, 2) / ddd);
    //                //d2E_d2xm += k_mi * ( 1 - l_mi / d + l_mi * dx * dx / ddd );
    //                dxmdym += k * l * dx * dy / ddd;
    //                //d2E_d2ym += k_mi * ( 1 - l_mi / d + l_mi * dy * dy / ddd );
    //                //TODO isn't it wrong?
    //                d2ym += k * (1 - l * Math.Pow(dx, 2) / ddd);
    //            }
    //        }
    //        // d2E_dymdxm equals to d2E_dxmdym
    //        dymdxm = dxmdym;

    //        double denomi = d2xm * d2ym - dxmdym * dymdxm;
    //        double deltaX = (dxmdym * dym - d2ym * dxm) / denomi;
    //        double deltaY = (dymdxm * dxm - d2xm * dym) / denomi;
    //        return new Vector(deltaX, deltaY);
    //    }

    //    /// <summary>
    //    /// Calculates the gradient energy of a vertex.
    //    /// </summary>
    //    /// <param name="m">The index of the vertex.</param>
    //    /// <returns>Calculates the gradient energy of the vertex <code>m</code>.</returns>
    //    private double CalculateEnergyGradient(int m)
    //    {
    //        double dxm = 0, dym = 0, dx, dy, d, common;
    //        //        {  1, if m < i
    //        // sign = { 
    //        //        { -1, if m > i
    //        for (int i = 0; i < vertices.Length; i++)
    //        {
    //            if (i == m)
    //                continue;

    //            //differences of the positions
    //            dx = (positions[m].X - positions[i].X);
    //            dy = (positions[m].Y - positions[i].Y);

    //            //distances of the two vertex (by positions)
    //            d = Math.Sqrt(dx * dx + dy * dy);

    //            common = springConstants[m, i] * (1 - edgeLengths[m, i] / d);
    //            dxm += common * dx;
    //            dym += common * dy;
    //        }
    //        // delta_m = sqrt((dE/dx)^2 + (dE/dy)^2)
    //        return Math.Sqrt(dxm * dxm + dym * dym);
    //    }
    //}
}