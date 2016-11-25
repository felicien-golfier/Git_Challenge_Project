using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SettlersEngine
{
    public class MyPathNode : SettlersEngine.IPathNode<System.Object>
	{
	    public Int32 x { get; set; }
	    public Int32 y { get; set; }
	    public Boolean wall {get; set;}
	
	    public bool IsWalkable(System.Object unused)
	    {
	        return !wall;
	    }
	}
	
	public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
	{
	    protected override Double Heuristic(PathNode inStart, PathNode inEnd)
	    {
	        return Math.Abs(inStart.x - inEnd.x) + Math.Abs(inStart.y - inEnd.y);
            //return Math.Sqrt((inStart.x - inEnd.x) * (inStart.x - inEnd.x) + (inStart.y - inEnd.y) * (inStart.y - inEnd.y));
        }
	
	    protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
	    {
            //return GamePawnManager.instance.GetCost(inStart.x, inStart.y);
	        return Heuristic(inStart, inEnd);
	    }
	
	    public MySolver(TPathNode[,] inGrid)
	        : base(inGrid)
	    {
	    }
	}
	
	
}