﻿using Baron.Entity;
using Baron.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.History
{
	[Serializable]
	public class Save //todo
	{//InteractionEntry
		[JsonProperty(PropertyName = "isEnabled")]
		public bool IsEnabled;

		[JsonProperty(PropertyName = "id")]
		public string Id;
	
		[JsonProperty(PropertyName = "name")]
		public string Name;
		/**
		 * Creation date of the save
		 */
		[JsonProperty(PropertyName = "createdAt")]
		public string CreatedAt;
		/**
		 * Save order
		 */
		[JsonProperty(PropertyName = "order")]
		public int Order;
		/**
		 * Information about current scenario
		 */
		[JsonProperty(PropertyName = "scenario")]
		public Scenario Scenario;
		/**
		 * Day count on current iteration
		 */
		[JsonProperty(PropertyName = "day")]
		public int Day;
		/**
		 * Obtained items on current iteration
		 */
		[JsonProperty(PropertyName = "inventory")]
		public List<Entry> Inventory;//todo
									 /**
									  * Visited branches on current iteration
									  */
		[JsonProperty(PropertyName = "steps")]
		public Stack<Entry> Steps;
		/**
		 * Disabled branches on current iteration
		 */
		[JsonProperty(PropertyName = "disabledBranchCID")]
		public HashSet<string> DisabledBranchCID;
		/**
		 * Disabled options on current iteration
		 */
		[JsonProperty(PropertyName = "disabledOptions")]
		public HashSet<string> DisabledOptions;
		/**
		 * Completed actions in visited branches on current iteration
		 */
		[JsonProperty(PropertyName = "completedOptionActions")]
		public List<OptionActionEntry> CompletedOptionActions;
		/**
		 * Current track container
		 */
		[JsonProperty(PropertyName = "currentBackground")]
		public string CurrentBackground;

		/**
  * Last background image that was used instead of `unknown`
  */
		[JsonProperty(PropertyName = "lastUnknownBackground")]
		public string LastUnknownBackground;
		/**
		 * Protractor interaction attempt count. Resets on NG
		 */
		[JsonProperty(PropertyName = "protractorAttemptCount")]
		public int ProtractorAttemptCount;
		/**
		 * Cid of the last branch that was clicked by player
		 */
		[JsonProperty(PropertyName = "clickedBranches")]
		public Stack<string> ClickedBranches;

		[JsonProperty(PropertyName = "completedInteractions")]
		public List<InteractionEntry> CompletedInteractions;

		public Save()
		{

			Id = StringUtils.Cid();
			Order = 0;

			Day = History.DEFAULT_DAY;

			Steps = new Stack<Entry>();
			Inventory = new List<Entry>();
			DisabledBranchCID = new HashSet<string>();
			DisabledOptions = new HashSet<string>();
			CompletedOptionActions = new List<OptionActionEntry>();
			Scenario = new Scenario();
			ClickedBranches = new Stack<string>();
			CompletedInteractions = new List<InteractionEntry>();

			Day = History.DEFAULT_DAY;
			ProtractorAttemptCount = 1;
		}

		public void Reset()
		{
			Inventory.Clear();
			DisabledBranchCID.Clear();
			DisabledOptions.Clear();
			Steps.Clear();
			CompletedOptionActions.Clear();
			CompletedInteractions.Clear();

			Scenario = new Scenario();

			Day = History.DEFAULT_DAY;
			ProtractorAttemptCount = 1;
			ClickedBranches.Clear();
		}

		public bool IsValid()
		{
			if (Id == null) return false;

			if (IsEnabled)
			{
				if (Day < History.DEFAULT_DAY || Name == null || CreatedAt == null)
				{
					return false;
				}
			}

			return true;
		}
	}
}
