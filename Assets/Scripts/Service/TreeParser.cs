﻿using Baron.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Service
{
	public class TreeParser
	{
		public static Branch FindBranchByOption(GameBase gameBase, String id, bool hasAction, HashSet<string> excludeIds)
		{
			Tree tree = gameBase.Tree;
			if (tree == null) return null;

			return FindBranchByOption(tree, id, hasAction, excludeIds);
		}

		public static Branch FindBranchByOption(Branch root, string id,  bool hasAction, HashSet<string> excludeIds)
		{
			if (id == null)
				throw new ArgumentNullException("Id should not be null");

			if (id.Equals(root.OptionId))
			{
				if (excludeIds == null || !excludeIds.Contains(root.Cid))
				{
					if (!hasAction || root.Action != null)
					{
						return root;
					}
				}
			}

			foreach (InventoryBranch inventoryBranch in root.InventoryBranches)
			{
				foreach (Branch currentBranch in inventoryBranch.Branches)
				{
					Branch branch = FindBranchByOption(currentBranch, id, hasAction, excludeIds);
					if (branch != null)
					{
						return branch;
					}
				}
			}

			return null;
		}
		public static Branch FindBranchByCid(GameBase gameBase, string cid)
		{
			if (gameBase == null) return null;

			Tree tree = gameBase.Tree;
			if (tree == null) return null;

			return FindBranchByCid(tree, cid);
		}

		public static Branch FindBranchByOption(GameBase gameBase, String id)
		{
			Tree tree = gameBase.Tree;
			if (tree == null) return null;

			return FindBranchByOption(tree, id);
		}

		private static Branch FindBranchByOption(Branch root, string id)
		{
			if (id == null)
				throw new ArgumentException("Id should not be null");

			if (id.Equals(root.OptionId))
			{
				return root;
			}

			foreach (InventoryBranch inventoryBranch in root.InventoryBranches)
			{
				foreach (Branch currentBranch in inventoryBranch.Branches)
				{
					Branch branch = FindBranchByOption(currentBranch, id);
					if (branch != null)
					{
						return branch;
					}
				}
			}

			return null;
		}

		private static Branch FindBranchByCid(Branch branch, string cid)
		{
			if (cid == null)
				throw new ArgumentNullException("Cid should not be null");

			if (cid.Equals(branch.Cid))
			{
				return branch;
			}

			foreach (InventoryBranch inventoryBranch in branch.InventoryBranches)
			{
				foreach (Branch innerBranch in inventoryBranch.Branches)
				{
					Branch innerMatch = FindBranchByCid(innerBranch, cid);
					if (innerMatch != null)
					{
						return innerMatch;
					}
				}
			}

			return null;
		}

		public static Branch FindNextBranchByInventory(GameBase gameBase, Branch currentBranch)
		{

			InventoryBranch inventoryBranch = FindInventoryBranch(gameBase, currentBranch);
			if (inventoryBranch == null) return null;

			return inventoryBranch.Branches[0];
		}

		public static InventoryBranch FindInventoryBranch(GameBase gameBase, Branch currentBranch)
		{
			History.History history = gameBase.History;
			if (history == null) return null;
			HashSet<string> temp = new HashSet<string>();
			return FindInventoryBranch(gameBase, currentBranch, temp// history.getGlobalInventory()
				);
		}
		public static InventoryBranch FindInventoryBranch(GameBase gameBase, Branch currentBranch, HashSet<String> uniqueItems)
		{

			return null;
		}
	}
}
