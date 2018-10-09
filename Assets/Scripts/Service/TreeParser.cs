using Baron.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Service
{
	public class TreeParser
	{
		public static Branch FindBranchByOption(GameBase gameBase, String id, bool hasAction, HashSet<string> excludeIds)//checked 10_09_18
		{
			Tree tree = gameBase.Tree;
			if (tree == null) return null;

			return FindBranchByOption(tree, id, hasAction, excludeIds);
		}

		public static Branch FindBranchByOption(Branch root, string id,  bool hasAction, HashSet<string> excludeIds)//checked 10_09_18
		{
			if (id == null)
				throw new ArgumentNullException("Id should not be null");

			if (id.Equals(root.OptionId)) // reason
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

		public static Branch FindBranchByCid(GameBase gameBase, string cid)//checked 10_09_18
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

		private static Branch FindBranchByCid(Branch branch, string cid)//checked 10_09_18
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

			//if (currentBranch.InventoryBranches.Count == 0)
			//{
			//	throw new ArgumentException("Missing inventory branches: " + currentBranch);
			//}

			//History.History history = gameBase.History;
			//if (history == null) return null;

			////int currentDay = history.getDay();

			//InventoryBranch currentInventoryBranch = null;
			//InventoryBranch defaultInventoryBranch = null;
			//InventoryBranch emptyInventoryBranch = null;

			//foreach (InventoryBranch inventoryBranch in currentBranch.InventoryBranches)
			//{
			//	return null;
			//	List<String> inventory = inventoryBranch.Inventory;

			//	if (IsEmptyInventory(inventory))
			//	{

			//		emptyInventoryBranch = inventoryBranch;

			//	}
			//	else if (IsDefaultInventory(inventory))
			//	{

			//		defaultInventoryBranch = inventoryBranch;

			//	}
			//	else
			//	{

			//		List<bool> canSeeInventoryBranch = new List<bool>(inventory.Count);

			//		foreach (String item in inventory)
			//		{

			//			if (IsGlobalInventory(gameBase, item))
			//			{

			//				canSeeInventoryBranch.add(history.globalInventory.size() > 0 && history.globalInventory.contains(item));

			//			}
			//			else if (isDayInventory(item))
			//			{

			//				int requestedDay = Integer.parseInt(item.replace(InventoryBranch.DAY_PREFIX, ""));
			//				canSeeInventoryBranch.add(requestedDay == currentDay);

			//			}
			//			else
			//			{

			//				canSeeInventoryBranch.add(!uniqueItems.isEmpty() && uniqueItems.contains(item));

			//			}

			//		}


			//		if (canSeeInventoryBranch.size() > 0 && canSeeInventoryBranch.indexOf(false) == -1)
			//		{
			//			currentInventoryBranch = inventoryBranch;
			//			break;
			//		}
			//	}
			//}

			//if (currentInventoryBranch == null)
			//{
			//	if (emptyInventoryBranch != null)
			//	{
			//		currentInventoryBranch = emptyInventoryBranch;
			//	}
			//	else if (defaultInventoryBranch != null)
			//	{
			//		currentInventoryBranch = defaultInventoryBranch;
			//	}
			//}

			//if (currentInventoryBranch == null)
			//	throw new ArgumentException("Could not determine InventoryBranch");

			//return currentInventoryBranch;
		}

		public static bool IsEmptyInventory(List<string> inventory)
		{
			if (inventory.Count == 0) throw new ArgumentException("Missing inventory");

			foreach (string item in inventory)
			{
				if (InventoryBranch.EMPTY.Equals(item))
				{
					return true;
				}
			}

			return false;
		}

		public static bool IsDefaultInventory(List<string> inventory)
		{
			if (inventory.Count == 0) throw new ArgumentException("Missing inventory");

			foreach (string item in inventory)
			{
				if (InventoryBranch.DEFAULT.Equals(item))
				{
					return true;
				}
			}

			return false;
		}
		public static bool IsGlobalInventory(GameBase gameBase, string name)
		{
			//Item item = ItemRepository.find(gameBase, name);

			//return item != null && item.isGlobal;
			return false;
		}
	}
}
