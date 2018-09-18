namespace Poker
{
    public static class Constants
    {
        public const int NoPlayerId = -1;

        public const int PlayerNameLength = 10;
        public const string SoundEnabledStr = "SoundEnabled";
        public const string PurchaseListAmountStr = "PurchaseListAmount";
        public const string PurchaseListStr = "PurchaseList";

        public const string AvatarBlackListStr = "AvatarBlackList";
        public const string ChatBlackListStr = "ChatBlackList";
        public const string SelectedAvatarIdStr = "SelectedAvatarId";
        public const string ActiveAvatarIdStr = "ActiveAvatarId";
        public const string AvatarName = "avatar_";
        public const string IsChatEnabled = "IsChatEnabled";
        public const string CurrentProtocolVersion = "CurrentProtocolVersion";
        public const string UnityBuildNumber = "UnityBuildNumber";
        public const string LastBonusTime = "lastBonusTime";
        public const float Epsilon = .00001f;
        public const float EpsilonForSlider = .01f;

        public const float CardFadingTime = 0.5f;
        public const float PauseBeforePotMove = 2.0f;
        public const int CardMoveDuration = 200;
        public const int MoveToPotDuration = 550;
        public const float MoveFromPotDuration = .75f;
        public const float CardFlipDuration = .4f;
        public const int PauseBetweenCardMove = 200;
        public const int ShowDuration = 3000;
        public const int WaitNextHandCloseTimeOut = 4000;
        public const long ChipsBonusPeriod = 7200000;
        public const string ProblemReportServerUrl = "http://problemreports.linuxdev.solverlabs.com";

        public static class Texts
        {
            public const string K_UNLOCK_ALL_AVATARS = "This in-app purchase will unlock all premium avatars";
            public const string K_THIS_PURCHASE_IS_DISABLED = "This purchase is temporarily disabled!";
            public const string K_PURCHASE_SUCCESSFUL_MESSAGE = "The purchase of {purchase} was successful";
            public const string K_DISMISS = "Dismiss";
            public const string K_REPORT_THE_PROBLEM = "Report the problem";
            public const string K_RETRY = "Retry";
            public const string K_REPORT = "Report";
            public const string K_QUIT = "Quit";
            public const string K_NO_TABLES_AVAILABLE_FOR_BUY_IN = "There are no vacant tables available. Please try again in a moment";
            public const string K_LOGGED_OUT_BY_SERVER = "Logged out by server!";
            public const string K_INVITATIONS_TO_PLAY_SENT = "The invitations were successfully sent";
            public const string K_SHOULD_SEATED_IN_TO_SEND_INVITATIONS_TO_PLAY =
                "You should be seated in to be able to send invitations to play.";
            public const string K_FACEBOOK_INVITATION_POSTED =
                "The invitation was successfully posted. Your friend have to click the invitation and follow the instructions for you to be able to get the chips bonus";
            public const string K_INVITE_FRIENDS_FOR_BONUS = "Get {chips} in chips for each friend invited to download Poker " +
                "(you will get the chips only after your friend installs Poker and connects it to {social network})";

            public const string K_YOU_ARE_LOGGING_WITH_ACCOUNT = "You're going to log in with {account}.";
            public const string K_YOU_HAVE_TO_LEAVE_THE_TABLE = "Warning: you'll have to leave the table.";
            public const string K_ACCOUNT_NOT_TRANSFERABLE_WARNING = "HINT: your chips will be left in the current account ({account}).\n" +
                "To get back to the account you'll have to: \n" +
                "1. Go to options.\n" +
                "2. Click Logout button.\n" +
                "3. Click Login with {account}.";
            public const string K_WOULD_YOU_LIKE_TO_PROCEED = "Would you like to proceed?";

            public const string K_FACEBOOK_NOT_FOUND_FRIENDS_THAT_CAN_BE_INVITED =
                "Couldn't find any new friends to invite matching your search criteria";

            public const string K_BLOCK_AVATAR = "Block avatar";
            public const string K_UNBLOCK_AVATAR = "Unblock avatar";
            public const string K_BLOCK_CHAT = "Block chat";
            public const string K_UNBLOCK_CHAT = "Unblock chat";

            public const string K_ROUND_TURN = "Turn";
            public const string K_ROUND_FINAL = "Final";
            public const string K_ROUND_WAITING = "Waiting";
            public const string K_ROUND_PREFLOP = "Pre-flop";
            public const string K_ROUND_FLOP = "Flop";

            public const string K_LEAVE_TABLE_CONFIRMATION = "Are you sure you want to leave the table?";
            public const string K_NOT_ENOUGHT_CHIPS = "Not enough chips";
            public const string K_NOT_ENOUGH_CHIPS_TO_JOIN = "Not enough chips to join any table";
            public const string K_NOT_ENOUGHT_CHIPS_TO_SIT_IN = "Not enough chips for buy-in!";
            public const string K_WAITING_FOR_ANOTHER_PLAYER = "Waiting for another player...";
            public const string K_SIT_IN_FAILED = "Buy-in failed";
            public const string K_CONNECTION_FAILED = "Connection failed. Please make sure you have a working Internet connection";
            public const string K_EXIT = "Are you sure you want to exit?";

	        public static string K_SPEED_NORMAL = "SPEED_NORMAL";
	        public static string K_SPEED_FAST = "SPEED_FAST";

            public const string K_BACK_TO_LOBBY = "Back to lobby";
            public const string K_CHANGE_TABLE = "Change table";

            //GameControlsManager text
            public const string K_RAISE_CONFIRM = "Confirm";
            public const string K_RAISE_CANCEL = "Cancel";


            public const string K_CHECK = "Check";
            //  public const string K_CHECK_FOLD = "Check/Fold";
            public const string K_CALL = "Call";
            public const string K_RAISE = "Raise";

            public const string K_FOLD = "Fold";
            public const string K_ALL_IN = "All-in";
            public const string K_CALL_ANY = "Call any";
            public const string K_SMALL_BLIND = "Sm.Blind";
            public const string K_BIG_BLIND = "Big Blind";
            public const string K_WINNER = "Winner";

            public static string K_MSG_CHECK_RAISE = "Check or raise";
            public static string K_MSG_TO_CALL = " to call";

            public const string K_WAIT_NEXT_HAND = "Please wait for the next hand";

            public const string K_PLEASE_WAIT = "Please wait";
            public const string K_PLEASE_UPDATE = "Please update Poker to the latest version";
            public const string K_LOGOUT_GAME = "Are you sure want to logout?";
            public const string K_DIALOGBOX_TITLE = "Poker";
            public const string K_DEFAULT_YES_TEXT = "YES";
            public const string K_DEFAULT_NO_TEXT = "NO";
            public const string K_DEFAULT_OK_TEXT = "OK";
            public const string K_DEFAULT_CANCEL_TEXT = "CANCEL";
            public const string K_PREMIUM_AVATARS_LOCKED = "Premium avatars are locked";

            public const string K_DID_NOT_FINISHED_TRANSACTION = "Previous transaction did not finished";
            public const string K_ILLEGAL_TEXT_ENTERED = "An illegal text entered";
            public static readonly string[] ACTION_TEXTS =
            {
                K_CALL,
                K_FOLD,
                K_RAISE,
                K_CHECK,
                K_SMALL_BLIND,
                K_BIG_BLIND,
                K_ALL_IN,
                K_WINNER
            };

            public static string[] TIPS_GRATITUDE_MESSAGES =
            {
		        "Thank you, ",
		        "Awesome tip! Thanks, ",
		        "Much appreciated, ",
		        "Nice tip! Good luck, ",
		        "Thank you very much, ",
		        "Thanks for the tip, ",
		        "How kind of you, ",
		        "Very generous of you, "
            };

            public const string K_TO_TIP_DEALER = "Would you like to tip the dealer?";
            public const string YOUR_ACCOUNT_BALANCE = "Your account balance: ";
            public const string CHAT_IS_DISABLED = "Chat is disabled. Would you like to enable it now?";
        }

        public static class StatisticEventNames
        {
            public const string REGISTRATION_SCREEN_LOG_IN_AS_GUEST_BUTTON_CLICK = "registration_screen_log_in_as_guest_button_click";
            public const string REGISTRATION_SCREEN_LOG_IN_WITH_FACEBOOK_BUTTON_CLICK = "registration_screen_log_in_with_facebook_button_click";
	        public const string REGISTRATION_SCREEN_LOG_IN_WITH_BBM_BUTTON_CLICK = "registration_screen_log_in_with_bbm_button_click";

	        public const string LOBBY_SCREEN_PROFILE_BUTTON_CLICK = "lobby_screen_profile_button_click";
	        public const string LOBBY_SCREEN_BUY_CHIPS_BUTTON_CLICK = "lobby_screen_buy_chips_button_click";
	        public const string LOBBY_SCREEN_HELP_BUTTON_CLICK = "lobby_screen_help_button_click";
	        public const string LOBBY_SCREEN_OPTIONS_BUTTON_CLICK = "lobby_screen_options_button_click";
	        public const string LOBBY_SCREEN_LIKE_IT_BUTTON_CLICK = "lobby_screen_like_it_button_click";
	        public const string LOBBY_SCREEN_MORE_APPS_BUTTON_CLICK = "lobby_screen_more_apps_button_click";
	        public const string LOBBY_SCREEN_PLAY_NOW_BUTTON_CLICK = "lobby_screen_play_now_button_click";
	        public const string LOBBY_SCREEN_HOLDEM_TABLES_BUTTON_CLICK = "lobby_screen_holdem_tables_button_click";
	        public const string LOBBY_SCREEN_SUGGESTION_BUTTON_CLICK = "lobby_screen_suggestion_button_click";
	        public const string LOBBY_SCREEN_MINI_GAMES_BUTTON_CLICKED = "lobby_screen_mini_games_button_click";
	        public const string LOBBY_SCREEN_MYSTERYBOXES_BUTTON_CLICK = "lobby_screen_mysteryboxes_button_click";

	        public const string PROFILE_SCREEN_BACK_BUTTON_CLICK = "profile_screen_back_button_click";
	        public const string PROFILE_SCREEN_EDIT_AVATAR_BUTTON_CLICK = "profile_screen_edit_avatar_button_click";
	        public const string PROFILE_SCREEN_CHANGE_NAME_BUTTON_CLICK = "profile_screen_change_name_button_click";
	        public const string PROFILE_SCREEN_SAVE_NAME_CLICKED = "profile_screen_save_name_clicked";
	        public const string PROFILE_SCREEN_NAME_CHANGED = "profile_screen_name_changed";
	        public const string PROFILE_SCREEN_GET_CHIPS_BUTTON_CLICK = "profile_screen_get_chips_button_click";
	        public const string PROFILE_SCREEN_PICK_YOUR_POKER_FACE_BUTTON_CLICK = "profile_screen_pick_your_poker_face_button_click";
	        public const string PROFILE_SCREEN_SWITCH_TO_FACEBOOK_AVATAR_BUTTON_CLICK = "profile_screen_switch_to_facebook_avatar_button_click";
	        public const string PROFILE_SCREEN_PLAYER_DATA_SYNCHRONIZED = "profile_screen_player_data_synchronized";
	        public const string CHANGE_DISPLAY_NAME_FAILED_DIALOG_DISMISS_BUTTON_CLICK = "change_display_name_failed_dialog_dismiss_button_click";
	        public const string CHANGE_DISPLAY_NAME_FAILED_DIALOG_REPORT_THE_PROBLEM_BUTTON_CLICK = "change_display_name_failed_dialog_report_the_problem_button_click";

	        public const string CHANGE_AVATAR_SCREEN_SELECT_AVATAR_BUTTON_CLICK = "change_avatar_screen_select_avatar_button_click";
	        public const string CHANGE_AVATAR_SCREEN_SAVE_BUTTON_CLICK = "change_avatar_screen_save_button_click";
	        public const string CHANGE_AVATAR_SCREEN_CANCEL_BUTTON_CLICK = "change_avatar_screen_cancel_button_click";
	        public const string CHANGE_AVATAR_SCREEN_DISMISS_BUTTON_CLICK = "change_avatar_screen_dismiss_button_click";

	        public const string BUY_CHIPS_SCREEN_BACK_BUTTON_CLICK = "buy_chips_screen_back_button_click";
	        public const string BUY_CHIPS_SCREEN_INVITE_FRIEND_BUTTON_CLICK = "buy_chips_screen_invite_friend_button_click";
	        public const string BUY_CHIPS_SCREEN_BUY_CHIPS_BUTTON_CLICK = "buy_chips_screen_buy_chips_button_click";
	        public const string BUY_CHIPS_SCREEN_PURCHASE_IS_DISABLED_DIALOG_SHOW = "buy_chips_screen_purchase_is_disabled_dialog_show";

	        public const string HELP_SCREEN_BACK_BUTTON_CLICK = "help_screen_back_button_click";
	        public const string HELP_SCREEN_TUTORIAL_BUTTON_CLICK = "help_screen_tutorial_button_click";
	        public const string HELP_SCREEN_HAND_RANK_BUTTON_CLICK = "help_screen_hand_rank_button_click";
	        public const string HELP_SCREEN_ABOUT_BUTTON_CLICK = "help_screen_about_button_click";
	        public const string HELP_SCREEN_REPORT_PROBLEM_BUTTON_CLICK = "help_screen_report_problem_button_click";
	        public const string HELP_SCREEN_OPTIONS_BUTTON_CLICK = "help_screen_options_button_clicked";

	        public const string OPTIONS_SCREEN_BACK_BUTTON_CLICK = "options_screen_back_button_click";
	        public const string OPTIONS_SCREEN_SOUND_CHECKBOX_CLICK = "options_screen_sound_checkbox_click";
	        public const string OPTIONS_SCREEN_VIBRO_CHECKBOX_CLICK = "options_screen_vibro_checkbox_click";
	        public const string OPTIONS_SCREEN_CHAT_CHECKBOX_CLICK = "options_screen_chat_checkbox_click";
	        public const string OPTIONS_SCREEN_SIGNAL_CHECKBOX_CLICK = "options_screen_signal_checkbox_click";
	        public const string OPTIONS_SCREEN_FACEBOOK_AVATARS_CHECKBOX_CLICK = "options_screen_facebook_avatars_checkbox_click";
	        public const string OPTIONS_SCREEN_RESTART_APPLICATION_TO_APPLY_CHANGES_DIALOG_SHOW = "options_screen_restart_application_to_apply_changes_dialog_show";

	        public const string LIKE_IT_DIALOG_SHOW_AFTER_TABLE_CLOSE = "like_it_dialog_show_after_table_close";
	        public const string LIKE_IT_DIALOG_LIKE_IT_BUTTON_CLICK = "like_it_dialog_like_it_button_click";
	        public const string LIKE_IT_DIALOG_LATER_BUTTON_CLICK = "like_it_dialog_later_button_click";
	        public const string LIKE_IT_DIALOG_DISMISS_BUTTON_CLICK = "like_it_dialog_dismiss_button_click";

	        public const string TABLE_FILTER_DIALOG_SPEED_BUTTON_CLICK = "table_filter_dialog_speed_button_click";
	        public const string TABLE_FILTER_DIALOG_CLOSE_BUTTON_CLICK = "table_filter_dialog_close_button_click";
	        public const string TABLE_FILTER_DIALOG_PLAY_NOW_BUTTON_CLICK = "table_filter_dialog_play_now_button_click";

	        public const string FACEBOOK_SUGGESTION_DIALOG_SEARCH_BUTTON_CLICK = "facebook_suggestion_dialog_search_button_click";
	        public const string FACEBOOK_SUGGESTION_INVITE_SELECTED_FRIEND_CLICK = "facebook_suggestion_invite_selected_friend_click";
	        public const string FACEBOOK_SUGGESTION_DIALOG_CLOSE_BUTTON_CLICK = "facebook_suggestion_dialog_close_button_click";

	        public const string FACEBOOK_MYSTERYBOX_SELECTED_FRIEND_CLICK = "facebook_mysterybox_selected_friend_click";
	        public const string MYSTERYBOX_SELECT_FRIENDS_TO_SEND_BUTTON_CLICK = "mysterybox_select_friends_to_send_button_click";

	        public const string WAITING_FOR_ANOTHER_PLAYER_DIALOG_CHANGE_TABLE_BUTTON_CLICK = "waiting_for_another_player_dialog_change_table_button_click";
	        public const string WAITING_FOR_ANOTHER_PLAYER_DIALOG_BACK_TO_LOBBY_BUTTON_CLICK = "waiting_for_another_player_dialog_back_to_lobby_button_click";
	        public const string WAITING_FOR_ANOTHER_PLAYER_DIALOG_INVITE_FRIENDS_BUTTON_CLICK = "waiting_for_another_player_dialog_invite_friends_button_click";

	        public const string BUY_IN_DIALOG_PLAY_NOW_BUTTON_CLICK = "buy_in_dialog_play_now_button_click";
	        public const string BUY_IN_DIALOG_CLOSE_BUTTON_CLICK = "buy_in_dialog_close_button_click";
	        public const string ARG_SIT_IN_WITH_MAX_CHIPS_COUNT = "sit_in_with_max_chips_count";
	        public const string ARG_BUY_IN_PERCENT_FROM_MAX = "buy_in_percent_from_max";

	        public const string CHAT_DIALOG_SEND_MESSAGE = "chat_dialog_send_message";
	        public const string CHAT_DIALOG_CLOSE_BUTTON_CLICK = "chat_dialog_close_button_click";
	        public const string CHAT_DIALOG_CLOSED = "chat_dialog_closed";
	        public const string CHAT_DIALOG_QUICK_MESSAGE_BUTTON_CLICK = "chat_dialog_quick_message_button_click";

	        public const string PROFILE_POPUP_SHOW = "profile_popup_show";
	        public const string PROFILE_POPUP_HIDE = "profile_popup_hide";
	        public const string PROFILE_POPUP_DIALOG_CLOSE_BUTTON_CLICK = "profile_popup_dialog_close_button_click";
	        public const string PROFILE_POPUP_DIALOG_UNBLOCK_CHAT_BUTTON_CLICK = "profile_popup_dialog_unblock_chat_button_click";
	        public const string PROFILE_POPUP_DIALOG_BLOCK_CHAT_BUTTON_CLICK = "profile_popup_dialog_block_chat_button_click";
	        public const string PROFILE_POPUP_DIALOG_UNBLOCK_AVATAR_BUTTON_CLICK = "profile_popup_dialog_unblock_avatar_button_click";
	        public const string PROFILE_POPUP_DIALOG_BLOCK_AVATAR_BUTTON_CLICK = "profile_popup_dialog_block_avatar_button_click";

	        public const string GAME_TABLE_SHOW_CHAT_BUTTON_CLICK = "game_table_show_chat_button_click";
	        public const string GAME_TABLE_TIP_DEALER_BUTTON_CLICK = "game_table_tip_dealer_button_click";
	        public const string GAME_TABLE_SHOW_RAISE_SLIDER = "game_table_show_raise_slider";
	        public const string GAME_TABLE_BACK_BUTTON_CLICK = "game_table_back_button_click";
	        public const string GAME_TABLE_CHECKABLE_BUTTON_CLICK = "game_table_checkable_button_click_";
	        public const string GAME_TABLE_SHOW_BUY_IN_DIALOG = "game_table_show_buy_in_dialog";
	        public const string GAME_TABLE_ON_INVITE_TO_PLAY_CLICK = "game_table_invite_to_play_click";
	        public const string GAME_TABLE_EXECUTE_SCHEDULED_PLAYER_ACTION = "game_table_execute_scheduled_player_action";
	        public const string GAME_TABLE_ACTION_BUTTON_CLICK = "game_table_action_button_click_";

	        public const string NO_TABLES_AVAILABLE_DIALOG_DISMISS_BUTTON_CLICK = "no_tables_available_dialog_dismiss_button_click";
	        public const string NO_TABLES_AVAILABLE_DIALOG_RETRY_BUTTON_CLICK = "no_tables_available_dialog_retry_button_click";
	        public const string NO_TABLES_AVAILABLE_DIALOG_REPORT_THE_PROBLEM_BUTTON_CLICK = "no_tables_available_dialog_report_the_problem_button_click";

	        public const string NOT_ENOUGH_CHIPS_TO_JOIN_DIALOG_DISMISS_BUTTON_CLICK = "not_enough_chips_to_join_dialog_dismiss_button_click";
	        public const string NOT_ENOUGH_CHIPS_TO_JOIN_DIALOG_GET_CHIPS_BUTTON_CLICK = "not_enough_chips_to_join_dialog_get_chips_button_click";
	        public const string NOT_ENOUGH_CHIPS_TO_SIT_IN_DIALOG_GET_CHIPS_BUTTON_CLICK = "not_enough_chips_to_sit_in_dialog_get_chips_button_click";
	        public const string NOT_ENOUGH_CHIPS_TO_SIT_IN_DIALOG_WATCH_BUTTON_CLICK = "not_enough_chips_to_sit_in_dialog_watch_button_click";

	        public const string CONNECT_FAILED_DIALOG_REPORT_PROBLEM_BUTTON_CLICK = "connect_failed_dialog_report_problem_button_click";
	        public const string CONNECT_FAILED_DIALOG_QUIT_GAME_BUTTON_CLICK = "connect_failed_dialog_quit_game_button_click";
	        public const string CONNECT_FAILED_DIALOG_QUIT_BUTTON_CLICK = "connect_failed_dialog_quit_button_click";
	        public const string CONNECT_FAILED_DIALOG_RETRY_BUTTON_CLICK = "connect_failed_dialog_retry_button_click";

	        public const string BLOCK_CHAT_DIALOG_REPORT_ABUSE_CLICK = "block_chat_dialog_report_abuse_click";
	        public const string BLOCK_CHAT_DIALOG_DISABLE_ALL_CHAT_BUTTON_CLICK = "block_chat_dialog_disable_all_chat_button_click";
	        public const string BLOCK_CHAT_DIALOG_BLOCK_THIS_PERSON_BUTTON_CLICK = "block_chat_dialog_block_this_person_button_click";

	        public const string ASK_FOR_TIPS_DIALOG_YES_BUTTON_CLICK = "ask_for_tips_dialog_yes_button_click";
	        public const string ASK_FOR_TIPS_DIALOG_CANCEL_BUTTON_CLICK = "ask_for_tips_dialog_cancel_button_click";

	        public const string ACTION_WAIT_DIALOG_CANCEL_BUTTON_CLICK = "action_wait_dialog_cancel_button_click";

	        public const string SIT_IN_FAILED_DIALOG_CHANGE_TABLE_BUTTON_CLICK = "sit_in_failed_dialog_change_table_button_click";
	        public const string SIT_IN_FAILED_DIALOG_BACK_TO_LOBBY_BUTTON_CLICK = "sit_in_failed_dialog_back_to_lobby_button_click";
	        public const string SIT_IN_FAILED_DIALOG_REPORT_PROBLEM_BUTTON_CLICK = "sit_in_failed_dialog_report_problem_button_click";

	        public const string BUY_GIFT_DIALOG_BUY_GIFT_BUTTON_CLICK = "buy_gift_dialog_buy_gift_button_click";
	        public const string BUY_GIFT_DIALOG_BUY_GIFT_FOR_TABLE_BUTTON_CLICK = "buy_gift_dialog_buy_gift_for_table_button_click";
	        public const string BUY_GIFT_DIALOG_CLOSE_BUTTON_CLICK = "buy_gift_dialog_close_button_click";
	        public const string BUY_GIFT_DIALOG_SELECT_GIFT_BUTTON_CLICK = "buy_gift_dialog_select_gift_button_click";
	        public const string BUY_GIFT_DIALOG_PLEASE_SELECT_GIFT_TO_BUY_DIALOG_SHOW = "buy_gift_dialog_please_select_gift_to_buy_dialog_show";

	        public const string CHOOSE_SOCIAL_NETWORK_DIALOG_BBM_BUTTON_CLICK = "choose_social_network_dialog_bbm_button_click";
	        public const string CHOOSE_SOCIAL_NETWORK_DIALOG_FACEBOOK_BUTTON_CLICK = "choose_social_network_dialog_facebook_button_click";
	        public const string CHOOSE_SOCIAL_NETWORK_DIALOG_CLOSE_BUTTON_CLICK = "choose_social_network_dialog_close_button_click";

	        public const string INVITE_FRIEND_FOR_BONUS_MESSAGE_DIALOG_SHOW = "invite_friend_for_bonus_message_dialog_show";
	
	        //AndroidPlatformUtils
	        public const string SHOW_MESSAGE_DIALOG_OK_BUTTON_CLICK = "show_message_dialog_ok_button_click";
	        public const string APPLICATION_OPEN_BROWSER = "application_open_browser";

	        //AccountController
	        public const string ASK_RELOGIN_WITH_FACEBOOK_DIALOG_YES_BUTTON_CLICK = "ask_relogin_with_facebook_dialog_yes_button_click";
	        public const string ASK_RELOGIN_WITH_BBM_DIALOG_YES_BUTTON_CLICK = "ask_relogin_with_bbm_dialog_yes_button_click";
	        public const string ASK_RELOGIN_WITH_BBM_DIALOG_SHOW = "ask_relogin_with_bbm_dialog_show";
	        public const string ASK_RELOGIN_WITH_BBM_DIALOG_NO_BUTTON_CLICK = "ask_relogin_with_bbm_dialog_no_button_click";
	        public const string ASK_RELOGIN_WITH_FACEBOOK_DIALOG_SHOW = "ask_relogin_with_facebook_dialog_show";
	        public const string ASK_RELOGIN_WITH_FACEBOOK_DIALOG_NO_BUTTON_CLICK = "ask_relogin_with_facebook_dialog_no_button_click";
	        public const string PREMIUM_AVATARS_PURCHASE_DONE = "premium_avatars_purchase_done";
	        public const string UPDATE_CHIPS_COUNT = "update_chips_count";
	        public const string UPDATE_PLAYER_AVATAR = "update_player_avatar";
	        public const string CHANGE_AVATAR = "change_avatar";
	        public const string CHANGE_DISPLAY_NAME = "change_display_name";
	        public const string CHANGE_AVATAR_FAILED = "change_avatar_failed";
	        public const string CHANGE_DISPLAY_NAME_FAILED = "change_display_name_failed";
	        public const string CHANGE_AVATAR_SUCCEEDED = "change_avatar_succeeded";
	        public const string CHANGE_DISPLAY_NAME_SUCCEEDED = "change_display_name_succeeded";
	        public const string ACCOUNT_ACTION_FAILED = "account_action_failed";
	        public const string RELOGIN_NEEDED_DIALOG_SHOW = "relogin_needed_dialog_show";
	        public const string RELOGIN_DIALOG_YES_BUTTON_CLICK = "relogin_dialog_yes_button_click";
	        public const string EXECUTE_LOAD_PLAYER = "execute_load_player";
	        public const string EXECUTE_PURCHASE_PREMIUM_AVATARS = "execute_purchase_premium_avatars";
	        public const string EXECUTE_PURCHASE_CHIPS = "execute_purchase_chips";
	        public const string LOAD_PLAYER_SUCCEEDED = "load_player_succeeded";
	        public const string LOAD_PLAYER_FAILED = "load_player_failed";
	        public const string PURCHASE_ALLOWED = "purchase_allowed";
	        public const string PURCHASE_DENIED = "purchase_denied";
	        public const string PURCHASE_DENIED_ERROR_DIALOG_SHOW = "purchase_denied_error_dialog_show";
	        public const string ON_SUGGESTION_CLICKED = "on_suggestion_clicked";
	        public const string ON_BBM_SUGGESTION_CLICKED = "on_bbm_suggestion_clicked";
	        public const string RELOGIN_DIALOG_NO_BUTTON_CLICK = "relogin_dialog_no_button_click";
	        public const string SLOTS_SPIN_SUCCEEDED = "slots_spin_succeeded";
	        public const string SLOTS_SPIN_FAILED = "slots_spin_failed";
	        public const string EXECUTE_SEND_MYSTERYBOXES = "execute_send_mysteryboxes";
	        public const string MYSTERYBOXES_COUNT = "mysteryboxes_count";

	        //FacebookAccountController
	        public const string EXECUTE_SWITCH_TO_FACEBOOK_AVATAR = "execute_switch_to_facebook_avatar";
	        public const string HANDLE_FIND_FACEBOOK_FRIENDS_REQUEST_QUEUED = "handle_find_facebook_friends_request_queued";
	        public const string HANDLE_FIND_FACEBOOK_FRIENDS_REQUEST_FAILED = "handle_find_facebook_friends_request_failed";
	        public const string ON_FACEBOOK_SUGGESTION_CLICKED = "on_facebook_suggestion_clicked";
	        public const string ON_FACEBOOK_FRIENDS_FOUND = "on_facebook_friends_found";
	        public const string FACEBOOK_GET_SUGGESTION_URL_FAILED = "facebook_get_suggestion_url_failed";
	        public const string FACEBOOK_GET_SUGGESTION_URL_SUCCEEDED = "facebook_get_suggestion_url_succeeded";
	        public const string FACEBOOK_SUGGESTION_FAILED = "facebook_suggestion_failed";
	        public const string FACEBOOK_SUGGESTION_SUCCEEDED = "facebook_suggestion_succeeded";
	        public const string FACEBOOK_SUGGESTION_CANCELED = "facebook_suggestion_canceled";
	        public const string FACEBOOK_SUGGESTION_FAILED_DIALOG_REPORT_A_PROBLEM_BUTTON_CLICK = "facebook_suggestion_failed_dialog_report_a_problem_button_click";
	        public const string FACEBOOK_SUGGESTION_SUCCESSFULLY_POSTED_DIALOG_SHOW = "facebook_suggestion_successfully_posted_dialog_show";
	        public const string INVITE_FRIEND_FOR_BONUS_MESSAGE_SHOW = "invite_friend_for_bonus_message_show";
	        public const string NOT_FOUND_FRIENDS_THAT_CAN_BE_INVITED_DIALOG_SHOW = "not_found_friends_that_can_be_invited_dialog_show";
	        public const string NOT_FOUND_FRIENDS_THAT_MYSTERYBOX_CAN_BE_SENT_TO_DIALOG_SHOW = "not_found_friends_that_mysterybox_can_be_sent_to_dialog_show";

	        //BBMAccountController
	        public const string BBM_REGISTER_SUGGESTION = "bbm_register_suggestion";

	        //AppController
	        public const string APPLICATION_ON_FOREGROUND = "application_on_foreground";
	        public const string APPLICATION_ON_BACKGROUND = "application_on_background";
	        public const string SCREEN_ON_SLEEP = "screen_on_sleep";
	        public const string SCREEN_ON_WAKE_UP = "screen_on_wake_up";
	        public const string RECOMMENDED_UPDATE_NEEDED = "recommended_update_needed";
	        public const string CRITICAL_UPDATE_NEEDED = "critical_update_needed";
	        public const string TABLE_CLOSED = "table_closed";
	        public const string RECONNECT_SUCCEEDED = "reconnect_succeeded";
	        public const string REMOTE_LOGOUT_DIALOG_SHOW = "remote_logout_dialog_show";
	        public const string PURCHASE_WILL_UNLOCK_ALL_AVATARS_DIALOG_SHOW = "purchase_will_unlock_all_avatars_dialog_show";
	        public const string CLOSE_APPLICATION = "close_application";
	        public const string LOGOUT_DIALOG_CONFIRM_BUTTON_CLICK = "logout_dialog_confirm_button_click";

	        //ClientController
	        public const string EXECUTE_CONNECT = "execute_connect";
	        public const string EXECUTE_DISCONNECT = "execute_disconnect";
	        public const string EXECUTE_LOGIN = "execute_login";
	        public const string EXECUTE_LOGOUT = "execute_logout";
	        public const string LOGIN_FAILED = "login_failed";
	        public const string LOGOUT_SUCCEEDED = "logout_succeeded";
	        public const string LOGOUT_FAILED = "logout_failed";
	        public const string CONNECT_FAILED = "connect_failed";
	        public const string CONNECTION_LOST = "connection_lost";
	        public const string RECONNECT_FAILED = "reconnect_failed";
	        public const string NETWORK_ACTION_EXCEPTION = "network_action_exception";
	        public const string BBM_IS_NOT_SUPPORTED_DIALOG_SHOW = "bbm_is_not_supported_dialog_show";
	        public const string FAILED_TO_LOG_IN_WITH_FACEBOOK_DIALOG_SHOW = "failed_to_log_in_with_facebook_dialog_show";
	        public const string REMOTE_ERROR_MESSAGE_SHOW = "remote_error_message_show";
	        public const string REMOTE_LOGOUT = "remote_logout";
	        public const string REMOTE_NOTIFICATION_DIALOG_SHOW = "remote_notification_dialog_show";
	        public const string REMOTE_ERROR_DIALOG_REPORT_A_PROBLEM_BUTTON_CLICK = "remote_error_dialog_report_a_problem_button_click";
	        public const string REMOTE_ERROR_DIALOG_OK_BUTTON_CLICK = "remote_error_dialog_ok_button_click";

	        //LobbyController
	        public const string TABLE_JOIN_SUCCEEDED = "table_join_succeeded";
	        public const string TABLE_JOIN_FAILED = "table_join_failed";
	        public const string EXECUTE_JOIN_HOLDEM_TABLE = "execute_join_holdem_table";
	        public const string JOIN_HOLDEM_TABLE_WITH_FILTER = "join_holdem_table_with_filter";
	        public const string SIT_IN_ANY_WHERE_FAILED = "sit_in_any_where_failed";
	        public const string INFO_MESSAGE_DIALOG_SHOW = "info_message_dialog_show";

	        //GameController
	        public const string ALERT_ON_LOCAL_PLAYER_TURN = "alert_on_local_player_turn";
	        public const string TABLE_SIT_IN_ANY_WHERE_SUCCEEDED = "table_sit_in_any_where_succeeded";
	        public const string TABLE_SEAT_CHANGED = "table_seat_changed";
	        public const string TABLE_SIT_IN_ANYWHERE_FAILED = "table_sit_in_anywhere_failed";
	        public const string GIVE_TIPS_FAILED = "give_tips_failed";
	        public const string SEND_GIFT_FAILED = "send_gift_failed";
	        public const string BUY_GIFT = "buy_gift";
	        public const string LEAVE_TABLE_SUCCEEDED = "leave_table_succeeded";
	        public const string LEAVE_TABLE_FAILED = "leave_table_failed";
	        public const string LEAVE_TABLE_FINISHED = "leave_table_finished";
	        public const string SIT_IN_SUCCEEDED = "sit_in_succeeded";
	        public const string SIT_IN_FAILED = "sit_in_failed";
	        public const string ON_LOCAL_PLAYER_TURN = "on_local_player_turn";
	        public const string ON_NEW_DEAL = "on_new_deal";
	        public const string ON_NEW_ROUND = "on_new_round";
	        public const string ON_BET_RAISED = "on_bet_raised";
	        public const string EXECUTE_LEAVE_TABLE = "execute_leave_table";
	        public const string EXECUTE_SEND_INVITATION_TO_PLAY = "execute_send_invitation_to_play_";
	        public const string EXECUTE_GIVE_TIPS = "execute_give_tips";
	        public const string EXECUTE_SEND_GIFT = "execute_send_gift";
	        public const string EXECUTE_SEND_GIFT_FOR_TABLE = "execute_send_gift_for_table";
	        public const string EXECUTE_SIT_IN = "execute_sit_in";
	        public const string NOT_ENOUGH_CHIPS_DIALOG_SHOW = "not_enough_chips_dialog_show";
	        public const string ASK_ENABLE_CHAT_DIALOG_SHOW = "ask_enable_chat_dialog_show";
	        public const string ASK_ENABLE_CHAT_DIALOG_YES_BUTTON_CLICK = "ask_enable_chat_dialog_yes_button_click";
	        public const string ASK_ENABLE_CHAT_DIALOG_NO_BUTTON_CLICK = "ask_enable_chat_dialog_no_button_click";
	        public const string FOLD_ACTION = "fold_action";
	        public const string CHECK_CALL_ACTION = "check_call_action";
	        public const string CHECK_FOLD_ACTION = "check_fold_action";
	        public const string CALL_ANY_ACTION = "call_any_action";
	        public const string INVITATIONS_COUNT = "invitations_count";

	        //GameTableServerEventListener
	        public const string ON_GIFT_RECEIVE = "on_gift_receive";
	        public const string WON = "won";
	        public const string LOSS = "loss";
	        public const string ALL_IN = "all_in";

	        //GameControlsManager
	        public const string SHOW_CONTROLS = "show_controls";
	        public const string HIDE_CONTROLS = "hide_controls";

	        //PokerApp
	        public const string ON_UPDATE_NEEDED_DIALOG_SHOW = "on_update_needed_dialog_show";
	        public const string BAD_DEVICE_PIN_ASK_REPORT_A_PROBLEM_DIALOG_SHOW = "bad_device_pin_ask_report_a_problem_dialog_show";
	        public const string BAD_DEVICE_PIN_ASK_REPORT_A_PROBLEM_DIALOG_YES_BUTTON_CLICK = "bad_device_pin_ask_report_a_problem_dialog_yes_button_click";
	        public const string BAD_DEVICE_PIN_ASK_REPORT_A_PROBLEM_DIALOG_NO_BUTTON_CLICK = "bad_device_pin_ask_report_a_problem_dialog_no_button_click";

	        //AvatarsPurchaseController
	        public const string SUCCESSFULLY_PURCHASED_PREMIUM_AVATARS_DIALOG_SHOW = "successfully_purchased_premium_avatars_dialog_show";
	        public const string PURCHASE_WAS_SUCCESSFUL_DIALOG_SHOW = "purchase_was_successful_dialog_show";

	        //ChipsPurchaseController
	        public const string CONSUMABLE_PURCHASE_SUCCESSFUL_DIALOG_SHOW = "consumable_purchase_successful_dialog_show";

	        //AmazonPurchase
	        public const string REASON = "reason";
	        public const string PURCHASE_SKU_KEY = "purchase_sku";
	        public const string IN_APP_PURCHASE_FAILED = "in_app_purchase_failed";
	        public const string IN_APP_PURCHASE_STARTED = "in_app_purchase_started";

	        public const string MYSTERY_BOX_SELECT_FRIENDS_CLICKED = "mystery_box_select_friends_clicked";
	        public const string MYSTERY_BOX_KEY_TYPE = "type";
	        public const string MYSTERY_BOX_RECEIVED = "mystery_box_received";
	        public const string MYSTERY_BOX_ACTION = "mystery_box_action";
	        public const string MYSTERY_BOX_ACTION_KEY = "action";
        }
    }
}