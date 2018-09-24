using Baron.Entity;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Service
{
	public class BranchScenarioManager
	{
		public Scenario CreateScenario(GameBase gameBase, String cid)
		{
			Scenario scenario = new Scenario();
			scenario.Cid = StringUtils.Cid();

			Branch branch = TreeParser.FindBranchByCid(gameBase, cid);

			if (branch.IsReference)
			{
				Branch reference = TreeParser.FindBranchByOption(
						gameBase, branch.OptionId, true, null);

				if (reference == null)
				{
					throw new ArgumentException("Branch reference not found for " + branch);
				}

				branch = reference;
			}

			FindScenario(gameBase, branch, scenario);

			scenario.Init();

			StringBuilder builder = new StringBuilder();
			foreach (TrackBranch trackBranch in scenario.Branches)
			{
				Branch b = TreeParser.FindBranchByCid(gameBase, trackBranch.Id);
				builder.Append("=> "
						+ trackBranch.StartsAt + "-" + trackBranch.FinishesAt
						+ " " + b
						+ ";\r\n");
			}

			String log = builder.ToString();

			CustomLogger.Log("BranchScenarioManager Created scenario:\r\n" + log);

			if (!scenario.IsValid())
			{
				throw new ArgumentException("Created scenario is not valid!");
			}

			return scenario;
		}

		private void FindScenario(GameBase gameBase, Branch from, Scenario scenario)
		{
			CustomLogger.Log("BranchScenarioManager  findScenario " + from + " #" + scenario.Branches.Count);

			BranchDecisionManager decisionManager = new BranchDecisionManager(gameBase);

			Branch altBranch = decisionManager.DecideCurrentBranch(from);
			Option option = OptionRepository.find(gameBase, altBranch.OptionId);

			if (option != null //&& BranchPresenter.isCreated()
				)
			{
				if (option.IsInteraction)
				{
					//InteractionStrategy strategy = InteractionFactory.CreateInstance(option.interaction);

					//BranchPresenter presenter = BranchPresenter.getInstance();
					//RobinzonActivity activity = presenter.getActivity();

					if (false//!strategy.isSupported(activity)
						)
					{
						//Branch nextBranch = TreeParser.FindNextBranchByInventory(gameBase, altBranch);
						//if (nextBranch != null)
						//{
					//		FindScenario(gameBase, nextBranch, scenario);
					//		return;
					//	}
					}
				}
			}

			if (!CanStartScenario(scenario, option))
			{

				//NOTE Scenario is completed due to INCREMENT_DAY action in option.
				//But new scenario will be created after.
				TrackBranch lastBranch = scenario.Last();
				if (lastBranch != null)
				{
					lastBranch.IsBeforeNewScenario = true;
				}
				return;
			}

			TrackBranch trackBranch = new TrackBranch();
			trackBranch.Id = altBranch.Cid;

			scenario.Branches.Add(trackBranch);

			if (option != null)
			{
				trackBranch.Duration = option.Duration;
				trackBranch.OptionId = option.Id;
				trackBranch.OptionType = option.Type;
				trackBranch.ContainsDayAction = option.ContainsDayAction();
			}

			if (CanFinishScenario(altBranch, option)) return;

			Branch match = null;// TreeParser.FindNextBranchByInventory(gameBase, altBranch);

			if (match != null)
			{

				if (!match.IsFinal && match.IsReference)
				{
					Branch reference = TreeParser.FindBranchByOption(
							gameBase, match.OptionId, true, null);

					if (reference == null)
					{
						throw new ArgumentException("Branch reference not found for " + match);
					}

					match = reference;
				}

				FindScenario(gameBase, match, scenario);
			}
		}

		public bool CanStartScenario(Scenario scenario, Option option)
		{
			if (scenario.Branches.Count == 0) return true;

			foreach (TrackBranch branch in scenario.Branches)
			{
				if (branch.ContainsDayAction)
				{
					return false;
				}
			}

			return option == null || !option.ContainsDayAction();
		}

		public bool CanFinishScenario(Branch branch, Option option)
		{

			bool isFinalBranch = branch.IsFinal || branch.IsClick;
			bool isInteraction = option != null && option.IsInteraction;

			return isFinalBranch || isInteraction;
		}
	}
}
