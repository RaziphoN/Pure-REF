/* 
	REF - Raziphon Extension Framework
	Config
	@ Copyright https://github.com/RaziphoN
*/

// Logger
// NOTE: Uncomment required log level for build

//#if DEVELOPMENT_BUILD || UNITY_EDITOR
//	#define REF_LOG_LEVEL_VERBOSE
//	//#define REF_LOG_LEVEL_WARNING
//	//#define REF_LOG_LEVEL_ERROR
//	//#define REF_LOG_LEVEL_ASSERT
//	//#define REF_LOG_LEVEL_EXCEPTION
//#else
//	#define REF_LOG_LEVEL_VERBOSE
//	//#define REF_LOG_LEVEL_WARNING
//	//#define REF_LOG_LEVEL_ERROR
//	//#define REF_LOG_LEVEL_ASSERT
//	//#define REF_LOG_LEVEL_EXCEPTION
//#endif

//// Online Services

////	Analytics
////#define REF_ONLINE_ANALYTICS

//#if REF_ONLINE_ANALYTICS
//	//#define REF_FIREBASE_ANALYTICS
//	//#define REF_FACEBOOK_ANALYTICS
//#endif

////	Auth
////#define REF_ONLINE_AUTH

//#if REF_ONLINE_AUTH
//	//#define REF_FIREBASE_AUTH
//#endif

////	Crash Reports
////#define REF_ONLINE_CRASH_REPORT

//#if REF_ONLINE_CRASH_REPORT
//	//#define REF_FIREBASE_CRASH_REPORT
//#endif


//// Dynamic Links
////#define REF_ONLINE_DYNAMIC_LINK

//#if REF_ONLINE_DYNAMIC_LINK
//	//#define REF_FIREBASE_DYNAMIC_LINK
//#endif

////	In App Messaging
////#define REF_ONLINE_IN_APP_MESSAGING

//#if REF_ONLINE_IN_APP_MESSAGING
//	//#define REF_FIREBASE_IN_APP_MESSAGING
//#endif

////	Push Notifications
////#define REF_ONLINE_PUSH_NOTIFICATION

//#if REF_ONLINE_PUSH_NOTIFICATION
//	//#define REF_FIREBASE_PUSH_NOTIFICATION
//#endif

////	Remote Config
////#define REF_ONLINE_REMOTE_CONFIG

//#if REF_ONLINE_REMOTE_CONFIG
//	//#define REF_FIREBASE_REMOTE_CONFIG
//#endif

//// Social
////#define REF_ONLINE_SOCIAL

//#if REF_ONLINE_SOCIAL
//	//#define REF_FACEBOOK_SOCIAL
//#endif

//// Store
////#define REF_STORE

//#if REF_STORE
	//#define REF_OFFLINE_STORE
	//#define REF_UNITY_STORE
//#endif

// Dependencies
//#define REF_USE_FIREBASE
//#define REF_USE_FACEBOOK
