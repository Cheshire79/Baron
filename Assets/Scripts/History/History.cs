using Baron.Entity;
using Baron.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.History
{
	public class History
	{
		public static int DEFAULT_DAY = 1;
		public static int SAVE_LIMIT = 4;
		private GameBase _gameBase;
		public Branch _initialBranch;
		private Scenario _scenario;
		public History(GameBase gameBase)
		{
			_gameBase = gameBase;
			_scenario = new Scenario();//todo
		}
		public void UpdateInitialBranch()
		{
			if (_gameBase == null) return;

			Tree tree = _gameBase.Tree;
			if (tree == null) return;

			Branch initial = TreeParser.FindBranchByOption(_gameBase, GameBase.INITIAL_BRANCH, true, null);
			if (initial == null) return;
			_initialBranch = initial;

		}
		public Scenario GetScenario() //todo
		{
			return _scenario;
		}
		public void SetScenario(Scenario scenario) //todo
		{
			_scenario=scenario;
		}
		public Branch GetInitialBranch()
		{
			return _initialBranch;
		}
	}
}
