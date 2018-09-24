using Baron.Entity;
using Baron.Utils;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Service
{
	public class BranchDecisionManager
	{
		public static string BRANCH_A29FAA = "a29faa";
		public const string BRANCH_A29FAB = "a29fab";
		public const string RANDOM_BRANCH_A4BA = "a4ba";
		public const string RANDOM_BRANCH_A29FA = "a29fa";
		public const string ALTERNATIVE_OPTION_1 = "a4baa";
		public const string ALTERNATIVE_OPTION_2 = "a4bab";
		public const string ALTERNATIVE_OPTION_3 = "a4bac";
		public const string ALTERNATIVE_OPTION_4 = "a4bad";
		public const string ALTERNATIVE_OPTION_5 = "a4bae";
		public const string ALTERNATIVE_OPTION_6 = "a4baf";
		public const string ALTERNATIVE_OPTION_7 = "a4bag";
		private GameBase _gameBase;

		private Random _random;
		public BranchDecisionManager(GameBase gameBase)
		{
			_gameBase = gameBase;
			_random = new Random((int)DateTime.Now.Ticks);

		}

		public Branch DecideCurrentBranch(Branch branch)
		{

			Branch newBranch = branch;

			try
			{
				switch (newBranch.OptionId)
				{
					case RANDOM_BRANCH_A4BA:
						CustomLogger.Log("BranchDecisionManager Selected A4BA option with random feature!");

						Branch[] alternatives = getAlternatives();
						newBranch = RMath.Random(alternatives);

						break;
					case RANDOM_BRANCH_A29FA:
						CustomLogger.Log("BranchDecisionManager Selected A29FA option with random feature!");

						Branch a29faa = TreeParser.FindBranchByOption(_gameBase, BRANCH_A29FAA);
						Branch a29fab = TreeParser.FindBranchByOption(_gameBase, BRANCH_A29FAB);

						if (a29faa != null && a29fab != null)
						{
							int randomValue = _random.Next(0, 10000 + 1);
							if (randomValue <= 1000)
							{
								newBranch = a29faa;
							}
							else
							{
								newBranch = a29fab;
							}
						}

						break;
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			return newBranch;
		}

		private Branch[] getAlternatives()
		{
			List<Branch> alternatives = new List<Branch>();

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_1));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_2));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_3));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_4));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_5));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_6));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			try
			{
				alternatives.Add(TreeParser.FindBranchByOption(_gameBase, ALTERNATIVE_OPTION_7));
			}
			catch (Exception e)
			{
				CustomLogger.Log("BBranchDecisionManager Exc" + e.Message);
			}

			//Branch[] a = new Branch[alternatives.Count]; check todo
			return alternatives.ToArray();
		}
	}
}
