
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if UNITY
using System.Collections.Generic;
#endif

namespace System.ComponentModel
{
	partial class Win32Exception
	{
#if !MOBILE
		[DllImport ("Kernel32", CharSet = CharSet.Unicode)]
		static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId,
			[Out] StringBuilder lpBuffer, int nSize, IntPtr[] arguments);
#endif

#if UNITY
		static bool s_ErrorMessagesInitialized = false;
		static Dictionary<int, string> s_ErrorMessage = new Dictionary<int, string>();
#endif

		internal static string GetErrorMessage (int error)
		{
#if !UNITY && !MOBILE
			if (Environment.IsRunningOnWindows) {
				StringBuilder sb = new StringBuilder (256);

				int result = FormatMessage (0x1200 /* FORMAT_MESSAGE_IGNORE_INSERTS | FORMAT_MESSAGE_FROM_SYSTEM */,
					IntPtr.Zero, (uint)error, 0, sb, sb.Capacity, null);

				if (result == 0)
					return "Error looking up error string";

				return sb.ToString ();
			}
#endif
#if UNITY
			if (!s_ErrorMessagesInitialized)
				InitializeErrorMessages();
			string message;
			if (s_ErrorMessage.TryGetValue(error, out message))
				return message;
			return string.Format ("mono-io-layer-error ({0})", error);
#else
			switch (error) {
			case 0: /* ERROR_SUCCESS */ return "Success";
			case 2: /* ERROR_FILE_NOT_FOUND */ return "Cannot find the specified file";
			case 3: /* ERROR_PATH_NOT_FOUND */ return "Cannot find the specified file";
			case 4: /* ERROR_TOO_MANY_OPEN_FILES */ return "Too many open files";
			case 5: /* ERROR_ACCESS_DENIED */ return "Access denied";
			case 6: /* ERROR_INVALID_HANDLE */ return "Invalid handle";
			case 13: /* ERROR_INVALID_DATA */ return "Invalid data";
			case 14: /* ERROR_OUTOFMEMORY */ return "Out of memory";
			case 17: /* ERROR_NOT_SAME_DEVICE */ return "Not same device";
			case 18: /* ERROR_NO_MORE_FILES */ return "No more files";
			case 24: /* ERROR_BAD_LENGTH */ return "Bad length";
			case 31: /* ERROR_GEN_FAILURE */ return "General failure";
			case 32: /* ERROR_SHARING_VIOLATION */ return "Sharing violation";
			case 33: /* ERROR_LOCK_VIOLATION */ return "Lock violation";
			case 50: /* ERROR_NOT_SUPPORTED */ return "Operation not supported";
			case 55: /* ERROR_DEV_NOT_EXIST */ return "Device does not exist";
			case 87: /* ERROR_INVALID_PARAMETER */ return "Invalid parameter";
			case 120: /* ERROR_CALL_NOT_IMPLEMENTED */ return "Call not implemented";
			case 123: /* ERROR_INVALID_NAME */ return "Invalid name";
			case 127: /* ERROR_PROC_NOT_FOUND */ return "Process not found";
			case 183: /* ERROR_ALREADY_EXISTS */ return "Already exists";
			case 267: /* ERROR_DIRECTORY */ return "Is a directory";
			case 995: /* ERROR_OPERATION_ABORTED */ return "Operation aborted";
			case 6000: /* ERROR_ENCRYPTION_FAILED */ return "Encryption failed";
			case 10004: /* WSAEINTR */ return "interrupted";
			case 10009: /* WSAEBADF */ return "Bad file number";
			case 10013: /* WSAEACCES */ return "Access denied";
			case 10014: /* WSAEFAULT */ return "Bad address";
			case 10022: /* WSAEINVAL */ return "Invalid arguments";
			case 10024: /* WSAEMFILE */ return "Too many open files";
			case 10035: /* WSAEWOULDBLOCK */ return "Operation on non-blocking socket would block";
			case 10036: /* WSAEINPROGRESS */ return "Operation in progress";
			case 10037: /* WSAEALREADY */ return "Operation already in progress";
			case 10038: /* WSAENOTSOCK */ return "The descriptor is not a socket";
			case 10039: /* WSAEDESTADDRREQ */ return "Destination address required";
			case 10040: /* WSAEMSGSIZE */ return "Message too long";
			case 10041: /* WSAEPROTOTYPE */ return "Protocol wrong type for socket";
			case 10042: /* WSAENOPROTOOPT */ return "Protocol option not supported";
			case 10043: /* WSAEPROTONOSUPPORT */ return "Protocol not supported";
			case 10044: /* WSAESOCKTNOSUPPORT */ return "Socket not supported";
			case 10045: /* WSAEOPNOTSUPP */ return "Operation not supported";
			case 10046: /* WSAEPFNOSUPPORT */ return "Protocol family not supported";
			case 10047: /* WSAEAFNOSUPPORT */ return "An address incompatible with the requested protocol was used";
			case 10048: /* WSAEADDRINUSE */ return "Address already in use";
			case 10049: /* WSAEADDRNOTAVAIL */ return "The requested address is not valid in this context";
			case 10050: /* WSAENETDOWN */ return "Network subsystem is down";
			case 10051: /* WSAENETUNREACH */ return "Network is unreachable";
			case 10052: /* WSAENETRESET */ return "Connection broken, keep-alive detected a problem";
			case 10053: /* WSAECONNABORTED */ return "An established connection was aborted in your host machine.";
			case 10054: /* WSAECONNRESET */ return "Connection reset by peer";
			case 10055: /* WSAENOBUFS */ return "Not enough buffer space is available";
			case 10056: /* WSAEISCONN */ return "Socket is already connected";
			case 10057: /* WSAENOTCONN */ return "The socket is not connected";
			case 10058: /* WSAESHUTDOWN */ return "The socket has been shut down";
			case 10059: /* WSAETOOMANYREFS */ return "Too many references: cannot splice";
			case 10060: /* WSAETIMEDOUT */ return "Connection timed out";
			case 10061: /* WSAECONNREFUSED */ return "Connection refused";
			case 10062: /* WSAELOOP */ return "Too many symbolic links encountered";
			case 10063: /* WSAENAMETOOLONG */ return "File name too long";
			case 10064: /* WSAEHOSTDOWN */ return "Host is down";
			case 10065: /* WSAEHOSTUNREACH */ return "No route to host";
			case 10066: /* WSAENOTEMPTY */ return "Directory not empty";
			case 10067: /* WSAEPROCLIM */ return "EPROCLIM";
			case 10068: /* WSAEUSERS */ return "Too many users";
			case 10069: /* WSAEDQUOT */ return "Quota exceeded";
			case 10070: /* WSAESTALE */ return "Stale NFS file handle";
			case 10071: /* WSAEREMOTE */ return "Object is remote";
			case 10091: /* WSASYSNOTREADY */ return "SYSNOTREADY";
			case 10092: /* WSAVERNOTSUPPORTED */ return "VERNOTSUPPORTED";
			case 10093: /* WSANOTINITIALISED */ return "Winsock not initialised";
			case 10101: /* WSAEDISCON */ return "EDISCON";
			case 10102: /* WSAENOMORE */ return "ENOMORE";
			case 10103: /* WSAECANCELLED */ return "Operation canceled";
			case 10104: /* WSAEINVALIDPROCTABLE */ return "EINVALIDPROCTABLE";
			case 10105: /* WSAEINVALIDPROVIDER */ return "EINVALIDPROVIDER";
			case 10106: /* WSAEPROVIDERFAILEDINIT */ return "EPROVIDERFAILEDINIT";
			case 10107: /* WSASYSCALLFAILURE */ return "System call failed";
			case 10108: /* WSASERVICE_NOT_FOUND */ return "SERVICE_NOT_FOUND";
			case 10109: /* WSATYPE_NOT_FOUND */ return "TYPE_NOT_FOUND";
			case 10112: /* WSAEREFUSED */ return "EREFUSED";
			case 11001: /* WSAHOST_NOT_FOUND */ return "No such host is known";
			case 11002: /* WSATRY_AGAIN */ return "A temporary error occurred on an authoritative name server.  Try again later.";
			case 11003: /* WSANO_RECOVERY */ return "No recovery";
			case 11004: /* WSANO_DATA */ return "No data";
#if !MOBILE
			case 1: /* ERROR_INVALID_FUNCTION */ return "Invalid function";
			case 7: /* ERROR_ARENA_TRASHED */ return "Arena trashed";
			case 8: /* ERROR_NOT_ENOUGH_MEMORY */ return "Not enough memory";
			case 9: /* ERROR_INVALID_BLOCK */ return "Invalid block";
			case 10: /* ERROR_BAD_ENVIRONMENT */ return "Bad environment";
			case 11: /* ERROR_BAD_FORMAT */ return "Bad format";
			case 12: /* ERROR_INVALID_ACCESS */ return "Invalid access";
			case 15: /* ERROR_INVALID_DRIVE */ return "Invalid drive";
			case 16: /* ERROR_CURRENT_DIRECTORY */ return "Current directory";
			case 19: /* ERROR_WRITE_PROTECT */ return "Write protect";
			case 20: /* ERROR_BAD_UNIT */ return "Bad unit";
			case 21: /* ERROR_NOT_READY */ return "Not ready";
			case 22: /* ERROR_BAD_COMMAND */ return "Bad command";
			case 23: /* ERROR_CRC */ return "CRC";
			case 25: /* ERROR_SEEK */ return "Seek";
			case 26: /* ERROR_NOT_DOS_DISK */ return "Not DOS disk";
			case 27: /* ERROR_SECTOR_NOT_FOUND */ return "Sector not found";
			case 28: /* ERROR_OUT_OF_PAPER */ return "Out of paper";
			case 29: /* ERROR_WRITE_FAULT */ return "Write fault";
			case 30: /* ERROR_READ_FAULT */ return "Read fault";
			case 34: /* ERROR_WRONG_DISK */ return "Wrong disk";
			case 36: /* ERROR_SHARING_BUFFER_EXCEEDED */ return "Sharing buffer exceeded";
			case 38: /* ERROR_HANDLE_EOF */ return "Handle EOF";
			case 39: /* ERROR_HANDLE_DISK_FULL */ return "Handle disk full";
			case 51: /* ERROR_REM_NOT_LIST */ return "Rem not list";
			case 52: /* ERROR_DUP_NAME */ return "Duplicate name";
			case 53: /* ERROR_BAD_NETPATH */ return "Bad netpath";
			case 54: /* ERROR_NETWORK_BUSY */ return "Network busy";
			case 56: /* ERROR_TOO_MANY_CMDS */ return "Too many commands";
			case 57: /* ERROR_ADAP_HDW_ERR */ return "ADAP HDW error";
			case 58: /* ERROR_BAD_NET_RESP */ return "Bad net response";
			case 59: /* ERROR_UNEXP_NET_ERR */ return "Unexpected net error";
			case 60: /* ERROR_BAD_REM_ADAP */ return "Bad rem adap";
			case 61: /* ERROR_PRINTQ_FULL */ return "Print queue full";
			case 62: /* ERROR_NO_SPOOL_SPACE */ return "No spool space";
			case 63: /* ERROR_PRINT_CANCELLED */ return "Print cancelled";
			case 64: /* ERROR_NETNAME_DELETED */ return "Netname deleted";
			case 65: /* ERROR_NETWORK_ACCESS_DENIED */ return "Network access denied";
			case 66: /* ERROR_BAD_DEV_TYPE */ return "Bad device type";
			case 67: /* ERROR_BAD_NET_NAME */ return "Bad net name";
			case 68: /* ERROR_TOO_MANY_NAMES */ return "Too many names";
			case 69: /* ERROR_TOO_MANY_SESS */ return "Too many sessions";
			case 70: /* ERROR_SHARING_PAUSED */ return "Sharing paused";
			case 71: /* ERROR_REQ_NOT_ACCEP */ return "Req not accep";
			case 72: /* ERROR_REDIR_PAUSED */ return "Redir paused";
			case 80: /* ERROR_FILE_EXISTS */ return "File exists";
			case 82: /* ERROR_CANNOT_MAKE */ return "Cannot make";
			case 83: /* ERROR_FAIL_I24 */ return "Fail i24";
			case 84: /* ERROR_OUT_OF_STRUCTURES */ return "Out of structures";
			case 85: /* ERROR_ALREADY_ASSIGNED */ return "Already assigned";
			case 86: /* ERROR_INVALID_PASSWORD */ return "Invalid password";
			case 88: /* ERROR_NET_WRITE_FAULT */ return "Net write fault";
			case 89: /* ERROR_NO_PROC_SLOTS */ return "No proc slots";
			case 100: /* ERROR_TOO_MANY_SEMAPHORES */ return "Too many semaphores";
			case 101: /* ERROR_EXCL_SEM_ALREADY_OWNED */ return "Exclusive semaphore already owned";
			case 102: /* ERROR_SEM_IS_SET */ return "Semaphore is set";
			case 103: /* ERROR_TOO_MANY_SEM_REQUESTS */ return "Too many semaphore requests";
			case 104: /* ERROR_INVALID_AT_INTERRUPT_TIME */ return "Invalid at interrupt time";
			case 105: /* ERROR_SEM_OWNER_DIED */ return "Semaphore owner died";
			case 106: /* ERROR_SEM_USER_LIMIT */ return "Semaphore user limit";
			case 107: /* ERROR_DISK_CHANGE */ return "Disk change";
			case 108: /* ERROR_DRIVE_LOCKED */ return "Drive locked";
			case 109: /* ERROR_BROKEN_PIPE */ return "Broken pipe";
			case 110: /* ERROR_OPEN_FAILED */ return "Open failed";
			case 111: /* ERROR_BUFFER_OVERFLOW */ return "Buffer overflow";
			case 112: /* ERROR_DISK_FULL */ return "Disk full";
			case 113: /* ERROR_NO_MORE_SEARCH_HANDLES */ return "No more search handles";
			case 114: /* ERROR_INVALID_TARGET_HANDLE */ return "Invalid target handle";
			case 117: /* ERROR_INVALID_CATEGORY */ return "Invalid category";
			case 118: /* ERROR_INVALID_VERIFY_SWITCH */ return "Invalid verify switch";
			case 119: /* ERROR_BAD_DRIVER_LEVEL */ return "Bad driver level";
			case 121: /* ERROR_SEM_TIMEOUT */ return "Semaphore timeout";
			case 122: /* ERROR_INSUFFICIENT_BUFFER */ return "Insufficient buffer";
			case 124: /* ERROR_INVALID_LEVEL */ return "Invalid level";
			case 125: /* ERROR_NO_VOLUME_LABEL */ return "No volume label";
			case 126: /* ERROR_MOD_NOT_FOUND */ return "Module not found";
			case 128: /* ERROR_WAIT_NO_CHILDREN */ return "Wait no children";
			case 129: /* ERROR_CHILD_NOT_COMPLETE */ return "Child not complete";
			case 130: /* ERROR_DIRECT_ACCESS_HANDLE */ return "Direct access handle";
			case 131: /* ERROR_NEGATIVE_SEEK */ return "Negative seek";
			case 132: /* ERROR_SEEK_ON_DEVICE */ return "Seek on device";
			case 133: /* ERROR_IS_JOIN_TARGET */ return "Is join target";
			case 134: /* ERROR_IS_JOINED */ return "Is joined";
			case 135: /* ERROR_IS_SUBSTED */ return "Is substed";
			case 136: /* ERROR_NOT_JOINED */ return "Not joined";
			case 137: /* ERROR_NOT_SUBSTED */ return "Not substed";
			case 138: /* ERROR_JOIN_TO_JOIN */ return "Join to join";
			case 139: /* ERROR_SUBST_TO_SUBST */ return "Subst to subst";
			case 140: /* ERROR_JOIN_TO_SUBST */ return "Join to subst";
			case 141: /* ERROR_SUBST_TO_JOIN */ return "Subst to join";
			case 142: /* ERROR_BUSY_DRIVE */ return "Busy drive";
			case 143: /* ERROR_SAME_DRIVE */ return "Same drive";
			case 144: /* ERROR_DIR_NOT_ROOT */ return "Directory not root";
			case 145: /* ERROR_DIR_NOT_EMPTY */ return "Directory not empty";
			case 146: /* ERROR_IS_SUBST_PATH */ return "Is subst path";
			case 147: /* ERROR_IS_JOIN_PATH */ return "Is join path";
			case 148: /* ERROR_PATH_BUSY */ return "Path busy";
			case 149: /* ERROR_IS_SUBST_TARGET */ return "Is subst target";
			case 150: /* ERROR_SYSTEM_TRACE */ return "System trace";
			case 151: /* ERROR_INVALID_EVENT_COUNT */ return "Invalid event count";
			case 152: /* ERROR_TOO_MANY_MUXWAITERS */ return "Too many muxwaiters";
			case 153: /* ERROR_INVALID_LIST_FORMAT */ return "Invalid list format";
			case 154: /* ERROR_LABEL_TOO_LONG */ return "Label too long";
			case 155: /* ERROR_TOO_MANY_TCBS */ return "Too many TCBs";
			case 156: /* ERROR_SIGNAL_REFUSED */ return "Signal refused";
			case 157: /* ERROR_DISCARDED */ return "Discarded";
			case 158: /* ERROR_NOT_LOCKED */ return "Not locked";
			case 159: /* ERROR_BAD_THREADID_ADDR */ return "Bad thread ID addr";
			case 160: /* ERROR_BAD_ARGUMENTS */ return "Bad arguments";
			case 161: /* ERROR_BAD_PATHNAME */ return "Bad pathname";
			case 162: /* ERROR_SIGNAL_PENDING */ return "Signal pending";
			case 164: /* ERROR_MAX_THRDS_REACHED */ return "Max thrds reached";
			case 167: /* ERROR_LOCK_FAILED */ return "Lock failed";
			case 170: /* ERROR_BUSY */ return "Busy";
			case 173: /* ERROR_CANCEL_VIOLATION */ return "Cancel violation";
			case 174: /* ERROR_ATOMIC_LOCKS_NOT_SUPPORTED */ return "Atomic locks not supported";
			case 180: /* ERROR_INVALID_SEGMENT_NUMBER */ return "Invalid segment number";
			case 182: /* ERROR_INVALID_ORDINAL */ return "Invalid ordinal";
			case 186: /* ERROR_INVALID_FLAG_NUMBER */ return "Invalid flag number";
			case 187: /* ERROR_SEM_NOT_FOUND */ return "Sem not found";
			case 188: /* ERROR_INVALID_STARTING_CODESEG */ return "Invalid starting codeseg";
			case 189: /* ERROR_INVALID_STACKSEG */ return "Invalid stackseg";
			case 190: /* ERROR_INVALID_MODULETYPE */ return "Invalid moduletype";
			case 191: /* ERROR_INVALID_EXE_SIGNATURE */ return "Invalid exe signature";
			case 192: /* ERROR_EXE_MARKED_INVALID */ return "Exe marked invalid";
			case 193: /* ERROR_BAD_EXE_FORMAT */ return "Bad exe format";
			case 194: /* ERROR_ITERATED_DATA_EXCEEDS_64k */ return "Iterated data exceeds 64k (and that should be enough for anybody!)";
			case 195: /* ERROR_INVALID_MINALLOCSIZE */ return "Invalid minallocsize";
			case 196: /* ERROR_DYNLINK_FROM_INVALID_RING */ return "Dynlink from invalid ring";
			case 197: /* ERROR_IOPL_NOT_ENABLED */ return "IOPL not enabled";
			case 198: /* ERROR_INVALID_SEGDPL */ return "Invalid segdpl";
			case 199: /* ERROR_AUTODATASEG_EXCEEDS_64k */ return "Autodataseg exceeds 64k";
			case 200: /* ERROR_RING2SEG_MUST_BE_MOVABLE */ return "Ring2seg must be movable";
			case 201: /* ERROR_RELOC_CHAIN_XEEDS_SEGLIM */ return "Reloc chain exceeds seglim";
			case 202: /* ERROR_INFLOOP_IN_RELOC_CHAIN */ return "Infloop in reloc chain";
			case 203: /* ERROR_ENVVAR_NOT_FOUND */ return "Env var not found";
			case 205: /* ERROR_NO_SIGNAL_SENT */ return "No signal sent";
			case 206: /* ERROR_FILENAME_EXCED_RANGE */ return "Filename exceeds range";
			case 207: /* ERROR_RING2_STACK_IN_USE */ return "Ring2 stack in use";
			case 208: /* ERROR_META_EXPANSION_TOO_LONG */ return "Meta expansion too long";
			case 209: /* ERROR_INVALID_SIGNAL_NUMBER */ return "Invalid signal number";
			case 210: /* ERROR_THREAD_1_INACTIVE */ return "Thread 1 inactive";
			case 212: /* ERROR_LOCKED */ return "Locked";
			case 214: /* ERROR_TOO_MANY_MODULES */ return "Too many modules";
			case 215: /* ERROR_NESTING_NOT_ALLOWED */ return "Nesting not allowed";
			case 216: /* ERROR_EXE_MACHINE_TYPE_MISMATCH */ return "Exe machine type mismatch";
			case 230: /* ERROR_BAD_PIPE */ return "Bad pipe";
			case 231: /* ERROR_PIPE_BUSY */ return "Pipe busy";
			case 232: /* ERROR_NO_DATA */ return "No data";
			case 233: /* ERROR_PIPE_NOT_CONNECTED */ return "Pipe not connected";
			case 234: /* ERROR_MORE_DATA */ return "More data";
			case 240: /* ERROR_VC_DISCONNECTED */ return "VC disconnected";
			case 254: /* ERROR_INVALID_EA_NAME */ return "Invalid EA name";
			case 255: /* ERROR_EA_LIST_INCONSISTENT */ return "EA list inconsistent";
			case 258: /* WAIT_TIMEOUT */ return "Wait timeout";
			case 259: /* ERROR_NO_MORE_ITEMS */ return "No more items";
			case 266: /* ERROR_CANNOT_COPY */ return "Cannot copy";
			case 275: /* ERROR_EAS_DIDNT_FIT */ return "EAS didnt fit";
			case 276: /* ERROR_EA_FILE_CORRUPT */ return "EA file corrupt";
			case 277: /* ERROR_EA_TABLE_FULL */ return "EA table full";
			case 278: /* ERROR_INVALID_EA_HANDLE */ return "Invalid EA handle";
			case 282: /* ERROR_EAS_NOT_SUPPORTED */ return "EAs not supported";
			case 288: /* ERROR_NOT_OWNER */ return "Not owner";
			case 298: /* ERROR_TOO_MANY_POSTS */ return "Too many posts";
			case 299: /* ERROR_PARTIAL_COPY */ return "Partial copy";
			case 300: /* ERROR_OPLOCK_NOT_GRANTED */ return "Oplock not granted";
			case 301: /* ERROR_INVALID_OPLOCK_PROTOCOL */ return "Invalid oplock protocol";
			case 302: /* ERROR_DISK_TOO_FRAGMENTED */ return "Disk too fragmented";
			case 303: /* ERROR_DELETE_PENDING */ return "Delete pending";
			case 317: /* ERROR_MR_MID_NOT_FOUND */ return "Mr Mid not found";
			case 487: /* ERROR_INVALID_ADDRESS */ return "Invalid address";
			case 534: /* ERROR_ARITHMETIC_OVERFLOW */ return "Arithmetic overflow";
			case 535: /* ERROR_PIPE_CONNECTED */ return "Pipe connected";
			case 536: /* ERROR_PIPE_LISTENING */ return "Pipe listening";
			case 994: /* ERROR_EA_ACCESS_DENIED */ return "EA access denied";
			case 996: /* ERROR_IO_INCOMPLETE */ return "IO incomplete";
			case 997: /* ERROR_IO_PENDING */ return "IO pending";
			case 998: /* ERROR_NOACCESS */ return "No access";
			case 999: /* ERROR_SWAPERROR */ return "Swap error";
			case 1001: /* ERROR_STACK_OVERFLOW */ return "Stack overflow";
			case 1002: /* ERROR_INVALID_MESSAGE */ return "Invalid message";
			case 1003: /* ERROR_CAN_NOT_COMPLETE */ return "Can not complete";
			case 1004: /* ERROR_INVALID_FLAGS */ return "Invalid flags";
			case 1005: /* ERROR_UNRECOGNIZED_VOLUME */ return "Unrecognised volume";
			case 1006: /* ERROR_FILE_INVALID */ return "File invalid";
			case 1007: /* ERROR_FULLSCREEN_MODE */ return "Full screen mode";
			case 1008: /* ERROR_NO_TOKEN */ return "No token";
			case 1009: /* ERROR_BADDB */ return "Bad DB";
			case 1010: /* ERROR_BADKEY */ return "Bad key";
			case 1011: /* ERROR_CANTOPEN */ return "Can't open";
			case 1012: /* ERROR_CANTREAD */ return "Can't read";
			case 1013: /* ERROR_CANTWRITE */ return "Can't write";
			case 1014: /* ERROR_REGISTRY_RECOVERED */ return "Registry recovered";
			case 1015: /* ERROR_REGISTRY_CORRUPT */ return "Registry corrupt";
			case 1016: /* ERROR_REGISTRY_IO_FAILED */ return "Registry IO failed";
			case 1017: /* ERROR_NOT_REGISTRY_FILE */ return "Not registry file";
			case 1018: /* ERROR_KEY_DELETED */ return "Key deleted";
			case 1019: /* ERROR_NO_LOG_SPACE */ return "No log space";
			case 1020: /* ERROR_KEY_HAS_CHILDREN */ return "Key has children";
			case 1021: /* ERROR_CHILD_MUST_BE_VOLATILE */ return "Child must be volatile";
			case 1022: /* ERROR_NOTIFY_ENUM_DIR */ return "Notify enum dir";
			case 1051: /* ERROR_DEPENDENT_SERVICES_RUNNING */ return "Dependent services running";
			case 1052: /* ERROR_INVALID_SERVICE_CONTROL */ return "Invalid service control";
			case 1053: /* ERROR_SERVICE_REQUEST_TIMEOUT */ return "Service request timeout";
			case 1054: /* ERROR_SERVICE_NO_THREAD */ return "Service no thread";
			case 1055: /* ERROR_SERVICE_DATABASE_LOCKED */ return "Service database locked";
			case 1056: /* ERROR_SERVICE_ALREADY_RUNNING */ return "Service already running";
			case 1057: /* ERROR_INVALID_SERVICE_ACCOUNT */ return "Invalid service account";
			case 1058: /* ERROR_SERVICE_DISABLED */ return "Service disabled";
			case 1059: /* ERROR_CIRCULAR_DEPENDENCY */ return "Circular dependency";
			case 1060: /* ERROR_SERVICE_DOES_NOT_EXIST */ return "Service does not exist";
			case 1061: /* ERROR_SERVICE_CANNOT_ACCEPT_CTRL */ return "Service cannot accept ctrl";
			case 1062: /* ERROR_SERVICE_NOT_ACTIVE */ return "Service not active";
			case 1063: /* ERROR_FAILED_SERVICE_CONTROLLER_CONNECT */ return "Failed service controller connect";
			case 1064: /* ERROR_EXCEPTION_IN_SERVICE */ return "Exception in service";
			case 1065: /* ERROR_DATABASE_DOES_NOT_EXIST */ return "Database does not exist";
			case 1066: /* ERROR_SERVICE_SPECIFIC_ERROR */ return "Service specific error";
			case 1067: /* ERROR_PROCESS_ABORTED */ return "Process aborted";
			case 1068: /* ERROR_SERVICE_DEPENDENCY_FAIL */ return "Service dependency fail";
			case 1069: /* ERROR_SERVICE_LOGON_FAILED */ return "Service logon failed";
			case 1070: /* ERROR_SERVICE_START_HANG */ return "Service start hang";
			case 1071: /* ERROR_INVALID_SERVICE_LOCK */ return "Invalid service lock";
			case 1072: /* ERROR_SERVICE_MARKED_FOR_DELETE */ return "Service marked for delete";
			case 1073: /* ERROR_SERVICE_EXISTS */ return "Service exists";
			case 1074: /* ERROR_ALREADY_RUNNING_LKG */ return "Already running lkg";
			case 1075: /* ERROR_SERVICE_DEPENDENCY_DELETED */ return "Service dependency deleted";
			case 1076: /* ERROR_BOOT_ALREADY_ACCEPTED */ return "Boot already accepted";
			case 1077: /* ERROR_SERVICE_NEVER_STARTED */ return "Service never started";
			case 1078: /* ERROR_DUPLICATE_SERVICE_NAME */ return "Duplicate service name";
			case 1079: /* ERROR_DIFFERENT_SERVICE_ACCOUNT */ return "Different service account";
			case 1080: /* ERROR_CANNOT_DETECT_DRIVER_FAILURE */ return "Cannot detect driver failure";
			case 1081: /* ERROR_CANNOT_DETECT_PROCESS_ABORT */ return "Cannot detect process abort";
			case 1082: /* ERROR_NO_RECOVERY_PROGRAM */ return "No recovery program";
			case 1083: /* ERROR_SERVICE_NOT_IN_EXE */ return "Service not in exe";
			case 1084: /* ERROR_NOT_SAFEBOOT_SERVICE */ return "Not safeboot service";
			case 1100: /* ERROR_END_OF_MEDIA */ return "End of media";
			case 1101: /* ERROR_FILEMARK_DETECTED */ return "Filemark detected";
			case 1102: /* ERROR_BEGINNING_OF_MEDIA */ return "Beginning of media";
			case 1103: /* ERROR_SETMARK_DETECTED */ return "Setmark detected";
			case 1104: /* ERROR_NO_DATA_DETECTED */ return "No data detected";
			case 1105: /* ERROR_PARTITION_FAILURE */ return "Partition failure";
			case 1106: /* ERROR_INVALID_BLOCK_LENGTH */ return "Invalid block length";
			case 1107: /* ERROR_DEVICE_NOT_PARTITIONED */ return "Device not partitioned";
			case 1108: /* ERROR_UNABLE_TO_LOCK_MEDIA */ return "Unable to lock media";
			case 1109: /* ERROR_UNABLE_TO_UNLOAD_MEDIA */ return "Unable to unload media";
			case 1110: /* ERROR_MEDIA_CHANGED */ return "Media changed";
			case 1111: /* ERROR_BUS_RESET */ return "Bus reset";
			case 1112: /* ERROR_NO_MEDIA_IN_DRIVE */ return "No media in drive";
			case 1113: /* ERROR_NO_UNICODE_TRANSLATION */ return "No unicode translation";
			case 1114: /* ERROR_DLL_INIT_FAILED */ return "DLL init failed";
			case 1115: /* ERROR_SHUTDOWN_IN_PROGRESS */ return "Shutdown in progress";
			case 1116: /* ERROR_NO_SHUTDOWN_IN_PROGRESS */ return "No shutdown in progress";
			case 1117: /* ERROR_IO_DEVICE */ return "IO device";
			case 1118: /* ERROR_SERIAL_NO_DEVICE */ return "Serial IO device";
			case 1119: /* ERROR_IRQ_BUSY */ return "IRQ busy";
			case 1120: /* ERROR_MORE_WRITES */ return "More writes";
			case 1121: /* ERROR_COUNTER_TIMEOUT */ return "Counter timeout";
			case 1122: /* ERROR_FLOPPY_ID_MARK_NOT_FOUND */ return "Floppy ID mark not found";
			case 1123: /* ERROR_FLOPPY_WRONG_CYLINDER */ return "Floppy wrong cylinder";
			case 1124: /* ERROR_FLOPPY_UNKNOWN_ERROR */ return "Floppy unknown error";
			case 1125: /* ERROR_FLOPPY_BAD_REGISTERS */ return "Floppy bad registers";
			case 1126: /* ERROR_DISK_RECALIBRATE_FAILED */ return "Disk recalibrate failed";
			case 1127: /* ERROR_DISK_OPERATION_FAILED */ return "Disk operation failed";
			case 1128: /* ERROR_DISK_RESET_FAILED */ return "Disk reset failed";
			case 1129: /* ERROR_EOM_OVERFLOW */ return "EOM overflow";
			case 1130: /* ERROR_NOT_ENOUGH_SERVER_MEMORY */ return "Not enough server memory";
			case 1131: /* ERROR_POSSIBLE_DEADLOCK */ return "Possible deadlock";
			case 1132: /* ERROR_MAPPED_ALIGNMENT */ return "Mapped alignment";
			case 1140: /* ERROR_SET_POWER_STATE_VETOED */ return "Set power state vetoed";
			case 1141: /* ERROR_SET_POWER_STATE_FAILED */ return "Set power state failed";
			case 1142: /* ERROR_TOO_MANY_LINKS */ return "Too many links";
			case 1150: /* ERROR_OLD_WIN_VERSION */ return "Old win version";
			case 1151: /* ERROR_APP_WRONG_OS */ return "App wrong OS";
			case 1152: /* ERROR_SINGLE_INSTANCE_APP */ return "Single instance app";
			case 1153: /* ERROR_RMODE_APP */ return "Rmode app";
			case 1154: /* ERROR_INVALID_DLL */ return "Invalid DLL";
			case 1155: /* ERROR_NO_ASSOCIATION */ return "No association";
			case 1156: /* ERROR_DDE_FAIL */ return "DDE fail";
			case 1157: /* ERROR_DLL_NOT_FOUND */ return "DLL not found";
			case 1158: /* ERROR_NO_MORE_USER_HANDLES */ return "No more user handles";
			case 1159: /* ERROR_MESSAGE_SYNC_ONLY */ return "Message sync only";
			case 1160: /* ERROR_SOURCE_ELEMENT_EMPTY */ return "Source element empty";
			case 1161: /* ERROR_DESTINATION_ELEMENT_FULL */ return "Destination element full";
			case 1162: /* ERROR_ILLEGAL_ELEMENT_ADDRESS */ return "Illegal element address";
			case 1163: /* ERROR_MAGAZINE_NOT_PRESENT */ return "Magazine not present";
			case 1164: /* ERROR_DEVICE_REINITIALIZATION_NEEDED */ return "Device reinitialization needed";
			case 1165: /* ERROR_DEVICE_REQUIRES_CLEANING */ return "Device requires cleaning";
			case 1166: /* ERROR_DEVICE_DOOR_OPEN */ return "Device door open";
			case 1167: /* ERROR_DEVICE_NOT_CONNECTED */ return "Device not connected";
			case 1168: /* ERROR_NOT_FOUND */ return "Not found";
			case 1169: /* ERROR_NO_MATCH */ return "No match";
			case 1170: /* ERROR_SET_NOT_FOUND */ return "Set not found";
			case 1171: /* ERROR_POINT_NOT_FOUND */ return "Point not found";
			case 1172: /* ERROR_NO_TRACKING_SERVICE */ return "No tracking service";
			case 1173: /* ERROR_NO_VOLUME_ID */ return "No volume ID";
			case 1175: /* ERROR_UNABLE_TO_REMOVE_REPLACED */ return "Unable to remove replaced";
			case 1176: /* ERROR_UNABLE_TO_MOVE_REPLACEMENT */ return "Unable to move replacement";
			case 1177: /* ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 */ return "Unable to move replacement 2";
			case 1178: /* ERROR_JOURNAL_DELETE_IN_PROGRESS */ return "Journal delete in progress";
			case 1179: /* ERROR_JOURNAL_NOT_ACTIVE */ return "Journal not active";
			case 1180: /* ERROR_POTENTIAL_FILE_FOUND */ return "Potential file found";
			case 1181: /* ERROR_JOURNAL_ENTRY_DELETED */ return "Journal entry deleted";
			case 1200: /* ERROR_BAD_DEVICE */ return "Bad device";
			case 1201: /* ERROR_CONNECTION_UNAVAIL */ return "Connection unavail";
			case 1202: /* ERROR_DEVICE_ALREADY_REMEMBERED */ return "Device already remembered";
			case 1203: /* ERROR_NO_NET_OR_BAD_PATH */ return "No net or bad path";
			case 1204: /* ERROR_BAD_PROVIDER */ return "Bad provider";
			case 1205: /* ERROR_CANNOT_OPEN_PROFILE */ return "Cannot open profile";
			case 1206: /* ERROR_BAD_PROFILE */ return "Bad profile";
			case 1207: /* ERROR_NOT_CONTAINER */ return "Not container";
			case 1208: /* ERROR_EXTENDED_ERROR */ return "Extended error";
			case 1209: /* ERROR_INVALID_GROUPNAME */ return "Invalid group name";
			case 1210: /* ERROR_INVALID_COMPUTERNAME */ return "Invalid computer name";
			case 1211: /* ERROR_INVALID_EVENTNAME */ return "Invalid event name";
			case 1212: /* ERROR_INVALID_DOMAINNAME */ return "Invalid domain name";
			case 1213: /* ERROR_INVALID_SERVICENAME */ return "Invalid service name";
			case 1214: /* ERROR_INVALID_NETNAME */ return "Invalid net name";
			case 1215: /* ERROR_INVALID_SHARENAME */ return "Invalid share name";
			case 1216: /* ERROR_INVALID_PASSWORDNAME */ return "Invalid password name";
			case 1217: /* ERROR_INVALID_MESSAGENAME */ return "Invalid message name";
			case 1218: /* ERROR_INVALID_MESSAGEDEST */ return "Invalid message dest";
			case 1219: /* ERROR_SESSION_CREDENTIAL_CONFLICT */ return "Session credential conflict";
			case 1220: /* ERROR_REMOTE_SESSION_LIMIT_EXCEEDED */ return "Remote session limit exceeded";
			case 1221: /* ERROR_DUP_DOMAINNAME */ return "Dup domain name";
			case 1222: /* ERROR_NO_NETWORK */ return "No network";
			case 1223: /* ERROR_CANCELLED */ return "Cancelled";
			case 1224: /* ERROR_USER_MAPPED_FILE */ return "User mapped file";
			case 1225: /* ERROR_CONNECTION_REFUSED */ return "Connection refused";
			case 1226: /* ERROR_GRACEFUL_DISCONNECT */ return "Graceful disconnect";
			case 1227: /* ERROR_ADDRESS_ALREADY_ASSOCIATED */ return "Address already associated";
			case 1228: /* ERROR_ADDRESS_NOT_ASSOCIATED */ return "Address not associated";
			case 1229: /* ERROR_CONNECTION_INVALID */ return "Connected invalid";
			case 1230: /* ERROR_CONNECTION_ACTIVE */ return "Connection active";
			case 1231: /* ERROR_NETWORK_UNREACHABLE */ return "Network unreachable";
			case 1232: /* ERROR_HOST_UNREACHABLE */ return "Host unreachable";
			case 1233: /* ERROR_PROTOCOL_UNREACHABLE */ return "Protocol unreachable";
			case 1234: /* ERROR_PORT_UNREACHABLE */ return "Port unreachable";
			case 1235: /* ERROR_REQUEST_ABORTED */ return "Request aborted";
			case 1236: /* ERROR_CONNECTION_ABORTED */ return "Connection aborted";
			case 1237: /* ERROR_RETRY */ return "Retry";
			case 1238: /* ERROR_CONNECTION_COUNT_LIMIT */ return "Connection count limit";
			case 1239: /* ERROR_LOGIN_TIME_RESTRICTION */ return "Login time restriction";
			case 1240: /* ERROR_LOGIN_WKSTA_RESTRICTION */ return "Login wksta restriction";
			case 1241: /* ERROR_INCORRECT_ADDRESS */ return "Incorrect address";
			case 1242: /* ERROR_ALREADY_REGISTERED */ return "Already registered";
			case 1243: /* ERROR_SERVICE_NOT_FOUND */ return "Service not found";
			case 1244: /* ERROR_NOT_AUTHENTICATED */ return "Not authenticated";
			case 1245: /* ERROR_NOT_LOGGED_ON */ return "Not logged on";
			case 1246: /* ERROR_CONTINUE */ return "Continue";
			case 1247: /* ERROR_ALREADY_INITIALIZED */ return "Already initialised";
			case 1248: /* ERROR_NO_MORE_DEVICES */ return "No more devices";
			case 1249: /* ERROR_NO_SUCH_SITE */ return "No such site";
			case 1250: /* ERROR_DOMAIN_CONTROLLER_EXISTS */ return "Domain controller exists";
			case 1251: /* ERROR_ONLY_IF_CONNECTED */ return "Only if connected";
			case 1252: /* ERROR_OVERRIDE_NOCHANGES */ return "Override no changes";
			case 1253: /* ERROR_BAD_USER_PROFILE */ return "Bad user profile";
			case 1254: /* ERROR_NOT_SUPPORTED_ON_SBS */ return "Not supported on SBS";
			case 1255: /* ERROR_SERVER_SHUTDOWN_IN_PROGRESS */ return "Server shutdown in progress";
			case 1256: /* ERROR_HOST_DOWN */ return "Host down";
			case 1257: /* ERROR_NON_ACCOUNT_SID */ return "Non account sid";
			case 1258: /* ERROR_NON_DOMAIN_SID */ return "Non domain sid";
			case 1259: /* ERROR_APPHELP_BLOCK */ return "Apphelp block";
			case 1260: /* ERROR_ACCESS_DISABLED_BY_POLICY */ return "Access disabled by policy";
			case 1261: /* ERROR_REG_NAT_CONSUMPTION */ return "Reg nat consumption";
			case 1262: /* ERROR_CSCSHARE_OFFLINE */ return "CSC share offline";
			case 1263: /* ERROR_PKINIT_FAILURE */ return "PK init failure";
			case 1264: /* ERROR_SMARTCARD_SUBSYSTEM_FAILURE */ return "Smartcard subsystem failure";
			case 1265: /* ERROR_DOWNGRADE_DETECTED */ return "Downgrade detected";
			case 1266: /* SEC_E_SMARTCARD_CERT_REVOKED */ return "Smartcard cert revoked";
			case 1267: /* SEC_E_ISSUING_CA_UNTRUSTED */ return "Issuing CA untrusted";
			case 1268: /* SEC_E_REVOCATION_OFFLINE_C */ return "Revocation offline";
			case 1269: /* SEC_E_PKINIT_CLIENT_FAILUR */ return "PK init client failure";
			case 1270: /* SEC_E_SMARTCARD_CERT_EXPIRED */ return "Smartcard cert expired";
			case 1271: /* ERROR_MACHINE_LOCKED */ return "Machine locked";
			case 1273: /* ERROR_CALLBACK_SUPPLIED_INVALID_DATA */ return "Callback supplied invalid data";
			case 1274: /* ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED */ return "Sync foreground refresh required";
			case 1275: /* ERROR_DRIVER_BLOCKED */ return "Driver blocked";
			case 1276: /* ERROR_INVALID_IMPORT_OF_NON_DLL */ return "Invalid import of non DLL";
			case 1300: /* ERROR_NOT_ALL_ASSIGNED */ return "Not all assigned";
			case 1301: /* ERROR_SOME_NOT_MAPPED */ return "Some not mapped";
			case 1302: /* ERROR_NO_QUOTAS_FOR_ACCOUNT */ return "No quotas for account";
			case 1303: /* ERROR_LOCAL_USER_SESSION_KEY */ return "Local user session key";
			case 1304: /* ERROR_NULL_LM_PASSWORD */ return "Null LM password";
			case 1305: /* ERROR_UNKNOWN_REVISION */ return "Unknown revision";
			case 1306: /* ERROR_REVISION_MISMATCH */ return "Revision mismatch";
			case 1307: /* ERROR_INVALID_OWNER */ return "Invalid owner";
			case 1308: /* ERROR_INVALID_PRIMARY_GROUP */ return "Invalid primary group";
			case 1309: /* ERROR_NO_IMPERSONATION_TOKEN */ return "No impersonation token";
			case 1310: /* ERROR_CANT_DISABLE_MANDATORY */ return "Can't disable mandatory";
			case 1311: /* ERROR_NO_LOGON_SERVERS */ return "No logon servers";
			case 1312: /* ERROR_NO_SUCH_LOGON_SESSION */ return "No such logon session";
			case 1313: /* ERROR_NO_SUCH_PRIVILEGE */ return "No such privilege";
			case 1314: /* ERROR_PRIVILEGE_NOT_HELD */ return "Privilege not held";
			case 1315: /* ERROR_INVALID_ACCOUNT_NAME */ return "Invalid account name";
			case 1316: /* ERROR_USER_EXISTS */ return "User exists";
			case 1317: /* ERROR_NO_SUCH_USER */ return "No such user";
			case 1318: /* ERROR_GROUP_EXISTS */ return "Group exists";
			case 1319: /* ERROR_NO_SUCH_GROUP */ return "No such group";
			case 1320: /* ERROR_MEMBER_IN_GROUP */ return "Member in group";
			case 1321: /* ERROR_MEMBER_NOT_IN_GROUP */ return "Member not in group";
			case 1322: /* ERROR_LAST_ADMIN */ return "Last admin";
			case 1323: /* ERROR_WRONG_PASSWORD */ return "Wrong password";
			case 1324: /* ERROR_ILL_FORMED_PASSWORD */ return "Ill formed password";
			case 1325: /* ERROR_PASSWORD_RESTRICTION */ return "Password restriction";
			case 1326: /* ERROR_LOGON_FAILURE */ return "Logon failure";
			case 1327: /* ERROR_ACCOUNT_RESTRICTION */ return "Account restriction";
			case 1328: /* ERROR_INVALID_LOGON_HOURS */ return "Invalid logon hours";
			case 1329: /* ERROR_INVALID_WORKSTATION */ return "Invalid workstation";
			case 1330: /* ERROR_PASSWORD_EXPIRED */ return "Password expired";
			case 1331: /* ERROR_ACCOUNT_DISABLED */ return "Account disabled";
			case 1332: /* ERROR_NONE_MAPPED */ return "None mapped";
			case 1333: /* ERROR_TOO_MANY_LUIDS_REQUESTED */ return "Too many LUIDs requested";
			case 1334: /* ERROR_LUIDS_EXHAUSTED */ return "LUIDs exhausted";
			case 1335: /* ERROR_INVALID_SUB_AUTHORITY */ return "Invalid sub authority";
			case 1336: /* ERROR_INVALID_ACL */ return "Invalid ACL";
			case 1337: /* ERROR_INVALID_SID */ return "Invalid SID";
			case 1338: /* ERROR_INVALID_SECURITY_DESCR */ return "Invalid security descr";
			case 1340: /* ERROR_BAD_INHERITANCE_ACL */ return "Bad inheritance ACL";
			case 1341: /* ERROR_SERVER_DISABLED */ return "Server disabled";
			case 1342: /* ERROR_SERVER_NOT_DISABLED */ return "Server not disabled";
			case 1343: /* ERROR_INVALID_ID_AUTHORITY */ return "Invalid ID authority";
			case 1344: /* ERROR_ALLOTTED_SPACE_EXCEEDED */ return "Allotted space exceeded";
			case 1345: /* ERROR_INVALID_GROUP_ATTRIBUTES */ return "Invalid group attributes";
			case 1346: /* ERROR_BAD_IMPERSONATION_LEVEL */ return "Bad impersonation level";
			case 1347: /* ERROR_CANT_OPEN_ANONYMOUS */ return "Can't open anonymous";
			case 1348: /* ERROR_BAD_VALIDATION_CLASS */ return "Bad validation class";
			case 1349: /* ERROR_BAD_TOKEN_TYPE */ return "Bad token type";
			case 1350: /* ERROR_NO_SECURITY_ON_OBJECT */ return "No security on object";
			case 1351: /* ERROR_CANT_ACCESS_DOMAIN_INFO */ return "Can't access domain info";
			case 1352: /* ERROR_INVALID_SERVER_STATE */ return "Invalid server state";
			case 1353: /* ERROR_INVALID_DOMAIN_STATE */ return "Invalid domain state";
			case 1354: /* ERROR_INVALID_DOMAIN_ROLE */ return "Invalid domain role";
			case 1355: /* ERROR_NO_SUCH_DOMAIN */ return "No such domain";
			case 1356: /* ERROR_DOMAIN_EXISTS */ return "Domain exists";
			case 1357: /* ERROR_DOMAIN_LIMIT_EXCEEDED */ return "Domain limit exceeded";
			case 1358: /* ERROR_INTERNAL_DB_CORRUPTION */ return "Internal DB corruption";
			case 1359: /* ERROR_INTERNAL_ERROR */ return "Internal error";
			case 1360: /* ERROR_GENERIC_NOT_MAPPED */ return "Generic not mapped";
			case 1361: /* ERROR_BAD_DESCRIPTOR_FORMAT */ return "Bad descriptor format";
			case 1362: /* ERROR_NOT_LOGON_PROCESS */ return "Not logon process";
			case 1363: /* ERROR_LOGON_SESSION_EXISTS */ return "Logon session exists";
			case 1364: /* ERROR_NO_SUCH_PACKAGE */ return "No such package";
			case 1365: /* ERROR_BAD_LOGON_SESSION_STATE */ return "Bad logon session state";
			case 1366: /* ERROR_LOGON_SESSION_COLLISION */ return "Logon session collision";
			case 1367: /* ERROR_INVALID_LOGON_TYPE */ return "Invalid logon type";
			case 1368: /* ERROR_CANNOT_IMPERSONATE */ return "Cannot impersonate";
			case 1369: /* ERROR_RXACT_INVALID_STATE */ return "Rxact invalid state";
			case 1370: /* ERROR_RXACT_COMMIT_FAILURE */ return "Rxact commit failure";
			case 1371: /* ERROR_SPECIAL_ACCOUNT */ return "Special account";
			case 1372: /* ERROR_SPECIAL_GROUP */ return "Special group";
			case 1373: /* ERROR_SPECIAL_USER */ return "Special user";
			case 1374: /* ERROR_MEMBERS_PRIMARY_GROUP */ return "Members primary group";
			case 1375: /* ERROR_TOKEN_ALREADY_IN_USE */ return "Token already in use";
			case 1376: /* ERROR_NO_SUCH_ALIAS */ return "No such alias";
			case 1377: /* ERROR_MEMBER_NOT_IN_ALIAS */ return "Member not in alias";
			case 1378: /* ERROR_MEMBER_IN_ALIAS */ return "Member in alias";
			case 1379: /* ERROR_ALIAS_EXISTS */ return "Alias exists";
			case 1380: /* ERROR_LOGON_NOT_GRANTED */ return "Logon not granted";
			case 1381: /* ERROR_TOO_MANY_SECRETS */ return "Too many secrets";
			case 1382: /* ERROR_SECRET_TOO_LONG */ return "Secret too long";
			case 1383: /* ERROR_INTERNAL_DB_ERROR */ return "Internal DB error";
			case 1384: /* ERROR_TOO_MANY_CONTEXT_IDS */ return "Too many context IDs";
			case 1385: /* ERROR_LOGON_TYPE_NOT_GRANTED */ return "Logon type not granted";
			case 1386: /* ERROR_NT_CROSS_ENCRYPTION_REQUIRED */ return "NT cross encryption required";
			case 1387: /* ERROR_NO_SUCH_MEMBER */ return "No such member";
			case 1388: /* ERROR_INVALID_MEMBER */ return "Invalid member";
			case 1389: /* ERROR_TOO_MANY_SIDS */ return "Too many SIDs";
			case 1390: /* ERROR_LM_CROSS_ENCRYPTION_REQUIRED */ return "LM cross encryption required";
			case 1391: /* ERROR_NO_INHERITANCE */ return "No inheritance";
			case 1392: /* ERROR_FILE_CORRUPT */ return "File corrupt";
			case 1393: /* ERROR_DISK_CORRUPT */ return "Disk corrupt";
			case 1394: /* ERROR_NO_USER_SESSION_KEY */ return "No user session key";
			case 1395: /* ERROR_LICENSE_QUOTA_EXCEEDED */ return "Licence quota exceeded";
			case 1396: /* ERROR_WRONG_TARGET_NAME */ return "Wrong target name";
			case 1397: /* ERROR_MUTUAL_AUTH_FAILED */ return "Mutual auth failed";
			case 1398: /* ERROR_TIME_SKEW */ return "Time skew";
			case 1399: /* ERROR_CURRENT_DOMAIN_NOT_ALLOWED */ return "Current domain not allowed";
			case 1400: /* ERROR_INVALID_WINDOW_HANDLE */ return "Invalid window handle";
			case 1401: /* ERROR_INVALID_MENU_HANDLE */ return "Invalid menu handle";
			case 1402: /* ERROR_INVALID_CURSOR_HANDLE */ return "Invalid cursor handle";
			case 1403: /* ERROR_INVALID_ACCEL_HANDLE */ return "Invalid accel handle";
			case 1404: /* ERROR_INVALID_HOOK_HANDLE */ return "Invalid hook handle";
			case 1405: /* ERROR_INVALID_DWP_HANDLE */ return "Invalid DWP handle";
			case 1406: /* ERROR_TLW_WITH_WSCHILD */ return "TLW with wschild";
			case 1407: /* ERROR_CANNOT_FIND_WND_CLASS */ return "Cannot find WND class";
			case 1408: /* ERROR_WINDOW_OF_OTHER_THREAD */ return "Window of other thread";
			case 1409: /* ERROR_HOTKEY_ALREADY_REGISTERED */ return "Hotkey already registered";
			case 1410: /* ERROR_CLASS_ALREADY_EXISTS */ return "Class already exists";
			case 1411: /* ERROR_CLASS_DOES_NOT_EXIST */ return "Class does not exist";
			case 1412: /* ERROR_CLASS_HAS_WINDOWS */ return "Class has windows";
			case 1413: /* ERROR_INVALID_INDEX */ return "Invalid index";
			case 1414: /* ERROR_INVALID_ICON_HANDLE */ return "Invalid icon handle";
			case 1415: /* ERROR_PRIVATE_DIALOG_INDEX */ return "Private dialog index";
			case 1416: /* ERROR_LISTBOX_ID_NOT_FOUND */ return "Listbox ID not found";
			case 1417: /* ERROR_NO_WILDCARD_CHARACTERS */ return "No wildcard characters";
			case 1418: /* ERROR_CLIPBOARD_NOT_OPEN */ return "Clipboard not open";
			case 1419: /* ERROR_HOTKEY_NOT_REGISTERED */ return "Hotkey not registered";
			case 1420: /* ERROR_WINDOW_NOT_DIALOG */ return "Window not dialog";
			case 1421: /* ERROR_CONTROL_ID_NOT_FOUND */ return "Control ID not found";
			case 1422: /* ERROR_INVALID_COMBOBOX_MESSAGE */ return "Invalid combobox message";
			case 1423: /* ERROR_WINDOW_NOT_COMBOBOX */ return "Window not combobox";
			case 1424: /* ERROR_INVALID_EDIT_HEIGHT */ return "Invalid edit height";
			case 1425: /* ERROR_DC_NOT_FOUND */ return "DC not found";
			case 1426: /* ERROR_INVALID_HOOK_FILTER */ return "Invalid hook filter";
			case 1427: /* ERROR_INVALID_FILTER_PROC */ return "Invalid filter proc";
			case 1428: /* ERROR_HOOK_NEEDS_HMOD */ return "Hook needs HMOD";
			case 1429: /* ERROR_GLOBAL_ONLY_HOOK */ return "Global only hook";
			case 1430: /* ERROR_JOURNAL_HOOK_SET */ return "Journal hook set";
			case 1431: /* ERROR_HOOK_NOT_INSTALLED */ return "Hook not installed";
			case 1432: /* ERROR_INVALID_LB_MESSAGE */ return "Invalid LB message";
			case 1433: /* ERROR_SETCOUNT_ON_BAD_LB */ return "Setcount on bad LB";
			case 1434: /* ERROR_LB_WITHOUT_TABSTOPS */ return "LB without tabstops";
			case 1435: /* ERROR_DESTROY_OBJECT_OF_OTHER_THREAD */ return "Destroy object of other thread";
			case 1436: /* ERROR_CHILD_WINDOW_MENU */ return "Child window menu";
			case 1437: /* ERROR_NO_SYSTEM_MENU */ return "No system menu";
			case 1438: /* ERROR_INVALID_MSGBOX_STYLE */ return "Invalid msgbox style";
			case 1439: /* ERROR_INVALID_SPI_VALUE */ return "Invalid SPI value";
			case 1440: /* ERROR_SCREEN_ALREADY_LOCKED */ return "Screen already locked";
			case 1441: /* ERROR_HWNDS_HAVE_DIFF_PARENT */ return "HWNDs have different parent";
			case 1442: /* ERROR_NOT_CHILD_WINDOW */ return "Not child window";
			case 1443: /* ERROR_INVALID_GW_COMMAND */ return "Invalid GW command";
			case 1444: /* ERROR_INVALID_THREAD_ID */ return "Invalid thread ID";
			case 1445: /* ERROR_NON_MDICHILD_WINDOW */ return "Non MDI child window";
			case 1446: /* ERROR_POPUP_ALREADY_ACTIVE */ return "Popup already active";
			case 1447: /* ERROR_NO_SCROLLBARS */ return "No scrollbars";
			case 1448: /* ERROR_INVALID_SCROLLBAR_RANGE */ return "Invalid scrollbar range";
			case 1449: /* ERROR_INVALID_SHOWWIN_COMMAND */ return "Invalid showwin command";
			case 1450: /* ERROR_NO_SYSTEM_RESOURCES */ return "No system resources";
			case 1451: /* ERROR_NONPAGED_SYSTEM_RESOURCES */ return "Nonpaged system resources";
			case 1452: /* ERROR_PAGED_SYSTEM_RESOURCES */ return "Paged system resources";
			case 1453: /* ERROR_WORKING_SET_QUOTA */ return "Working set quota";
			case 1454: /* ERROR_PAGEFILE_QUOTA */ return "Pagefile quota";
			case 1455: /* ERROR_COMMITMENT_LIMIT */ return "Commitment limit";
			case 1456: /* ERROR_MENU_ITEM_NOT_FOUND */ return "Menu item not found";
			case 1457: /* ERROR_INVALID_KEYBOARD_HANDLE */ return "Invalid keyboard handle";
			case 1458: /* ERROR_HOOK_TYPE_NOT_ALLOWED */ return "Hook type not allowed";
			case 1459: /* ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION */ return "Requires interactive windowstation";
			case 1460: /* ERROR_TIMEOUT */ return "Timeout";
			case 1461: /* ERROR_INVALID_MONITOR_HANDLE */ return "Invalid monitor handle";
			case 1500: /* ERROR_EVENTLOG_FILE_CORRUPT */ return "Eventlog file corrupt";
			case 1501: /* ERROR_EVENTLOG_CANT_START */ return "Eventlog can't start";
			case 1502: /* ERROR_LOG_FILE_FULL */ return "Log file full";
			case 1503: /* ERROR_EVENTLOG_FILE_CHANGED */ return "Eventlog file changed";
			case 1601: /* ERROR_INSTALL_SERVICE_FAILURE */ return "Install service failure";
			case 1602: /* ERROR_INSTALL_USEREXIT */ return "Install userexit";
			case 1603: /* ERROR_INSTALL_FAILURE */ return "Install failure";
			case 1604: /* ERROR_INSTALL_SUSPEND */ return "Install suspend";
			case 1605: /* ERROR_UNKNOWN_PRODUCT */ return "Unknown product";
			case 1606: /* ERROR_UNKNOWN_FEATURE */ return "Unknown feature";
			case 1607: /* ERROR_UNKNOWN_COMPONENT */ return "Unknown component";
			case 1608: /* ERROR_UNKNOWN_PROPERTY */ return "Unknown property";
			case 1609: /* ERROR_INVALID_HANDLE_STATE */ return "Invalid handle state";
			case 1610: /* ERROR_BAD_CONFIGURATION */ return "Bad configuration";
			case 1611: /* ERROR_INDEX_ABSENT */ return "Index absent";
			case 1612: /* ERROR_INSTALL_SOURCE_ABSENT */ return "Install source absent";
			case 1613: /* ERROR_INSTALL_PACKAGE_VERSION */ return "Install package version";
			case 1614: /* ERROR_PRODUCT_UNINSTALLED */ return "Product uninstalled";
			case 1615: /* ERROR_BAD_QUERY_SYNTAX */ return "Bad query syntax";
			case 1616: /* ERROR_INVALID_FIELD */ return "Invalid field";
			case 1617: /* ERROR_DEVICE_REMOVED */ return "Device removed";
			case 1618: /* ERROR_INSTALL_ALREADY_RUNNING */ return "Install already running";
			case 1619: /* ERROR_INSTALL_PACKAGE_OPEN_FAILED */ return "Install package open failed";
			case 1620: /* ERROR_INSTALL_PACKAGE_INVALID */ return "Install package invalid";
			case 1621: /* ERROR_INSTALL_UI_FAILURE */ return "Install UI failure";
			case 1622: /* ERROR_INSTALL_LOG_FAILURE */ return "Install log failure";
			case 1623: /* ERROR_INSTALL_LANGUAGE_UNSUPPORTED */ return "Install language unsupported";
			case 1624: /* ERROR_INSTALL_TRANSFORM_FAILURE */ return "Install transform failure";
			case 1625: /* ERROR_INSTALL_PACKAGE_REJECTED */ return "Install package rejected";
			case 1626: /* ERROR_FUNCTION_NOT_CALLED */ return "Function not called";
			case 1627: /* ERROR_FUNCTION_FAILED */ return "Function failed";
			case 1628: /* ERROR_INVALID_TABLE */ return "Invalid table";
			case 1629: /* ERROR_DATATYPE_MISMATCH */ return "Datatype mismatch";
			case 1630: /* ERROR_UNSUPPORTED_TYPE */ return "Unsupported type";
			case 1631: /* ERROR_CREATE_FAILED */ return "Create failed";
			case 1632: /* ERROR_INSTALL_TEMP_UNWRITABLE */ return "Install temp unwritable";
			case 1633: /* ERROR_INSTALL_PLATFORM_UNSUPPORTED */ return "Install platform unsupported";
			case 1634: /* ERROR_INSTALL_NOTUSED */ return "Install notused";
			case 1635: /* ERROR_PATCH_PACKAGE_OPEN_FAILED */ return "Patch package open failed";
			case 1636: /* ERROR_PATCH_PACKAGE_INVALID */ return "Patch package invalid";
			case 1637: /* ERROR_PATCH_PACKAGE_UNSUPPORTED */ return "Patch package unsupported";
			case 1638: /* ERROR_PRODUCT_VERSION */ return "Product version";
			case 1639: /* ERROR_INVALID_COMMAND_LINE */ return "Invalid command line";
			case 1640: /* ERROR_INSTALL_REMOTE_DISALLOWED */ return "Install remote disallowed";
			case 1641: /* ERROR_SUCCESS_REBOOT_INITIATED */ return "Success reboot initiated";
			case 1642: /* ERROR_PATCH_TARGET_NOT_FOUND */ return "Patch target not found";
			case 1643: /* ERROR_PATCH_PACKAGE_REJECTED */ return "Patch package rejected";
			case 1644: /* ERROR_INSTALL_TRANSFORM_REJECTED */ return "Install transform rejected";
			case 1700: /* RPC_S_INVALID_STRING_BINDING */ return "RPC S Invalid string binding";
			case 1701: /* RPC_S_WRONG_KIND_OF_BINDING */ return "RPC S Wrong kind of binding";
			case 1702: /* RPC_S_INVALID_BINDING */ return "RPC S Invalid binding";
			case 1703: /* RPC_S_PROTSEQ_NOT_SUPPORTED */ return "RPC S Protseq not supported";
			case 1704: /* RPC_S_INVALID_RPC_PROTSEQ */ return "RPC S Invalid RPC protseq";
			case 1705: /* RPC_S_INVALID_STRING_UUID */ return "RPC S Invalid string UUID";
			case 1706: /* RPC_S_INVALID_ENDPOINT_FORMAT */ return "RPC S Invalid endpoint format";
			case 1707: /* RPC_S_INVALID_NET_ADDR */ return "RPC S Invalid net addr";
			case 1708: /* RPC_S_NO_ENDPOINT_FOUND */ return "RPC S No endpoint found";
			case 1709: /* RPC_S_INVALID_TIMEOUT */ return "RPC S Invalid timeout";
			case 1710: /* RPC_S_OBJECT_NOT_FOUND */ return "RPC S Object not found";
			case 1711: /* RPC_S_ALREADY_REGISTERED */ return "RPC S Already registered";
			case 1712: /* RPC_S_TYPE_ALREADY_REGISTERED */ return "RPC S Type already registered";
			case 1713: /* RPC_S_ALREADY_LISTENING */ return "RPC S Already listening";
			case 1714: /* RPC_S_NO_PROTSEQS_REGISTERED */ return "RPC S Not protseqs registered";
			case 1715: /* RPC_S_NOT_LISTENING */ return "RPC S Not listening";
			case 1716: /* RPC_S_UNKNOWN_MGR_TYPE */ return "RPC S Unknown mgr type";
			case 1717: /* RPC_S_UNKNOWN_IF */ return "RPC S Unknown IF";
			case 1718: /* RPC_S_NO_BINDINGS */ return "RPC S No bindings";
			case 1719: /* RPC_S_NO_PROTSEQS */ return "RPC S Not protseqs";
			case 1720: /* RPC_S_CANT_CREATE_ENDPOINT */ return "RPC S Can't create endpoint";
			case 1721: /* RPC_S_OUT_OF_RESOURCES */ return "RPC S Out of resources";
			case 1722: /* RPC_S_SERVER_UNAVAILABLE */ return "RPC S Server unavailable";
			case 1723: /* RPC_S_SERVER_TOO_BUSY */ return "RPC S Server too busy";
			case 1724: /* RPC_S_INVALID_NETWORK_OPTIONS */ return "RPC S Invalid network options";
			case 1725: /* RPC_S_NO_CALL_ACTIVE */ return "RPC S No call active";
			case 1726: /* RPC_S_CALL_FAILED */ return "RPC S Call failed";
			case 1727: /* RPC_S_CALL_FAILED_DNE */ return "RPC S Call failed DNE";
			case 1728: /* RPC_S_PROTOCOL_ERROR */ return "RPC S Protocol error";
			case 1730: /* RPC_S_UNSUPPORTED_TRANS_SYN */ return "RPC S Unsupported trans syn";
			case 1732: /* RPC_S_UNSUPPORTED_TYPE */ return "RPC S Unsupported type";
			case 1733: /* RPC_S_INVALID_TAG */ return "RPC S Invalid tag";
			case 1734: /* RPC_S_INVALID_BOUND */ return "RPC S Invalid bound";
			case 1735: /* RPC_S_NO_ENTRY_NAME */ return "RPC S No entry name";
			case 1736: /* RPC_S_INVALID_NAME_SYNTAX */ return "RPC S Invalid name syntax";
			case 1737: /* RPC_S_UNSUPPORTED_NAME_SYNTAX */ return "RPC S Unsupported name syntax";
			case 1739: /* RPC_S_UUID_NO_ADDRESS */ return "RPC S UUID no address";
			case 1740: /* RPC_S_DUPLICATE_ENDPOINT */ return "RPC S Duplicate endpoint";
			case 1741: /* RPC_S_UNKNOWN_AUTHN_TYPE */ return "RPC S Unknown authn type";
			case 1742: /* RPC_S_MAX_CALLS_TOO_SMALL */ return "RPC S Max calls too small";
			case 1743: /* RPC_S_STRING_TOO_LONG */ return "RPC S String too long";
			case 1744: /* RPC_S_PROTSEQ_NOT_FOUND */ return "RPC S Protseq not found";
			case 1745: /* RPC_S_PROCNUM_OUT_OF_RANGE */ return "RPC S Procnum out of range";
			case 1746: /* RPC_S_BINDING_HAS_NO_AUTH */ return "RPC S Binding has no auth";
			case 1747: /* RPC_S_UNKNOWN_AUTHN_SERVICE */ return "RPC S Unknown authn service";
			case 1748: /* RPC_S_UNKNOWN_AUTHN_LEVEL */ return "RPC S Unknown authn level";
			case 1749: /* RPC_S_INVALID_AUTH_IDENTITY */ return "RPC S Invalid auth identity";
			case 1750: /* RPC_S_UNKNOWN_AUTHZ_SERVICE */ return "RPC S Unknown authz service";
			case 1751: /* EPT_S_INVALID_ENTRY */ return "EPT S Invalid entry";
			case 1752: /* EPT_S_CANT_PERFORM_OP */ return "EPT S Can't perform op";
			case 1753: /* EPT_S_NOT_REGISTERED */ return "EPT S Not registered";
			case 1754: /* RPC_S_NOTHING_TO_EXPORT */ return "RPC S Nothing to export";
			case 1755: /* RPC_S_INCOMPLETE_NAME */ return "RPC S Incomplete name";
			case 1756: /* RPC_S_INVALID_VERS_OPTION */ return "RPC S Invalid vers option";
			case 1757: /* RPC_S_NO_MORE_MEMBERS */ return "RPC S No more members";
			case 1758: /* RPC_S_NOT_ALL_OBJS_UNEXPORTED */ return "RPC S Not all objs unexported";
			case 1759: /* RPC_S_INTERFACE_NOT_FOUND */ return "RPC S Interface not found";
			case 1760: /* RPC_S_ENTRY_ALREADY_EXISTS */ return "RPC S Entry already exists";
			case 1761: /* RPC_S_ENTRY_NOT_FOUND */ return "RPC S Entry not found";
			case 1762: /* RPC_S_NAME_SERVICE_UNAVAILABLE */ return "RPC S Name service unavailable";
			case 1763: /* RPC_S_INVALID_NAF_ID */ return "RPC S Invalid naf ID";
			case 1764: /* RPC_S_CANNOT_SUPPORT */ return "RPC S Cannot support";
			case 1765: /* RPC_S_NO_CONTEXT_AVAILABLE */ return "RPC S No context available";
			case 1766: /* RPC_S_INTERNAL_ERROR */ return "RPC S Internal error";
			case 1767: /* RPC_S_ZERO_DIVIDE */ return "RPC S Zero divide";
			case 1768: /* RPC_S_ADDRESS_ERROR */ return "RPC S Address error";
			case 1769: /* RPC_S_FP_DIV_ZERO */ return "RPC S FP div zero";
			case 1770: /* RPC_S_FP_UNDERFLOW */ return "RPC S FP Underflow";
			case 1771: /* RPC_S_FP_OVERFLOW */ return "RPC S Overflow";
			case 1772: /* RPC_X_NO_MORE_ENTRIES */ return "RPC X No more entries";
			case 1773: /* RPC_X_SS_CHAR_TRANS_OPEN_FAIL */ return "RPC X SS char trans open fail";
			case 1774: /* RPC_X_SS_CHAR_TRANS_SHORT_FILE */ return "RPC X SS char trans short file";
			case 1775: /* RPC_X_SS_IN_NULL_CONTEXT */ return "RPC S SS in null context";
			case 1777: /* RPC_X_SS_CONTEXT_DAMAGED */ return "RPC X SS context damaged";
			case 1778: /* RPC_X_SS_HANDLES_MISMATCH */ return "RPC X SS handles mismatch";
			case 1779: /* RPC_X_SS_CANNOT_GET_CALL_HANDLE */ return "RPC X SS cannot get call handle";
			case 1780: /* RPC_X_NULL_REF_POINTER */ return "RPC X Null ref pointer";
			case 1781: /* RPC_X_ENUM_VALUE_OUT_OF_RANGE */ return "RPC X enum value out of range";
			case 1782: /* RPC_X_BYTE_COUNT_TOO_SMALL */ return "RPC X byte count too small";
			case 1783: /* RPC_X_BAD_STUB_DATA */ return "RPC X bad stub data";
			case 1784: /* ERROR_INVALID_USER_BUFFER */ return "Invalid user buffer";
			case 1785: /* ERROR_UNRECOGNIZED_MEDIA */ return "Unrecognised media";
			case 1786: /* ERROR_NO_TRUST_LSA_SECRET */ return "No trust lsa secret";
			case 1787: /* ERROR_NO_TRUST_SAM_ACCOUNT */ return "No trust sam account";
			case 1788: /* ERROR_TRUSTED_DOMAIN_FAILURE */ return "Trusted domain failure";
			case 1789: /* ERROR_TRUSTED_RELATIONSHIP_FAILURE */ return "Trusted relationship failure";
			case 1790: /* ERROR_TRUST_FAILURE */ return "Trust failure";
			case 1791: /* RPC_S_CALL_IN_PROGRESS */ return "RPC S call in progress";
			case 1792: /* ERROR_NETLOGON_NOT_STARTED */ return "Error netlogon not started";
			case 1793: /* ERROR_ACCOUNT_EXPIRED */ return "Account expired";
			case 1794: /* ERROR_REDIRECTOR_HAS_OPEN_HANDLES */ return "Redirector has open handles";
			case 1795: /* ERROR_PRINTER_DRIVER_ALREADY_INSTALLED */ return "Printer driver already installed";
			case 1796: /* ERROR_UNKNOWN_PORT */ return "Unknown port";
			case 1797: /* ERROR_UNKNOWN_PRINTER_DRIVER */ return "Unknown printer driver";
			case 1798: /* ERROR_UNKNOWN_PRINTPROCESSOR */ return "Unknown printprocessor";
			case 1799: /* ERROR_INVALID_SEPARATOR_FILE */ return "Invalid separator file";
			case 1800: /* ERROR_INVALID_PRIORITY */ return "Invalid priority";
			case 1801: /* ERROR_INVALID_PRINTER_NAME */ return "Invalid printer name";
			case 1802: /* ERROR_PRINTER_ALREADY_EXISTS */ return "Printer already exists";
			case 1803: /* ERROR_INVALID_PRINTER_COMMAND */ return "Invalid printer command";
			case 1804: /* ERROR_INVALID_DATATYPE */ return "Invalid datatype";
			case 1805: /* ERROR_INVALID_ENVIRONMENT */ return "Invalid environment";
			case 1806: /* RPC_S_NO_MORE_BINDINGS */ return "RPC S no more bindings";
			case 1807: /* ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT */ return "Nologon interdomain trust account";
			case 1808: /* ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT */ return "Nologon workstation trust account";
			case 1809: /* ERROR_NOLOGON_SERVER_TRUST_ACCOUNT */ return "Nologon server trust account";
			case 1810: /* ERROR_DOMAIN_TRUST_INCONSISTENT */ return "Domain trust inconsistent";
			case 1811: /* ERROR_SERVER_HAS_OPEN_HANDLES */ return "Server has open handles";
			case 1812: /* ERROR_RESOURCE_DATA_NOT_FOUND */ return "Resource data not found";
			case 1813: /* ERROR_RESOURCE_TYPE_NOT_FOUND */ return "Resource type not found";
			case 1814: /* ERROR_RESOURCE_NAME_NOT_FOUND */ return "Resource name not found";
			case 1815: /* ERROR_RESOURCE_LANG_NOT_FOUND */ return "Resource lang not found";
			case 1816: /* ERROR_NOT_ENOUGH_QUOTA */ return "Not enough quota";
			case 1817: /* RPC_S_NO_INTERFACES */ return "RPC S no interfaces";
			case 1818: /* RPC_S_CALL_CANCELLED */ return "RPC S Call cancelled";
			case 1819: /* RPC_S_BINDING_INCOMPLETE */ return "RPC S Binding incomplete";
			case 1820: /* RPC_S_COMM_FAILURE */ return "RPC S Comm failure";
			case 1821: /* RPC_S_UNSUPPORTED_AUTHN_LEVEL */ return "RPC S Unsupported authn level";
			case 1822: /* RPC_S_NO_PRINC_NAME */ return "RPC S No princ name";
			case 1823: /* RPC_S_NOT_RPC_ERROR */ return "RPC S Not RPC error";
			case 1824: /* RPC_S_UUID_LOCAL_ONLY */ return "RPC U UUID local only";
			case 1825: /* RPC_S_SEC_PKG_ERROR */ return "RPC S Sec pkg error";
			case 1826: /* RPC_S_NOT_CANCELLED */ return "RPC S Not cancelled";
			case 1827: /* RPC_X_INVALID_ES_ACTION */ return "RPC X Invalid ES action";
			case 1828: /* RPC_X_WRONG_ES_VERSION */ return "RPC X Wrong ES version";
			case 1829: /* RPC_X_WRONG_STUB_VERSION */ return "RPC X Wrong stub version";
			case 1830: /* RPC_X_INVALID_PIPE_OBJECT */ return "RPC X Invalid pipe object";
			case 1831: /* RPC_X_WRONG_PIPE_ORDER */ return "RPC X Wrong pipe order";
			case 1832: /* RPC_X_WRONG_PIPE_VERSION */ return "RPC X Wrong pipe version";
			case 1898: /* RPC_S_GROUP_MEMBER_NOT_FOUND */ return "RPC S group member not found";
			case 1899: /* EPT_S_CANT_CREATE */ return "EPT S Can't create";
			case 1900: /* RPC_S_INVALID_OBJECT */ return "RPC S Invalid object";
			case 1901: /* ERROR_INVALID_TIME */ return "Invalid time";
			case 1902: /* ERROR_INVALID_FORM_NAME */ return "Invalid form name";
			case 1903: /* ERROR_INVALID_FORM_SIZE */ return "Invalid form size";
			case 1904: /* ERROR_ALREADY_WAITING */ return "Already waiting";
			case 1905: /* ERROR_PRINTER_DELETED */ return "Printer deleted";
			case 1906: /* ERROR_INVALID_PRINTER_STATE */ return "Invalid printer state";
			case 1907: /* ERROR_PASSWORD_MUST_CHANGE */ return "Password must change";
			case 1908: /* ERROR_DOMAIN_CONTROLLER_NOT_FOUND */ return "Domain controller not found";
			case 1909: /* ERROR_ACCOUNT_LOCKED_OUT */ return "Account locked out";
			case 1910: /* OR_INVALID_OXID */ return "OR Invalid OXID";
			case 1911: /* OR_INVALID_OID */ return "OR Invalid OID";
			case 1912: /* OR_INVALID_SET */ return "OR Invalid set";
			case 1913: /* RPC_S_SEND_INCOMPLETE */ return "RPC S Send incomplete";
			case 1914: /* RPC_S_INVALID_ASYNC_HANDLE */ return "RPC S Invalid async handle";
			case 1915: /* RPC_S_INVALID_ASYNC_CALL */ return "RPC S Invalid async call";
			case 1916: /* RPC_X_PIPE_CLOSED */ return "RPC X Pipe closed";
			case 1917: /* RPC_X_PIPE_DISCIPLINE_ERROR */ return "RPC X Pipe discipline error";
			case 1918: /* RPC_X_PIPE_EMPTY */ return "RPC X Pipe empty";
			case 1919: /* ERROR_NO_SITENAME */ return "No sitename";
			case 1920: /* ERROR_CANT_ACCESS_FILE */ return "Can't access file";
			case 1921: /* ERROR_CANT_RESOLVE_FILENAME */ return "Can't resolve filename";
			case 1922: /* RPC_S_ENTRY_TYPE_MISMATCH */ return "RPC S Entry type mismatch";
			case 1923: /* RPC_S_NOT_ALL_OBJS_EXPORTED */ return "RPC S Not all objs exported";
			case 1924: /* RPC_S_INTERFACE_NOT_EXPORTED */ return "RPC S Interface not exported";
			case 1925: /* RPC_S_PROFILE_NOT_ADDED */ return "RPC S Profile not added";
			case 1926: /* RPC_S_PRF_ELT_NOT_ADDED */ return "RPC S PRF ELT not added";
			case 1927: /* RPC_S_PRF_ELT_NOT_REMOVED */ return "RPC S PRF ELT not removed";
			case 1928: /* RPC_S_GRP_ELT_NOT_ADDED */ return "RPC S GRP ELT not added";
			case 1929: /* RPC_S_GRP_ELT_NOT_REMOVED */ return "RPC S GRP ELT not removed";
			case 1930: /* ERROR_KM_DRIVER_BLOCKED */ return "KM driver blocked";
			case 1931: /* ERROR_CONTEXT_EXPIRED */ return "Context expired";
			case 2000: /* ERROR_INVALID_PIXEL_FORMAT */ return "Invalid pixel format";
			case 2001: /* ERROR_BAD_DRIVER */ return "Bad driver";
			case 2002: /* ERROR_INVALID_WINDOW_STYLE */ return "Invalid window style";
			case 2003: /* ERROR_METAFILE_NOT_SUPPORTED */ return "Metafile not supported";
			case 2004: /* ERROR_TRANSFORM_NOT_SUPPORTED */ return "Transform not supported";
			case 2005: /* ERROR_CLIPPING_NOT_SUPPORTED */ return "Clipping not supported";
			case 2010: /* ERROR_INVALID_CMM */ return "Invalid CMM";
			case 2011: /* ERROR_INVALID_PROFILE */ return "Invalid profile";
			case 2012: /* ERROR_TAG_NOT_FOUND */ return "Tag not found";
			case 2013: /* ERROR_TAG_NOT_PRESENT */ return "Tag not present";
			case 2014: /* ERROR_DUPLICATE_TAG */ return "Duplicate tag";
			case 2015: /* ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE */ return "Profile not associated with device";
			case 2016: /* ERROR_PROFILE_NOT_FOUND */ return "Profile not found";
			case 2017: /* ERROR_INVALID_COLORSPACE */ return "Invalid colorspace";
			case 2018: /* ERROR_ICM_NOT_ENABLED */ return "ICM not enabled";
			case 2019: /* ERROR_DELETING_ICM_XFORM */ return "Deleting ICM xform";
			case 2020: /* ERROR_INVALID_TRANSFORM */ return "Invalid transform";
			case 2021: /* ERROR_COLORSPACE_MISMATCH */ return "Colorspace mismatch";
			case 2022: /* ERROR_INVALID_COLORINDEX */ return "Invalid colorindex";
			case 2108: /* ERROR_CONNECTED_OTHER_PASSWORD */ return "Connected other password";
			case 2109: /* ERROR_CONNECTED_OTHER_PASSWORD_DEFAULT */ return "Connected other password default";
			case 2202: /* ERROR_BAD_USERNAME */ return "Bad username";
			case 2250: /* ERROR_NOT_CONNECTED */ return "Not connected";
			case 2401: /* ERROR_OPEN_FILES */ return "Open files";
			case 2402: /* ERROR_ACTIVE_CONNECTIONS */ return "Active connections";
			case 2404: /* ERROR_DEVICE_IN_USE */ return "Device in use";
			case 3000: /* ERROR_UNKNOWN_PRINT_MONITOR */ return "Unknown print monitor";
			case 3001: /* ERROR_PRINTER_DRIVER_IN_USE */ return "Printer driver in use";
			case 3002: /* ERROR_SPOOL_FILE_NOT_FOUND */ return "Spool file not found";
			case 3003: /* ERROR_SPL_NO_STARTDOC */ return "SPL no startdoc";
			case 3004: /* ERROR_SPL_NO_ADDJOB */ return "SPL no addjob";
			case 3005: /* ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED */ return "Print processor already installed";
			case 3006: /* ERROR_PRINT_MONITOR_ALREADY_INSTALLED */ return "Print monitor already installed";
			case 3007: /* ERROR_INVALID_PRINT_MONITOR */ return "Invalid print monitor";
			case 3008: /* ERROR_PRINT_MONITOR_IN_USE */ return "Print monitor in use";
			case 3009: /* ERROR_PRINTER_HAS_JOBS_QUEUED */ return "Printer has jobs queued";
			case 3010: /* ERROR_SUCCESS_REBOOT_REQUIRED */ return "Success reboot required";
			case 3011: /* ERROR_SUCCESS_RESTART_REQUIRED */ return "Success restart required";
			case 3012: /* ERROR_PRINTER_NOT_FOUND */ return "Printer not found";
			case 3013: /* ERROR_PRINTER_DRIVER_WARNED */ return "Printer driver warned";
			case 3014: /* ERROR_PRINTER_DRIVER_BLOCKED */ return "Printer driver blocked";
			case 4000: /* ERROR_WINS_INTERNAL */ return "Wins internal";
			case 4001: /* ERROR_CAN_NOT_DEL_LOCAL_WINS */ return "Can not del local wins";
			case 4002: /* ERROR_STATIC_INIT */ return "Static init";
			case 4003: /* ERROR_INC_BACKUP */ return "Inc backup";
			case 4004: /* ERROR_FULL_BACKUP */ return "Full backup";
			case 4005: /* ERROR_REC_NON_EXISTENT */ return "Rec not existent";
			case 4006: /* ERROR_RPL_NOT_ALLOWED */ return "RPL not allowed";
			case 4100: /* ERROR_DHCP_ADDRESS_CONFLICT */ return "DHCP address conflict";
			case 4200: /* ERROR_WMI_GUID_NOT_FOUND */ return "WMU GUID not found";
			case 4201: /* ERROR_WMI_INSTANCE_NOT_FOUND */ return "WMI instance not found";
			case 4202: /* ERROR_WMI_ITEMID_NOT_FOUND */ return "WMI ItemID not found";
			case 4203: /* ERROR_WMI_TRY_AGAIN */ return "WMI try again";
			case 4204: /* ERROR_WMI_DP_NOT_FOUND */ return "WMI DP not found";
			case 4205: /* ERROR_WMI_UNRESOLVED_INSTANCE_REF */ return "WMI unresolved instance ref";
			case 4206: /* ERROR_WMI_ALREADY_ENABLED */ return "WMU already enabled";
			case 4207: /* ERROR_WMI_GUID_DISCONNECTED */ return "WMU GUID disconnected";
			case 4208: /* ERROR_WMI_SERVER_UNAVAILABLE */ return "WMI server unavailable";
			case 4209: /* ERROR_WMI_DP_FAILED */ return "WMI DP failed";
			case 4210: /* ERROR_WMI_INVALID_MOF */ return "WMI invalid MOF";
			case 4211: /* ERROR_WMI_INVALID_REGINFO */ return "WMI invalid reginfo";
			case 4212: /* ERROR_WMI_ALREADY_DISABLED */ return "WMI already disabled";
			case 4213: /* ERROR_WMI_READ_ONLY */ return "WMI read only";
			case 4214: /* ERROR_WMI_SET_FAILURE */ return "WMI set failure";
			case 4300: /* ERROR_INVALID_MEDIA */ return "Invalid media";
			case 4301: /* ERROR_INVALID_LIBRARY */ return "Invalid library";
			case 4302: /* ERROR_INVALID_MEDIA_POOL */ return "Invalid media pool";
			case 4303: /* ERROR_DRIVE_MEDIA_MISMATCH */ return "Drive media mismatch";
			case 4304: /* ERROR_MEDIA_OFFLINE */ return "Media offline";
			case 4305: /* ERROR_LIBRARY_OFFLINE */ return "Library offline";
			case 4306: /* ERROR_EMPTY */ return "Empty";
			case 4307: /* ERROR_NOT_EMPTY */ return "Not empty";
			case 4308: /* ERROR_MEDIA_UNAVAILABLE */ return "Media unavailable";
			case 4309: /* ERROR_RESOURCE_DISABLED */ return "Resource disabled";
			case 4310: /* ERROR_INVALID_CLEANER */ return "Invalid cleaner";
			case 4311: /* ERROR_UNABLE_TO_CLEAN */ return "Unable to clean";
			case 4312: /* ERROR_OBJECT_NOT_FOUND */ return "Object not found";
			case 4313: /* ERROR_DATABASE_FAILURE */ return "Database failure";
			case 4314: /* ERROR_DATABASE_FULL */ return "Database full";
			case 4315: /* ERROR_MEDIA_INCOMPATIBLE */ return "Media incompatible";
			case 4316: /* ERROR_RESOURCE_NOT_PRESENT */ return "Resource not present";
			case 4317: /* ERROR_INVALID_OPERATION */ return "Invalid operation";
			case 4318: /* ERROR_MEDIA_NOT_AVAILABLE */ return "Media not available";
			case 4319: /* ERROR_DEVICE_NOT_AVAILABLE */ return "Device not available";
			case 4320: /* ERROR_REQUEST_REFUSED */ return "Request refused";
			case 4321: /* ERROR_INVALID_DRIVE_OBJECT */ return "Invalid drive object";
			case 4322: /* ERROR_LIBRARY_FULL */ return "Library full";
			case 4323: /* ERROR_MEDIUM_NOT_ACCESSIBLE */ return "Medium not accessible";
			case 4324: /* ERROR_UNABLE_TO_LOAD_MEDIUM */ return "Unable to load medium";
			case 4325: /* ERROR_UNABLE_TO_INVENTORY_DRIVE */ return "Unable to inventory drive";
			case 4326: /* ERROR_UNABLE_TO_INVENTORY_SLOT */ return "Unable to inventory slot";
			case 4327: /* ERROR_UNABLE_TO_INVENTORY_TRANSPORT */ return "Unable to inventory transport";
			case 4328: /* ERROR_TRANSPORT_FULL */ return "Transport full";
			case 4329: /* ERROR_CONTROLLING_IEPORT */ return "Controlling ieport";
			case 4330: /* ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA */ return "Unable to eject mounted media";
			case 4331: /* ERROR_CLEANER_SLOT_SET */ return "Cleaner slot set";
			case 4332: /* ERROR_CLEANER_SLOT_NOT_SET */ return "Cleaner slot not set";
			case 4333: /* ERROR_CLEANER_CARTRIDGE_SPENT */ return "Cleaner cartridge spent";
			case 4334: /* ERROR_UNEXPECTED_OMID */ return "Unexpected omid";
			case 4335: /* ERROR_CANT_DELETE_LAST_ITEM */ return "Can't delete last item";
			case 4336: /* ERROR_MESSAGE_EXCEEDS_MAX_SIZE */ return "Message exceeds max size";
			case 4337: /* ERROR_VOLUME_CONTAINS_SYS_FILES */ return "Volume contains sys files";
			case 4338: /* ERROR_INDIGENOUS_TYPE */ return "Indigenous type";
			case 4339: /* ERROR_NO_SUPPORTING_DRIVES */ return "No supporting drives";
			case 4340: /* ERROR_CLEANER_CARTRIDGE_INSTALLED */ return "Cleaner cartridge installed";
			case 4350: /* ERROR_FILE_OFFLINE */ return "Fill offline";
			case 4351: /* ERROR_REMOTE_STORAGE_NOT_ACTIVE */ return "Remote storage not active";
			case 4352: /* ERROR_REMOTE_STORAGE_MEDIA_ERROR */ return "Remote storage media error";
			case 4390: /* ERROR_NOT_A_REPARSE_POINT */ return "Not a reparse point";
			case 4391: /* ERROR_REPARSE_ATTRIBUTE_CONFLICT */ return "Reparse attribute conflict";
			case 4392: /* ERROR_INVALID_REPARSE_DATA */ return "Invalid reparse data";
			case 4393: /* ERROR_REPARSE_TAG_INVALID */ return "Reparse tag invalid";
			case 4394: /* ERROR_REPARSE_TAG_MISMATCH */ return "Reparse tag mismatch";
			case 4500: /* ERROR_VOLUME_NOT_SIS_ENABLED */ return "Volume not sis enabled";
			case 5001: /* ERROR_DEPENDENT_RESOURCE_EXISTS */ return "Dependent resource exists";
			case 5002: /* ERROR_DEPENDENCY_NOT_FOUND */ return "Dependency not found";
			case 5003: /* ERROR_DEPENDENCY_ALREADY_EXISTS */ return "Dependency already exists";
			case 5004: /* ERROR_RESOURCE_NOT_ONLINE */ return "Resource not online";
			case 5005: /* ERROR_HOST_NODE_NOT_AVAILABLE */ return "Host node not available";
			case 5006: /* ERROR_RESOURCE_NOT_AVAILABLE */ return "Resource not available";
			case 5007: /* ERROR_RESOURCE_NOT_FOUND */ return "Resource not found";
			case 5008: /* ERROR_SHUTDOWN_CLUSTER */ return "Shutdown cluster";
			case 5009: /* ERROR_CANT_EVICT_ACTIVE_NODE */ return "Can't evict active node";
			case 5010: /* ERROR_OBJECT_ALREADY_EXISTS */ return "Object already exists";
			case 5011: /* ERROR_OBJECT_IN_LIST */ return "Object in list";
			case 5012: /* ERROR_GROUP_NOT_AVAILABLE */ return "Group not available";
			case 5013: /* ERROR_GROUP_NOT_FOUND */ return "Group not found";
			case 5014: /* ERROR_GROUP_NOT_ONLINE */ return "Group not online";
			case 5015: /* ERROR_HOST_NODE_NOT_RESOURCE_OWNER */ return "Host node not resource owner";
			case 5016: /* ERROR_HOST_NODE_NOT_GROUP_OWNER */ return "Host node not group owner";
			case 5017: /* ERROR_RESMON_CREATE_FAILED */ return "Resmon create failed";
			case 5018: /* ERROR_RESMON_ONLINE_FAILED */ return "Resmon online failed";
			case 5019: /* ERROR_RESOURCE_ONLINE */ return "Resource online";
			case 5020: /* ERROR_QUORUM_RESOURCE */ return "Quorum resource";
			case 5021: /* ERROR_NOT_QUORUM_CAPABLE */ return "Not quorum capable";
			case 5022: /* ERROR_CLUSTER_SHUTTING_DOWN */ return "Cluster shutting down";
			case 5023: /* ERROR_INVALID_STATE */ return "Invalid state";
			case 5024: /* ERROR_RESOURCE_PROPERTIES_STORED */ return "Resource properties stored";
			case 5025: /* ERROR_NOT_QUORUM_CLASS */ return "Not quorum class";
			case 5026: /* ERROR_CORE_RESOURCE */ return "Core resource";
			case 5027: /* ERROR_QUORUM_RESOURCE_ONLINE_FAILED */ return "Quorum resource online failed";
			case 5028: /* ERROR_QUORUMLOG_OPEN_FAILED */ return "Quorumlog open failed";
			case 5029: /* ERROR_CLUSTERLOG_CORRUPT */ return "Clusterlog corrupt";
			case 5030: /* ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE */ return "Clusterlog record exceeds maxsize";
			case 5031: /* ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE */ return "Clusterlog exceeds maxsize";
			case 5032: /* ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND */ return "Clusterlog chkpoint not found";
			case 5033: /* ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE */ return "Clusterlog not enough space";
			case 5034: /* ERROR_QUORUM_OWNER_ALIVE */ return "Quorum owner alive";
			case 5035: /* ERROR_NETWORK_NOT_AVAILABLE */ return "Network not available";
			case 5036: /* ERROR_NODE_NOT_AVAILABLE */ return "Node not available";
			case 5037: /* ERROR_ALL_NODES_NOT_AVAILABLE */ return "All nodes not available";
			case 5038: /* ERROR_RESOURCE_FAILED */ return "Resource failed";
			case 5039: /* ERROR_CLUSTER_INVALID_NODE */ return "Cluster invalid node";
			case 5040: /* ERROR_CLUSTER_NODE_EXISTS */ return "Cluster node exists";
			case 5041: /* ERROR_CLUSTER_JOIN_IN_PROGRESS */ return "Cluster join in progress";
			case 5042: /* ERROR_CLUSTER_NODE_NOT_FOUND */ return "Cluster node not found";
			case 5043: /* ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND */ return "Cluster local node not found";
			case 5044: /* ERROR_CLUSTER_NETWORK_EXISTS */ return "Cluster network exists";
			case 5045: /* ERROR_CLUSTER_NETWORK_NOT_FOUND */ return "Cluster network not found";
			case 5046: /* ERROR_CLUSTER_NETINTERFACE_EXISTS */ return "Cluster netinterface exists";
			case 5047: /* ERROR_CLUSTER_NETINTERFACE_NOT_FOUND */ return "Cluster netinterface not found";
			case 5048: /* ERROR_CLUSTER_INVALID_REQUEST */ return "Cluster invalid request";
			case 5049: /* ERROR_CLUSTER_INVALID_NETWORK_PROVIDER */ return "Cluster invalid network provider";
			case 5050: /* ERROR_CLUSTER_NODE_DOWN */ return "Cluster node down";
			case 5051: /* ERROR_CLUSTER_NODE_UNREACHABLE */ return "Cluster node unreachable";
			case 5052: /* ERROR_CLUSTER_NODE_NOT_MEMBER */ return "Cluster node not member";
			case 5053: /* ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS */ return "Cluster join not in progress";
			case 5054: /* ERROR_CLUSTER_INVALID_NETWORK */ return "Cluster invalid network";
			case 5056: /* ERROR_CLUSTER_NODE_UP */ return "Cluster node up";
			case 5057: /* ERROR_CLUSTER_IPADDR_IN_USE */ return "Cluster ipaddr in use";
			case 5058: /* ERROR_CLUSTER_NODE_NOT_PAUSED */ return "Cluster node not paused";
			case 5059: /* ERROR_CLUSTER_NO_SECURITY_CONTEXT */ return "Cluster no security context";
			case 5060: /* ERROR_CLUSTER_NETWORK_NOT_INTERNAL */ return "Cluster network not internal";
			case 5061: /* ERROR_CLUSTER_NODE_ALREADY_UP */ return "Cluster node already up";
			case 5062: /* ERROR_CLUSTER_NODE_ALREADY_DOWN */ return "Cluster node already down";
			case 5063: /* ERROR_CLUSTER_NETWORK_ALREADY_ONLINE */ return "Cluster network already online";
			case 5064: /* ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE */ return "Cluster network already offline";
			case 5065: /* ERROR_CLUSTER_NODE_ALREADY_MEMBER */ return "Cluster node already member";
			case 5066: /* ERROR_CLUSTER_LAST_INTERNAL_NETWORK */ return "Cluster last internal network";
			case 5067: /* ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS */ return "Cluster network has dependents";
			case 5068: /* ERROR_INVALID_OPERATION_ON_QUORUM */ return "Invalid operation on quorum";
			case 5069: /* ERROR_DEPENDENCY_NOT_ALLOWED */ return "Dependency not allowed";
			case 5070: /* ERROR_CLUSTER_NODE_PAUSED */ return "Cluster node paused";
			case 5071: /* ERROR_NODE_CANT_HOST_RESOURCE */ return "Node can't host resource";
			case 5072: /* ERROR_CLUSTER_NODE_NOT_READY */ return "Cluster node not ready";
			case 5073: /* ERROR_CLUSTER_NODE_SHUTTING_DOWN */ return "Cluster node shutting down";
			case 5074: /* ERROR_CLUSTER_JOIN_ABORTED */ return "Cluster join aborted";
			case 5075: /* ERROR_CLUSTER_INCOMPATIBLE_VERSIONS */ return "Cluster incompatible versions";
			case 5076: /* ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED */ return "Cluster maxnum of resources exceeded";
			case 5077: /* ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED */ return "Cluster system config changed";
			case 5078: /* ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND */ return "Cluster resource type not found";
			case 5079: /* ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED */ return "Cluster restype not supported";
			case 5080: /* ERROR_CLUSTER_RESNAME_NOT_FOUND */ return "Cluster resname not found";
			case 5081: /* ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED */ return "Cluster no RPC packages registered";
			case 5082: /* ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST */ return "Cluster owner not in preflist";
			case 5083: /* ERROR_CLUSTER_DATABASE_SEQMISMATCH */ return "Cluster database seqmismatch";
			case 5084: /* ERROR_RESMON_INVALID_STATE */ return "Resmon invalid state";
			case 5085: /* ERROR_CLUSTER_GUM_NOT_LOCKER */ return "Cluster gum not locker";
			case 5086: /* ERROR_QUORUM_DISK_NOT_FOUND */ return "Quorum disk not found";
			case 5087: /* ERROR_DATABASE_BACKUP_CORRUPT */ return "Database backup corrupt";
			case 5088: /* ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT */ return "Cluster node already has DFS root";
			case 5089: /* ERROR_RESOURCE_PROPERTY_UNCHANGEABLE */ return "Resource property unchangeable";
			case 5890: /* ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE */ return "Cluster membership invalid state";
			case 5891: /* ERROR_CLUSTER_QUORUMLOG_NOT_FOUND */ return "Cluster quorumlog not found";
			case 5892: /* ERROR_CLUSTER_MEMBERSHIP_HALT */ return "Cluster membership halt";
			case 5893: /* ERROR_CLUSTER_INSTANCE_ID_MISMATCH */ return "Cluster instance ID mismatch";
			case 5894: /* ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP */ return "Cluster network not found for IP";
			case 5895: /* ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH */ return "Cluster property data type mismatch";
			case 5896: /* ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP */ return "Cluster evict without cleanup";
			case 5897: /* ERROR_CLUSTER_PARAMETER_MISMATCH */ return "Cluster parameter mismatch";
			case 5898: /* ERROR_NODE_CANNOT_BE_CLUSTERED */ return "Node cannot be clustered";
			case 5899: /* ERROR_CLUSTER_WRONG_OS_VERSION */ return "Cluster wrong OS version";
			case 5900: /* ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME */ return "Cluster can't create dup cluster name";
			case 6001: /* ERROR_DECRYPTION_FAILED */ return "Decryption failed";
			case 6002: /* ERROR_FILE_ENCRYPTED */ return "File encrypted";
			case 6003: /* ERROR_NO_RECOVERY_POLICY */ return "No recovery policy";
			case 6004: /* ERROR_NO_EFS */ return "No EFS";
			case 6005: /* ERROR_WRONG_EFS */ return "Wrong EFS";
			case 6006: /* ERROR_NO_USER_KEYS */ return "No user keys";
			case 6007: /* ERROR_FILE_NOT_ENCRYPTED */ return "File not encryped";
			case 6008: /* ERROR_NOT_EXPORT_FORMAT */ return "Not export format";
			case 6009: /* ERROR_FILE_READ_ONLY */ return "File read only";
			case 6010: /* ERROR_DIR_EFS_DISALLOWED */ return "Dir EFS disallowed";
			case 6011: /* ERROR_EFS_SERVER_NOT_TRUSTED */ return "EFS server not trusted";
			case 6012: /* ERROR_BAD_RECOVERY_POLICY */ return "Bad recovery policy";
			case 6013: /* ERROR_EFS_ALG_BLOB_TOO_BIG */ return "ETS alg blob too big";
			case 6014: /* ERROR_VOLUME_NOT_SUPPORT_EFS */ return "Volume not support EFS";
			case 6015: /* ERROR_EFS_DISABLED */ return "EFS disabled";
			case 6016: /* ERROR_EFS_VERSION_NOT_SUPPORT */ return "EFS version not support";
			case 6118: /* ERROR_NO_BROWSER_SERVERS_FOUND */ return "No browser servers found";
			case 6200: /* SCHED_E_SERVICE_NOT_LOCALSYSTEM */ return "Sched E service not localsystem";
			case 7001: /* ERROR_CTX_WINSTATION_NAME_INVALID */ return "Ctx winstation name invalid";
			case 7002: /* ERROR_CTX_INVALID_PD */ return "Ctx invalid PD";
			case 7003: /* ERROR_CTX_PD_NOT_FOUND */ return "Ctx PD not found";
			case 7004: /* ERROR_CTX_WD_NOT_FOUND */ return "Ctx WD not found";
			case 7005: /* ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY */ return "Ctx cannot make eventlog entry";
			case 7006: /* ERROR_CTX_SERVICE_NAME_COLLISION */ return "Ctx service name collision";
			case 7007: /* ERROR_CTX_CLOSE_PENDING */ return "Ctx close pending";
			case 7008: /* ERROR_CTX_NO_OUTBUF */ return "Ctx no outbuf";
			case 7009: /* ERROR_CTX_MODEM_INF_NOT_FOUND */ return "Ctx modem inf not found";
			case 7010: /* ERROR_CTX_INVALID_MODEMNAME */ return "Ctx invalid modemname";
			case 7011: /* ERROR_CTX_MODEM_RESPONSE_ERROR */ return "Ctx modem response error";
			case 7012: /* ERROR_CTX_MODEM_RESPONSE_TIMEOUT */ return "Ctx modem response timeout";
			case 7013: /* ERROR_CTX_MODEM_RESPONSE_NO_CARRIER */ return "Ctx modem response no carrier";
			case 7014: /* ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE */ return "Ctx modem response no dial tone";
			case 7015: /* ERROR_CTX_MODEM_RESPONSE_BUSY */ return "Ctx modem response busy";
			case 7016: /* ERROR_CTX_MODEM_RESPONSE_VOICE */ return "Ctx modem response voice";
			case 7017: /* ERROR_CTX_TD_ERROR */ return "Ctx TD error";
			case 7022: /* ERROR_CTX_WINSTATION_NOT_FOUND */ return "Ctx winstation not found";
			case 7023: /* ERROR_CTX_WINSTATION_ALREADY_EXISTS */ return "Ctx winstation already exists";
			case 7024: /* ERROR_CTX_WINSTATION_BUSY */ return "Ctx winstation busy";
			case 7025: /* ERROR_CTX_BAD_VIDEO_MODE */ return "Ctx bad video mode";
			case 7035: /* ERROR_CTX_GRAPHICS_INVALID */ return "Ctx graphics invalid";
			case 7037: /* ERROR_CTX_LOGON_DISABLED */ return "Ctx logon disabled";
			case 7038: /* ERROR_CTX_NOT_CONSOLE */ return "Ctx not console";
			case 7040: /* ERROR_CTX_CLIENT_QUERY_TIMEOUT */ return "Ctx client query timeout";
			case 7041: /* ERROR_CTX_CONSOLE_DISCONNECT */ return "Ctx console disconnect";
			case 7042: /* ERROR_CTX_CONSOLE_CONNECT */ return "Ctx console connect";
			case 7044: /* ERROR_CTX_SHADOW_DENIED */ return "Ctx shadow denied";
			case 7045: /* ERROR_CTX_WINSTATION_ACCESS_DENIED */ return "Ctx winstation access denied";
			case 7049: /* ERROR_CTX_INVALID_WD */ return "Ctx invalid WD";
			case 7050: /* ERROR_CTX_SHADOW_INVALID */ return "Ctx shadow invalid";
			case 7051: /* ERROR_CTX_SHADOW_DISABLED */ return "Ctx shadow disabled";
			case 7052: /* ERROR_CTX_CLIENT_LICENSE_IN_USE */ return "Ctx client licence in use";
			case 7053: /* ERROR_CTX_CLIENT_LICENSE_NOT_SET */ return "Ctx client licence not set";
			case 7054: /* ERROR_CTX_LICENSE_NOT_AVAILABLE */ return "Ctx licence not available";
			case 7055: /* ERROR_CTX_LICENSE_CLIENT_INVALID */ return "Ctx licence client invalid";
			case 7056: /* ERROR_CTX_LICENSE_EXPIRED */ return "Ctx licence expired";
			case 7057: /* ERROR_CTX_SHADOW_NOT_RUNNING */ return "Ctx shadow not running";
			case 7058: /* ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE */ return "Ctx shadow ended by mode change";
			case 8001: /* FRS_ERR_INVALID_API_SEQUENCE */ return "FRS err invalid API sequence";
			case 8002: /* FRS_ERR_STARTING_SERVICE */ return "FRS err starting service";
			case 8003: /* FRS_ERR_STOPPING_SERVICE */ return "FRS err stopping service";
			case 8004: /* FRS_ERR_INTERNAL_API */ return "FRS err internal API";
			case 8005: /* FRS_ERR_INTERNAL */ return "FRS err internal";
			case 8006: /* FRS_ERR_SERVICE_COMM */ return "FRS err service comm";
			case 8007: /* FRS_ERR_INSUFFICIENT_PRIV */ return "FRS err insufficient priv";
			case 8008: /* FRS_ERR_AUTHENTICATION */ return "FRS err authentication";
			case 8009: /* FRS_ERR_PARENT_INSUFFICIENT_PRIV */ return "FRS err parent insufficient priv";
			case 8010: /* FRS_ERR_PARENT_AUTHENTICATION */ return "FRS err parent authentication";
			case 8011: /* FRS_ERR_CHILD_TO_PARENT_COMM */ return "FRS err child to parent comm";
			case 8012: /* FRS_ERR_PARENT_TO_CHILD_COMM */ return "FRS err parent to child comm";
			case 8013: /* FRS_ERR_SYSVOL_POPULATE */ return "FRS err sysvol populate";
			case 8014: /* FRS_ERR_SYSVOL_POPULATE_TIMEOUT */ return "FRS err sysvol populate timeout";
			case 8015: /* FRS_ERR_SYSVOL_IS_BUSY */ return "FRS err sysvol is busy";
			case 8016: /* FRS_ERR_SYSVOL_DEMOTE */ return "FRS err sysvol demote";
			case 8017: /* FRS_ERR_INVALID_SERVICE_PARAMETER */ return "FRS err invalid service parameter";
			case 8200: /* ERROR_DS_NOT_INSTALLED */ return "DS not installed";
			case 8201: /* ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY */ return "DS membership evaluated locally";
			case 8202: /* ERROR_DS_NO_ATTRIBUTE_OR_VALUE */ return "DS no attribute or value";
			case 8203: /* ERROR_DS_INVALID_ATTRIBUTE_SYNTAX */ return "DS invalid attribute syntax";
			case 8204: /* ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED */ return "DS attribute type undefined";
			case 8205: /* ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS */ return "DS attribute or value exists";
			case 8206: /* ERROR_DS_BUSY */ return "DS busy";
			case 8207: /* ERROR_DS_UNAVAILABLE */ return "DS unavailable";
			case 8208: /* ERROR_DS_NO_RIDS_ALLOCATED */ return "DS no rids allocated";
			case 8209: /* ERROR_DS_NO_MORE_RIDS */ return "DS no more rids";
			case 8210: /* ERROR_DS_INCORRECT_ROLE_OWNER */ return "DS incorrect role owner";
			case 8211: /* ERROR_DS_RIDMGR_INIT_ERROR */ return "DS ridmgr init error";
			case 8212: /* ERROR_DS_OBJ_CLASS_VIOLATION */ return "DS obj class violation";
			case 8213: /* ERROR_DS_CANT_ON_NON_LEAF */ return "DS can't on non leaf";
			case 8214: /* ERROR_DS_CANT_ON_RDN */ return "DS can't on rnd";
			case 8215: /* ERROR_DS_CANT_MOD_OBJ_CLASS */ return "DS can't mod obj class";
			case 8216: /* ERROR_DS_CROSS_DOM_MOVE_ERROR */ return "DS cross dom move error";
			case 8217: /* ERROR_DS_GC_NOT_AVAILABLE */ return "DS GC not available";
			case 8218: /* ERROR_SHARED_POLICY */ return "Shared policy";
			case 8219: /* ERROR_POLICY_OBJECT_NOT_FOUND */ return "Policy object not found";
			case 8220: /* ERROR_POLICY_ONLY_IN_DS */ return "Policy only in DS";
			case 8221: /* ERROR_PROMOTION_ACTIVE */ return "Promotion active";
			case 8222: /* ERROR_NO_PROMOTION_ACTIVE */ return "No promotion active";
			case 8224: /* ERROR_DS_OPERATIONS_ERROR */ return "DS operations error";
			case 8225: /* ERROR_DS_PROTOCOL_ERROR */ return "DS protocol error";
			case 8226: /* ERROR_DS_TIMELIMIT_EXCEEDED */ return "DS timelimit exceeded";
			case 8227: /* ERROR_DS_SIZELIMIT_EXCEEDED */ return "DS sizelimit exceeded";
			case 8228: /* ERROR_DS_ADMIN_LIMIT_EXCEEDED */ return "DS admin limit exceeded";
			case 8229: /* ERROR_DS_COMPARE_FALSE */ return "DS compare false";
			case 8230: /* ERROR_DS_COMPARE_TRUE */ return "DS compare true";
			case 8231: /* ERROR_DS_AUTH_METHOD_NOT_SUPPORTED */ return "DS auth method not supported";
			case 8232: /* ERROR_DS_STRONG_AUTH_REQUIRED */ return "DS strong auth required";
			case 8233: /* ERROR_DS_INAPPROPRIATE_AUTH */ return "DS inappropriate auth";
			case 8234: /* ERROR_DS_AUTH_UNKNOWN */ return "DS auth unknown";
			case 8235: /* ERROR_DS_REFERRAL */ return "DS referral";
			case 8236: /* ERROR_DS_UNAVAILABLE_CRIT_EXTENSION */ return "DS unavailable crit extension";
			case 8237: /* ERROR_DS_CONFIDENTIALITY_REQUIRED */ return "DS confidentiality required";
			case 8238: /* ERROR_DS_INAPPROPRIATE_MATCHING */ return "DS inappropriate matching";
			case 8239: /* ERROR_DS_CONSTRAINT_VIOLATION */ return "DS constraint violation";
			case 8240: /* ERROR_DS_NO_SUCH_OBJECT */ return "DS no such object";
			case 8241: /* ERROR_DS_ALIAS_PROBLEM */ return "DS alias problem";
			case 8242: /* ERROR_DS_INVALID_DN_SYNTAX */ return "DS invalid dn syntax";
			case 8243: /* ERROR_DS_IS_LEAF */ return "DS is leaf";
			case 8244: /* ERROR_DS_ALIAS_DEREF_PROBLEM */ return "DS alias deref problem";
			case 8245: /* ERROR_DS_UNWILLING_TO_PERFORM */ return "DS unwilling to perform";
			case 8246: /* ERROR_DS_LOOP_DETECT */ return "DS loop detect";
			case 8247: /* ERROR_DS_NAMING_VIOLATION */ return "DS naming violation";
			case 8248: /* ERROR_DS_OBJECT_RESULTS_TOO_LARGE */ return "DS object results too large";
			case 8249: /* ERROR_DS_AFFECTS_MULTIPLE_DSAS */ return "DS affects multiple dsas";
			case 8250: /* ERROR_DS_SERVER_DOWN */ return "DS server down";
			case 8251: /* ERROR_DS_LOCAL_ERROR */ return "DS local error";
			case 8252: /* ERROR_DS_ENCODING_ERROR */ return "DS encoding error";
			case 8253: /* ERROR_DS_DECODING_ERROR */ return "DS decoding error";
			case 8254: /* ERROR_DS_FILTER_UNKNOWN */ return "DS filter unknown";
			case 8255: /* ERROR_DS_PARAM_ERROR */ return "DS param error";
			case 8256: /* ERROR_DS_NOT_SUPPORTED */ return "DS not supported";
			case 8257: /* ERROR_DS_NO_RESULTS_RETURNED */ return "DS no results returned";
			case 8258: /* ERROR_DS_CONTROL_NOT_FOUND */ return "DS control not found";
			case 8259: /* ERROR_DS_CLIENT_LOOP */ return "DS client loop";
			case 8260: /* ERROR_DS_REFERRAL_LIMIT_EXCEEDED */ return "DS referral limit exceeded";
			case 8261: /* ERROR_DS_SORT_CONTROL_MISSING */ return "DS sort control missing";
			case 8262: /* ERROR_DS_OFFSET_RANGE_ERROR */ return "DS offset range error";
			case 8301: /* ERROR_DS_ROOT_MUST_BE_NC */ return "DS root must be nc";
			case 8302: /* ERROR_DS_ADD_REPLICA_INHIBITED */ return "DS and replica inhibited";
			case 8303: /* ERROR_DS_ATT_NOT_DEF_IN_SCHEMA */ return "DS att not def in schema";
			case 8304: /* ERROR_DS_MAX_OBJ_SIZE_EXCEEDED */ return "DS max obj size exceeded";
			case 8305: /* ERROR_DS_OBJ_STRING_NAME_EXISTS */ return "DS obj string name exists";
			case 8306: /* ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA */ return "DS no rdn defined in schema";
			case 8307: /* ERROR_DS_RDN_DOESNT_MATCH_SCHEMA */ return "DS rdn doesn't match schema";
			case 8308: /* ERROR_DS_NO_REQUESTED_ATTS_FOUND */ return "DS no requested atts found";
			case 8309: /* ERROR_DS_USER_BUFFER_TO_SMALL */ return "DS user buffer too small";
			case 8310: /* ERROR_DS_ATT_IS_NOT_ON_OBJ */ return "DS att is not on obj";
			case 8311: /* ERROR_DS_ILLEGAL_MOD_OPERATION */ return "DS illegal mod operation";
			case 8312: /* ERROR_DS_OBJ_TOO_LARGE */ return "DS obj too large";
			case 8313: /* ERROR_DS_BAD_INSTANCE_TYPE */ return "DS bad instance type";
			case 8314: /* ERROR_DS_MASTERDSA_REQUIRED */ return "DS masterdsa required";
			case 8315: /* ERROR_DS_OBJECT_CLASS_REQUIRED */ return "DS object class required";
			case 8316: /* ERROR_DS_MISSING_REQUIRED_ATT */ return "DS missing required att";
			case 8317: /* ERROR_DS_ATT_NOT_DEF_FOR_CLASS */ return "DS att not def for class";
			case 8318: /* ERROR_DS_ATT_ALREADY_EXISTS */ return "DS att already exists";
			case 8320: /* ERROR_DS_CANT_ADD_ATT_VALUES */ return "DS can't add att values";
			case 8321: /* ERROR_DS_SINGLE_VALUE_CONSTRAINT */ return "DS single value constraint";
			case 8322: /* ERROR_DS_RANGE_CONSTRAINT */ return "DS range constraint";
			case 8323: /* ERROR_DS_ATT_VAL_ALREADY_EXISTS */ return "DS att val already exists";
			case 8324: /* ERROR_DS_CANT_REM_MISSING_ATT */ return "DS can't rem missing att";
			case 8325: /* ERROR_DS_CANT_REM_MISSING_ATT_VAL */ return "DS can't rem missing att val";
			case 8326: /* ERROR_DS_ROOT_CANT_BE_SUBREF */ return "DS root can't be subref";
			case 8327: /* ERROR_DS_NO_CHAINING */ return "DS no chaining";
			case 8328: /* ERROR_DS_NO_CHAINED_EVAL */ return "DS no chained eval";
			case 8329: /* ERROR_DS_NO_PARENT_OBJECT */ return "DS no parent object";
			case 8330: /* ERROR_DS_PARENT_IS_AN_ALIAS */ return "DS parent is an alias";
			case 8331: /* ERROR_DS_CANT_MIX_MASTER_AND_REPS */ return "DS can't mix master and reps";
			case 8332: /* ERROR_DS_CHILDREN_EXIST */ return "DS children exist";
			case 8333: /* ERROR_DS_OBJ_NOT_FOUND */ return "DS obj not found";
			case 8334: /* ERROR_DS_ALIASED_OBJ_MISSING */ return "DS aliased obj missing";
			case 8335: /* ERROR_DS_BAD_NAME_SYNTAX */ return "DS bad name syntax";
			case 8336: /* ERROR_DS_ALIAS_POINTS_TO_ALIAS */ return "DS alias points to alias";
			case 8337: /* ERROR_DS_CANT_DEREF_ALIAS */ return "DS can't redef alias";
			case 8338: /* ERROR_DS_OUT_OF_SCOPE */ return "DS out of scope";
			case 8339: /* ERROR_DS_OBJECT_BEING_REMOVED */ return "DS object being removed";
			case 8340: /* ERROR_DS_CANT_DELETE_DSA_OBJ */ return "DS can't delete dsa obj";
			case 8341: /* ERROR_DS_GENERIC_ERROR */ return "DS generic error";
			case 8342: /* ERROR_DS_DSA_MUST_BE_INT_MASTER */ return "DS dsa must be int master";
			case 8343: /* ERROR_DS_CLASS_NOT_DSA */ return "DS class not dsa";
			case 8344: /* ERROR_DS_INSUFF_ACCESS_RIGHTS */ return "DS insuff access rights";
			case 8345: /* ERROR_DS_ILLEGAL_SUPERIOR */ return "DS illegal superior";
			case 8346: /* ERROR_DS_ATTRIBUTE_OWNED_BY_SAM */ return "DS attribute owned by sam";
			case 8347: /* ERROR_DS_NAME_TOO_MANY_PARTS */ return "DS name too many parts";
			case 8348: /* ERROR_DS_NAME_TOO_LONG */ return "DS name too long";
			case 8349: /* ERROR_DS_NAME_VALUE_TOO_LONG */ return "DS name value too long";
			case 8350: /* ERROR_DS_NAME_UNPARSEABLE */ return "DS name unparseable";
			case 8351: /* ERROR_DS_NAME_TYPE_UNKNOWN */ return "DS name type unknown";
			case 8352: /* ERROR_DS_NOT_AN_OBJECT */ return "DS not an object";
			case 8353: /* ERROR_DS_SEC_DESC_TOO_SHORT */ return "DS sec desc too short";
			case 8354: /* ERROR_DS_SEC_DESC_INVALID */ return "DS sec desc invalid";
			case 8355: /* ERROR_DS_NO_DELETED_NAME */ return "DS no deleted name";
			case 8356: /* ERROR_DS_SUBREF_MUST_HAVE_PARENT */ return "DS subref must have parent";
			case 8357: /* ERROR_DS_NCNAME_MUST_BE_NC */ return "DS ncname must be nc";
			case 8358: /* ERROR_DS_CANT_ADD_SYSTEM_ONLY */ return "DS can't add system only";
			case 8359: /* ERROR_DS_CLASS_MUST_BE_CONCRETE */ return "DS class must be concrete";
			case 8360: /* ERROR_DS_INVALID_DMD */ return "DS invalid dmd";
			case 8361: /* ERROR_DS_OBJ_GUID_EXISTS */ return "DS obj GUID exists";
			case 8362: /* ERROR_DS_NOT_ON_BACKLINK */ return "DS not on backlink";
			case 8363: /* ERROR_DS_NO_CROSSREF_FOR_NC */ return "DS no crossref for nc";
			case 8364: /* ERROR_DS_SHUTTING_DOWN */ return "DS shutting down";
			case 8365: /* ERROR_DS_UNKNOWN_OPERATION */ return "DS unknown operation";
			case 8366: /* ERROR_DS_INVALID_ROLE_OWNER */ return "DS invalid role owner";
			case 8367: /* ERROR_DS_COULDNT_CONTACT_FSMO */ return "DS couldn't contact fsmo";
			case 8368: /* ERROR_DS_CROSS_NC_DN_RENAME */ return "DS cross nc dn rename";
			case 8369: /* ERROR_DS_CANT_MOD_SYSTEM_ONLY */ return "DS can't mod system only";
			case 8370: /* ERROR_DS_REPLICATOR_ONLY */ return "DS replicator only";
			case 8371: /* ERROR_DS_OBJ_CLASS_NOT_DEFINED */ return "DS obj class not defined";
			case 8372: /* ERROR_DS_OBJ_CLASS_NOT_SUBCLASS */ return "DS obj class not subclass";
			case 8373: /* ERROR_DS_NAME_REFERENCE_INVALID */ return "DS name reference invalid";
			case 8374: /* ERROR_DS_CROSS_REF_EXISTS */ return "DS cross ref exists";
			case 8375: /* ERROR_DS_CANT_DEL_MASTER_CROSSREF */ return "DS can't del master crossref";
			case 8376: /* ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD */ return "DS subtree notify not nc head";
			case 8377: /* ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX */ return "DS notify filter too complex";
			case 8378: /* ERROR_DS_DUP_RDN */ return "DS dup rdn";
			case 8379: /* ERROR_DS_DUP_OID */ return "DS dup oid";
			case 8380: /* ERROR_DS_DUP_MAPI_ID */ return "DS dup mapi ID";
			case 8381: /* ERROR_DS_DUP_SCHEMA_ID_GUID */ return "DS dup schema ID GUID";
			case 8382: /* ERROR_DS_DUP_LDAP_DISPLAY_NAME */ return "DS dup LDAP display name";
			case 8383: /* ERROR_DS_SEMANTIC_ATT_TEST */ return "DS semantic att test";
			case 8384: /* ERROR_DS_SYNTAX_MISMATCH */ return "DS syntax mismatch";
			case 8385: /* ERROR_DS_EXISTS_IN_MUST_HAVE */ return "DS exists in must have";
			case 8386: /* ERROR_DS_EXISTS_IN_MAY_HAVE */ return "DS exists in may have";
			case 8387: /* ERROR_DS_NONEXISTENT_MAY_HAVE */ return "DS nonexistent may have";
			case 8388: /* ERROR_DS_NONEXISTENT_MUST_HAVE */ return "DS nonexistent must have";
			case 8389: /* ERROR_DS_AUX_CLS_TEST_FAIL */ return "DS aux cls test fail";
			case 8390: /* ERROR_DS_NONEXISTENT_POSS_SUP */ return "DS nonexistent poss sup";
			case 8391: /* ERROR_DS_SUB_CLS_TEST_FAIL */ return "DS sub cls test fail";
			case 8392: /* ERROR_DS_BAD_RDN_ATT_ID_SYNTAX */ return "DS bad rdn att ID syntax";
			case 8393: /* ERROR_DS_EXISTS_IN_AUX_CLS */ return "DS exists in aux cls";
			case 8394: /* ERROR_DS_EXISTS_IN_SUB_CLS */ return "DS exists in sub cls";
			case 8395: /* ERROR_DS_EXISTS_IN_POSS_SUP */ return "DS exists in poss sup";
			case 8396: /* ERROR_DS_RECALCSCHEMA_FAILED */ return "DS recalcschema failed";
			case 8397: /* ERROR_DS_TREE_DELETE_NOT_FINISHED */ return "DS tree delete not finished";
			case 8398: /* ERROR_DS_CANT_DELETE */ return "DS can't delete";
			case 8399: /* ERROR_DS_ATT_SCHEMA_REQ_ID */ return "DS att schema req ID";
			case 8400: /* ERROR_DS_BAD_ATT_SCHEMA_SYNTAX */ return "DS bad att schema syntax";
			case 8401: /* ERROR_DS_CANT_CACHE_ATT */ return "DS can't cache att";
			case 8402: /* ERROR_DS_CANT_CACHE_CLASS */ return "DS can't cache class";
			case 8403: /* ERROR_DS_CANT_REMOVE_ATT_CACHE */ return "DS can't remove att cache";
			case 8404: /* ERROR_DS_CANT_REMOVE_CLASS_CACHE */ return "DS can't remove class cache";
			case 8405: /* ERROR_DS_CANT_RETRIEVE_DN */ return "DS can't retrieve DN";
			case 8406: /* ERROR_DS_MISSING_SUPREF */ return "DS missing supref";
			case 8407: /* ERROR_DS_CANT_RETRIEVE_INSTANCE */ return "DS can't retrieve instance";
			case 8408: /* ERROR_DS_CODE_INCONSISTENCY */ return "DS code inconsistency";
			case 8409: /* ERROR_DS_DATABASE_ERROR */ return "DS database error";
			case 8410: /* ERROR_DS_GOVERNSID_MISSING */ return "DS governsid missing";
			case 8411: /* ERROR_DS_MISSING_EXPECTED_ATT */ return "DS missing expected att";
			case 8412: /* ERROR_DS_NCNAME_MISSING_CR_REF */ return "DS ncname missing cr ref";
			case 8413: /* ERROR_DS_SECURITY_CHECKING_ERROR */ return "DS security checking error";
			case 8414: /* ERROR_DS_SCHEMA_NOT_LOADED */ return "DS schema not loaded";
			case 8415: /* ERROR_DS_SCHEMA_ALLOC_FAILED */ return "DS schema alloc failed";
			case 8416: /* ERROR_DS_ATT_SCHEMA_REQ_SYNTAX */ return "DS att schema req syntax";
			case 8417: /* ERROR_DS_GCVERIFY_ERROR */ return "DS gcverify error";
			case 8418: /* ERROR_DS_DRA_SCHEMA_MISMATCH */ return "DS dra schema mismatch";
			case 8419: /* ERROR_DS_CANT_FIND_DSA_OBJ */ return "DS can't find dsa obj";
			case 8420: /* ERROR_DS_CANT_FIND_EXPECTED_NC */ return "DS can't find expected nc";
			case 8421: /* ERROR_DS_CANT_FIND_NC_IN_CACHE */ return "DS can't find nc in cache";
			case 8422: /* ERROR_DS_CANT_RETRIEVE_CHILD */ return "DS can't retrieve child";
			case 8423: /* ERROR_DS_SECURITY_ILLEGAL_MODIFY */ return "DS security illegal modify";
			case 8424: /* ERROR_DS_CANT_REPLACE_HIDDEN_REC */ return "DS can't replace hidden rec";
			case 8425: /* ERROR_DS_BAD_HIERARCHY_FILE */ return "DS bad hierarchy file";
			case 8426: /* ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED */ return "DS build hierarchy table failed";
			case 8427: /* ERROR_DS_CONFIG_PARAM_MISSING */ return "DS config param missing";
			case 8428: /* ERROR_DS_COUNTING_AB_INDICES_FAILED */ return "DS counting ab indices failed";
			case 8429: /* ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED */ return "DS hierarchy table malloc failed";
			case 8430: /* ERROR_DS_INTERNAL_FAILURE */ return "DS internal failure";
			case 8431: /* ERROR_DS_UNKNOWN_ERROR */ return "DS unknown error";
			case 8432: /* ERROR_DS_ROOT_REQUIRES_CLASS_TOP */ return "DS root requires class top";
			case 8433: /* ERROR_DS_REFUSING_FSMO_ROLES */ return "DS refusing fmso roles";
			case 8434: /* ERROR_DS_MISSING_FSMO_SETTINGS */ return "DS missing fmso settings";
			case 8435: /* ERROR_DS_UNABLE_TO_SURRENDER_ROLES */ return "DS unable to surrender roles";
			case 8436: /* ERROR_DS_DRA_GENERIC */ return "DS dra generic";
			case 8437: /* ERROR_DS_DRA_INVALID_PARAMETER */ return "DS dra invalid parameter";
			case 8438: /* ERROR_DS_DRA_BUSY */ return "DS dra busy";
			case 8439: /* ERROR_DS_DRA_BAD_DN */ return "DS dra bad dn";
			case 8440: /* ERROR_DS_DRA_BAD_NC */ return "DS dra bad nc";
			case 8441: /* ERROR_DS_DRA_DN_EXISTS */ return "DS dra dn exists";
			case 8442: /* ERROR_DS_DRA_INTERNAL_ERROR */ return "DS dra internal error";
			case 8443: /* ERROR_DS_DRA_INCONSISTENT_DIT */ return "DS dra inconsistent dit";
			case 8444: /* ERROR_DS_DRA_CONNECTION_FAILED */ return "DS dra connection failed";
			case 8445: /* ERROR_DS_DRA_BAD_INSTANCE_TYPE */ return "DS dra bad instance type";
			case 8446: /* ERROR_DS_DRA_OUT_OF_MEM */ return "DS dra out of mem";
			case 8447: /* ERROR_DS_DRA_MAIL_PROBLEM */ return "DS dra mail problem";
			case 8448: /* ERROR_DS_DRA_REF_ALREADY_EXISTS */ return "DS dra ref already exists";
			case 8449: /* ERROR_DS_DRA_REF_NOT_FOUND */ return "DS dra ref not found";
			case 8450: /* ERROR_DS_DRA_OBJ_IS_REP_SOURCE */ return "DS dra obj is rep source";
			case 8451: /* ERROR_DS_DRA_DB_ERROR */ return "DS dra db error";
			case 8452: /* ERROR_DS_DRA_NO_REPLICA */ return "DS dra no replica";
			case 8453: /* ERROR_DS_DRA_ACCESS_DENIED */ return "DS dra access denied";
			case 8454: /* ERROR_DS_DRA_NOT_SUPPORTED */ return "DS dra not supported";
			case 8455: /* ERROR_DS_DRA_RPC_CANCELLED */ return "DS dra RPC cancelled";
			case 8456: /* ERROR_DS_DRA_SOURCE_DISABLED */ return "DS dra source disabled";
			case 8457: /* ERROR_DS_DRA_SINK_DISABLED */ return "DS dra sink disabled";
			case 8458: /* ERROR_DS_DRA_NAME_COLLISION */ return "DS dra name collision";
			case 8459: /* ERROR_DS_DRA_SOURCE_REINSTALLED */ return "DS dra source reinstalled";
			case 8460: /* ERROR_DS_DRA_MISSING_PARENT */ return "DS dra missing parent";
			case 8461: /* ERROR_DS_DRA_PREEMPTED */ return "DS dra preempted";
			case 8462: /* ERROR_DS_DRA_ABANDON_SYNC */ return "DS dra abandon sync";
			case 8463: /* ERROR_DS_DRA_SHUTDOWN */ return "DS dra shutdown";
			case 8464: /* ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET */ return "DS dra incompatible partial set";
			case 8465: /* ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA */ return "DS dra source is partial replica";
			case 8466: /* ERROR_DS_DRA_EXTN_CONNECTION_FAILED */ return "DS dra extn connection failed";
			case 8467: /* ERROR_DS_INSTALL_SCHEMA_MISMATCH */ return "DS install schema mismatch";
			case 8468: /* ERROR_DS_DUP_LINK_ID */ return "DS dup link ID";
			case 8469: /* ERROR_DS_NAME_ERROR_RESOLVING */ return "DS name error resolving";
			case 8470: /* ERROR_DS_NAME_ERROR_NOT_FOUND */ return "DS name error not found";
			case 8471: /* ERROR_DS_NAME_ERROR_NOT_UNIQUE */ return "DS name error not unique";
			case 8472: /* ERROR_DS_NAME_ERROR_NO_MAPPING */ return "DS name error no mapping";
			case 8473: /* ERROR_DS_NAME_ERROR_DOMAIN_ONLY */ return "DS name error domain only";
			case 8474: /* ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING */ return "DS name error no syntactical mapping";
			case 8475: /* ERROR_DS_CONSTRUCTED_ATT_MOD */ return "DS constructed att mod";
			case 8476: /* ERROR_DS_WRONG_OM_OBJ_CLASS */ return "DS wrong om obj class";
			case 8477: /* ERROR_DS_DRA_REPL_PENDING */ return "DS dra repl pending";
			case 8478: /* ERROR_DS_DS_REQUIRED */ return "DS ds required";
			case 8479: /* ERROR_DS_INVALID_LDAP_DISPLAY_NAME */ return "DS invalid LDAP display name";
			case 8480: /* ERROR_DS_NON_BASE_SEARCH */ return "DS non base search";
			case 8481: /* ERROR_DS_CANT_RETRIEVE_ATTS */ return "DS can't retrieve atts";
			case 8482: /* ERROR_DS_BACKLINK_WITHOUT_LINK */ return "DS backlink without link";
			case 8483: /* ERROR_DS_EPOCH_MISMATCH */ return "DS epoch mismatch";
			case 8484: /* ERROR_DS_SRC_NAME_MISMATCH */ return "DS src name mismatch";
			case 8485: /* ERROR_DS_SRC_AND_DST_NC_IDENTICAL */ return "DS src and dst nc identical";
			case 8486: /* ERROR_DS_DST_NC_MISMATCH */ return "DS dst nc mismatch";
			case 8487: /* ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC */ return "DS not authoritive for dst nc";
			case 8488: /* ERROR_DS_SRC_GUID_MISMATCH */ return "DS src GUID mismatch";
			case 8489: /* ERROR_DS_CANT_MOVE_DELETED_OBJECT */ return "DS can't move deleted object";
			case 8490: /* ERROR_DS_PDC_OPERATION_IN_PROGRESS */ return "DS pdc operation in progress";
			case 8491: /* ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD */ return "DS cross domain cleanup reqd";
			case 8492: /* ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION */ return "DS illegal xdom move operation";
			case 8493: /* ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS */ return "DS can't with acct group membershps";
			case 8494: /* ERROR_DS_NC_MUST_HAVE_NC_PARENT */ return "DS nc must have nc parent";
			case 8496: /* ERROR_DS_DST_DOMAIN_NOT_NATIVE */ return "DS dst domain not native";
			case 8497: /* ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER */ return "DS missing infrastructure container";
			case 8498: /* ERROR_DS_CANT_MOVE_ACCOUNT_GROUP */ return "DS can't move account group";
			case 8499: /* ERROR_DS_CANT_MOVE_RESOURCE_GROUP */ return "DS can't move resource group";
			case 8500: /* ERROR_DS_INVALID_SEARCH_FLAG */ return "DS invalid search flag";
			case 8501: /* ERROR_DS_NO_TREE_DELETE_ABOVE_NC */ return "DS no tree delete above nc";
			case 8502: /* ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE */ return "DS couldn't lock tree for delete";
			case 8503: /* ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE */ return "DS couldn't identify objects for tree delete";
			case 8504: /* ERROR_DS_SAM_INIT_FAILURE */ return "DS sam init failure";
			case 8505: /* ERROR_DS_SENSITIVE_GROUP_VIOLATION */ return "DS sensitive group violation";
			case 8506: /* ERROR_DS_CANT_MOD_PRIMARYGROUPID */ return "DS can't mod primarygroupid";
			case 8507: /* ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD */ return "DS illegal base schema mod";
			case 8508: /* ERROR_DS_NONSAFE_SCHEMA_CHANGE */ return "DS nonsafe schema change";
			case 8509: /* ERROR_DS_SCHEMA_UPDATE_DISALLOWED */ return "DS schema update disallowed";
			case 8510: /* ERROR_DS_CANT_CREATE_UNDER_SCHEMA */ return "DS can't create under schema";
			case 8511: /* ERROR_DS_INSTALL_NO_SRC_SCH_VERSION */ return "DS install no src sch version";
			case 8512: /* ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE */ return "DS install no sch version in inifile";
			case 8513: /* ERROR_DS_INVALID_GROUP_TYPE */ return "DS invalid group type";
			case 8514: /* ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN */ return "DS no nest globalgroup in mixeddomain";
			case 8515: /* ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN */ return "DS no nest localgroup in mixeddomain";
			case 8516: /* ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER */ return "DS global can't have local member";
			case 8517: /* ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER */ return "DS global can't have universal member";
			case 8518: /* ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER */ return "DS universal can't have local member";
			case 8519: /* ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER */ return "DS global can't have crossdomain member";
			case 8520: /* ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER */ return "DS local can't have crossdomain local member";
			case 8521: /* ERROR_DS_HAVE_PRIMARY_MEMBERS */ return "DS have primary members";
			case 8522: /* ERROR_DS_STRING_SD_CONVERSION_FAILED */ return "DS string sd conversion failed";
			case 8523: /* ERROR_DS_NAMING_MASTER_GC */ return "DS naming master gc";
			case 8524: /* ERROR_DS_LOOKUP_FAILURE */ return "DS lookup failure";
			case 8525: /* ERROR_DS_COULDNT_UPDATE_SPNS */ return "DS couldn't update spns";
			case 8526: /* ERROR_DS_CANT_RETRIEVE_SD */ return "DS can't retrieve sd";
			case 8527: /* ERROR_DS_KEY_NOT_UNIQUE */ return "DS key not unique";
			case 8528: /* ERROR_DS_WRONG_LINKED_ATT_SYNTAX */ return "DS wrong linked att syntax";
			case 8529: /* ERROR_DS_SAM_NEED_BOOTKEY_PASSWORD */ return "DS sam need bootkey password";
			case 8530: /* ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY */ return "DS sam need bootkey floppy";
			case 8531: /* ERROR_DS_CANT_START */ return "DS can't start";
			case 8532: /* ERROR_DS_INIT_FAILURE */ return "DS init failure";
			case 8533: /* ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION */ return "DS no pkt privacy on connection";
			case 8534: /* ERROR_DS_SOURCE_DOMAIN_IN_FOREST */ return "DS source domain in forest";
			case 8535: /* ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST */ return "DS destination domain not in forest";
			case 8536: /* ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED */ return "DS destination auditing not enabled";
			case 8537: /* ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN */ return "DS can't find dc for src domain";
			case 8538: /* ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER */ return "DS src obj not group or user";
			case 8539: /* ERROR_DS_SRC_SID_EXISTS_IN_FOREST */ return "DS src sid exists in forest";
			case 8540: /* ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH */ return "DS src and dst object class mismatch";
			case 8541: /* ERROR_SAM_INIT_FAILURE */ return "Sam init failure";
			case 8542: /* ERROR_DS_DRA_SCHEMA_INFO_SHIP */ return "DS dra schema info ship";
			case 8543: /* ERROR_DS_DRA_SCHEMA_CONFLICT */ return "DS dra schema conflict";
			case 8544: /* ERROR_DS_DRA_EARLIER_SCHEMA_CONLICT */ return "DS dra earlier schema conflict";
			case 8545: /* ERROR_DS_DRA_OBJ_NC_MISMATCH */ return "DS dra obj nc mismatch";
			case 8546: /* ERROR_DS_NC_STILL_HAS_DSAS */ return "DS nc still has dsas";
			case 8547: /* ERROR_DS_GC_REQUIRED */ return "DS gc required";
			case 8548: /* ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY */ return "DS local member of local only";
			case 8549: /* ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS */ return "DS no fpo in universal groups";
			case 8550: /* ERROR_DS_CANT_ADD_TO_GC */ return "DS can't add to gc";
			case 8551: /* ERROR_DS_NO_CHECKPOINT_WITH_PDC */ return "DS no checkpoint with pdc";
			case 8552: /* ERROR_DS_SOURCE_AUDITING_NOT_ENABLED */ return "DS source auditing not enabled";
			case 8553: /* ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC */ return "DS can't create in nondomain nc";
			case 8554: /* ERROR_DS_INVALID_NAME_FOR_SPN */ return "DS invalid name for spn";
			case 8555: /* ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS */ return "DS filter uses constructed attrs";
			case 8556: /* ERROR_DS_UNICODEPWD_NOT_IN_QUOTES */ return "DS unicodepwd not in quotes";
			case 8557: /* ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED */ return "DS machine account quota exceeded";
			case 8558: /* ERROR_DS_MUST_BE_RUN_ON_DST_DC */ return "DS must be run on dst dc";
			case 8559: /* ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER */ return "DS src dc must be sp4 or greater";
			case 8560: /* ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ */ return "DS can't tree delete critical obj";
			case 8561: /* ERROR_DS_INIT_FAILURE_CONSOLE */ return "DS init failure console";
			case 8562: /* ERROR_DS_SAM_INIT_FAILURE_CONSOLE */ return "DS sam init failure console";
			case 8563: /* ERROR_DS_FOREST_VERSION_TOO_HIGH */ return "DS forest version too high";
			case 8564: /* ERROR_DS_DOMAIN_VERSION_TOO_HIGH */ return "DS domain version too high";
			case 8565: /* ERROR_DS_FOREST_VERSION_TOO_LOW */ return "DS forest version too low";
			case 8566: /* ERROR_DS_DOMAIN_VERSION_TOO_LOW */ return "DS domain version too low";
			case 8567: /* ERROR_DS_INCOMPATIBLE_VERSION */ return "DS incompatible version";
			case 8568: /* ERROR_DS_LOW_DSA_VERSION */ return "DS low dsa version";
			case 8569: /* ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN */ return "DS no behaviour version in mixeddomain";
			case 8570: /* ERROR_DS_NOT_SUPPORTED_SORT_ORDER */ return "DS not supported sort order";
			case 8571: /* ERROR_DS_NAME_NOT_UNIQUE */ return "DS name not unique";
			case 8572: /* ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 */ return "DS machine account created prent4";
			case 8573: /* ERROR_DS_OUT_OF_VERSION_STORE */ return "DS out of version store";
			case 8574: /* ERROR_DS_INCOMPATIBLE_CONTROLS_USED */ return "DS incompatible controls used";
			case 8575: /* ERROR_DS_NO_REF_DOMAIN */ return "DS no ref domain";
			case 8576: /* ERROR_DS_RESERVED_LINK_ID */ return "DS reserved link ID";
			case 8577: /* ERROR_DS_LINK_ID_NOT_AVAILABLE */ return "DS link ID not available";
			case 8578: /* ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER */ return "DS ag can't have universal member";
			case 8579: /* ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE */ return "DS modifydn disallowed by instance type";
			case 8580: /* ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC */ return "DS no object move in schema nc";
			case 8581: /* ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG */ return "DS modifydn disallowed by flag";
			case 8582: /* ERROR_DS_MODIFYDN_WRONG_GRANDPARENT */ return "DS modifydn wrong grandparent";
			case 8583: /* ERROR_DS_NAME_ERROR_TRUST_REFERRAL */ return "DS name error trust referral";
			case 8584: /* ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER */ return "Not supported on standard server";
			case 8585: /* ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD */ return "DS can't access remote part of ad";
			case 8586: /* ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE */ return "DS cr impossible to validate";
			case 8587: /* ERROR_DS_THREAD_LIMIT_EXCEEDED */ return "DS thread limit exceeded";
			case 8588: /* ERROR_DS_NOT_CLOSEST */ return "DS not closest";
			case 8589: /* ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF */ return "DS can't derive spn without server ref";
			case 8590: /* ERROR_DS_SINGLE_USER_MODE_FAILED */ return "DS single user mode failed";
			case 8591: /* ERROR_DS_NTDSCRIPT_SYNTAX_ERROR */ return "DS ntdscript syntax error";
			case 8592: /* ERROR_DS_NTDSCRIPT_PROCESS_ERROR */ return "DS ntdscript process error";
			case 8593: /* ERROR_DS_DIFFERENT_REPL_EPOCHS */ return "DS different repl epochs";
			case 8594: /* ERROR_DS_DRS_EXTENSIONS_CHANGED */ return "DS drs extensions changed";
			case 8595: /* ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR */ return "DS replica set change not allowed on disabled cr";
			case 8596: /* ERROR_DS_NO_MSDS_INTID */ return "DS no msds intid";
			case 8597: /* ERROR_DS_DUP_MSDS_INTID */ return "DS dup msds intid";
			case 8598: /* ERROR_DS_EXISTS_IN_RDNATTID */ return "DS exists in rdnattid";
			case 8599: /* ERROR_DS_AUTHORIZATION_FAILED */ return "DS authorisation failed";
			case 8600: /* ERROR_DS_INVALID_SCRIPT */ return "DS invalid script";
			case 8601: /* ERROR_DS_REMOTE_CROSSREF_OP_FAILED */ return "DS remote crossref op failed";
			case 9001: /* DNS_ERROR_RCODE_FORMAT_ERROR */ return "DNS error rcode format error";
			case 9002: /* DNS_ERROR_RCODE_SERVER_FAILURE */ return "DNS error rcode server failure";
			case 9003: /* DNS_ERROR_RCODE_NAME_ERROR */ return "DNS error rcode name error";
			case 9004: /* DNS_ERROR_RCODE_NOT_IMPLEMENTED */ return "DNS error rcode not implemented";
			case 9005: /* DNS_ERROR_RCODE_REFUSED */ return "DNS error rcode refused";
			case 9006: /* DNS_ERROR_RCODE_YXDOMAIN */ return "DNS error rcode yxdomain";
			case 9007: /* DNS_ERROR_RCODE_YXRRSET */ return "DNS error rcode yxrrset";
			case 9008: /* DNS_ERROR_RCODE_NXRRSET */ return "DNS error rcode nxrrset";
			case 9009: /* DNS_ERROR_RCODE_NOTAUTH */ return "DNS error rcode notauth";
			case 9010: /* DNS_ERROR_RCODE_NOTZONE */ return "DNS error rcode notzone";
			case 9016: /* DNS_ERROR_RCODE_BADSIG */ return "DNS error rcode badsig";
			case 9017: /* DNS_ERROR_RCODE_BADKEY */ return "DNS error rcode badkey";
			case 9018: /* DNS_ERROR_RCODE_BADTIME */ return "DNS error rcode badtime";
			case 9501: /* DNS_INFO_NO_RECORDS */ return "DNS info no records";
			case 9502: /* DNS_ERROR_BAD_PACKET */ return "DNS error bad packet";
			case 9503: /* DNS_ERROR_NO_PACKET */ return "DNS error no packet";
			case 9504: /* DNS_ERROR_RCODE */ return "DNS error rcode";
			case 9505: /* DNS_ERROR_UNSECURE_PACKET */ return "DNS error unsecure packet";
			case 9551: /* DNS_ERROR_INVALID_TYPE */ return "DNS error invalid type";
			case 9552: /* DNS_ERROR_INVALID_IP_ADDRESS */ return "DNS error invalid IP address";
			case 9553: /* DNS_ERROR_INVALID_PROPERTY */ return "DNS error invalid property";
			case 9554: /* DNS_ERROR_TRY_AGAIN_LATER */ return "DNS error try again later";
			case 9555: /* DNS_ERROR_NOT_UNIQUE */ return "DNS error not unique";
			case 9556: /* DNS_ERROR_NON_RFC_NAME */ return "DNS error non RFC name";
			case 9557: /* DNS_STATUS_FQDN */ return "DNS status FQDN";
			case 9558: /* DNS_STATUS_DOTTED_NAME */ return "DNS status dotted name";
			case 9559: /* DNS_STATUS_SINGLE_PART_NAME */ return "DNS status single part name";
			case 9560: /* DNS_ERROR_INVALID_NAME_CHAR */ return "DNS error invalid name char";
			case 9561: /* DNS_ERROR_NUMERIC_NAME */ return "DNS error numeric name";
			case 9562: /* DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER */ return "DNS error not allowed on root server";
			case 9601: /* DNS_ERROR_ZONE_DOES_NOT_EXIST */ return "DNS error zone does not exist";
			case 9602: /* DNS_ERROR_NO_ZONE_INFO */ return "DNS error not zone info";
			case 9603: /* DNS_ERROR_INVALID_ZONE_OPERATION */ return "DNS error invalid zone operation";
			case 9604: /* DNS_ERROR_ZONE_CONFIGURATION_ERROR */ return "DNS error zone configuration error";
			case 9605: /* DNS_ERROR_ZONE_HAS_NO_SOA_RECORD */ return "DNS error zone has not SOA record";
			case 9606: /* DNS_ERROR_ZONE_HAS_NO_NS_RECORDS */ return "DNS error zone has no NS records";
			case 9607: /* DNS_ERROR_ZONE_LOCKED */ return "DNS error zone locked";
			case 9608: /* DNS_ERROR_ZONE_CREATION_FAILED */ return "DNS error zone creation failed";
			case 9609: /* DNS_ERROR_ZONE_ALREADY_EXISTS */ return "DNS error zone already exists";
			case 9610: /* DNS_ERROR_AUTOZONE_ALREADY_EXISTS */ return "DNS error autozone already exists";
			case 9611: /* DNS_ERROR_INVALID_ZONE_TYPE */ return "DNS error invalid zone type";
			case 9612: /* DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP */ return "DNS error secondary requires master IP";
			case 9613: /* DNS_ERROR_ZONE_NOT_SECONDARY */ return "DNS error zone not secondary";
			case 9614: /* DNS_ERROR_NEED_SECONDARY_ADDRESSES */ return "DNS error need secondary addresses";
			case 9615: /* DNS_ERROR_WINS_INIT_FAILED */ return "DNS error wins init failed";
			case 9616: /* DNS_ERROR_NEED_WINS_SERVERS */ return "DNS error need wins servers";
			case 9617: /* DNS_ERROR_NBSTAT_INIT_FAILED */ return "DNS error nbstat init failed";
			case 9618: /* DNS_ERROR_SOA_DELETE_INVALID */ return "DNS error SOA delete invalid";
			case 9619: /* DNS_ERROR_FORWARDER_ALREADY_EXISTS */ return "DNS error forwarder already exists";
			case 9620: /* DNS_ERROR_ZONE_REQUIRES_MASTER_IP */ return "DNS error zone requires master IP";
			case 9621: /* DNS_ERROR_ZONE_IS_SHUTDOWN */ return "DNS error zone is shutdown";
			case 9651: /* DNS_ERROR_PRIMARY_REQUIRES_DATAFILE */ return "DNS error primary requires datafile";
			case 9652: /* DNS_ERROR_INVALID_DATAFILE_NAME */ return "DNS error invalid datafile name";
			case 9653: /* DNS_ERROR_DATAFILE_OPEN_FAILURE */ return "DNS error datafile open failure";
			case 9654: /* DNS_ERROR_FILE_WRITEBACK_FAILED */ return "DNS error file writeback failed";
			case 9655: /* DNS_ERROR_DATAFILE_PARSING */ return "DNS error datafile parsing";
			case 9701: /* DNS_ERROR_RECORD_DOES_NOT_EXIST */ return "DNS error record does not exist";
			case 9702: /* DNS_ERROR_RECORD_FORMAT */ return "DNS error record format";
			case 9703: /* DNS_ERROR_NODE_CREATION_FAILED */ return "DNS error node creation failed";
			case 9704: /* DNS_ERROR_UNKNOWN_RECORD_TYPE */ return "DNS error unknown record type";
			case 9705: /* DNS_ERROR_RECORD_TIMED_OUT */ return "DNS error record timed out";
			case 9706: /* DNS_ERROR_NAME_NOT_IN_ZONE */ return "DNS error name not in zone";
			case 9707: /* DNS_ERROR_CNAME_LOOP */ return "DNS error CNAME loop";
			case 9708: /* DNS_ERROR_NODE_IS_CNAME */ return "DNS error node is CNAME";
			case 9709: /* DNS_ERROR_CNAME_COLLISION */ return "DNS error CNAME collision";
			case 9710: /* DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT */ return "DNS error record only at zone root";
			case 9711: /* DNS_ERROR_RECORD_ALREADY_EXISTS */ return "DNS error record already exists";
			case 9712: /* DNS_ERROR_SECONDARY_DATA */ return "DNS error secondary data";
			case 9713: /* DNS_ERROR_NO_CREATE_CACHE_DATA */ return "DNS error no create cache data";
			case 9714: /* DNS_ERROR_NAME_DOES_NOT_EXIST */ return "DNS error name does not exist";
			case 9715: /* DNS_WARNING_PTR_CREATE_FAILED */ return "DNS warning PTR create failed";
			case 9716: /* DNS_WARNING_DOMAIN_UNDELETED */ return "DNS warning domain undeleted";
			case 9717: /* DNS_ERROR_DS_UNAVAILABLE */ return "DNS error ds unavailable";
			case 9718: /* DNS_ERROR_DS_ZONE_ALREADY_EXISTS */ return "DNS error ds zone already exists";
			case 9719: /* DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE */ return "DNS error no bootfile if ds zone";
			case 9751: /* DNS_INFO_AXFR_COMPLETE */ return "DNS info AXFR complete";
			case 9752: /* DNS_ERROR_AXFR */ return "DNS error AXFR";
			case 9753: /* DNS_INFO_ADDED_LOCAL_WINS */ return "DNS info added local wins";
			case 9801: /* DNS_STATUS_CONTINUE_NEEDED */ return "DNS status continue needed";
			case 9851: /* DNS_ERROR_NO_TCPIP */ return "DNS error no TCPIP";
			case 9852: /* DNS_ERROR_NO_DNS_SERVERS */ return "DNS error no DNS servers";
			case 9901: /* DNS_ERROR_DP_DOES_NOT_EXIST */ return "DNS error dp does not exist";
			case 9902: /* DNS_ERROR_DP_ALREADY_EXISTS */ return "DNS error dp already exists";
			case 9903: /* DNS_ERROR_DP_NOT_ENLISTED */ return "DNS error dp not enlisted";
			case 9904: /* DNS_ERROR_DP_ALREADY_ENLISTED */ return "DNS error dp already enlisted";
			case 10110: /* WSA_E_NO_MORE */ return "E_NO_MORE";
			case 10111: /* WSA_E_CANCELLED */ return "E_CANCELLED";
			case 11005: /* WSA_QOS_RECEIVERS */ return "QOS receivers";
			case 11006: /* WSA_QOS_SENDERS */ return "QOS senders";
			case 11007: /* WSA_QOS_NO_SENDERS */ return "QOS no senders";
			case 11008: /* WSA_QOS_NO_RECEIVERS */ return "QOS no receivers";
			case 11009: /* WSA_QOS_REQUEST_CONFIRMED */ return "QOS request confirmed";
			case 11010: /* WSA_QOS_ADMISSION_FAILURE */ return "QOS admission failure";
			case 11011: /* WSA_QOS_POLICY_FAILURE */ return "QOS policy failure";
			case 11012: /* WSA_QOS_BAD_STYLE */ return "QOS bad style";
			case 11013: /* WSA_QOS_BAD_OBJECT */ return "QOS bad object";
			case 11014: /* WSA_QOS_TRAFFIC_CTRL_ERROR */ return "QOS traffic ctrl error";
			case 11015: /* WSA_QOS_GENERIC_ERROR */ return "QOS generic error";
			case 11016: /* WSA_QOS_ESERVICETYPE */ return "QOS eservicetype";
			case 11017: /* WSA_QOS_EFLOWSPEC */ return "QOS eflowspec";
			case 11018: /* WSA_QOS_EPROVSPECBUF */ return "QOS eprovspecbuf";
			case 11019: /* WSA_QOS_EFILTERSTYLE */ return "QOS efilterstyle";
			case 11020: /* WSA_QOS_EFILTERTYPE */ return "QOS efiltertype";
			case 11021: /* WSA_QOS_EFILTERCOUNT */ return "QOS efiltercount";
			case 11022: /* WSA_QOS_EOBJLENGTH */ return "QOS eobjlength";
			case 11023: /* WSA_QOS_EFLOWCOUNT */ return "QOS eflowcount";
			case 11024: /* WSA_QOS_EUNKNOWNPSOBJ */ return "QOS eunknownpsobj";
			case 11025: /* WSA_QOS_EPOLICYOBJ */ return "QOS epolicyobj";
			case 11026: /* WSA_QOS_EFLOWDESC */ return "QOS eflowdesc";
			case 11027: /* WSA_QOS_EPSFLOWSPEC */ return "QOS epsflowspec";
			case 11028: /* WSA_QOS_EPSFILTERSPEC */ return "QOS epsfilterspec";
			case 11029: /* WSA_QOS_ESDMODEOBJ */ return "QOS esdmodeobj";
			case 11030: /* WSA_QOS_ESHAPERATEOBJ */ return "QOS eshaperateobj";
			case 11031: /* WSA_QOS_RESERVED_PETYPE */ return "QOS reserved petype";
			case 13000: /* ERROR_IPSEC_QM_POLICY_EXISTS */ return "IPSEC qm policy exists";
			case 13001: /* ERROR_IPSEC_QM_POLICY_NOT_FOUND */ return "IPSEC qm policy not found";
			case 13002: /* ERROR_IPSEC_QM_POLICY_IN_USE */ return "IPSEC qm policy in use";
			case 13003: /* ERROR_IPSEC_MM_POLICY_EXISTS */ return "IPSEC mm policy exists";
			case 13004: /* ERROR_IPSEC_MM_POLICY_NOT_FOUND */ return "IPSEC mm policy not found";
			case 13005: /* ERROR_IPSEC_MM_POLICY_IN_USE */ return "IPSEC mm policy in use";
			case 13006: /* ERROR_IPSEC_MM_FILTER_EXISTS */ return "IPSEC mm filter exists";
			case 13007: /* ERROR_IPSEC_MM_FILTER_NOT_FOUND */ return "IPSEC mm filter not found";
			case 13008: /* ERROR_IPSEC_TRANSPORT_FILTER_EXISTS */ return "IPSEC transport filter exists";
			case 13009: /* ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND */ return "IPSEC transport filter not found";
			case 13010: /* ERROR_IPSEC_MM_AUTH_EXISTS */ return "IPSEC mm auth exists";
			case 13011: /* ERROR_IPSEC_MM_AUTH_NOT_FOUND */ return "IPSEC mm auth not found";
			case 13012: /* ERROR_IPSEC_MM_AUTH_IN_USE */ return "IPSEC mm auth in use";
			case 13013: /* ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND */ return "IPSEC default mm policy not found";
			case 13014: /* ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND */ return "IPSEC default mm auth not found";
			case 13015: /* ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND */ return "IPSEC default qm policy not found";
			case 13016: /* ERROR_IPSEC_TUNNEL_FILTER_EXISTS */ return "IPSEC tunnel filter exists";
			case 13017: /* ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND */ return "IPSEC tunnel filter not found";
			case 13018: /* ERROR_IPSEC_MM_FILTER_PENDING_DELETION */ return "IPSEC mm filter pending deletion";
			case 13019: /* ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION */ return "IPSEC transport filter pending deletion";
			case 13020: /* ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION */ return "IPSEC tunnel filter pending deletion";
			case 13021: /* ERROR_IPSEC_MM_POLICY_PENDING_DELETION */ return "IPSEC mm policy pending deletion";
			case 13022: /* ERROR_IPSEC_MM_AUTH_PENDING_DELETION */ return "IPSEC mm auth pending deletion";
			case 13023: /* ERROR_IPSEC_QM_POLICY_PENDING_DELETION */ return "IPSEC qm policy pending deletion";
			case 13801: /* ERROR_IPSEC_IKE_AUTH_FAIL */ return "IPSEC IKE auth fail";
			case 13802: /* ERROR_IPSEC_IKE_ATTRIB_FAIL */ return "IPSEC IKE attrib fail";
			case 13803: /* ERROR_IPSEC_IKE_NEGOTIATION_PENDING */ return "IPSEC IKE negotiation pending";
			case 13804: /* ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR */ return "IPSEC IKE general processing error";
			case 13805: /* ERROR_IPSEC_IKE_TIMED_OUT */ return "IPSEC IKE timed out";
			case 13806: /* ERROR_IPSEC_IKE_NO_CERT */ return "IPSEC IKE no cert";
			case 13807: /* ERROR_IPSEC_IKE_SA_DELETED */ return "IPSEC IKE sa deleted";
			case 13808: /* ERROR_IPSEC_IKE_SA_REAPED */ return "IPSEC IKE sa reaped";
			case 13809: /* ERROR_IPSEC_IKE_MM_ACQUIRE_DROP */ return "IPSEC IKE mm acquire drop";
			case 13810: /* ERROR_IPSEC_IKE_QM_ACQUIRE_DROP */ return "IPSEC IKE qm acquire drop";
			case 13811: /* ERROR_IPSEC_IKE_QUEUE_DROP_MM */ return "IPSEC IKE queue drop mm";
			case 13812: /* ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM */ return "IPSEC IKE queue drop no mm";
			case 13813: /* ERROR_IPSEC_IKE_DROP_NO_RESPONSE */ return "IPSEC IKE drop no response";
			case 13814: /* ERROR_IPSEC_IKE_MM_DELAY_DROP */ return "IPSEC IKE mm delay drop";
			case 13815: /* ERROR_IPSEC_IKE_QM_DELAY_DROP */ return "IPSEC IKE qm delay drop";
			case 13816: /* ERROR_IPSEC_IKE_ERROR */ return "IPSEC IKE error";
			case 13817: /* ERROR_IPSEC_IKE_CRL_FAILED */ return "IPSEC IKE crl failed";
			case 13818: /* ERROR_IPSEC_IKE_INVALID_KEY_USAGE */ return "IPSEC IKE invalid key usage";
			case 13819: /* ERROR_IPSEC_IKE_INVALID_CERT_TYPE */ return "IPSEC IKE invalid cert type";
			case 13820: /* ERROR_IPSEC_IKE_NO_PRIVATE_KEY */ return "IPSEC IKE no private key";
			case 13822: /* ERROR_IPSEC_IKE_DH_FAIL */ return "IPSEC IKE dh fail";
			case 13824: /* ERROR_IPSEC_IKE_INVALID_HEADER */ return "IPSEC IKE invalid header";
			case 13825: /* ERROR_IPSEC_IKE_NO_POLICY */ return "IPSEC IKE no policy";
			case 13826: /* ERROR_IPSEC_IKE_INVALID_SIGNATURE */ return "IPSEC IKE invalid signature";
			case 13827: /* ERROR_IPSEC_IKE_KERBEROS_ERROR */ return "IPSEC IKE kerberos error";
			case 13828: /* ERROR_IPSEC_IKE_NO_PUBLIC_KEY */ return "IPSEC IKE no public key";
			case 13829: /* ERROR_IPSEC_IKE_PROCESS_ERR */ return "IPSEC IKE process err";
			case 13830: /* ERROR_IPSEC_IKE_PROCESS_ERR_SA */ return "IPSEC IKE process err sa";
			case 13831: /* ERROR_IPSEC_IKE_PROCESS_ERR_PROP */ return "IPSEC IKE process err prop";
			case 13832: /* ERROR_IPSEC_IKE_PROCESS_ERR_TRANS */ return "IPSEC IKE process err trans";
			case 13833: /* ERROR_IPSEC_IKE_PROCESS_ERR_KE */ return "IPSEC IKE process err ke";
			case 13834: /* ERROR_IPSEC_IKE_PROCESS_ERR_ID */ return "IPSEC IKE process err ID";
			case 13835: /* ERROR_IPSEC_IKE_PROCESS_ERR_CERT */ return "IPSEC IKE process err cert";
			case 13836: /* ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ */ return "IPSEC IKE process err cert req";
			case 13837: /* ERROR_IPSEC_IKE_PROCESS_ERR_HASH */ return "IPSEC IKE process err hash";
			case 13838: /* ERROR_IPSEC_IKE_PROCESS_ERR_SIG */ return "IPSEC IKE process err sig";
			case 13839: /* ERROR_IPSEC_IKE_PROCESS_ERR_NONCE */ return "IPSEC IKE process err nonce";
			case 13840: /* ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY */ return "IPSEC IKE process err notify";
			case 13841: /* ERROR_IPSEC_IKE_PROCESS_ERR_DELETE */ return "IPSEC IKE process err delete";
			case 13842: /* ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR */ return "IPSEC IKE process err vendor";
			case 13843: /* ERROR_IPSEC_IKE_INVALID_PAYLOAD */ return "IPSEC IKE invalid payload";
			case 13844: /* ERROR_IPSEC_IKE_LOAD_SOFT_SA */ return "IPSEC IKE load soft sa";
			case 13845: /* ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN */ return "IPSEC IKE soft sa torn down";
			case 13846: /* ERROR_IPSEC_IKE_INVALID_COOKIE */ return "IPSEC IKE invalid cookie";
			case 13847: /* ERROR_IPSEC_IKE_NO_PEER_CERT */ return "IPSEC IKE no peer cert";
			case 13848: /* ERROR_IPSEC_IKE_PEER_CRL_FAILED */ return "IPSEC IKE peer CRL failed";
			case 13849: /* ERROR_IPSEC_IKE_POLICY_CHANGE */ return "IPSEC IKE policy change";
			case 13850: /* ERROR_IPSEC_IKE_NO_MM_POLICY */ return "IPSEC IKE no mm policy";
			case 13851: /* ERROR_IPSEC_IKE_NOTCBPRIV */ return "IPSEC IKE notcbpriv";
			case 13852: /* ERROR_IPSEC_IKE_SECLOADFAIL */ return "IPSEC IKE secloadfail";
			case 13853: /* ERROR_IPSEC_IKE_FAILSSPINIT */ return "IPSEC IKE failsspinit";
			case 13854: /* ERROR_IPSEC_IKE_FAILQUERYSSP */ return "IPSEC IKE failqueryssp";
			case 13855: /* ERROR_IPSEC_IKE_SRVACQFAIL */ return "IPSEC IKE srvacqfail";
			case 13856: /* ERROR_IPSEC_IKE_SRVQUERYCRED */ return "IPSEC IKE srvquerycred";
			case 13857: /* ERROR_IPSEC_IKE_GETSPIFAIL */ return "IPSEC IKE getspifail";
			case 13858: /* ERROR_IPSEC_IKE_INVALID_FILTER */ return "IPSEC IKE invalid filter";
			case 13859: /* ERROR_IPSEC_IKE_OUT_OF_MEMORY */ return "IPSEC IKE out of memory";
			case 13860: /* ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED */ return "IPSEC IKE add update key failed";
			case 13861: /* ERROR_IPSEC_IKE_INVALID_POLICY */ return "IPSEC IKE invalid policy";
			case 13862: /* ERROR_IPSEC_IKE_UNKNOWN_DOI */ return "IPSEC IKE unknown doi";
			case 13863: /* ERROR_IPSEC_IKE_INVALID_SITUATION */ return "IPSEC IKE invalid situation";
			case 13864: /* ERROR_IPSEC_IKE_DH_FAILURE */ return "IPSEC IKE dh failure";
			case 13865: /* ERROR_IPSEC_IKE_INVALID_GROUP */ return "IPSEC IKE invalid group";
			case 13866: /* ERROR_IPSEC_IKE_ENCRYPT */ return "IPSEC IKE encrypt";
			case 13867: /* ERROR_IPSEC_IKE_DECRYPT */ return "IPSEC IKE decrypt";
			case 13868: /* ERROR_IPSEC_IKE_POLICY_MATCH */ return "IPSEC IKE policy match";
			case 13869: /* ERROR_IPSEC_IKE_UNSUPPORTED_ID */ return "IPSEC IKE unsupported ID";
			case 13870: /* ERROR_IPSEC_IKE_INVALID_HASH */ return "IPSEC IKE invalid hash";
			case 13871: /* ERROR_IPSEC_IKE_INVALID_HASH_ALG */ return "IPSEC IKE invalid hash alg";
			case 13872: /* ERROR_IPSEC_IKE_INVALID_HASH_SIZE */ return "IPSEC IKE invalid hash size";
			case 13873: /* ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG */ return "IPSEC IKE invalid encrypt alg";
			case 13874: /* ERROR_IPSEC_IKE_INVALID_AUTH_ALG */ return "IPSEC IKE invalid auth alg";
			case 13875: /* ERROR_IPSEC_IKE_INVALID_SIG */ return "IPSEC IKE invalid sig";
			case 13876: /* ERROR_IPSEC_IKE_LOAD_FAILED */ return "IPSEC IKE load failed";
			case 13877: /* ERROR_IPSEC_IKE_RPC_DELETE */ return "IPSEC IKE rpc delete";
			case 13878: /* ERROR_IPSEC_IKE_BENIGN_REINIT */ return "IPSEC IKE benign reinit";
			case 13879: /* ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY */ return "IPSEC IKE invalid responder lifetime notify";
			case 13881: /* ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN */ return "IPSEC IKE invalid cert keylen";
			case 13882: /* ERROR_IPSEC_IKE_MM_LIMIT */ return "IPSEC IKE mm limit";
			case 13883: /* ERROR_IPSEC_IKE_NEGOTIATION_DISABLED */ return "IPSEC IKE negotiation disabled";
			case 13884: /* ERROR_IPSEC_IKE_NEG_STATUS_END */ return "IPSEC IKE neg status end";
			case 100001: /* WSAENXIO */ return "Device not configured";
#endif // MOBILE
			default:
				return string.Format ("mono-io-layer-error ({0})", error);
			}
#endif // UNITY
	}

#if UNITY
		static void InitializeErrorMessages()
		{
			if (s_ErrorMessagesInitialized)
				return;

			lock (s_ErrorMessage)
			{
				if (s_ErrorMessagesInitialized)
					return;

				// The Unity WebGL build has a nested loop limit per method.
				// These many error messages go beyond that limit, so break them
				// up into a number of smaller functions.
				InitializeErrorMessages1();
#if !MOBILE
				InitializeErrorMessages2();
				InitializeErrorMessages3();
				InitializeErrorMessages4();
				InitializeErrorMessages5();
				InitializeErrorMessages6();
				InitializeErrorMessages7();
				InitializeErrorMessages8();
				InitializeErrorMessages9();
				InitializeErrorMessages10();
				InitializeErrorMessages11();
				InitializeErrorMessages12();
				InitializeErrorMessages13();
				InitializeErrorMessages14();
				InitializeErrorMessages15();
#endif // MOBILE
				s_ErrorMessagesInitialized = true;
			}
		}

		public static void InitializeErrorMessages1()
		{
			s_ErrorMessage.Add(10036, "Operation in progress"); /* WSAEINPROGRESS */
			s_ErrorMessage.Add(10037, "Operation already in progress"); /* WSAEALREADY */
			s_ErrorMessage.Add(10038, "The descriptor is not a socket"); /* WSAENOTSOCK */
			s_ErrorMessage.Add(10039, "Destination address required"); /* WSAEDESTADDRREQ */
			s_ErrorMessage.Add(10040, "Message too long"); /* WSAEMSGSIZE */
			s_ErrorMessage.Add(10041, "Protocol wrong type for socket"); /* WSAEPROTOTYPE */
			s_ErrorMessage.Add(10042, "Protocol option not supported"); /* WSAENOPROTOOPT */
			s_ErrorMessage.Add(10043, "Protocol not supported"); /* WSAEPROTONOSUPPORT */
			s_ErrorMessage.Add(10044, "Socket not supported"); /* WSAESOCKTNOSUPPORT */
			s_ErrorMessage.Add(10045, "Operation not supported"); /* WSAEOPNOTSUPP */
			s_ErrorMessage.Add(10046, "Protocol family not supported"); /* WSAEPFNOSUPPORT */
			s_ErrorMessage.Add(10047, "An address incompatible with the requested protocol was used"); /* WSAEAFNOSUPPORT */
			s_ErrorMessage.Add(10048, "Address already in use"); /* WSAEADDRINUSE */
			s_ErrorMessage.Add(10049, "The requested address is not valid in this context"); /* WSAEADDRNOTAVAIL */
			s_ErrorMessage.Add(10050, "Network subsystem is down"); /* WSAENETDOWN */
			s_ErrorMessage.Add(10051, "Network is unreachable"); /* WSAENETUNREACH */
			s_ErrorMessage.Add(10052, "Connection broken, keep-alive detected a problem"); /* WSAENETRESET */
			s_ErrorMessage.Add(10053, "An established connection was aborted in your host machine."); /* WSAECONNABORTED */
			s_ErrorMessage.Add(10054, "Connection reset by peer"); /* WSAECONNRESET */
			s_ErrorMessage.Add(10055, "Not enough buffer space is available"); /* WSAENOBUFS */
			s_ErrorMessage.Add(10056, "Socket is already connected"); /* WSAEISCONN */
			s_ErrorMessage.Add(10057, "The socket is not connected"); /* WSAENOTCONN */
			s_ErrorMessage.Add(10058, "The socket has been shut down"); /* WSAESHUTDOWN */
			s_ErrorMessage.Add(10059, "Too many references: cannot splice"); /* WSAETOOMANYREFS */
			s_ErrorMessage.Add(10060, "Connection timed out"); /* WSAETIMEDOUT */
			s_ErrorMessage.Add(10061, "Connection refused"); /* WSAECONNREFUSED */
			s_ErrorMessage.Add(10062, "Too many symbolic links encountered"); /* WSAELOOP */
			s_ErrorMessage.Add(10063, "File name too long"); /* WSAENAMETOOLONG */
			s_ErrorMessage.Add(10064, "Host is down"); /* WSAEHOSTDOWN */
			s_ErrorMessage.Add(10065, "No route to host"); /* WSAEHOSTUNREACH */
			s_ErrorMessage.Add(10066, "Directory not empty"); /* WSAENOTEMPTY */
			s_ErrorMessage.Add(10067, "EPROCLIM"); /* WSAEPROCLIM */
			s_ErrorMessage.Add(10068, "Too many users"); /* WSAEUSERS */
			s_ErrorMessage.Add(10069, "Quota exceeded"); /* WSAEDQUOT */
			s_ErrorMessage.Add(10070, "Stale NFS file handle"); /* WSAESTALE */
			s_ErrorMessage.Add(10071, "Object is remote"); /* WSAEREMOTE */
			s_ErrorMessage.Add(10091, "SYSNOTREADY"); /* WSASYSNOTREADY */
			s_ErrorMessage.Add(10092, "VERNOTSUPPORTED"); /* WSAVERNOTSUPPORTED */
			s_ErrorMessage.Add(10093, "Winsock not initialised"); /* WSANOTINITIALISED */
			s_ErrorMessage.Add(10101, "EDISCON"); /* WSAEDISCON */
			s_ErrorMessage.Add(10102, "ENOMORE"); /* WSAENOMORE */
			s_ErrorMessage.Add(10103, "Operation canceled"); /* WSAECANCELLED */
			s_ErrorMessage.Add(10104, "EINVALIDPROCTABLE"); /* WSAEINVALIDPROCTABLE */
			s_ErrorMessage.Add(10105, "EINVALIDPROVIDER"); /* WSAEINVALIDPROVIDER */
			s_ErrorMessage.Add(10106, "EPROVIDERFAILEDINIT"); /* WSAEPROVIDERFAILEDINIT */
			s_ErrorMessage.Add(10107, "System call failed"); /* WSASYSCALLFAILURE */
			s_ErrorMessage.Add(10108, "SERVICE_NOT_FOUND"); /* WSASERVICE_NOT_FOUND */
			s_ErrorMessage.Add(10109, "TYPE_NOT_FOUND"); /* WSATYPE_NOT_FOUND */
			s_ErrorMessage.Add(10112, "EREFUSED"); /* WSAEREFUSED */
			s_ErrorMessage.Add(11001, "No such host is known"); /* WSAHOST_NOT_FOUND */
			s_ErrorMessage.Add(11002, "A temporary error occurred on an authoritative name server.  Try again later."); /* WSATRY_AGAIN */
			s_ErrorMessage.Add(11003, "No recovery"); /* WSANO_RECOVERY */
			s_ErrorMessage.Add(11004, "No data"); /* WSANO_DATA */
		}

		public static void InitializeErrorMessages2()
		{
			s_ErrorMessage.Add(1, "Invalid function"); /* ERROR_INVALID_FUNCTION */
			s_ErrorMessage.Add(7, "Arena trashed"); /* ERROR_ARENA_TRASHED */
			s_ErrorMessage.Add(8, "Not enough memory"); /* ERROR_NOT_ENOUGH_MEMORY */
			s_ErrorMessage.Add(9, "Invalid block"); /* ERROR_INVALID_BLOCK */
			s_ErrorMessage.Add(10, "Bad environment"); /* ERROR_BAD_ENVIRONMENT */
			s_ErrorMessage.Add(11, "Bad format"); /* ERROR_BAD_FORMAT */
			s_ErrorMessage.Add(12, "Invalid access"); /* ERROR_INVALID_ACCESS */
			s_ErrorMessage.Add(15, "Invalid drive"); /* ERROR_INVALID_DRIVE */
			s_ErrorMessage.Add(16, "Current directory"); /* ERROR_CURRENT_DIRECTORY */
			s_ErrorMessage.Add(19, "Write protect"); /* ERROR_WRITE_PROTECT */
			s_ErrorMessage.Add(20, "Bad unit"); /* ERROR_BAD_UNIT */
			s_ErrorMessage.Add(21, "Not ready"); /* ERROR_NOT_READY */
			s_ErrorMessage.Add(22, "Bad command"); /* ERROR_BAD_COMMAND */
			s_ErrorMessage.Add(23, "CRC"); /* ERROR_CRC */
			s_ErrorMessage.Add(25, "Seek"); /* ERROR_SEEK */
			s_ErrorMessage.Add(26, "Not DOS disk"); /* ERROR_NOT_DOS_DISK */
			s_ErrorMessage.Add(27, "Sector not found"); /* ERROR_SECTOR_NOT_FOUND */
			s_ErrorMessage.Add(28, "Out of paper"); /* ERROR_OUT_OF_PAPER */
			s_ErrorMessage.Add(29, "Write fault"); /* ERROR_WRITE_FAULT */
			s_ErrorMessage.Add(30, "Read fault"); /* ERROR_READ_FAULT */
			s_ErrorMessage.Add(34, "Wrong disk"); /* ERROR_WRONG_DISK */
			s_ErrorMessage.Add(36, "Sharing buffer exceeded"); /* ERROR_SHARING_BUFFER_EXCEEDED */
			s_ErrorMessage.Add(38, "Handle EOF"); /* ERROR_HANDLE_EOF */
			s_ErrorMessage.Add(39, "Handle disk full"); /* ERROR_HANDLE_DISK_FULL */
			s_ErrorMessage.Add(51, "Rem not list"); /* ERROR_REM_NOT_LIST */
			s_ErrorMessage.Add(52, "Duplicate name"); /* ERROR_DUP_NAME */
			s_ErrorMessage.Add(53, "Bad netpath"); /* ERROR_BAD_NETPATH */
			s_ErrorMessage.Add(54, "Network busy"); /* ERROR_NETWORK_BUSY */
			s_ErrorMessage.Add(56, "Too many commands"); /* ERROR_TOO_MANY_CMDS */
			s_ErrorMessage.Add(57, "ADAP HDW error"); /* ERROR_ADAP_HDW_ERR */
			s_ErrorMessage.Add(58, "Bad net response"); /* ERROR_BAD_NET_RESP */
			s_ErrorMessage.Add(59, "Unexpected net error"); /* ERROR_UNEXP_NET_ERR */
			s_ErrorMessage.Add(60, "Bad rem adap"); /* ERROR_BAD_REM_ADAP */
			s_ErrorMessage.Add(61, "Print queue full"); /* ERROR_PRINTQ_FULL */
			s_ErrorMessage.Add(62, "No spool space"); /* ERROR_NO_SPOOL_SPACE */
			s_ErrorMessage.Add(63, "Print cancelled"); /* ERROR_PRINT_CANCELLED */
			s_ErrorMessage.Add(64, "Netname deleted"); /* ERROR_NETNAME_DELETED */
			s_ErrorMessage.Add(65, "Network access denied"); /* ERROR_NETWORK_ACCESS_DENIED */
			s_ErrorMessage.Add(66, "Bad device type"); /* ERROR_BAD_DEV_TYPE */
			s_ErrorMessage.Add(67, "Bad net name"); /* ERROR_BAD_NET_NAME */
			s_ErrorMessage.Add(68, "Too many names"); /* ERROR_TOO_MANY_NAMES */
			s_ErrorMessage.Add(69, "Too many sessions"); /* ERROR_TOO_MANY_SESS */
			s_ErrorMessage.Add(70, "Sharing paused"); /* ERROR_SHARING_PAUSED */
			s_ErrorMessage.Add(71, "Req not accep"); /* ERROR_REQ_NOT_ACCEP */
			s_ErrorMessage.Add(72, "Redir paused"); /* ERROR_REDIR_PAUSED */
			s_ErrorMessage.Add(80, "File exists"); /* ERROR_FILE_EXISTS */
			s_ErrorMessage.Add(82, "Cannot make"); /* ERROR_CANNOT_MAKE */
			s_ErrorMessage.Add(83, "Fail i24"); /* ERROR_FAIL_I24 */
			s_ErrorMessage.Add(84, "Out of structures"); /* ERROR_OUT_OF_STRUCTURES */
			s_ErrorMessage.Add(85, "Already assigned"); /* ERROR_ALREADY_ASSIGNED */
			s_ErrorMessage.Add(86, "Invalid password"); /* ERROR_INVALID_PASSWORD */
			s_ErrorMessage.Add(88, "Net write fault"); /* ERROR_NET_WRITE_FAULT */
			s_ErrorMessage.Add(89, "No proc slots"); /* ERROR_NO_PROC_SLOTS */
			s_ErrorMessage.Add(100, "Too many semaphores"); /* ERROR_TOO_MANY_SEMAPHORES */
			s_ErrorMessage.Add(101, "Exclusive semaphore already owned"); /* ERROR_EXCL_SEM_ALREADY_OWNED */
			s_ErrorMessage.Add(102, "Semaphore is set"); /* ERROR_SEM_IS_SET */
			s_ErrorMessage.Add(103, "Too many semaphore requests"); /* ERROR_TOO_MANY_SEM_REQUESTS */
			s_ErrorMessage.Add(104, "Invalid at interrupt time"); /* ERROR_INVALID_AT_INTERRUPT_TIME */
			s_ErrorMessage.Add(105, "Semaphore owner died"); /* ERROR_SEM_OWNER_DIED */
			s_ErrorMessage.Add(106, "Semaphore user limit"); /* ERROR_SEM_USER_LIMIT */
			s_ErrorMessage.Add(107, "Disk change"); /* ERROR_DISK_CHANGE */
		}

		public static void InitializeErrorMessages3()
		{
			s_ErrorMessage.Add(108, "Drive locked"); /* ERROR_DRIVE_LOCKED */
			s_ErrorMessage.Add(109, "Broken pipe"); /* ERROR_BROKEN_PIPE */
			s_ErrorMessage.Add(110, "Open failed"); /* ERROR_OPEN_FAILED */
			s_ErrorMessage.Add(111, "Buffer overflow"); /* ERROR_BUFFER_OVERFLOW */
			s_ErrorMessage.Add(112, "Disk full"); /* ERROR_DISK_FULL */
			s_ErrorMessage.Add(113, "No more search handles"); /* ERROR_NO_MORE_SEARCH_HANDLES */
			s_ErrorMessage.Add(114, "Invalid target handle"); /* ERROR_INVALID_TARGET_HANDLE */
			s_ErrorMessage.Add(117, "Invalid category"); /* ERROR_INVALID_CATEGORY */
			s_ErrorMessage.Add(118, "Invalid verify switch"); /* ERROR_INVALID_VERIFY_SWITCH */
			s_ErrorMessage.Add(119, "Bad driver level"); /* ERROR_BAD_DRIVER_LEVEL */
			s_ErrorMessage.Add(121, "Semaphore timeout"); /* ERROR_SEM_TIMEOUT */
			s_ErrorMessage.Add(122, "Insufficient buffer"); /* ERROR_INSUFFICIENT_BUFFER */
			s_ErrorMessage.Add(124, "Invalid level"); /* ERROR_INVALID_LEVEL */
			s_ErrorMessage.Add(125, "No volume label"); /* ERROR_NO_VOLUME_LABEL */
			s_ErrorMessage.Add(126, "Module not found"); /* ERROR_MOD_NOT_FOUND */
			s_ErrorMessage.Add(128, "Wait no children"); /* ERROR_WAIT_NO_CHILDREN */
			s_ErrorMessage.Add(129, "Child not complete"); /* ERROR_CHILD_NOT_COMPLETE */
			s_ErrorMessage.Add(130, "Direct access handle"); /* ERROR_DIRECT_ACCESS_HANDLE */
			s_ErrorMessage.Add(131, "Negative seek"); /* ERROR_NEGATIVE_SEEK */
			s_ErrorMessage.Add(132, "Seek on device"); /* ERROR_SEEK_ON_DEVICE */
			s_ErrorMessage.Add(133, "Is join target"); /* ERROR_IS_JOIN_TARGET */
			s_ErrorMessage.Add(134, "Is joined"); /* ERROR_IS_JOINED */
			s_ErrorMessage.Add(135, "Is substed"); /* ERROR_IS_SUBSTED */
			s_ErrorMessage.Add(136, "Not joined"); /* ERROR_NOT_JOINED */
			s_ErrorMessage.Add(137, "Not substed"); /* ERROR_NOT_SUBSTED */
			s_ErrorMessage.Add(138, "Join to join"); /* ERROR_JOIN_TO_JOIN */
			s_ErrorMessage.Add(139, "Subst to subst"); /* ERROR_SUBST_TO_SUBST */
			s_ErrorMessage.Add(140, "Join to subst"); /* ERROR_JOIN_TO_SUBST */
			s_ErrorMessage.Add(141, "Subst to join"); /* ERROR_SUBST_TO_JOIN */
			s_ErrorMessage.Add(142, "Busy drive"); /* ERROR_BUSY_DRIVE */
			s_ErrorMessage.Add(143, "Same drive"); /* ERROR_SAME_DRIVE */
			s_ErrorMessage.Add(144, "Directory not root"); /* ERROR_DIR_NOT_ROOT */
			s_ErrorMessage.Add(145, "Directory not empty"); /* ERROR_DIR_NOT_EMPTY */
			s_ErrorMessage.Add(146, "Is subst path"); /* ERROR_IS_SUBST_PATH */
			s_ErrorMessage.Add(147, "Is join path"); /* ERROR_IS_JOIN_PATH */
			s_ErrorMessage.Add(148, "Path busy"); /* ERROR_PATH_BUSY */
			s_ErrorMessage.Add(149, "Is subst target"); /* ERROR_IS_SUBST_TARGET */
			s_ErrorMessage.Add(150, "System trace"); /* ERROR_SYSTEM_TRACE */
			s_ErrorMessage.Add(151, "Invalid event count"); /* ERROR_INVALID_EVENT_COUNT */
			s_ErrorMessage.Add(152, "Too many muxwaiters"); /* ERROR_TOO_MANY_MUXWAITERS */
			s_ErrorMessage.Add(153, "Invalid list format"); /* ERROR_INVALID_LIST_FORMAT */
			s_ErrorMessage.Add(154, "Label too long"); /* ERROR_LABEL_TOO_LONG */
			s_ErrorMessage.Add(155, "Too many TCBs"); /* ERROR_TOO_MANY_TCBS */
			s_ErrorMessage.Add(156, "Signal refused"); /* ERROR_SIGNAL_REFUSED */
			s_ErrorMessage.Add(157, "Discarded"); /* ERROR_DISCARDED */
			s_ErrorMessage.Add(158, "Not locked"); /* ERROR_NOT_LOCKED */
			s_ErrorMessage.Add(159, "Bad thread ID addr"); /* ERROR_BAD_THREADID_ADDR */
			s_ErrorMessage.Add(160, "Bad arguments"); /* ERROR_BAD_ARGUMENTS */
			s_ErrorMessage.Add(161, "Bad pathname"); /* ERROR_BAD_PATHNAME */
			s_ErrorMessage.Add(162, "Signal pending"); /* ERROR_SIGNAL_PENDING */
			s_ErrorMessage.Add(164, "Max thrds reached"); /* ERROR_MAX_THRDS_REACHED */
			s_ErrorMessage.Add(167, "Lock failed"); /* ERROR_LOCK_FAILED */
			s_ErrorMessage.Add(170, "Busy"); /* ERROR_BUSY */
			s_ErrorMessage.Add(173, "Cancel violation"); /* ERROR_CANCEL_VIOLATION */
			s_ErrorMessage.Add(174, "Atomic locks not supported"); /* ERROR_ATOMIC_LOCKS_NOT_SUPPORTED */
			s_ErrorMessage.Add(180, "Invalid segment number"); /* ERROR_INVALID_SEGMENT_NUMBER */
			s_ErrorMessage.Add(182, "Invalid ordinal"); /* ERROR_INVALID_ORDINAL */
			s_ErrorMessage.Add(186, "Invalid flag number"); /* ERROR_INVALID_FLAG_NUMBER */
			s_ErrorMessage.Add(187, "Sem not found"); /* ERROR_SEM_NOT_FOUND */
			s_ErrorMessage.Add(188, "Invalid starting codeseg"); /* ERROR_INVALID_STARTING_CODESEG */
			s_ErrorMessage.Add(189, "Invalid stackseg"); /* ERROR_INVALID_STACKSEG */
			s_ErrorMessage.Add(190, "Invalid moduletype"); /* ERROR_INVALID_MODULETYPE */
			s_ErrorMessage.Add(191, "Invalid exe signature"); /* ERROR_INVALID_EXE_SIGNATURE */
			s_ErrorMessage.Add(192, "Exe marked invalid"); /* ERROR_EXE_MARKED_INVALID */
			s_ErrorMessage.Add(193, "Bad exe format"); /* ERROR_BAD_EXE_FORMAT */
			s_ErrorMessage.Add(194, "Iterated data exceeds 64k (and that should be enough for anybody!)"); /* ERROR_ITERATED_DATA_EXCEEDS_64k */
			s_ErrorMessage.Add(195, "Invalid minallocsize"); /* ERROR_INVALID_MINALLOCSIZE */
			s_ErrorMessage.Add(196, "Dynlink from invalid ring"); /* ERROR_DYNLINK_FROM_INVALID_RING */
			s_ErrorMessage.Add(197, "IOPL not enabled"); /* ERROR_IOPL_NOT_ENABLED */
			s_ErrorMessage.Add(198, "Invalid segdpl"); /* ERROR_INVALID_SEGDPL */
			s_ErrorMessage.Add(199, "Autodataseg exceeds 64k"); /* ERROR_AUTODATASEG_EXCEEDS_64k */
			s_ErrorMessage.Add(200, "Ring2seg must be movable"); /* ERROR_RING2SEG_MUST_BE_MOVABLE */
			s_ErrorMessage.Add(201, "Reloc chain exceeds seglim"); /* ERROR_RELOC_CHAIN_XEEDS_SEGLIM */
			s_ErrorMessage.Add(202, "Infloop in reloc chain"); /* ERROR_INFLOOP_IN_RELOC_CHAIN */
			s_ErrorMessage.Add(203, "Env var not found"); /* ERROR_ENVVAR_NOT_FOUND */
			s_ErrorMessage.Add(205, "No signal sent"); /* ERROR_NO_SIGNAL_SENT */
			s_ErrorMessage.Add(206, "Filename exceeds range"); /* ERROR_FILENAME_EXCED_RANGE */
			s_ErrorMessage.Add(207, "Ring2 stack in use"); /* ERROR_RING2_STACK_IN_USE */
			s_ErrorMessage.Add(208, "Meta expansion too long"); /* ERROR_META_EXPANSION_TOO_LONG */
			s_ErrorMessage.Add(209, "Invalid signal number"); /* ERROR_INVALID_SIGNAL_NUMBER */
		}

		public static void InitializeErrorMessages4()
		{
			s_ErrorMessage.Add(210, "Thread 1 inactive"); /* ERROR_THREAD_1_INACTIVE */
			s_ErrorMessage.Add(212, "Locked"); /* ERROR_LOCKED */
			s_ErrorMessage.Add(214, "Too many modules"); /* ERROR_TOO_MANY_MODULES */
			s_ErrorMessage.Add(215, "Nesting not allowed"); /* ERROR_NESTING_NOT_ALLOWED */
			s_ErrorMessage.Add(216, "Exe machine type mismatch"); /* ERROR_EXE_MACHINE_TYPE_MISMATCH */
			s_ErrorMessage.Add(230, "Bad pipe"); /* ERROR_BAD_PIPE */
			s_ErrorMessage.Add(231, "Pipe busy"); /* ERROR_PIPE_BUSY */
			s_ErrorMessage.Add(232, "No data"); /* ERROR_NO_DATA */
			s_ErrorMessage.Add(233, "Pipe not connected"); /* ERROR_PIPE_NOT_CONNECTED */
			s_ErrorMessage.Add(234, "More data"); /* ERROR_MORE_DATA */
			s_ErrorMessage.Add(240, "VC disconnected"); /* ERROR_VC_DISCONNECTED */
			s_ErrorMessage.Add(254, "Invalid EA name"); /* ERROR_INVALID_EA_NAME */
			s_ErrorMessage.Add(255, "EA list inconsistent"); /* ERROR_EA_LIST_INCONSISTENT */
			s_ErrorMessage.Add(258, "Wait timeout"); /* WAIT_TIMEOUT */
			s_ErrorMessage.Add(259, "No more items"); /* ERROR_NO_MORE_ITEMS */
			s_ErrorMessage.Add(266, "Cannot copy"); /* ERROR_CANNOT_COPY */
			s_ErrorMessage.Add(275, "EAS didnt fit"); /* ERROR_EAS_DIDNT_FIT */
			s_ErrorMessage.Add(276, "EA file corrupt"); /* ERROR_EA_FILE_CORRUPT */
			s_ErrorMessage.Add(277, "EA table full"); /* ERROR_EA_TABLE_FULL */
			s_ErrorMessage.Add(278, "Invalid EA handle"); /* ERROR_INVALID_EA_HANDLE */
			s_ErrorMessage.Add(282, "EAs not supported"); /* ERROR_EAS_NOT_SUPPORTED */
			s_ErrorMessage.Add(288, "Not owner"); /* ERROR_NOT_OWNER */
			s_ErrorMessage.Add(298, "Too many posts"); /* ERROR_TOO_MANY_POSTS */
			s_ErrorMessage.Add(299, "Partial copy"); /* ERROR_PARTIAL_COPY */
			s_ErrorMessage.Add(300, "Oplock not granted"); /* ERROR_OPLOCK_NOT_GRANTED */
			s_ErrorMessage.Add(301, "Invalid oplock protocol"); /* ERROR_INVALID_OPLOCK_PROTOCOL */
			s_ErrorMessage.Add(302, "Disk too fragmented"); /* ERROR_DISK_TOO_FRAGMENTED */
			s_ErrorMessage.Add(303, "Delete pending"); /* ERROR_DELETE_PENDING */
			s_ErrorMessage.Add(317, "Mr Mid not found"); /* ERROR_MR_MID_NOT_FOUND */
			s_ErrorMessage.Add(487, "Invalid address"); /* ERROR_INVALID_ADDRESS */
			s_ErrorMessage.Add(534, "Arithmetic overflow"); /* ERROR_ARITHMETIC_OVERFLOW */
			s_ErrorMessage.Add(535, "Pipe connected"); /* ERROR_PIPE_CONNECTED */
			s_ErrorMessage.Add(536, "Pipe listening"); /* ERROR_PIPE_LISTENING */
			s_ErrorMessage.Add(994, "EA access denied"); /* ERROR_EA_ACCESS_DENIED */
			s_ErrorMessage.Add(996, "IO incomplete"); /* ERROR_IO_INCOMPLETE */
			s_ErrorMessage.Add(997, "IO pending"); /* ERROR_IO_PENDING */
			s_ErrorMessage.Add(998, "No access"); /* ERROR_NOACCESS */
			s_ErrorMessage.Add(999, "Swap error"); /* ERROR_SWAPERROR */
			s_ErrorMessage.Add(1001, "Stack overflow"); /* ERROR_STACK_OVERFLOW */
			s_ErrorMessage.Add(1002, "Invalid message"); /* ERROR_INVALID_MESSAGE */
			s_ErrorMessage.Add(1003, "Can not complete"); /* ERROR_CAN_NOT_COMPLETE */
			s_ErrorMessage.Add(1004, "Invalid flags"); /* ERROR_INVALID_FLAGS */
			s_ErrorMessage.Add(1005, "Unrecognised volume"); /* ERROR_UNRECOGNIZED_VOLUME */
			s_ErrorMessage.Add(1006, "File invalid"); /* ERROR_FILE_INVALID */
			s_ErrorMessage.Add(1007, "Full screen mode"); /* ERROR_FULLSCREEN_MODE */
			s_ErrorMessage.Add(1008, "No token"); /* ERROR_NO_TOKEN */
			s_ErrorMessage.Add(1009, "Bad DB"); /* ERROR_BADDB */
			s_ErrorMessage.Add(1010, "Bad key"); /* ERROR_BADKEY */
			s_ErrorMessage.Add(1011, "Can't open"); /* ERROR_CANTOPEN */
			s_ErrorMessage.Add(1012, "Can't read"); /* ERROR_CANTREAD */
			s_ErrorMessage.Add(1013, "Can't write"); /* ERROR_CANTWRITE */
			s_ErrorMessage.Add(1014, "Registry recovered"); /* ERROR_REGISTRY_RECOVERED */
			s_ErrorMessage.Add(1015, "Registry corrupt"); /* ERROR_REGISTRY_CORRUPT */
			s_ErrorMessage.Add(1016, "Registry IO failed"); /* ERROR_REGISTRY_IO_FAILED */
			s_ErrorMessage.Add(1017, "Not registry file"); /* ERROR_NOT_REGISTRY_FILE */
			s_ErrorMessage.Add(1018, "Key deleted"); /* ERROR_KEY_DELETED */
			s_ErrorMessage.Add(1019, "No log space"); /* ERROR_NO_LOG_SPACE */
			s_ErrorMessage.Add(1020, "Key has children"); /* ERROR_KEY_HAS_CHILDREN */
			s_ErrorMessage.Add(1021, "Child must be volatile"); /* ERROR_CHILD_MUST_BE_VOLATILE */
			s_ErrorMessage.Add(1022, "Notify enum dir"); /* ERROR_NOTIFY_ENUM_DIR */
			s_ErrorMessage.Add(1051, "Dependent services running"); /* ERROR_DEPENDENT_SERVICES_RUNNING */
			s_ErrorMessage.Add(1052, "Invalid service control"); /* ERROR_INVALID_SERVICE_CONTROL */
		}

		public static void InitializeErrorMessages5()
		{
			s_ErrorMessage.Add(1053, "Service request timeout"); /* ERROR_SERVICE_REQUEST_TIMEOUT */
			s_ErrorMessage.Add(1054, "Service no thread"); /* ERROR_SERVICE_NO_THREAD */
			s_ErrorMessage.Add(1055, "Service database locked"); /* ERROR_SERVICE_DATABASE_LOCKED */
			s_ErrorMessage.Add(1056, "Service already running"); /* ERROR_SERVICE_ALREADY_RUNNING */
			s_ErrorMessage.Add(1057, "Invalid service account"); /* ERROR_INVALID_SERVICE_ACCOUNT */
			s_ErrorMessage.Add(1058, "Service disabled"); /* ERROR_SERVICE_DISABLED */
			s_ErrorMessage.Add(1059, "Circular dependency"); /* ERROR_CIRCULAR_DEPENDENCY */
			s_ErrorMessage.Add(1060, "Service does not exist"); /* ERROR_SERVICE_DOES_NOT_EXIST */
			s_ErrorMessage.Add(1061, "Service cannot accept ctrl"); /* ERROR_SERVICE_CANNOT_ACCEPT_CTRL */
			s_ErrorMessage.Add(1062, "Service not active"); /* ERROR_SERVICE_NOT_ACTIVE */
			s_ErrorMessage.Add(1063, "Failed service controller connect"); /* ERROR_FAILED_SERVICE_CONTROLLER_CONNECT */
			s_ErrorMessage.Add(1064, "Exception in service"); /* ERROR_EXCEPTION_IN_SERVICE */
			s_ErrorMessage.Add(1065, "Database does not exist"); /* ERROR_DATABASE_DOES_NOT_EXIST */
			s_ErrorMessage.Add(1066, "Service specific error"); /* ERROR_SERVICE_SPECIFIC_ERROR */
			s_ErrorMessage.Add(1067, "Process aborted"); /* ERROR_PROCESS_ABORTED */
			s_ErrorMessage.Add(1068, "Service dependency fail"); /* ERROR_SERVICE_DEPENDENCY_FAIL */
			s_ErrorMessage.Add(1069, "Service logon failed"); /* ERROR_SERVICE_LOGON_FAILED */
			s_ErrorMessage.Add(1070, "Service start hang"); /* ERROR_SERVICE_START_HANG */
			s_ErrorMessage.Add(1071, "Invalid service lock"); /* ERROR_INVALID_SERVICE_LOCK */
			s_ErrorMessage.Add(1072, "Service marked for delete"); /* ERROR_SERVICE_MARKED_FOR_DELETE */
			s_ErrorMessage.Add(1073, "Service exists"); /* ERROR_SERVICE_EXISTS */
			s_ErrorMessage.Add(1074, "Already running lkg"); /* ERROR_ALREADY_RUNNING_LKG */
			s_ErrorMessage.Add(1075, "Service dependency deleted"); /* ERROR_SERVICE_DEPENDENCY_DELETED */
			s_ErrorMessage.Add(1076, "Boot already accepted"); /* ERROR_BOOT_ALREADY_ACCEPTED */
			s_ErrorMessage.Add(1077, "Service never started"); /* ERROR_SERVICE_NEVER_STARTED */
			s_ErrorMessage.Add(1078, "Duplicate service name"); /* ERROR_DUPLICATE_SERVICE_NAME */
			s_ErrorMessage.Add(1079, "Different service account"); /* ERROR_DIFFERENT_SERVICE_ACCOUNT */
			s_ErrorMessage.Add(1080, "Cannot detect driver failure"); /* ERROR_CANNOT_DETECT_DRIVER_FAILURE */
			s_ErrorMessage.Add(1081, "Cannot detect process abort"); /* ERROR_CANNOT_DETECT_PROCESS_ABORT */
			s_ErrorMessage.Add(1082, "No recovery program"); /* ERROR_NO_RECOVERY_PROGRAM */
			s_ErrorMessage.Add(1083, "Service not in exe"); /* ERROR_SERVICE_NOT_IN_EXE */
			s_ErrorMessage.Add(1084, "Not safeboot service"); /* ERROR_NOT_SAFEBOOT_SERVICE */
			s_ErrorMessage.Add(1100, "End of media"); /* ERROR_END_OF_MEDIA */
			s_ErrorMessage.Add(1101, "Filemark detected"); /* ERROR_FILEMARK_DETECTED */
			s_ErrorMessage.Add(1102, "Beginning of media"); /* ERROR_BEGINNING_OF_MEDIA */
			s_ErrorMessage.Add(1103, "Setmark detected"); /* ERROR_SETMARK_DETECTED */
			s_ErrorMessage.Add(1104, "No data detected"); /* ERROR_NO_DATA_DETECTED */
			s_ErrorMessage.Add(1105, "Partition failure"); /* ERROR_PARTITION_FAILURE */
			s_ErrorMessage.Add(1106, "Invalid block length"); /* ERROR_INVALID_BLOCK_LENGTH */
			s_ErrorMessage.Add(1107, "Device not partitioned"); /* ERROR_DEVICE_NOT_PARTITIONED */
			s_ErrorMessage.Add(1108, "Unable to lock media"); /* ERROR_UNABLE_TO_LOCK_MEDIA */
			s_ErrorMessage.Add(1109, "Unable to unload media"); /* ERROR_UNABLE_TO_UNLOAD_MEDIA */
			s_ErrorMessage.Add(1110, "Media changed"); /* ERROR_MEDIA_CHANGED */
			s_ErrorMessage.Add(1111, "Bus reset"); /* ERROR_BUS_RESET */
			s_ErrorMessage.Add(1112, "No media in drive"); /* ERROR_NO_MEDIA_IN_DRIVE */
			s_ErrorMessage.Add(1113, "No unicode translation"); /* ERROR_NO_UNICODE_TRANSLATION */
			s_ErrorMessage.Add(1114, "DLL init failed"); /* ERROR_DLL_INIT_FAILED */
			s_ErrorMessage.Add(1115, "Shutdown in progress"); /* ERROR_SHUTDOWN_IN_PROGRESS */
			s_ErrorMessage.Add(1116, "No shutdown in progress"); /* ERROR_NO_SHUTDOWN_IN_PROGRESS */
			s_ErrorMessage.Add(1117, "IO device"); /* ERROR_IO_DEVICE */
			s_ErrorMessage.Add(1118, "Serial IO device"); /* ERROR_SERIAL_NO_DEVICE */
			s_ErrorMessage.Add(1119, "IRQ busy"); /* ERROR_IRQ_BUSY */
			s_ErrorMessage.Add(1120, "More writes"); /* ERROR_MORE_WRITES */
			s_ErrorMessage.Add(1121, "Counter timeout"); /* ERROR_COUNTER_TIMEOUT */
			s_ErrorMessage.Add(1122, "Floppy ID mark not found"); /* ERROR_FLOPPY_ID_MARK_NOT_FOUND */
			s_ErrorMessage.Add(1123, "Floppy wrong cylinder"); /* ERROR_FLOPPY_WRONG_CYLINDER */
			s_ErrorMessage.Add(1124, "Floppy unknown error"); /* ERROR_FLOPPY_UNKNOWN_ERROR */
			s_ErrorMessage.Add(1125, "Floppy bad registers"); /* ERROR_FLOPPY_BAD_REGISTERS */
			s_ErrorMessage.Add(1126, "Disk recalibrate failed"); /* ERROR_DISK_RECALIBRATE_FAILED */
			s_ErrorMessage.Add(1127, "Disk operation failed"); /* ERROR_DISK_OPERATION_FAILED */
			s_ErrorMessage.Add(1128, "Disk reset failed"); /* ERROR_DISK_RESET_FAILED */
			s_ErrorMessage.Add(1129, "EOM overflow"); /* ERROR_EOM_OVERFLOW */
			s_ErrorMessage.Add(1130, "Not enough server memory"); /* ERROR_NOT_ENOUGH_SERVER_MEMORY */
			s_ErrorMessage.Add(1131, "Possible deadlock"); /* ERROR_POSSIBLE_DEADLOCK */
			s_ErrorMessage.Add(1132, "Mapped alignment"); /* ERROR_MAPPED_ALIGNMENT */
			s_ErrorMessage.Add(1140, "Set power state vetoed"); /* ERROR_SET_POWER_STATE_VETOED */
			s_ErrorMessage.Add(1141, "Set power state failed"); /* ERROR_SET_POWER_STATE_FAILED */
			s_ErrorMessage.Add(1142, "Too many links"); /* ERROR_TOO_MANY_LINKS */
			s_ErrorMessage.Add(1150, "Old win version"); /* ERROR_OLD_WIN_VERSION */
			s_ErrorMessage.Add(1151, "App wrong OS"); /* ERROR_APP_WRONG_OS */
			s_ErrorMessage.Add(1152, "Single instance app"); /* ERROR_SINGLE_INSTANCE_APP */
			s_ErrorMessage.Add(1153, "Rmode app"); /* ERROR_RMODE_APP */
			s_ErrorMessage.Add(1154, "Invalid DLL"); /* ERROR_INVALID_DLL */
			s_ErrorMessage.Add(1155, "No association"); /* ERROR_NO_ASSOCIATION */
			s_ErrorMessage.Add(1156, "DDE fail"); /* ERROR_DDE_FAIL */
			s_ErrorMessage.Add(1157, "DLL not found"); /* ERROR_DLL_NOT_FOUND */
			s_ErrorMessage.Add(1158, "No more user handles"); /* ERROR_NO_MORE_USER_HANDLES */
			s_ErrorMessage.Add(1159, "Message sync only"); /* ERROR_MESSAGE_SYNC_ONLY */
			s_ErrorMessage.Add(1160, "Source element empty"); /* ERROR_SOURCE_ELEMENT_EMPTY */
			s_ErrorMessage.Add(1161, "Destination element full"); /* ERROR_DESTINATION_ELEMENT_FULL */
			s_ErrorMessage.Add(1162, "Illegal element address"); /* ERROR_ILLEGAL_ELEMENT_ADDRESS */
			s_ErrorMessage.Add(1163, "Magazine not present"); /* ERROR_MAGAZINE_NOT_PRESENT */
			s_ErrorMessage.Add(1164, "Device reinitialization needed"); /* ERROR_DEVICE_REINITIALIZATION_NEEDED */
			s_ErrorMessage.Add(1165, "Device requires cleaning"); /* ERROR_DEVICE_REQUIRES_CLEANING */
			s_ErrorMessage.Add(1166, "Device door open"); /* ERROR_DEVICE_DOOR_OPEN */
			s_ErrorMessage.Add(1167, "Device not connected"); /* ERROR_DEVICE_NOT_CONNECTED */
			s_ErrorMessage.Add(1168, "Not found"); /* ERROR_NOT_FOUND */
			s_ErrorMessage.Add(1169, "No match"); /* ERROR_NO_MATCH */
			s_ErrorMessage.Add(1170, "Set not found"); /* ERROR_SET_NOT_FOUND */
			s_ErrorMessage.Add(1171, "Point not found"); /* ERROR_POINT_NOT_FOUND */
			s_ErrorMessage.Add(1172, "No tracking service"); /* ERROR_NO_TRACKING_SERVICE */
			s_ErrorMessage.Add(1173, "No volume ID"); /* ERROR_NO_VOLUME_ID */
			s_ErrorMessage.Add(1175, "Unable to remove replaced"); /* ERROR_UNABLE_TO_REMOVE_REPLACED */
			s_ErrorMessage.Add(1176, "Unable to move replacement"); /* ERROR_UNABLE_TO_MOVE_REPLACEMENT */
			s_ErrorMessage.Add(1177, "Unable to move replacement 2"); /* ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 */
			s_ErrorMessage.Add(1178, "Journal delete in progress"); /* ERROR_JOURNAL_DELETE_IN_PROGRESS */
			s_ErrorMessage.Add(1179, "Journal not active"); /* ERROR_JOURNAL_NOT_ACTIVE */
			s_ErrorMessage.Add(1180, "Potential file found"); /* ERROR_POTENTIAL_FILE_FOUND */
			s_ErrorMessage.Add(1181, "Journal entry deleted"); /* ERROR_JOURNAL_ENTRY_DELETED */
			s_ErrorMessage.Add(1200, "Bad device"); /* ERROR_BAD_DEVICE */
			s_ErrorMessage.Add(1201, "Connection unavail"); /* ERROR_CONNECTION_UNAVAIL */
			s_ErrorMessage.Add(1202, "Device already remembered"); /* ERROR_DEVICE_ALREADY_REMEMBERED */
			s_ErrorMessage.Add(1203, "No net or bad path"); /* ERROR_NO_NET_OR_BAD_PATH */
			s_ErrorMessage.Add(1204, "Bad provider"); /* ERROR_BAD_PROVIDER */
			s_ErrorMessage.Add(1205, "Cannot open profile"); /* ERROR_CANNOT_OPEN_PROFILE */
			s_ErrorMessage.Add(1206, "Bad profile"); /* ERROR_BAD_PROFILE */
			s_ErrorMessage.Add(1207, "Not container"); /* ERROR_NOT_CONTAINER */
			s_ErrorMessage.Add(1208, "Extended error"); /* ERROR_EXTENDED_ERROR */
			s_ErrorMessage.Add(1209, "Invalid group name"); /* ERROR_INVALID_GROUPNAME */
			s_ErrorMessage.Add(1210, "Invalid computer name"); /* ERROR_INVALID_COMPUTERNAME */
			s_ErrorMessage.Add(1211, "Invalid event name"); /* ERROR_INVALID_EVENTNAME */
			s_ErrorMessage.Add(1212, "Invalid domain name"); /* ERROR_INVALID_DOMAINNAME */
			s_ErrorMessage.Add(1213, "Invalid service name"); /* ERROR_INVALID_SERVICENAME */
			s_ErrorMessage.Add(1214, "Invalid net name"); /* ERROR_INVALID_NETNAME */
			s_ErrorMessage.Add(1215, "Invalid share name"); /* ERROR_INVALID_SHARENAME */
			s_ErrorMessage.Add(1216, "Invalid password name"); /* ERROR_INVALID_PASSWORDNAME */
			s_ErrorMessage.Add(1217, "Invalid message name"); /* ERROR_INVALID_MESSAGENAME */
			s_ErrorMessage.Add(1218, "Invalid message dest"); /* ERROR_INVALID_MESSAGEDEST */
			s_ErrorMessage.Add(1219, "Session credential conflict"); /* ERROR_SESSION_CREDENTIAL_CONFLICT */
			s_ErrorMessage.Add(1220, "Remote session limit exceeded"); /* ERROR_REMOTE_SESSION_LIMIT_EXCEEDED */
			s_ErrorMessage.Add(1221, "Dup domain name"); /* ERROR_DUP_DOMAINNAME */
			s_ErrorMessage.Add(1222, "No network"); /* ERROR_NO_NETWORK */
			s_ErrorMessage.Add(1223, "Cancelled"); /* ERROR_CANCELLED */
			s_ErrorMessage.Add(1224, "User mapped file"); /* ERROR_USER_MAPPED_FILE */
			s_ErrorMessage.Add(1225, "Connection refused"); /* ERROR_CONNECTION_REFUSED */
			s_ErrorMessage.Add(1226, "Graceful disconnect"); /* ERROR_GRACEFUL_DISCONNECT */
			s_ErrorMessage.Add(1227, "Address already associated"); /* ERROR_ADDRESS_ALREADY_ASSOCIATED */
			s_ErrorMessage.Add(1228, "Address not associated"); /* ERROR_ADDRESS_NOT_ASSOCIATED */
			s_ErrorMessage.Add(1229, "Connected invalid"); /* ERROR_CONNECTION_INVALID */
			s_ErrorMessage.Add(1230, "Connection active"); /* ERROR_CONNECTION_ACTIVE */
			s_ErrorMessage.Add(1231, "Network unreachable"); /* ERROR_NETWORK_UNREACHABLE */
			s_ErrorMessage.Add(1232, "Host unreachable"); /* ERROR_HOST_UNREACHABLE */
			s_ErrorMessage.Add(1233, "Protocol unreachable"); /* ERROR_PROTOCOL_UNREACHABLE */
			s_ErrorMessage.Add(1234, "Port unreachable"); /* ERROR_PORT_UNREACHABLE */
			s_ErrorMessage.Add(1235, "Request aborted"); /* ERROR_REQUEST_ABORTED */
		}

		public static void InitializeErrorMessages6()
		{
			s_ErrorMessage.Add(1236, "Connection aborted"); /* ERROR_CONNECTION_ABORTED */
			s_ErrorMessage.Add(1237, "Retry"); /* ERROR_RETRY */
			s_ErrorMessage.Add(1238, "Connection count limit"); /* ERROR_CONNECTION_COUNT_LIMIT */
			s_ErrorMessage.Add(1239, "Login time restriction"); /* ERROR_LOGIN_TIME_RESTRICTION */
			s_ErrorMessage.Add(1240, "Login wksta restriction"); /* ERROR_LOGIN_WKSTA_RESTRICTION */
			s_ErrorMessage.Add(1241, "Incorrect address"); /* ERROR_INCORRECT_ADDRESS */
			s_ErrorMessage.Add(1242, "Already registered"); /* ERROR_ALREADY_REGISTERED */
			s_ErrorMessage.Add(1243, "Service not found"); /* ERROR_SERVICE_NOT_FOUND */
			s_ErrorMessage.Add(1244, "Not authenticated"); /* ERROR_NOT_AUTHENTICATED */
			s_ErrorMessage.Add(1245, "Not logged on"); /* ERROR_NOT_LOGGED_ON */
			s_ErrorMessage.Add(1246, "Continue"); /* ERROR_CONTINUE */
			s_ErrorMessage.Add(1247, "Already initialised"); /* ERROR_ALREADY_INITIALIZED */
			s_ErrorMessage.Add(1248, "No more devices"); /* ERROR_NO_MORE_DEVICES */
			s_ErrorMessage.Add(1249, "No such site"); /* ERROR_NO_SUCH_SITE */
			s_ErrorMessage.Add(1250, "Domain controller exists"); /* ERROR_DOMAIN_CONTROLLER_EXISTS */
			s_ErrorMessage.Add(1251, "Only if connected"); /* ERROR_ONLY_IF_CONNECTED */
			s_ErrorMessage.Add(1252, "Override no changes"); /* ERROR_OVERRIDE_NOCHANGES */
			s_ErrorMessage.Add(1253, "Bad user profile"); /* ERROR_BAD_USER_PROFILE */
			s_ErrorMessage.Add(1254, "Not supported on SBS"); /* ERROR_NOT_SUPPORTED_ON_SBS */
			s_ErrorMessage.Add(1255, "Server shutdown in progress"); /* ERROR_SERVER_SHUTDOWN_IN_PROGRESS */
			s_ErrorMessage.Add(1256, "Host down"); /* ERROR_HOST_DOWN */
			s_ErrorMessage.Add(1257, "Non account sid"); /* ERROR_NON_ACCOUNT_SID */
			s_ErrorMessage.Add(1258, "Non domain sid"); /* ERROR_NON_DOMAIN_SID */
			s_ErrorMessage.Add(1259, "Apphelp block"); /* ERROR_APPHELP_BLOCK */
			s_ErrorMessage.Add(1260, "Access disabled by policy"); /* ERROR_ACCESS_DISABLED_BY_POLICY */
			s_ErrorMessage.Add(1261, "Reg nat consumption"); /* ERROR_REG_NAT_CONSUMPTION */
			s_ErrorMessage.Add(1262, "CSC share offline"); /* ERROR_CSCSHARE_OFFLINE */
			s_ErrorMessage.Add(1263, "PK init failure"); /* ERROR_PKINIT_FAILURE */
			s_ErrorMessage.Add(1264, "Smartcard subsystem failure"); /* ERROR_SMARTCARD_SUBSYSTEM_FAILURE */
			s_ErrorMessage.Add(1265, "Downgrade detected"); /* ERROR_DOWNGRADE_DETECTED */
			s_ErrorMessage.Add(1266, "Smartcard cert revoked"); /* SEC_E_SMARTCARD_CERT_REVOKED */
			s_ErrorMessage.Add(1267, "Issuing CA untrusted"); /* SEC_E_ISSUING_CA_UNTRUSTED */
			s_ErrorMessage.Add(1268, "Revocation offline"); /* SEC_E_REVOCATION_OFFLINE_C */
			s_ErrorMessage.Add(1269, "PK init client failure"); /* SEC_E_PKINIT_CLIENT_FAILUR */
			s_ErrorMessage.Add(1270, "Smartcard cert expired"); /* SEC_E_SMARTCARD_CERT_EXPIRED */
			s_ErrorMessage.Add(1271, "Machine locked"); /* ERROR_MACHINE_LOCKED */
			s_ErrorMessage.Add(1273, "Callback supplied invalid data"); /* ERROR_CALLBACK_SUPPLIED_INVALID_DATA */
			s_ErrorMessage.Add(1274, "Sync foreground refresh required"); /* ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED */
			s_ErrorMessage.Add(1275, "Driver blocked"); /* ERROR_DRIVER_BLOCKED */
			s_ErrorMessage.Add(1276, "Invalid import of non DLL"); /* ERROR_INVALID_IMPORT_OF_NON_DLL */
			s_ErrorMessage.Add(1300, "Not all assigned"); /* ERROR_NOT_ALL_ASSIGNED */
			s_ErrorMessage.Add(1301, "Some not mapped"); /* ERROR_SOME_NOT_MAPPED */
			s_ErrorMessage.Add(1302, "No quotas for account"); /* ERROR_NO_QUOTAS_FOR_ACCOUNT */
			s_ErrorMessage.Add(1303, "Local user session key"); /* ERROR_LOCAL_USER_SESSION_KEY */
			s_ErrorMessage.Add(1304, "Null LM password"); /* ERROR_NULL_LM_PASSWORD */
			s_ErrorMessage.Add(1305, "Unknown revision"); /* ERROR_UNKNOWN_REVISION */
			s_ErrorMessage.Add(1306, "Revision mismatch"); /* ERROR_REVISION_MISMATCH */
			s_ErrorMessage.Add(1307, "Invalid owner"); /* ERROR_INVALID_OWNER */
			s_ErrorMessage.Add(1308, "Invalid primary group"); /* ERROR_INVALID_PRIMARY_GROUP */
			s_ErrorMessage.Add(1309, "No impersonation token"); /* ERROR_NO_IMPERSONATION_TOKEN */
			s_ErrorMessage.Add(1310, "Can't disable mandatory"); /* ERROR_CANT_DISABLE_MANDATORY */
			s_ErrorMessage.Add(1311, "No logon servers"); /* ERROR_NO_LOGON_SERVERS */
			s_ErrorMessage.Add(1312, "No such logon session"); /* ERROR_NO_SUCH_LOGON_SESSION */
			s_ErrorMessage.Add(1313, "No such privilege"); /* ERROR_NO_SUCH_PRIVILEGE */
			s_ErrorMessage.Add(1314, "Privilege not held"); /* ERROR_PRIVILEGE_NOT_HELD */
			s_ErrorMessage.Add(1315, "Invalid account name"); /* ERROR_INVALID_ACCOUNT_NAME */
			s_ErrorMessage.Add(1316, "User exists"); /* ERROR_USER_EXISTS */
			s_ErrorMessage.Add(1317, "No such user"); /* ERROR_NO_SUCH_USER */
			s_ErrorMessage.Add(1318, "Group exists"); /* ERROR_GROUP_EXISTS */
			s_ErrorMessage.Add(1319, "No such group"); /* ERROR_NO_SUCH_GROUP */
			s_ErrorMessage.Add(1320, "Member in group"); /* ERROR_MEMBER_IN_GROUP */
			s_ErrorMessage.Add(1321, "Member not in group"); /* ERROR_MEMBER_NOT_IN_GROUP */
			s_ErrorMessage.Add(1322, "Last admin"); /* ERROR_LAST_ADMIN */
			s_ErrorMessage.Add(1323, "Wrong password"); /* ERROR_WRONG_PASSWORD */
			s_ErrorMessage.Add(1324, "Ill formed password"); /* ERROR_ILL_FORMED_PASSWORD */
			s_ErrorMessage.Add(1325, "Password restriction"); /* ERROR_PASSWORD_RESTRICTION */
			s_ErrorMessage.Add(1326, "Logon failure"); /* ERROR_LOGON_FAILURE */
			s_ErrorMessage.Add(1327, "Account restriction"); /* ERROR_ACCOUNT_RESTRICTION */
			s_ErrorMessage.Add(1328, "Invalid logon hours"); /* ERROR_INVALID_LOGON_HOURS */
			s_ErrorMessage.Add(1329, "Invalid workstation"); /* ERROR_INVALID_WORKSTATION */
			s_ErrorMessage.Add(1330, "Password expired"); /* ERROR_PASSWORD_EXPIRED */
			s_ErrorMessage.Add(1331, "Account disabled"); /* ERROR_ACCOUNT_DISABLED */
			s_ErrorMessage.Add(1332, "None mapped"); /* ERROR_NONE_MAPPED */
			s_ErrorMessage.Add(1333, "Too many LUIDs requested"); /* ERROR_TOO_MANY_LUIDS_REQUESTED */
			s_ErrorMessage.Add(1334, "LUIDs exhausted"); /* ERROR_LUIDS_EXHAUSTED */
			s_ErrorMessage.Add(1335, "Invalid sub authority"); /* ERROR_INVALID_SUB_AUTHORITY */
			s_ErrorMessage.Add(1336, "Invalid ACL"); /* ERROR_INVALID_ACL */
			s_ErrorMessage.Add(1337, "Invalid SID"); /* ERROR_INVALID_SID */
			s_ErrorMessage.Add(1338, "Invalid security descr"); /* ERROR_INVALID_SECURITY_DESCR */
			s_ErrorMessage.Add(1340, "Bad inheritance ACL"); /* ERROR_BAD_INHERITANCE_ACL */
			s_ErrorMessage.Add(1341, "Server disabled"); /* ERROR_SERVER_DISABLED */
			s_ErrorMessage.Add(1342, "Server not disabled"); /* ERROR_SERVER_NOT_DISABLED */
			s_ErrorMessage.Add(1343, "Invalid ID authority"); /* ERROR_INVALID_ID_AUTHORITY */
			s_ErrorMessage.Add(1344, "Allotted space exceeded"); /* ERROR_ALLOTTED_SPACE_EXCEEDED */
			s_ErrorMessage.Add(1345, "Invalid group attributes"); /* ERROR_INVALID_GROUP_ATTRIBUTES */
			s_ErrorMessage.Add(1346, "Bad impersonation level"); /* ERROR_BAD_IMPERSONATION_LEVEL */
			s_ErrorMessage.Add(1347, "Can't open anonymous"); /* ERROR_CANT_OPEN_ANONYMOUS */
			s_ErrorMessage.Add(1348, "Bad validation class"); /* ERROR_BAD_VALIDATION_CLASS */
			s_ErrorMessage.Add(1349, "Bad token type"); /* ERROR_BAD_TOKEN_TYPE */
			s_ErrorMessage.Add(1350, "No security on object"); /* ERROR_NO_SECURITY_ON_OBJECT */
			s_ErrorMessage.Add(1351, "Can't access domain info"); /* ERROR_CANT_ACCESS_DOMAIN_INFO */
			s_ErrorMessage.Add(1352, "Invalid server state"); /* ERROR_INVALID_SERVER_STATE */
			s_ErrorMessage.Add(1353, "Invalid domain state"); /* ERROR_INVALID_DOMAIN_STATE */
			s_ErrorMessage.Add(1354, "Invalid domain role"); /* ERROR_INVALID_DOMAIN_ROLE */
			s_ErrorMessage.Add(1355, "No such domain"); /* ERROR_NO_SUCH_DOMAIN */
			s_ErrorMessage.Add(1356, "Domain exists"); /* ERROR_DOMAIN_EXISTS */
			s_ErrorMessage.Add(1357, "Domain limit exceeded"); /* ERROR_DOMAIN_LIMIT_EXCEEDED */
			s_ErrorMessage.Add(1358, "Internal DB corruption"); /* ERROR_INTERNAL_DB_CORRUPTION */
			s_ErrorMessage.Add(1359, "Internal error"); /* ERROR_INTERNAL_ERROR */
			s_ErrorMessage.Add(1360, "Generic not mapped"); /* ERROR_GENERIC_NOT_MAPPED */
			s_ErrorMessage.Add(1361, "Bad descriptor format"); /* ERROR_BAD_DESCRIPTOR_FORMAT */
			s_ErrorMessage.Add(1362, "Not logon process"); /* ERROR_NOT_LOGON_PROCESS */
			s_ErrorMessage.Add(1363, "Logon session exists"); /* ERROR_LOGON_SESSION_EXISTS */
			s_ErrorMessage.Add(1364, "No such package"); /* ERROR_NO_SUCH_PACKAGE */
			s_ErrorMessage.Add(1365, "Bad logon session state"); /* ERROR_BAD_LOGON_SESSION_STATE */
			s_ErrorMessage.Add(1366, "Logon session collision"); /* ERROR_LOGON_SESSION_COLLISION */
			s_ErrorMessage.Add(1367, "Invalid logon type"); /* ERROR_INVALID_LOGON_TYPE */
		}

		public static void InitializeErrorMessages7()
		{
			s_ErrorMessage.Add(1368, "Cannot impersonate"); /* ERROR_CANNOT_IMPERSONATE */
			s_ErrorMessage.Add(1369, "Rxact invalid state"); /* ERROR_RXACT_INVALID_STATE */
			s_ErrorMessage.Add(1370, "Rxact commit failure"); /* ERROR_RXACT_COMMIT_FAILURE */
			s_ErrorMessage.Add(1371, "Special account"); /* ERROR_SPECIAL_ACCOUNT */
			s_ErrorMessage.Add(1372, "Special group"); /* ERROR_SPECIAL_GROUP */
			s_ErrorMessage.Add(1373, "Special user"); /* ERROR_SPECIAL_USER */
			s_ErrorMessage.Add(1374, "Members primary group"); /* ERROR_MEMBERS_PRIMARY_GROUP */
			s_ErrorMessage.Add(1375, "Token already in use"); /* ERROR_TOKEN_ALREADY_IN_USE */
			s_ErrorMessage.Add(1376, "No such alias"); /* ERROR_NO_SUCH_ALIAS */
			s_ErrorMessage.Add(1377, "Member not in alias"); /* ERROR_MEMBER_NOT_IN_ALIAS */
			s_ErrorMessage.Add(1378, "Member in alias"); /* ERROR_MEMBER_IN_ALIAS */
			s_ErrorMessage.Add(1379, "Alias exists"); /* ERROR_ALIAS_EXISTS */
			s_ErrorMessage.Add(1380, "Logon not granted"); /* ERROR_LOGON_NOT_GRANTED */
			s_ErrorMessage.Add(1381, "Too many secrets"); /* ERROR_TOO_MANY_SECRETS */
			s_ErrorMessage.Add(1382, "Secret too long"); /* ERROR_SECRET_TOO_LONG */
			s_ErrorMessage.Add(1383, "Internal DB error"); /* ERROR_INTERNAL_DB_ERROR */
			s_ErrorMessage.Add(1384, "Too many context IDs"); /* ERROR_TOO_MANY_CONTEXT_IDS */
			s_ErrorMessage.Add(1385, "Logon type not granted"); /* ERROR_LOGON_TYPE_NOT_GRANTED */
			s_ErrorMessage.Add(1386, "NT cross encryption required"); /* ERROR_NT_CROSS_ENCRYPTION_REQUIRED */
			s_ErrorMessage.Add(1387, "No such member"); /* ERROR_NO_SUCH_MEMBER */
			s_ErrorMessage.Add(1388, "Invalid member"); /* ERROR_INVALID_MEMBER */
			s_ErrorMessage.Add(1389, "Too many SIDs"); /* ERROR_TOO_MANY_SIDS */
			s_ErrorMessage.Add(1390, "LM cross encryption required"); /* ERROR_LM_CROSS_ENCRYPTION_REQUIRED */
			s_ErrorMessage.Add(1391, "No inheritance"); /* ERROR_NO_INHERITANCE */
			s_ErrorMessage.Add(1392, "File corrupt"); /* ERROR_FILE_CORRUPT */
			s_ErrorMessage.Add(1393, "Disk corrupt"); /* ERROR_DISK_CORRUPT */
			s_ErrorMessage.Add(1394, "No user session key"); /* ERROR_NO_USER_SESSION_KEY */
			s_ErrorMessage.Add(1395, "Licence quota exceeded"); /* ERROR_LICENSE_QUOTA_EXCEEDED */
			s_ErrorMessage.Add(1396, "Wrong target name"); /* ERROR_WRONG_TARGET_NAME */
			s_ErrorMessage.Add(1397, "Mutual auth failed"); /* ERROR_MUTUAL_AUTH_FAILED */
			s_ErrorMessage.Add(1398, "Time skew"); /* ERROR_TIME_SKEW */
			s_ErrorMessage.Add(1399, "Current domain not allowed"); /* ERROR_CURRENT_DOMAIN_NOT_ALLOWED */
			s_ErrorMessage.Add(1400, "Invalid window handle"); /* ERROR_INVALID_WINDOW_HANDLE */
			s_ErrorMessage.Add(1401, "Invalid menu handle"); /* ERROR_INVALID_MENU_HANDLE */
			s_ErrorMessage.Add(1402, "Invalid cursor handle"); /* ERROR_INVALID_CURSOR_HANDLE */
			s_ErrorMessage.Add(1403, "Invalid accel handle"); /* ERROR_INVALID_ACCEL_HANDLE */
			s_ErrorMessage.Add(1404, "Invalid hook handle"); /* ERROR_INVALID_HOOK_HANDLE */
			s_ErrorMessage.Add(1405, "Invalid DWP handle"); /* ERROR_INVALID_DWP_HANDLE */
			s_ErrorMessage.Add(1406, "TLW with wschild"); /* ERROR_TLW_WITH_WSCHILD */
			s_ErrorMessage.Add(1407, "Cannot find WND class"); /* ERROR_CANNOT_FIND_WND_CLASS */
			s_ErrorMessage.Add(1408, "Window of other thread"); /* ERROR_WINDOW_OF_OTHER_THREAD */
			s_ErrorMessage.Add(1409, "Hotkey already registered"); /* ERROR_HOTKEY_ALREADY_REGISTERED */
			s_ErrorMessage.Add(1410, "Class already exists"); /* ERROR_CLASS_ALREADY_EXISTS */
			s_ErrorMessage.Add(1411, "Class does not exist"); /* ERROR_CLASS_DOES_NOT_EXIST */
			s_ErrorMessage.Add(1412, "Class has windows"); /* ERROR_CLASS_HAS_WINDOWS */
			s_ErrorMessage.Add(1413, "Invalid index"); /* ERROR_INVALID_INDEX */
			s_ErrorMessage.Add(1414, "Invalid icon handle"); /* ERROR_INVALID_ICON_HANDLE */
			s_ErrorMessage.Add(1415, "Private dialog index"); /* ERROR_PRIVATE_DIALOG_INDEX */
			s_ErrorMessage.Add(1416, "Listbox ID not found"); /* ERROR_LISTBOX_ID_NOT_FOUND */
			s_ErrorMessage.Add(1417, "No wildcard characters"); /* ERROR_NO_WILDCARD_CHARACTERS */
			s_ErrorMessage.Add(1418, "Clipboard not open"); /* ERROR_CLIPBOARD_NOT_OPEN */
			s_ErrorMessage.Add(1419, "Hotkey not registered"); /* ERROR_HOTKEY_NOT_REGISTERED */
			s_ErrorMessage.Add(1420, "Window not dialog"); /* ERROR_WINDOW_NOT_DIALOG */
			s_ErrorMessage.Add(1421, "Control ID not found"); /* ERROR_CONTROL_ID_NOT_FOUND */
			s_ErrorMessage.Add(1422, "Invalid combobox message"); /* ERROR_INVALID_COMBOBOX_MESSAGE */
			s_ErrorMessage.Add(1423, "Window not combobox"); /* ERROR_WINDOW_NOT_COMBOBOX */
			s_ErrorMessage.Add(1424, "Invalid edit height"); /* ERROR_INVALID_EDIT_HEIGHT */
			s_ErrorMessage.Add(1425, "DC not found"); /* ERROR_DC_NOT_FOUND */
			s_ErrorMessage.Add(1426, "Invalid hook filter"); /* ERROR_INVALID_HOOK_FILTER */
			s_ErrorMessage.Add(1427, "Invalid filter proc"); /* ERROR_INVALID_FILTER_PROC */
			s_ErrorMessage.Add(1428, "Hook needs HMOD"); /* ERROR_HOOK_NEEDS_HMOD */
			s_ErrorMessage.Add(1429, "Global only hook"); /* ERROR_GLOBAL_ONLY_HOOK */
			s_ErrorMessage.Add(1430, "Journal hook set"); /* ERROR_JOURNAL_HOOK_SET */
			s_ErrorMessage.Add(1431, "Hook not installed"); /* ERROR_HOOK_NOT_INSTALLED */
			s_ErrorMessage.Add(1432, "Invalid LB message"); /* ERROR_INVALID_LB_MESSAGE */
			s_ErrorMessage.Add(1433, "Setcount on bad LB"); /* ERROR_SETCOUNT_ON_BAD_LB */
			s_ErrorMessage.Add(1434, "LB without tabstops"); /* ERROR_LB_WITHOUT_TABSTOPS */
			s_ErrorMessage.Add(1435, "Destroy object of other thread"); /* ERROR_DESTROY_OBJECT_OF_OTHER_THREAD */
			s_ErrorMessage.Add(1436, "Child window menu"); /* ERROR_CHILD_WINDOW_MENU */
			s_ErrorMessage.Add(1437, "No system menu"); /* ERROR_NO_SYSTEM_MENU */
			s_ErrorMessage.Add(1438, "Invalid msgbox style"); /* ERROR_INVALID_MSGBOX_STYLE */
			s_ErrorMessage.Add(1439, "Invalid SPI value"); /* ERROR_INVALID_SPI_VALUE */
			s_ErrorMessage.Add(1440, "Screen already locked"); /* ERROR_SCREEN_ALREADY_LOCKED */
			s_ErrorMessage.Add(1441, "HWNDs have different parent"); /* ERROR_HWNDS_HAVE_DIFF_PARENT */
			s_ErrorMessage.Add(1442, "Not child window"); /* ERROR_NOT_CHILD_WINDOW */
			s_ErrorMessage.Add(1443, "Invalid GW command"); /* ERROR_INVALID_GW_COMMAND */
			s_ErrorMessage.Add(1444, "Invalid thread ID"); /* ERROR_INVALID_THREAD_ID */
			s_ErrorMessage.Add(1445, "Non MDI child window"); /* ERROR_NON_MDICHILD_WINDOW */
			s_ErrorMessage.Add(1446, "Popup already active"); /* ERROR_POPUP_ALREADY_ACTIVE */
			s_ErrorMessage.Add(1447, "No scrollbars"); /* ERROR_NO_SCROLLBARS */
			s_ErrorMessage.Add(1448, "Invalid scrollbar range"); /* ERROR_INVALID_SCROLLBAR_RANGE */
			s_ErrorMessage.Add(1449, "Invalid showwin command"); /* ERROR_INVALID_SHOWWIN_COMMAND */
			s_ErrorMessage.Add(1450, "No system resources"); /* ERROR_NO_SYSTEM_RESOURCES */
			s_ErrorMessage.Add(1451, "Nonpaged system resources"); /* ERROR_NONPAGED_SYSTEM_RESOURCES */
			s_ErrorMessage.Add(1452, "Paged system resources"); /* ERROR_PAGED_SYSTEM_RESOURCES */
			s_ErrorMessage.Add(1453, "Working set quota"); /* ERROR_WORKING_SET_QUOTA */
			s_ErrorMessage.Add(1454, "Pagefile quota"); /* ERROR_PAGEFILE_QUOTA */
			s_ErrorMessage.Add(1455, "Commitment limit"); /* ERROR_COMMITMENT_LIMIT */
			s_ErrorMessage.Add(1456, "Menu item not found"); /* ERROR_MENU_ITEM_NOT_FOUND */
			s_ErrorMessage.Add(1457, "Invalid keyboard handle"); /* ERROR_INVALID_KEYBOARD_HANDLE */
			s_ErrorMessage.Add(1458, "Hook type not allowed"); /* ERROR_HOOK_TYPE_NOT_ALLOWED */
			s_ErrorMessage.Add(1459, "Requires interactive windowstation"); /* ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION */
			s_ErrorMessage.Add(1460, "Timeout"); /* ERROR_TIMEOUT */
			s_ErrorMessage.Add(1461, "Invalid monitor handle"); /* ERROR_INVALID_MONITOR_HANDLE */
			s_ErrorMessage.Add(1500, "Eventlog file corrupt"); /* ERROR_EVENTLOG_FILE_CORRUPT */
			s_ErrorMessage.Add(1501, "Eventlog can't start"); /* ERROR_EVENTLOG_CANT_START */
			s_ErrorMessage.Add(1502, "Log file full"); /* ERROR_LOG_FILE_FULL */
			s_ErrorMessage.Add(1503, "Eventlog file changed"); /* ERROR_EVENTLOG_FILE_CHANGED */
			s_ErrorMessage.Add(1601, "Install service failure"); /* ERROR_INSTALL_SERVICE_FAILURE */
			s_ErrorMessage.Add(1602, "Install userexit"); /* ERROR_INSTALL_USEREXIT */
			s_ErrorMessage.Add(1603, "Install failure"); /* ERROR_INSTALL_FAILURE */
			s_ErrorMessage.Add(1604, "Install suspend"); /* ERROR_INSTALL_SUSPEND */
			s_ErrorMessage.Add(1605, "Unknown product"); /* ERROR_UNKNOWN_PRODUCT */
			s_ErrorMessage.Add(1606, "Unknown feature"); /* ERROR_UNKNOWN_FEATURE */
			s_ErrorMessage.Add(1607, "Unknown component"); /* ERROR_UNKNOWN_COMPONENT */
			s_ErrorMessage.Add(1608, "Unknown property"); /* ERROR_UNKNOWN_PROPERTY */
			s_ErrorMessage.Add(1609, "Invalid handle state"); /* ERROR_INVALID_HANDLE_STATE */
			s_ErrorMessage.Add(1610, "Bad configuration"); /* ERROR_BAD_CONFIGURATION */
			s_ErrorMessage.Add(1611, "Index absent"); /* ERROR_INDEX_ABSENT */
			s_ErrorMessage.Add(1612, "Install source absent"); /* ERROR_INSTALL_SOURCE_ABSENT */
			s_ErrorMessage.Add(1613, "Install package version"); /* ERROR_INSTALL_PACKAGE_VERSION */
			s_ErrorMessage.Add(1614, "Product uninstalled"); /* ERROR_PRODUCT_UNINSTALLED */
			s_ErrorMessage.Add(1615, "Bad query syntax"); /* ERROR_BAD_QUERY_SYNTAX */
			s_ErrorMessage.Add(1616, "Invalid field"); /* ERROR_INVALID_FIELD */
			s_ErrorMessage.Add(1617, "Device removed"); /* ERROR_DEVICE_REMOVED */
			s_ErrorMessage.Add(1618, "Install already running"); /* ERROR_INSTALL_ALREADY_RUNNING */
			s_ErrorMessage.Add(1619, "Install package open failed"); /* ERROR_INSTALL_PACKAGE_OPEN_FAILED */
			s_ErrorMessage.Add(1620, "Install package invalid"); /* ERROR_INSTALL_PACKAGE_INVALID */
			s_ErrorMessage.Add(1621, "Install UI failure"); /* ERROR_INSTALL_UI_FAILURE */
			s_ErrorMessage.Add(1622, "Install log failure"); /* ERROR_INSTALL_LOG_FAILURE */
			s_ErrorMessage.Add(1623, "Install language unsupported"); /* ERROR_INSTALL_LANGUAGE_UNSUPPORTED */
			s_ErrorMessage.Add(1624, "Install transform failure"); /* ERROR_INSTALL_TRANSFORM_FAILURE */
			s_ErrorMessage.Add(1625, "Install package rejected"); /* ERROR_INSTALL_PACKAGE_REJECTED */
			s_ErrorMessage.Add(1626, "Function not called"); /* ERROR_FUNCTION_NOT_CALLED */
			s_ErrorMessage.Add(1627, "Function failed"); /* ERROR_FUNCTION_FAILED */
			s_ErrorMessage.Add(1628, "Invalid table"); /* ERROR_INVALID_TABLE */
			s_ErrorMessage.Add(1629, "Datatype mismatch"); /* ERROR_DATATYPE_MISMATCH */
			s_ErrorMessage.Add(1630, "Unsupported type"); /* ERROR_UNSUPPORTED_TYPE */
			s_ErrorMessage.Add(1631, "Create failed"); /* ERROR_CREATE_FAILED */
			s_ErrorMessage.Add(1632, "Install temp unwritable"); /* ERROR_INSTALL_TEMP_UNWRITABLE */
			s_ErrorMessage.Add(1633, "Install platform unsupported"); /* ERROR_INSTALL_PLATFORM_UNSUPPORTED */
			s_ErrorMessage.Add(1634, "Install notused"); /* ERROR_INSTALL_NOTUSED */
			s_ErrorMessage.Add(1635, "Patch package open failed"); /* ERROR_PATCH_PACKAGE_OPEN_FAILED */
			s_ErrorMessage.Add(1636, "Patch package invalid"); /* ERROR_PATCH_PACKAGE_INVALID */
			s_ErrorMessage.Add(1637, "Patch package unsupported"); /* ERROR_PATCH_PACKAGE_UNSUPPORTED */
			s_ErrorMessage.Add(1638, "Product version"); /* ERROR_PRODUCT_VERSION */
			s_ErrorMessage.Add(1639, "Invalid command line"); /* ERROR_INVALID_COMMAND_LINE */
			s_ErrorMessage.Add(1640, "Install remote disallowed"); /* ERROR_INSTALL_REMOTE_DISALLOWED */
			s_ErrorMessage.Add(1641, "Success reboot initiated"); /* ERROR_SUCCESS_REBOOT_INITIATED */
			s_ErrorMessage.Add(1642, "Patch target not found"); /* ERROR_PATCH_TARGET_NOT_FOUND */
			s_ErrorMessage.Add(1643, "Patch package rejected"); /* ERROR_PATCH_PACKAGE_REJECTED */
			s_ErrorMessage.Add(1644, "Install transform rejected"); /* ERROR_INSTALL_TRANSFORM_REJECTED */
			s_ErrorMessage.Add(1700, "RPC S Invalid string binding"); /* RPC_S_INVALID_STRING_BINDING */
			s_ErrorMessage.Add(1701, "RPC S Wrong kind of binding"); /* RPC_S_WRONG_KIND_OF_BINDING */
			s_ErrorMessage.Add(1702, "RPC S Invalid binding"); /* RPC_S_INVALID_BINDING */
			s_ErrorMessage.Add(1703, "RPC S Protseq not supported"); /* RPC_S_PROTSEQ_NOT_SUPPORTED */
		}

		public static void InitializeErrorMessages8()
		{
			s_ErrorMessage.Add(1704, "RPC S Invalid RPC protseq"); /* RPC_S_INVALID_RPC_PROTSEQ */
			s_ErrorMessage.Add(1705, "RPC S Invalid string UUID"); /* RPC_S_INVALID_STRING_UUID */
			s_ErrorMessage.Add(1706, "RPC S Invalid endpoint format"); /* RPC_S_INVALID_ENDPOINT_FORMAT */
			s_ErrorMessage.Add(1707, "RPC S Invalid net addr"); /* RPC_S_INVALID_NET_ADDR */
			s_ErrorMessage.Add(1708, "RPC S No endpoint found"); /* RPC_S_NO_ENDPOINT_FOUND */
			s_ErrorMessage.Add(1709, "RPC S Invalid timeout"); /* RPC_S_INVALID_TIMEOUT */
			s_ErrorMessage.Add(1710, "RPC S Object not found"); /* RPC_S_OBJECT_NOT_FOUND */
			s_ErrorMessage.Add(1711, "RPC S Already registered"); /* RPC_S_ALREADY_REGISTERED */
			s_ErrorMessage.Add(1712, "RPC S Type already registered"); /* RPC_S_TYPE_ALREADY_REGISTERED */
			s_ErrorMessage.Add(1713, "RPC S Already listening"); /* RPC_S_ALREADY_LISTENING */
			s_ErrorMessage.Add(1714, "RPC S Not protseqs registered"); /* RPC_S_NO_PROTSEQS_REGISTERED */
			s_ErrorMessage.Add(1715, "RPC S Not listening"); /* RPC_S_NOT_LISTENING */
			s_ErrorMessage.Add(1716, "RPC S Unknown mgr type"); /* RPC_S_UNKNOWN_MGR_TYPE */
			s_ErrorMessage.Add(1717, "RPC S Unknown IF"); /* RPC_S_UNKNOWN_IF */
			s_ErrorMessage.Add(1718, "RPC S No bindings"); /* RPC_S_NO_BINDINGS */
			s_ErrorMessage.Add(1719, "RPC S Not protseqs"); /* RPC_S_NO_PROTSEQS */
			s_ErrorMessage.Add(1720, "RPC S Can't create endpoint"); /* RPC_S_CANT_CREATE_ENDPOINT */
			s_ErrorMessage.Add(1721, "RPC S Out of resources"); /* RPC_S_OUT_OF_RESOURCES */
			s_ErrorMessage.Add(1722, "RPC S Server unavailable"); /* RPC_S_SERVER_UNAVAILABLE */
			s_ErrorMessage.Add(1723, "RPC S Server too busy"); /* RPC_S_SERVER_TOO_BUSY */
			s_ErrorMessage.Add(1724, "RPC S Invalid network options"); /* RPC_S_INVALID_NETWORK_OPTIONS */
			s_ErrorMessage.Add(1725, "RPC S No call active"); /* RPC_S_NO_CALL_ACTIVE */
			s_ErrorMessage.Add(1726, "RPC S Call failed"); /* RPC_S_CALL_FAILED */
			s_ErrorMessage.Add(1727, "RPC S Call failed DNE"); /* RPC_S_CALL_FAILED_DNE */
			s_ErrorMessage.Add(1728, "RPC S Protocol error"); /* RPC_S_PROTOCOL_ERROR */
			s_ErrorMessage.Add(1730, "RPC S Unsupported trans syn"); /* RPC_S_UNSUPPORTED_TRANS_SYN */
			s_ErrorMessage.Add(1732, "RPC S Unsupported type"); /* RPC_S_UNSUPPORTED_TYPE */
			s_ErrorMessage.Add(1733, "RPC S Invalid tag"); /* RPC_S_INVALID_TAG */
			s_ErrorMessage.Add(1734, "RPC S Invalid bound"); /* RPC_S_INVALID_BOUND */
			s_ErrorMessage.Add(1735, "RPC S No entry name"); /* RPC_S_NO_ENTRY_NAME */
			s_ErrorMessage.Add(1736, "RPC S Invalid name syntax"); /* RPC_S_INVALID_NAME_SYNTAX */
			s_ErrorMessage.Add(1737, "RPC S Unsupported name syntax"); /* RPC_S_UNSUPPORTED_NAME_SYNTAX */
			s_ErrorMessage.Add(1739, "RPC S UUID no address"); /* RPC_S_UUID_NO_ADDRESS */
			s_ErrorMessage.Add(1740, "RPC S Duplicate endpoint"); /* RPC_S_DUPLICATE_ENDPOINT */
			s_ErrorMessage.Add(1741, "RPC S Unknown authn type"); /* RPC_S_UNKNOWN_AUTHN_TYPE */
			s_ErrorMessage.Add(1742, "RPC S Max calls too small"); /* RPC_S_MAX_CALLS_TOO_SMALL */
			s_ErrorMessage.Add(1743, "RPC S String too long"); /* RPC_S_STRING_TOO_LONG */
			s_ErrorMessage.Add(1744, "RPC S Protseq not found"); /* RPC_S_PROTSEQ_NOT_FOUND */
			s_ErrorMessage.Add(1745, "RPC S Procnum out of range"); /* RPC_S_PROCNUM_OUT_OF_RANGE */
			s_ErrorMessage.Add(1746, "RPC S Binding has no auth"); /* RPC_S_BINDING_HAS_NO_AUTH */
			s_ErrorMessage.Add(1747, "RPC S Unknown authn service"); /* RPC_S_UNKNOWN_AUTHN_SERVICE */
			s_ErrorMessage.Add(1748, "RPC S Unknown authn level"); /* RPC_S_UNKNOWN_AUTHN_LEVEL */
			s_ErrorMessage.Add(1749, "RPC S Invalid auth identity"); /* RPC_S_INVALID_AUTH_IDENTITY */
			s_ErrorMessage.Add(1750, "RPC S Unknown authz service"); /* RPC_S_UNKNOWN_AUTHZ_SERVICE */
			s_ErrorMessage.Add(1751, "EPT S Invalid entry"); /* EPT_S_INVALID_ENTRY */
			s_ErrorMessage.Add(1752, "EPT S Can't perform op"); /* EPT_S_CANT_PERFORM_OP */
			s_ErrorMessage.Add(1753, "EPT S Not registered"); /* EPT_S_NOT_REGISTERED */
			s_ErrorMessage.Add(1754, "RPC S Nothing to export"); /* RPC_S_NOTHING_TO_EXPORT */
			s_ErrorMessage.Add(1755, "RPC S Incomplete name"); /* RPC_S_INCOMPLETE_NAME */
			s_ErrorMessage.Add(1756, "RPC S Invalid vers option"); /* RPC_S_INVALID_VERS_OPTION */
			s_ErrorMessage.Add(1757, "RPC S No more members"); /* RPC_S_NO_MORE_MEMBERS */
			s_ErrorMessage.Add(1758, "RPC S Not all objs unexported"); /* RPC_S_NOT_ALL_OBJS_UNEXPORTED */
			s_ErrorMessage.Add(1759, "RPC S Interface not found"); /* RPC_S_INTERFACE_NOT_FOUND */
			s_ErrorMessage.Add(1760, "RPC S Entry already exists"); /* RPC_S_ENTRY_ALREADY_EXISTS */
			s_ErrorMessage.Add(1761, "RPC S Entry not found"); /* RPC_S_ENTRY_NOT_FOUND */
			s_ErrorMessage.Add(1762, "RPC S Name service unavailable"); /* RPC_S_NAME_SERVICE_UNAVAILABLE */
			s_ErrorMessage.Add(1763, "RPC S Invalid naf ID"); /* RPC_S_INVALID_NAF_ID */
			s_ErrorMessage.Add(1764, "RPC S Cannot support"); /* RPC_S_CANNOT_SUPPORT */
			s_ErrorMessage.Add(1765, "RPC S No context available"); /* RPC_S_NO_CONTEXT_AVAILABLE */
			s_ErrorMessage.Add(1766, "RPC S Internal error"); /* RPC_S_INTERNAL_ERROR */
			s_ErrorMessage.Add(1767, "RPC S Zero divide"); /* RPC_S_ZERO_DIVIDE */
			s_ErrorMessage.Add(1768, "RPC S Address error"); /* RPC_S_ADDRESS_ERROR */
			s_ErrorMessage.Add(1769, "RPC S FP div zero"); /* RPC_S_FP_DIV_ZERO */
			s_ErrorMessage.Add(1770, "RPC S FP Underflow"); /* RPC_S_FP_UNDERFLOW */
			s_ErrorMessage.Add(1771, "RPC S Overflow"); /* RPC_S_FP_OVERFLOW */
			s_ErrorMessage.Add(1772, "RPC X No more entries"); /* RPC_X_NO_MORE_ENTRIES */
			s_ErrorMessage.Add(1773, "RPC X SS char trans open fail"); /* RPC_X_SS_CHAR_TRANS_OPEN_FAIL */
			s_ErrorMessage.Add(1774, "RPC X SS char trans short file"); /* RPC_X_SS_CHAR_TRANS_SHORT_FILE */
			s_ErrorMessage.Add(1775, "RPC S SS in null context"); /* RPC_X_SS_IN_NULL_CONTEXT */
			s_ErrorMessage.Add(1777, "RPC X SS context damaged"); /* RPC_X_SS_CONTEXT_DAMAGED */
			s_ErrorMessage.Add(1778, "RPC X SS handles mismatch"); /* RPC_X_SS_HANDLES_MISMATCH */
			s_ErrorMessage.Add(1779, "RPC X SS cannot get call handle"); /* RPC_X_SS_CANNOT_GET_CALL_HANDLE */
			s_ErrorMessage.Add(1780, "RPC X Null ref pointer"); /* RPC_X_NULL_REF_POINTER */
			s_ErrorMessage.Add(1781, "RPC X enum value out of range"); /* RPC_X_ENUM_VALUE_OUT_OF_RANGE */
			s_ErrorMessage.Add(1782, "RPC X byte count too small"); /* RPC_X_BYTE_COUNT_TOO_SMALL */
			s_ErrorMessage.Add(1783, "RPC X bad stub data"); /* RPC_X_BAD_STUB_DATA */
			s_ErrorMessage.Add(1784, "Invalid user buffer"); /* ERROR_INVALID_USER_BUFFER */
			s_ErrorMessage.Add(1785, "Unrecognised media"); /* ERROR_UNRECOGNIZED_MEDIA */
			s_ErrorMessage.Add(1786, "No trust lsa secret"); /* ERROR_NO_TRUST_LSA_SECRET */
			s_ErrorMessage.Add(1787, "No trust sam account"); /* ERROR_NO_TRUST_SAM_ACCOUNT */
			s_ErrorMessage.Add(1788, "Trusted domain failure"); /* ERROR_TRUSTED_DOMAIN_FAILURE */
			s_ErrorMessage.Add(1789, "Trusted relationship failure"); /* ERROR_TRUSTED_RELATIONSHIP_FAILURE */
			s_ErrorMessage.Add(1790, "Trust failure"); /* ERROR_TRUST_FAILURE */
			s_ErrorMessage.Add(1791, "RPC S call in progress"); /* RPC_S_CALL_IN_PROGRESS */
			s_ErrorMessage.Add(1792, "Error netlogon not started"); /* ERROR_NETLOGON_NOT_STARTED */
			s_ErrorMessage.Add(1793, "Account expired"); /* ERROR_ACCOUNT_EXPIRED */
			s_ErrorMessage.Add(1794, "Redirector has open handles"); /* ERROR_REDIRECTOR_HAS_OPEN_HANDLES */
			s_ErrorMessage.Add(1795, "Printer driver already installed"); /* ERROR_PRINTER_DRIVER_ALREADY_INSTALLED */
			s_ErrorMessage.Add(1796, "Unknown port"); /* ERROR_UNKNOWN_PORT */
			s_ErrorMessage.Add(1797, "Unknown printer driver"); /* ERROR_UNKNOWN_PRINTER_DRIVER */
			s_ErrorMessage.Add(1798, "Unknown printprocessor"); /* ERROR_UNKNOWN_PRINTPROCESSOR */
			s_ErrorMessage.Add(1799, "Invalid separator file"); /* ERROR_INVALID_SEPARATOR_FILE */
			s_ErrorMessage.Add(1800, "Invalid priority"); /* ERROR_INVALID_PRIORITY */
			s_ErrorMessage.Add(1801, "Invalid printer name"); /* ERROR_INVALID_PRINTER_NAME */
			s_ErrorMessage.Add(1802, "Printer already exists"); /* ERROR_PRINTER_ALREADY_EXISTS */
			s_ErrorMessage.Add(1803, "Invalid printer command"); /* ERROR_INVALID_PRINTER_COMMAND */
			s_ErrorMessage.Add(1804, "Invalid datatype"); /* ERROR_INVALID_DATATYPE */
			s_ErrorMessage.Add(1805, "Invalid environment"); /* ERROR_INVALID_ENVIRONMENT */
			s_ErrorMessage.Add(1806, "RPC S no more bindings"); /* RPC_S_NO_MORE_BINDINGS */
			s_ErrorMessage.Add(1807, "Nologon interdomain trust account"); /* ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT */
			s_ErrorMessage.Add(1808, "Nologon workstation trust account"); /* ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT */
			s_ErrorMessage.Add(1809, "Nologon server trust account"); /* ERROR_NOLOGON_SERVER_TRUST_ACCOUNT */
			s_ErrorMessage.Add(1810, "Domain trust inconsistent"); /* ERROR_DOMAIN_TRUST_INCONSISTENT */
			s_ErrorMessage.Add(1811, "Server has open handles"); /* ERROR_SERVER_HAS_OPEN_HANDLES */
			s_ErrorMessage.Add(1812, "Resource data not found"); /* ERROR_RESOURCE_DATA_NOT_FOUND */
			s_ErrorMessage.Add(1813, "Resource type not found"); /* ERROR_RESOURCE_TYPE_NOT_FOUND */
			s_ErrorMessage.Add(1814, "Resource name not found"); /* ERROR_RESOURCE_NAME_NOT_FOUND */
			s_ErrorMessage.Add(1815, "Resource lang not found"); /* ERROR_RESOURCE_LANG_NOT_FOUND */
			s_ErrorMessage.Add(1816, "Not enough quota"); /* ERROR_NOT_ENOUGH_QUOTA */
			s_ErrorMessage.Add(1817, "RPC S no interfaces"); /* RPC_S_NO_INTERFACES */
			s_ErrorMessage.Add(1818, "RPC S Call cancelled"); /* RPC_S_CALL_CANCELLED */
			s_ErrorMessage.Add(1819, "RPC S Binding incomplete"); /* RPC_S_BINDING_INCOMPLETE */
			s_ErrorMessage.Add(1820, "RPC S Comm failure"); /* RPC_S_COMM_FAILURE */
			s_ErrorMessage.Add(1821, "RPC S Unsupported authn level"); /* RPC_S_UNSUPPORTED_AUTHN_LEVEL */
			s_ErrorMessage.Add(1822, "RPC S No princ name"); /* RPC_S_NO_PRINC_NAME */
			s_ErrorMessage.Add(1823, "RPC S Not RPC error"); /* RPC_S_NOT_RPC_ERROR */
			s_ErrorMessage.Add(1824, "RPC U UUID local only"); /* RPC_S_UUID_LOCAL_ONLY */
			s_ErrorMessage.Add(1825, "RPC S Sec pkg error"); /* RPC_S_SEC_PKG_ERROR */
			s_ErrorMessage.Add(1826, "RPC S Not cancelled"); /* RPC_S_NOT_CANCELLED */
			s_ErrorMessage.Add(1827, "RPC X Invalid ES action"); /* RPC_X_INVALID_ES_ACTION */
			s_ErrorMessage.Add(1828, "RPC X Wrong ES version"); /* RPC_X_WRONG_ES_VERSION */
			s_ErrorMessage.Add(1829, "RPC X Wrong stub version"); /* RPC_X_WRONG_STUB_VERSION */
			s_ErrorMessage.Add(1830, "RPC X Invalid pipe object"); /* RPC_X_INVALID_PIPE_OBJECT */
			s_ErrorMessage.Add(1831, "RPC X Wrong pipe order"); /* RPC_X_WRONG_PIPE_ORDER */
			s_ErrorMessage.Add(1832, "RPC X Wrong pipe version"); /* RPC_X_WRONG_PIPE_VERSION */
			s_ErrorMessage.Add(1898, "RPC S group member not found"); /* RPC_S_GROUP_MEMBER_NOT_FOUND */
			s_ErrorMessage.Add(1899, "EPT S Can't create"); /* EPT_S_CANT_CREATE */
			s_ErrorMessage.Add(1900, "RPC S Invalid object"); /* RPC_S_INVALID_OBJECT */
			s_ErrorMessage.Add(1901, "Invalid time"); /* ERROR_INVALID_TIME */
			s_ErrorMessage.Add(1902, "Invalid form name"); /* ERROR_INVALID_FORM_NAME */
			s_ErrorMessage.Add(1903, "Invalid form size"); /* ERROR_INVALID_FORM_SIZE */
			s_ErrorMessage.Add(1904, "Already waiting"); /* ERROR_ALREADY_WAITING */
			s_ErrorMessage.Add(1905, "Printer deleted"); /* ERROR_PRINTER_DELETED */
			s_ErrorMessage.Add(1906, "Invalid printer state"); /* ERROR_INVALID_PRINTER_STATE */
		}

		public static void InitializeErrorMessages9()
		{
			s_ErrorMessage.Add(1907, "Password must change"); /* ERROR_PASSWORD_MUST_CHANGE */
			s_ErrorMessage.Add(1908, "Domain controller not found"); /* ERROR_DOMAIN_CONTROLLER_NOT_FOUND */
			s_ErrorMessage.Add(1909, "Account locked out"); /* ERROR_ACCOUNT_LOCKED_OUT */
			s_ErrorMessage.Add(1910, "OR Invalid OXID"); /* OR_INVALID_OXID */
			s_ErrorMessage.Add(1911, "OR Invalid OID"); /* OR_INVALID_OID */
			s_ErrorMessage.Add(1912, "OR Invalid set"); /* OR_INVALID_SET */
			s_ErrorMessage.Add(1913, "RPC S Send incomplete"); /* RPC_S_SEND_INCOMPLETE */
			s_ErrorMessage.Add(1914, "RPC S Invalid async handle"); /* RPC_S_INVALID_ASYNC_HANDLE */
			s_ErrorMessage.Add(1915, "RPC S Invalid async call"); /* RPC_S_INVALID_ASYNC_CALL */
			s_ErrorMessage.Add(1916, "RPC X Pipe closed"); /* RPC_X_PIPE_CLOSED */
			s_ErrorMessage.Add(1917, "RPC X Pipe discipline error"); /* RPC_X_PIPE_DISCIPLINE_ERROR */
			s_ErrorMessage.Add(1918, "RPC X Pipe empty"); /* RPC_X_PIPE_EMPTY */
			s_ErrorMessage.Add(1919, "No sitename"); /* ERROR_NO_SITENAME */
			s_ErrorMessage.Add(1920, "Can't access file"); /* ERROR_CANT_ACCESS_FILE */
			s_ErrorMessage.Add(1921, "Can't resolve filename"); /* ERROR_CANT_RESOLVE_FILENAME */
			s_ErrorMessage.Add(1922, "RPC S Entry type mismatch"); /* RPC_S_ENTRY_TYPE_MISMATCH */
			s_ErrorMessage.Add(1923, "RPC S Not all objs exported"); /* RPC_S_NOT_ALL_OBJS_EXPORTED */
			s_ErrorMessage.Add(1924, "RPC S Interface not exported"); /* RPC_S_INTERFACE_NOT_EXPORTED */
			s_ErrorMessage.Add(1925, "RPC S Profile not added"); /* RPC_S_PROFILE_NOT_ADDED */
			s_ErrorMessage.Add(1926, "RPC S PRF ELT not added"); /* RPC_S_PRF_ELT_NOT_ADDED */
			s_ErrorMessage.Add(1927, "RPC S PRF ELT not removed"); /* RPC_S_PRF_ELT_NOT_REMOVED */
			s_ErrorMessage.Add(1928, "RPC S GRP ELT not added"); /* RPC_S_GRP_ELT_NOT_ADDED */
			s_ErrorMessage.Add(1929, "RPC S GRP ELT not removed"); /* RPC_S_GRP_ELT_NOT_REMOVED */
			s_ErrorMessage.Add(1930, "KM driver blocked"); /* ERROR_KM_DRIVER_BLOCKED */
			s_ErrorMessage.Add(1931, "Context expired"); /* ERROR_CONTEXT_EXPIRED */
			s_ErrorMessage.Add(2000, "Invalid pixel format"); /* ERROR_INVALID_PIXEL_FORMAT */
			s_ErrorMessage.Add(2001, "Bad driver"); /* ERROR_BAD_DRIVER */
			s_ErrorMessage.Add(2002, "Invalid window style"); /* ERROR_INVALID_WINDOW_STYLE */
			s_ErrorMessage.Add(2003, "Metafile not supported"); /* ERROR_METAFILE_NOT_SUPPORTED */
			s_ErrorMessage.Add(2004, "Transform not supported"); /* ERROR_TRANSFORM_NOT_SUPPORTED */
			s_ErrorMessage.Add(2005, "Clipping not supported"); /* ERROR_CLIPPING_NOT_SUPPORTED */
			s_ErrorMessage.Add(2010, "Invalid CMM"); /* ERROR_INVALID_CMM */
			s_ErrorMessage.Add(2011, "Invalid profile"); /* ERROR_INVALID_PROFILE */
			s_ErrorMessage.Add(2012, "Tag not found"); /* ERROR_TAG_NOT_FOUND */
			s_ErrorMessage.Add(2013, "Tag not present"); /* ERROR_TAG_NOT_PRESENT */
			s_ErrorMessage.Add(2014, "Duplicate tag"); /* ERROR_DUPLICATE_TAG */
			s_ErrorMessage.Add(2015, "Profile not associated with device"); /* ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE */
			s_ErrorMessage.Add(2016, "Profile not found"); /* ERROR_PROFILE_NOT_FOUND */
			s_ErrorMessage.Add(2017, "Invalid colorspace"); /* ERROR_INVALID_COLORSPACE */
			s_ErrorMessage.Add(2018, "ICM not enabled"); /* ERROR_ICM_NOT_ENABLED */
			s_ErrorMessage.Add(2019, "Deleting ICM xform"); /* ERROR_DELETING_ICM_XFORM */
			s_ErrorMessage.Add(2020, "Invalid transform"); /* ERROR_INVALID_TRANSFORM */
			s_ErrorMessage.Add(2021, "Colorspace mismatch"); /* ERROR_COLORSPACE_MISMATCH */
			s_ErrorMessage.Add(2022, "Invalid colorindex"); /* ERROR_INVALID_COLORINDEX */
			s_ErrorMessage.Add(2108, "Connected other password"); /* ERROR_CONNECTED_OTHER_PASSWORD */
			s_ErrorMessage.Add(2109, "Connected other password default"); /* ERROR_CONNECTED_OTHER_PASSWORD_DEFAULT */
			s_ErrorMessage.Add(2202, "Bad username"); /* ERROR_BAD_USERNAME */
			s_ErrorMessage.Add(2250, "Not connected"); /* ERROR_NOT_CONNECTED */
			s_ErrorMessage.Add(2401, "Open files"); /* ERROR_OPEN_FILES */
			s_ErrorMessage.Add(2402, "Active connections"); /* ERROR_ACTIVE_CONNECTIONS */
			s_ErrorMessage.Add(2404, "Device in use"); /* ERROR_DEVICE_IN_USE */
			s_ErrorMessage.Add(3000, "Unknown print monitor"); /* ERROR_UNKNOWN_PRINT_MONITOR */
			s_ErrorMessage.Add(3001, "Printer driver in use"); /* ERROR_PRINTER_DRIVER_IN_USE */
			s_ErrorMessage.Add(3002, "Spool file not found"); /* ERROR_SPOOL_FILE_NOT_FOUND */
			s_ErrorMessage.Add(3003, "SPL no startdoc"); /* ERROR_SPL_NO_STARTDOC */
			s_ErrorMessage.Add(3004, "SPL no addjob"); /* ERROR_SPL_NO_ADDJOB */
			s_ErrorMessage.Add(3005, "Print processor already installed"); /* ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED */
			s_ErrorMessage.Add(3006, "Print monitor already installed"); /* ERROR_PRINT_MONITOR_ALREADY_INSTALLED */
			s_ErrorMessage.Add(3007, "Invalid print monitor"); /* ERROR_INVALID_PRINT_MONITOR */
			s_ErrorMessage.Add(3008, "Print monitor in use"); /* ERROR_PRINT_MONITOR_IN_USE */
			s_ErrorMessage.Add(3009, "Printer has jobs queued"); /* ERROR_PRINTER_HAS_JOBS_QUEUED */
			s_ErrorMessage.Add(3010, "Success reboot required"); /* ERROR_SUCCESS_REBOOT_REQUIRED */
			s_ErrorMessage.Add(3011, "Success restart required"); /* ERROR_SUCCESS_RESTART_REQUIRED */
			s_ErrorMessage.Add(3012, "Printer not found"); /* ERROR_PRINTER_NOT_FOUND */
			s_ErrorMessage.Add(3013, "Printer driver warned"); /* ERROR_PRINTER_DRIVER_WARNED */
			s_ErrorMessage.Add(3014, "Printer driver blocked"); /* ERROR_PRINTER_DRIVER_BLOCKED */
			s_ErrorMessage.Add(4000, "Wins internal"); /* ERROR_WINS_INTERNAL */
			s_ErrorMessage.Add(4001, "Can not del local wins"); /* ERROR_CAN_NOT_DEL_LOCAL_WINS */
			s_ErrorMessage.Add(4002, "Static init"); /* ERROR_STATIC_INIT */
			s_ErrorMessage.Add(4003, "Inc backup"); /* ERROR_INC_BACKUP */
			s_ErrorMessage.Add(4004, "Full backup"); /* ERROR_FULL_BACKUP */
			s_ErrorMessage.Add(4005, "Rec not existent"); /* ERROR_REC_NON_EXISTENT */
			s_ErrorMessage.Add(4006, "RPL not allowed"); /* ERROR_RPL_NOT_ALLOWED */
			s_ErrorMessage.Add(4100, "DHCP address conflict"); /* ERROR_DHCP_ADDRESS_CONFLICT */
			s_ErrorMessage.Add(4200, "WMU GUID not found"); /* ERROR_WMI_GUID_NOT_FOUND */
			s_ErrorMessage.Add(4201, "WMI instance not found"); /* ERROR_WMI_INSTANCE_NOT_FOUND */
			s_ErrorMessage.Add(4202, "WMI ItemID not found"); /* ERROR_WMI_ITEMID_NOT_FOUND */
			s_ErrorMessage.Add(4203, "WMI try again"); /* ERROR_WMI_TRY_AGAIN */
			s_ErrorMessage.Add(4204, "WMI DP not found"); /* ERROR_WMI_DP_NOT_FOUND */
			s_ErrorMessage.Add(4205, "WMI unresolved instance ref"); /* ERROR_WMI_UNRESOLVED_INSTANCE_REF */
			s_ErrorMessage.Add(4206, "WMU already enabled"); /* ERROR_WMI_ALREADY_ENABLED */
			s_ErrorMessage.Add(4207, "WMU GUID disconnected"); /* ERROR_WMI_GUID_DISCONNECTED */
			s_ErrorMessage.Add(4208, "WMI server unavailable"); /* ERROR_WMI_SERVER_UNAVAILABLE */
			s_ErrorMessage.Add(4209, "WMI DP failed"); /* ERROR_WMI_DP_FAILED */
			s_ErrorMessage.Add(4210, "WMI invalid MOF"); /* ERROR_WMI_INVALID_MOF */
			s_ErrorMessage.Add(4211, "WMI invalid reginfo"); /* ERROR_WMI_INVALID_REGINFO */
			s_ErrorMessage.Add(4212, "WMI already disabled"); /* ERROR_WMI_ALREADY_DISABLED */
			s_ErrorMessage.Add(4213, "WMI read only"); /* ERROR_WMI_READ_ONLY */
			s_ErrorMessage.Add(4214, "WMI set failure"); /* ERROR_WMI_SET_FAILURE */
			s_ErrorMessage.Add(4300, "Invalid media"); /* ERROR_INVALID_MEDIA */
			s_ErrorMessage.Add(4301, "Invalid library"); /* ERROR_INVALID_LIBRARY */
			s_ErrorMessage.Add(4302, "Invalid media pool"); /* ERROR_INVALID_MEDIA_POOL */
			s_ErrorMessage.Add(4303, "Drive media mismatch"); /* ERROR_DRIVE_MEDIA_MISMATCH */
			s_ErrorMessage.Add(4304, "Media offline"); /* ERROR_MEDIA_OFFLINE */
			s_ErrorMessage.Add(4305, "Library offline"); /* ERROR_LIBRARY_OFFLINE */
			s_ErrorMessage.Add(4306, "Empty"); /* ERROR_EMPTY */
			s_ErrorMessage.Add(4307, "Not empty"); /* ERROR_NOT_EMPTY */
			s_ErrorMessage.Add(4308, "Media unavailable"); /* ERROR_MEDIA_UNAVAILABLE */
			s_ErrorMessage.Add(4309, "Resource disabled"); /* ERROR_RESOURCE_DISABLED */
			s_ErrorMessage.Add(4310, "Invalid cleaner"); /* ERROR_INVALID_CLEANER */
			s_ErrorMessage.Add(4311, "Unable to clean"); /* ERROR_UNABLE_TO_CLEAN */
			s_ErrorMessage.Add(4312, "Object not found"); /* ERROR_OBJECT_NOT_FOUND */
			s_ErrorMessage.Add(4313, "Database failure"); /* ERROR_DATABASE_FAILURE */
			s_ErrorMessage.Add(4314, "Database full"); /* ERROR_DATABASE_FULL */
			s_ErrorMessage.Add(4315, "Media incompatible"); /* ERROR_MEDIA_INCOMPATIBLE */
			s_ErrorMessage.Add(4316, "Resource not present"); /* ERROR_RESOURCE_NOT_PRESENT */
			s_ErrorMessage.Add(4317, "Invalid operation"); /* ERROR_INVALID_OPERATION */
			s_ErrorMessage.Add(4318, "Media not available"); /* ERROR_MEDIA_NOT_AVAILABLE */
			s_ErrorMessage.Add(4319, "Device not available"); /* ERROR_DEVICE_NOT_AVAILABLE */
			s_ErrorMessage.Add(4320, "Request refused"); /* ERROR_REQUEST_REFUSED */
			s_ErrorMessage.Add(4321, "Invalid drive object"); /* ERROR_INVALID_DRIVE_OBJECT */
			s_ErrorMessage.Add(4322, "Library full"); /* ERROR_LIBRARY_FULL */
			s_ErrorMessage.Add(4323, "Medium not accessible"); /* ERROR_MEDIUM_NOT_ACCESSIBLE */
			s_ErrorMessage.Add(4324, "Unable to load medium"); /* ERROR_UNABLE_TO_LOAD_MEDIUM */
			s_ErrorMessage.Add(4325, "Unable to inventory drive"); /* ERROR_UNABLE_TO_INVENTORY_DRIVE */
			s_ErrorMessage.Add(4326, "Unable to inventory slot"); /* ERROR_UNABLE_TO_INVENTORY_SLOT */
			s_ErrorMessage.Add(4327, "Unable to inventory transport"); /* ERROR_UNABLE_TO_INVENTORY_TRANSPORT */
			s_ErrorMessage.Add(4328, "Transport full"); /* ERROR_TRANSPORT_FULL */
			s_ErrorMessage.Add(4329, "Controlling ieport"); /* ERROR_CONTROLLING_IEPORT */
			s_ErrorMessage.Add(4330, "Unable to eject mounted media"); /* ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA */
			s_ErrorMessage.Add(4331, "Cleaner slot set"); /* ERROR_CLEANER_SLOT_SET */
			s_ErrorMessage.Add(4332, "Cleaner slot not set"); /* ERROR_CLEANER_SLOT_NOT_SET */
			s_ErrorMessage.Add(4333, "Cleaner cartridge spent"); /* ERROR_CLEANER_CARTRIDGE_SPENT */
			s_ErrorMessage.Add(4334, "Unexpected omid"); /* ERROR_UNEXPECTED_OMID */
			s_ErrorMessage.Add(4335, "Can't delete last item"); /* ERROR_CANT_DELETE_LAST_ITEM */
			s_ErrorMessage.Add(4336, "Message exceeds max size"); /* ERROR_MESSAGE_EXCEEDS_MAX_SIZE */
			s_ErrorMessage.Add(4337, "Volume contains sys files"); /* ERROR_VOLUME_CONTAINS_SYS_FILES */
			s_ErrorMessage.Add(4338, "Indigenous type"); /* ERROR_INDIGENOUS_TYPE */
			s_ErrorMessage.Add(4339, "No supporting drives"); /* ERROR_NO_SUPPORTING_DRIVES */
			s_ErrorMessage.Add(4340, "Cleaner cartridge installed"); /* ERROR_CLEANER_CARTRIDGE_INSTALLED */
			s_ErrorMessage.Add(4350, "Fill offline"); /* ERROR_FILE_OFFLINE */
			s_ErrorMessage.Add(4351, "Remote storage not active"); /* ERROR_REMOTE_STORAGE_NOT_ACTIVE */
		}

		public static void InitializeErrorMessages10()
		{
			s_ErrorMessage.Add(4352, "Remote storage media error"); /* ERROR_REMOTE_STORAGE_MEDIA_ERROR */
			s_ErrorMessage.Add(4390, "Not a reparse point"); /* ERROR_NOT_A_REPARSE_POINT */
			s_ErrorMessage.Add(4391, "Reparse attribute conflict"); /* ERROR_REPARSE_ATTRIBUTE_CONFLICT */
			s_ErrorMessage.Add(4392, "Invalid reparse data"); /* ERROR_INVALID_REPARSE_DATA */
			s_ErrorMessage.Add(4393, "Reparse tag invalid"); /* ERROR_REPARSE_TAG_INVALID */
			s_ErrorMessage.Add(4394, "Reparse tag mismatch"); /* ERROR_REPARSE_TAG_MISMATCH */
			s_ErrorMessage.Add(4500, "Volume not sis enabled"); /* ERROR_VOLUME_NOT_SIS_ENABLED */
			s_ErrorMessage.Add(5001, "Dependent resource exists"); /* ERROR_DEPENDENT_RESOURCE_EXISTS */
			s_ErrorMessage.Add(5002, "Dependency not found"); /* ERROR_DEPENDENCY_NOT_FOUND */
			s_ErrorMessage.Add(5003, "Dependency already exists"); /* ERROR_DEPENDENCY_ALREADY_EXISTS */
			s_ErrorMessage.Add(5004, "Resource not online"); /* ERROR_RESOURCE_NOT_ONLINE */
			s_ErrorMessage.Add(5005, "Host node not available"); /* ERROR_HOST_NODE_NOT_AVAILABLE */
			s_ErrorMessage.Add(5006, "Resource not available"); /* ERROR_RESOURCE_NOT_AVAILABLE */
			s_ErrorMessage.Add(5007, "Resource not found"); /* ERROR_RESOURCE_NOT_FOUND */
			s_ErrorMessage.Add(5008, "Shutdown cluster"); /* ERROR_SHUTDOWN_CLUSTER */
			s_ErrorMessage.Add(5009, "Can't evict active node"); /* ERROR_CANT_EVICT_ACTIVE_NODE */
			s_ErrorMessage.Add(5010, "Object already exists"); /* ERROR_OBJECT_ALREADY_EXISTS */
			s_ErrorMessage.Add(5011, "Object in list"); /* ERROR_OBJECT_IN_LIST */
			s_ErrorMessage.Add(5012, "Group not available"); /* ERROR_GROUP_NOT_AVAILABLE */
			s_ErrorMessage.Add(5013, "Group not found"); /* ERROR_GROUP_NOT_FOUND */
			s_ErrorMessage.Add(5014, "Group not online"); /* ERROR_GROUP_NOT_ONLINE */
			s_ErrorMessage.Add(5015, "Host node not resource owner"); /* ERROR_HOST_NODE_NOT_RESOURCE_OWNER */
			s_ErrorMessage.Add(5016, "Host node not group owner"); /* ERROR_HOST_NODE_NOT_GROUP_OWNER */
			s_ErrorMessage.Add(5017, "Resmon create failed"); /* ERROR_RESMON_CREATE_FAILED */
			s_ErrorMessage.Add(5018, "Resmon online failed"); /* ERROR_RESMON_ONLINE_FAILED */
			s_ErrorMessage.Add(5019, "Resource online"); /* ERROR_RESOURCE_ONLINE */
			s_ErrorMessage.Add(5020, "Quorum resource"); /* ERROR_QUORUM_RESOURCE */
			s_ErrorMessage.Add(5021, "Not quorum capable"); /* ERROR_NOT_QUORUM_CAPABLE */
			s_ErrorMessage.Add(5022, "Cluster shutting down"); /* ERROR_CLUSTER_SHUTTING_DOWN */
			s_ErrorMessage.Add(5023, "Invalid state"); /* ERROR_INVALID_STATE */
			s_ErrorMessage.Add(5024, "Resource properties stored"); /* ERROR_RESOURCE_PROPERTIES_STORED */
			s_ErrorMessage.Add(5025, "Not quorum class"); /* ERROR_NOT_QUORUM_CLASS */
			s_ErrorMessage.Add(5026, "Core resource"); /* ERROR_CORE_RESOURCE */
			s_ErrorMessage.Add(5027, "Quorum resource online failed"); /* ERROR_QUORUM_RESOURCE_ONLINE_FAILED */
			s_ErrorMessage.Add(5028, "Quorumlog open failed"); /* ERROR_QUORUMLOG_OPEN_FAILED */
			s_ErrorMessage.Add(5029, "Clusterlog corrupt"); /* ERROR_CLUSTERLOG_CORRUPT */
			s_ErrorMessage.Add(5030, "Clusterlog record exceeds maxsize"); /* ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE */
			s_ErrorMessage.Add(5031, "Clusterlog exceeds maxsize"); /* ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE */
			s_ErrorMessage.Add(5032, "Clusterlog chkpoint not found"); /* ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND */
			s_ErrorMessage.Add(5033, "Clusterlog not enough space"); /* ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE */
			s_ErrorMessage.Add(5034, "Quorum owner alive"); /* ERROR_QUORUM_OWNER_ALIVE */
			s_ErrorMessage.Add(5035, "Network not available"); /* ERROR_NETWORK_NOT_AVAILABLE */
			s_ErrorMessage.Add(5036, "Node not available"); /* ERROR_NODE_NOT_AVAILABLE */
			s_ErrorMessage.Add(5037, "All nodes not available"); /* ERROR_ALL_NODES_NOT_AVAILABLE */
			s_ErrorMessage.Add(5038, "Resource failed"); /* ERROR_RESOURCE_FAILED */
			s_ErrorMessage.Add(5039, "Cluster invalid node"); /* ERROR_CLUSTER_INVALID_NODE */
			s_ErrorMessage.Add(5040, "Cluster node exists"); /* ERROR_CLUSTER_NODE_EXISTS */
			s_ErrorMessage.Add(5041, "Cluster join in progress"); /* ERROR_CLUSTER_JOIN_IN_PROGRESS */
			s_ErrorMessage.Add(5042, "Cluster node not found"); /* ERROR_CLUSTER_NODE_NOT_FOUND */
			s_ErrorMessage.Add(5043, "Cluster local node not found"); /* ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND */
			s_ErrorMessage.Add(5044, "Cluster network exists"); /* ERROR_CLUSTER_NETWORK_EXISTS */
			s_ErrorMessage.Add(5045, "Cluster network not found"); /* ERROR_CLUSTER_NETWORK_NOT_FOUND */
			s_ErrorMessage.Add(5046, "Cluster netinterface exists"); /* ERROR_CLUSTER_NETINTERFACE_EXISTS */
			s_ErrorMessage.Add(5047, "Cluster netinterface not found"); /* ERROR_CLUSTER_NETINTERFACE_NOT_FOUND */
			s_ErrorMessage.Add(5048, "Cluster invalid request"); /* ERROR_CLUSTER_INVALID_REQUEST */
			s_ErrorMessage.Add(5049, "Cluster invalid network provider"); /* ERROR_CLUSTER_INVALID_NETWORK_PROVIDER */
			s_ErrorMessage.Add(5050, "Cluster node down"); /* ERROR_CLUSTER_NODE_DOWN */
			s_ErrorMessage.Add(5051, "Cluster node unreachable"); /* ERROR_CLUSTER_NODE_UNREACHABLE */
			s_ErrorMessage.Add(5052, "Cluster node not member"); /* ERROR_CLUSTER_NODE_NOT_MEMBER */
			s_ErrorMessage.Add(5053, "Cluster join not in progress"); /* ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS */
			s_ErrorMessage.Add(5054, "Cluster invalid network"); /* ERROR_CLUSTER_INVALID_NETWORK */
			s_ErrorMessage.Add(5056, "Cluster node up"); /* ERROR_CLUSTER_NODE_UP */
			s_ErrorMessage.Add(5057, "Cluster ipaddr in use"); /* ERROR_CLUSTER_IPADDR_IN_USE */
			s_ErrorMessage.Add(5058, "Cluster node not paused"); /* ERROR_CLUSTER_NODE_NOT_PAUSED */
			s_ErrorMessage.Add(5059, "Cluster no security context"); /* ERROR_CLUSTER_NO_SECURITY_CONTEXT */
			s_ErrorMessage.Add(5060, "Cluster network not internal"); /* ERROR_CLUSTER_NETWORK_NOT_INTERNAL */
			s_ErrorMessage.Add(5061, "Cluster node already up"); /* ERROR_CLUSTER_NODE_ALREADY_UP */
			s_ErrorMessage.Add(5062, "Cluster node already down"); /* ERROR_CLUSTER_NODE_ALREADY_DOWN */
			s_ErrorMessage.Add(5063, "Cluster network already online"); /* ERROR_CLUSTER_NETWORK_ALREADY_ONLINE */
			s_ErrorMessage.Add(5064, "Cluster network already offline"); /* ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE */
			s_ErrorMessage.Add(5065, "Cluster node already member"); /* ERROR_CLUSTER_NODE_ALREADY_MEMBER */
			s_ErrorMessage.Add(5066, "Cluster last internal network"); /* ERROR_CLUSTER_LAST_INTERNAL_NETWORK */
			s_ErrorMessage.Add(5067, "Cluster network has dependents"); /* ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS */
			s_ErrorMessage.Add(5068, "Invalid operation on quorum"); /* ERROR_INVALID_OPERATION_ON_QUORUM */
			s_ErrorMessage.Add(5069, "Dependency not allowed"); /* ERROR_DEPENDENCY_NOT_ALLOWED */
			s_ErrorMessage.Add(5070, "Cluster node paused"); /* ERROR_CLUSTER_NODE_PAUSED */
			s_ErrorMessage.Add(5071, "Node can't host resource"); /* ERROR_NODE_CANT_HOST_RESOURCE */
			s_ErrorMessage.Add(5072, "Cluster node not ready"); /* ERROR_CLUSTER_NODE_NOT_READY */
			s_ErrorMessage.Add(5073, "Cluster node shutting down"); /* ERROR_CLUSTER_NODE_SHUTTING_DOWN */
			s_ErrorMessage.Add(5074, "Cluster join aborted"); /* ERROR_CLUSTER_JOIN_ABORTED */
			s_ErrorMessage.Add(5075, "Cluster incompatible versions"); /* ERROR_CLUSTER_INCOMPATIBLE_VERSIONS */
			s_ErrorMessage.Add(5076, "Cluster maxnum of resources exceeded"); /* ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED */
			s_ErrorMessage.Add(5077, "Cluster system config changed"); /* ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED */
			s_ErrorMessage.Add(5078, "Cluster resource type not found"); /* ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND */
			s_ErrorMessage.Add(5079, "Cluster restype not supported"); /* ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED */
			s_ErrorMessage.Add(5080, "Cluster resname not found"); /* ERROR_CLUSTER_RESNAME_NOT_FOUND */
			s_ErrorMessage.Add(5081, "Cluster no RPC packages registered"); /* ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED */
			s_ErrorMessage.Add(5082, "Cluster owner not in preflist"); /* ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST */
			s_ErrorMessage.Add(5083, "Cluster database seqmismatch"); /* ERROR_CLUSTER_DATABASE_SEQMISMATCH */
			s_ErrorMessage.Add(5084, "Resmon invalid state"); /* ERROR_RESMON_INVALID_STATE */
			s_ErrorMessage.Add(5085, "Cluster gum not locker"); /* ERROR_CLUSTER_GUM_NOT_LOCKER */
			s_ErrorMessage.Add(5086, "Quorum disk not found"); /* ERROR_QUORUM_DISK_NOT_FOUND */
			s_ErrorMessage.Add(5087, "Database backup corrupt"); /* ERROR_DATABASE_BACKUP_CORRUPT */
			s_ErrorMessage.Add(5088, "Cluster node already has DFS root"); /* ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT */
			s_ErrorMessage.Add(5089, "Resource property unchangeable"); /* ERROR_RESOURCE_PROPERTY_UNCHANGEABLE */
			s_ErrorMessage.Add(5890, "Cluster membership invalid state"); /* ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE */
			s_ErrorMessage.Add(5891, "Cluster quorumlog not found"); /* ERROR_CLUSTER_QUORUMLOG_NOT_FOUND */
			s_ErrorMessage.Add(5892, "Cluster membership halt"); /* ERROR_CLUSTER_MEMBERSHIP_HALT */
			s_ErrorMessage.Add(5893, "Cluster instance ID mismatch"); /* ERROR_CLUSTER_INSTANCE_ID_MISMATCH */
			s_ErrorMessage.Add(5894, "Cluster network not found for IP"); /* ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP */
			s_ErrorMessage.Add(5895, "Cluster property data type mismatch"); /* ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH */
			s_ErrorMessage.Add(5896, "Cluster evict without cleanup"); /* ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP */
			s_ErrorMessage.Add(5897, "Cluster parameter mismatch"); /* ERROR_CLUSTER_PARAMETER_MISMATCH */
			s_ErrorMessage.Add(5898, "Node cannot be clustered"); /* ERROR_NODE_CANNOT_BE_CLUSTERED */
			s_ErrorMessage.Add(5899, "Cluster wrong OS version"); /* ERROR_CLUSTER_WRONG_OS_VERSION */
			s_ErrorMessage.Add(5900, "Cluster can't create dup cluster name"); /* ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME */
			s_ErrorMessage.Add(6001, "Decryption failed"); /* ERROR_DECRYPTION_FAILED */
			s_ErrorMessage.Add(6002, "File encrypted"); /* ERROR_FILE_ENCRYPTED */
			s_ErrorMessage.Add(6003, "No recovery policy"); /* ERROR_NO_RECOVERY_POLICY */
			s_ErrorMessage.Add(6004, "No EFS"); /* ERROR_NO_EFS */
			s_ErrorMessage.Add(6005, "Wrong EFS"); /* ERROR_WRONG_EFS */
			s_ErrorMessage.Add(6006, "No user keys"); /* ERROR_NO_USER_KEYS */
			s_ErrorMessage.Add(6007, "File not encryped"); /* ERROR_FILE_NOT_ENCRYPTED */
			s_ErrorMessage.Add(6008, "Not export format"); /* ERROR_NOT_EXPORT_FORMAT */
			s_ErrorMessage.Add(6009, "File read only"); /* ERROR_FILE_READ_ONLY */
			s_ErrorMessage.Add(6010, "Dir EFS disallowed"); /* ERROR_DIR_EFS_DISALLOWED */
			s_ErrorMessage.Add(6011, "EFS server not trusted"); /* ERROR_EFS_SERVER_NOT_TRUSTED */
			s_ErrorMessage.Add(6012, "Bad recovery policy"); /* ERROR_BAD_RECOVERY_POLICY */
			s_ErrorMessage.Add(6013, "ETS alg blob too big"); /* ERROR_EFS_ALG_BLOB_TOO_BIG */
			s_ErrorMessage.Add(6014, "Volume not support EFS"); /* ERROR_VOLUME_NOT_SUPPORT_EFS */
			s_ErrorMessage.Add(6015, "EFS disabled"); /* ERROR_EFS_DISABLED */
			s_ErrorMessage.Add(6016, "EFS version not support"); /* ERROR_EFS_VERSION_NOT_SUPPORT */
		}

		public static void InitializeErrorMessages11()
		{
			s_ErrorMessage.Add(6118, "No browser servers found"); /* ERROR_NO_BROWSER_SERVERS_FOUND */
			s_ErrorMessage.Add(6200, "Sched E service not localsystem"); /* SCHED_E_SERVICE_NOT_LOCALSYSTEM */
			s_ErrorMessage.Add(7001, "Ctx winstation name invalid"); /* ERROR_CTX_WINSTATION_NAME_INVALID */
			s_ErrorMessage.Add(7002, "Ctx invalid PD"); /* ERROR_CTX_INVALID_PD */
			s_ErrorMessage.Add(7003, "Ctx PD not found"); /* ERROR_CTX_PD_NOT_FOUND */
			s_ErrorMessage.Add(7004, "Ctx WD not found"); /* ERROR_CTX_WD_NOT_FOUND */
			s_ErrorMessage.Add(7005, "Ctx cannot make eventlog entry"); /* ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY */
			s_ErrorMessage.Add(7006, "Ctx service name collision"); /* ERROR_CTX_SERVICE_NAME_COLLISION */
			s_ErrorMessage.Add(7007, "Ctx close pending"); /* ERROR_CTX_CLOSE_PENDING */
			s_ErrorMessage.Add(7008, "Ctx no outbuf"); /* ERROR_CTX_NO_OUTBUF */
			s_ErrorMessage.Add(7009, "Ctx modem inf not found"); /* ERROR_CTX_MODEM_INF_NOT_FOUND */
			s_ErrorMessage.Add(7010, "Ctx invalid modemname"); /* ERROR_CTX_INVALID_MODEMNAME */
			s_ErrorMessage.Add(7011, "Ctx modem response error"); /* ERROR_CTX_MODEM_RESPONSE_ERROR */
			s_ErrorMessage.Add(7012, "Ctx modem response timeout"); /* ERROR_CTX_MODEM_RESPONSE_TIMEOUT */
			s_ErrorMessage.Add(7013, "Ctx modem response no carrier"); /* ERROR_CTX_MODEM_RESPONSE_NO_CARRIER */
			s_ErrorMessage.Add(7014, "Ctx modem response no dial tone"); /* ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE */
			s_ErrorMessage.Add(7015, "Ctx modem response busy"); /* ERROR_CTX_MODEM_RESPONSE_BUSY */
			s_ErrorMessage.Add(7016, "Ctx modem response voice"); /* ERROR_CTX_MODEM_RESPONSE_VOICE */
			s_ErrorMessage.Add(7017, "Ctx TD error"); /* ERROR_CTX_TD_ERROR */
			s_ErrorMessage.Add(7022, "Ctx winstation not found"); /* ERROR_CTX_WINSTATION_NOT_FOUND */
			s_ErrorMessage.Add(7023, "Ctx winstation already exists"); /* ERROR_CTX_WINSTATION_ALREADY_EXISTS */
			s_ErrorMessage.Add(7024, "Ctx winstation busy"); /* ERROR_CTX_WINSTATION_BUSY */
			s_ErrorMessage.Add(7025, "Ctx bad video mode"); /* ERROR_CTX_BAD_VIDEO_MODE */
			s_ErrorMessage.Add(7035, "Ctx graphics invalid"); /* ERROR_CTX_GRAPHICS_INVALID */
			s_ErrorMessage.Add(7037, "Ctx logon disabled"); /* ERROR_CTX_LOGON_DISABLED */
			s_ErrorMessage.Add(7038, "Ctx not console"); /* ERROR_CTX_NOT_CONSOLE */
			s_ErrorMessage.Add(7040, "Ctx client query timeout"); /* ERROR_CTX_CLIENT_QUERY_TIMEOUT */
			s_ErrorMessage.Add(7041, "Ctx console disconnect"); /* ERROR_CTX_CONSOLE_DISCONNECT */
			s_ErrorMessage.Add(7042, "Ctx console connect"); /* ERROR_CTX_CONSOLE_CONNECT */
			s_ErrorMessage.Add(7044, "Ctx shadow denied"); /* ERROR_CTX_SHADOW_DENIED */
			s_ErrorMessage.Add(7045, "Ctx winstation access denied"); /* ERROR_CTX_WINSTATION_ACCESS_DENIED */
			s_ErrorMessage.Add(7049, "Ctx invalid WD"); /* ERROR_CTX_INVALID_WD */
			s_ErrorMessage.Add(7050, "Ctx shadow invalid"); /* ERROR_CTX_SHADOW_INVALID */
			s_ErrorMessage.Add(7051, "Ctx shadow disabled"); /* ERROR_CTX_SHADOW_DISABLED */
			s_ErrorMessage.Add(7052, "Ctx client licence in use"); /* ERROR_CTX_CLIENT_LICENSE_IN_USE */
			s_ErrorMessage.Add(7053, "Ctx client licence not set"); /* ERROR_CTX_CLIENT_LICENSE_NOT_SET */
			s_ErrorMessage.Add(7054, "Ctx licence not available"); /* ERROR_CTX_LICENSE_NOT_AVAILABLE */
			s_ErrorMessage.Add(7055, "Ctx licence client invalid"); /* ERROR_CTX_LICENSE_CLIENT_INVALID */
			s_ErrorMessage.Add(7056, "Ctx licence expired"); /* ERROR_CTX_LICENSE_EXPIRED */
			s_ErrorMessage.Add(7057, "Ctx shadow not running"); /* ERROR_CTX_SHADOW_NOT_RUNNING */
			s_ErrorMessage.Add(7058, "Ctx shadow ended by mode change"); /* ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE */
			s_ErrorMessage.Add(8001, "FRS err invalid API sequence"); /* FRS_ERR_INVALID_API_SEQUENCE */
			s_ErrorMessage.Add(8002, "FRS err starting service"); /* FRS_ERR_STARTING_SERVICE */
			s_ErrorMessage.Add(8003, "FRS err stopping service"); /* FRS_ERR_STOPPING_SERVICE */
			s_ErrorMessage.Add(8004, "FRS err internal API"); /* FRS_ERR_INTERNAL_API */
			s_ErrorMessage.Add(8005, "FRS err internal"); /* FRS_ERR_INTERNAL */
			s_ErrorMessage.Add(8006, "FRS err service comm"); /* FRS_ERR_SERVICE_COMM */
			s_ErrorMessage.Add(8007, "FRS err insufficient priv"); /* FRS_ERR_INSUFFICIENT_PRIV */
			s_ErrorMessage.Add(8008, "FRS err authentication"); /* FRS_ERR_AUTHENTICATION */
			s_ErrorMessage.Add(8009, "FRS err parent insufficient priv"); /* FRS_ERR_PARENT_INSUFFICIENT_PRIV */
			s_ErrorMessage.Add(8010, "FRS err parent authentication"); /* FRS_ERR_PARENT_AUTHENTICATION */
			s_ErrorMessage.Add(8011, "FRS err child to parent comm"); /* FRS_ERR_CHILD_TO_PARENT_COMM */
			s_ErrorMessage.Add(8012, "FRS err parent to child comm"); /* FRS_ERR_PARENT_TO_CHILD_COMM */
			s_ErrorMessage.Add(8013, "FRS err sysvol populate"); /* FRS_ERR_SYSVOL_POPULATE */
			s_ErrorMessage.Add(8014, "FRS err sysvol populate timeout"); /* FRS_ERR_SYSVOL_POPULATE_TIMEOUT */
			s_ErrorMessage.Add(8015, "FRS err sysvol is busy"); /* FRS_ERR_SYSVOL_IS_BUSY */
			s_ErrorMessage.Add(8016, "FRS err sysvol demote"); /* FRS_ERR_SYSVOL_DEMOTE */
			s_ErrorMessage.Add(8017, "FRS err invalid service parameter"); /* FRS_ERR_INVALID_SERVICE_PARAMETER */
			s_ErrorMessage.Add(8200, "DS not installed"); /* ERROR_DS_NOT_INSTALLED */
			s_ErrorMessage.Add(8201, "DS membership evaluated locally"); /* ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY */
			s_ErrorMessage.Add(8202, "DS no attribute or value"); /* ERROR_DS_NO_ATTRIBUTE_OR_VALUE */
			s_ErrorMessage.Add(8203, "DS invalid attribute syntax"); /* ERROR_DS_INVALID_ATTRIBUTE_SYNTAX */
			s_ErrorMessage.Add(8204, "DS attribute type undefined"); /* ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED */
			s_ErrorMessage.Add(8205, "DS attribute or value exists"); /* ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS */
			s_ErrorMessage.Add(8206, "DS busy"); /* ERROR_DS_BUSY */
			s_ErrorMessage.Add(8207, "DS unavailable"); /* ERROR_DS_UNAVAILABLE */
			s_ErrorMessage.Add(8208, "DS no rids allocated"); /* ERROR_DS_NO_RIDS_ALLOCATED */
			s_ErrorMessage.Add(8209, "DS no more rids"); /* ERROR_DS_NO_MORE_RIDS */
			s_ErrorMessage.Add(8210, "DS incorrect role owner"); /* ERROR_DS_INCORRECT_ROLE_OWNER */
			s_ErrorMessage.Add(8211, "DS ridmgr init error"); /* ERROR_DS_RIDMGR_INIT_ERROR */
			s_ErrorMessage.Add(8212, "DS obj class violation"); /* ERROR_DS_OBJ_CLASS_VIOLATION */
			s_ErrorMessage.Add(8213, "DS can't on non leaf"); /* ERROR_DS_CANT_ON_NON_LEAF */
			s_ErrorMessage.Add(8214, "DS can't on rnd"); /* ERROR_DS_CANT_ON_RDN */
			s_ErrorMessage.Add(8215, "DS can't mod obj class"); /* ERROR_DS_CANT_MOD_OBJ_CLASS */
			s_ErrorMessage.Add(8216, "DS cross dom move error"); /* ERROR_DS_CROSS_DOM_MOVE_ERROR */
			s_ErrorMessage.Add(8217, "DS GC not available"); /* ERROR_DS_GC_NOT_AVAILABLE */
			s_ErrorMessage.Add(8218, "Shared policy"); /* ERROR_SHARED_POLICY */
			s_ErrorMessage.Add(8219, "Policy object not found"); /* ERROR_POLICY_OBJECT_NOT_FOUND */
			s_ErrorMessage.Add(8220, "Policy only in DS"); /* ERROR_POLICY_ONLY_IN_DS */
			s_ErrorMessage.Add(8221, "Promotion active"); /* ERROR_PROMOTION_ACTIVE */
			s_ErrorMessage.Add(8222, "No promotion active"); /* ERROR_NO_PROMOTION_ACTIVE */
			s_ErrorMessage.Add(8224, "DS operations error"); /* ERROR_DS_OPERATIONS_ERROR */
			s_ErrorMessage.Add(8225, "DS protocol error"); /* ERROR_DS_PROTOCOL_ERROR */
			s_ErrorMessage.Add(8226, "DS timelimit exceeded"); /* ERROR_DS_TIMELIMIT_EXCEEDED */
			s_ErrorMessage.Add(8227, "DS sizelimit exceeded"); /* ERROR_DS_SIZELIMIT_EXCEEDED */
			s_ErrorMessage.Add(8228, "DS admin limit exceeded"); /* ERROR_DS_ADMIN_LIMIT_EXCEEDED */
			s_ErrorMessage.Add(8229, "DS compare false"); /* ERROR_DS_COMPARE_FALSE */
			s_ErrorMessage.Add(8230, "DS compare true"); /* ERROR_DS_COMPARE_TRUE */
			s_ErrorMessage.Add(8231, "DS auth method not supported"); /* ERROR_DS_AUTH_METHOD_NOT_SUPPORTED */
			s_ErrorMessage.Add(8232, "DS strong auth required"); /* ERROR_DS_STRONG_AUTH_REQUIRED */
			s_ErrorMessage.Add(8233, "DS inappropriate auth"); /* ERROR_DS_INAPPROPRIATE_AUTH */
			s_ErrorMessage.Add(8234, "DS auth unknown"); /* ERROR_DS_AUTH_UNKNOWN */
			s_ErrorMessage.Add(8235, "DS referral"); /* ERROR_DS_REFERRAL */
			s_ErrorMessage.Add(8236, "DS unavailable crit extension"); /* ERROR_DS_UNAVAILABLE_CRIT_EXTENSION */
			s_ErrorMessage.Add(8237, "DS confidentiality required"); /* ERROR_DS_CONFIDENTIALITY_REQUIRED */
			s_ErrorMessage.Add(8238, "DS inappropriate matching"); /* ERROR_DS_INAPPROPRIATE_MATCHING */
			s_ErrorMessage.Add(8239, "DS constraint violation"); /* ERROR_DS_CONSTRAINT_VIOLATION */
			s_ErrorMessage.Add(8240, "DS no such object"); /* ERROR_DS_NO_SUCH_OBJECT */
			s_ErrorMessage.Add(8241, "DS alias problem"); /* ERROR_DS_ALIAS_PROBLEM */
			s_ErrorMessage.Add(8242, "DS invalid dn syntax"); /* ERROR_DS_INVALID_DN_SYNTAX */
			s_ErrorMessage.Add(8243, "DS is leaf"); /* ERROR_DS_IS_LEAF */
			s_ErrorMessage.Add(8244, "DS alias deref problem"); /* ERROR_DS_ALIAS_DEREF_PROBLEM */
			s_ErrorMessage.Add(8245, "DS unwilling to perform"); /* ERROR_DS_UNWILLING_TO_PERFORM */
			s_ErrorMessage.Add(8246, "DS loop detect"); /* ERROR_DS_LOOP_DETECT */
			s_ErrorMessage.Add(8247, "DS naming violation"); /* ERROR_DS_NAMING_VIOLATION */
			s_ErrorMessage.Add(8248, "DS object results too large"); /* ERROR_DS_OBJECT_RESULTS_TOO_LARGE */
			s_ErrorMessage.Add(8249, "DS affects multiple dsas"); /* ERROR_DS_AFFECTS_MULTIPLE_DSAS */
			s_ErrorMessage.Add(8250, "DS server down"); /* ERROR_DS_SERVER_DOWN */
			s_ErrorMessage.Add(8251, "DS local error"); /* ERROR_DS_LOCAL_ERROR */
			s_ErrorMessage.Add(8252, "DS encoding error"); /* ERROR_DS_ENCODING_ERROR */
			s_ErrorMessage.Add(8253, "DS decoding error"); /* ERROR_DS_DECODING_ERROR */
			s_ErrorMessage.Add(8254, "DS filter unknown"); /* ERROR_DS_FILTER_UNKNOWN */
			s_ErrorMessage.Add(8255, "DS param error"); /* ERROR_DS_PARAM_ERROR */
			s_ErrorMessage.Add(8256, "DS not supported"); /* ERROR_DS_NOT_SUPPORTED */
			s_ErrorMessage.Add(8257, "DS no results returned"); /* ERROR_DS_NO_RESULTS_RETURNED */
			s_ErrorMessage.Add(8258, "DS control not found"); /* ERROR_DS_CONTROL_NOT_FOUND */
			s_ErrorMessage.Add(8259, "DS client loop"); /* ERROR_DS_CLIENT_LOOP */
			s_ErrorMessage.Add(8260, "DS referral limit exceeded"); /* ERROR_DS_REFERRAL_LIMIT_EXCEEDED */
			s_ErrorMessage.Add(8261, "DS sort control missing"); /* ERROR_DS_SORT_CONTROL_MISSING */
			s_ErrorMessage.Add(8262, "DS offset range error"); /* ERROR_DS_OFFSET_RANGE_ERROR */
			s_ErrorMessage.Add(8301, "DS root must be nc"); /* ERROR_DS_ROOT_MUST_BE_NC */
			s_ErrorMessage.Add(8302, "DS and replica inhibited"); /* ERROR_DS_ADD_REPLICA_INHIBITED */
			s_ErrorMessage.Add(8303, "DS att not def in schema"); /* ERROR_DS_ATT_NOT_DEF_IN_SCHEMA */
		}

		public static void InitializeErrorMessages12()
		{
			s_ErrorMessage.Add(8304, "DS max obj size exceeded"); /* ERROR_DS_MAX_OBJ_SIZE_EXCEEDED */
			s_ErrorMessage.Add(8305, "DS obj string name exists"); /* ERROR_DS_OBJ_STRING_NAME_EXISTS */
			s_ErrorMessage.Add(8306, "DS no rdn defined in schema"); /* ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA */
			s_ErrorMessage.Add(8307, "DS rdn doesn't match schema"); /* ERROR_DS_RDN_DOESNT_MATCH_SCHEMA */
			s_ErrorMessage.Add(8308, "DS no requested atts found"); /* ERROR_DS_NO_REQUESTED_ATTS_FOUND */
			s_ErrorMessage.Add(8309, "DS user buffer too small"); /* ERROR_DS_USER_BUFFER_TO_SMALL */
			s_ErrorMessage.Add(8310, "DS att is not on obj"); /* ERROR_DS_ATT_IS_NOT_ON_OBJ */
			s_ErrorMessage.Add(8311, "DS illegal mod operation"); /* ERROR_DS_ILLEGAL_MOD_OPERATION */
			s_ErrorMessage.Add(8312, "DS obj too large"); /* ERROR_DS_OBJ_TOO_LARGE */
			s_ErrorMessage.Add(8313, "DS bad instance type"); /* ERROR_DS_BAD_INSTANCE_TYPE */
			s_ErrorMessage.Add(8314, "DS masterdsa required"); /* ERROR_DS_MASTERDSA_REQUIRED */
			s_ErrorMessage.Add(8315, "DS object class required"); /* ERROR_DS_OBJECT_CLASS_REQUIRED */
			s_ErrorMessage.Add(8316, "DS missing required att"); /* ERROR_DS_MISSING_REQUIRED_ATT */
			s_ErrorMessage.Add(8317, "DS att not def for class"); /* ERROR_DS_ATT_NOT_DEF_FOR_CLASS */
			s_ErrorMessage.Add(8318, "DS att already exists"); /* ERROR_DS_ATT_ALREADY_EXISTS */
			s_ErrorMessage.Add(8320, "DS can't add att values"); /* ERROR_DS_CANT_ADD_ATT_VALUES */
			s_ErrorMessage.Add(8321, "DS single value constraint"); /* ERROR_DS_SINGLE_VALUE_CONSTRAINT */
			s_ErrorMessage.Add(8322, "DS range constraint"); /* ERROR_DS_RANGE_CONSTRAINT */
			s_ErrorMessage.Add(8323, "DS att val already exists"); /* ERROR_DS_ATT_VAL_ALREADY_EXISTS */
			s_ErrorMessage.Add(8324, "DS can't rem missing att"); /* ERROR_DS_CANT_REM_MISSING_ATT */
			s_ErrorMessage.Add(8325, "DS can't rem missing att val"); /* ERROR_DS_CANT_REM_MISSING_ATT_VAL */
			s_ErrorMessage.Add(8326, "DS root can't be subref"); /* ERROR_DS_ROOT_CANT_BE_SUBREF */
			s_ErrorMessage.Add(8327, "DS no chaining"); /* ERROR_DS_NO_CHAINING */
			s_ErrorMessage.Add(8328, "DS no chained eval"); /* ERROR_DS_NO_CHAINED_EVAL */
			s_ErrorMessage.Add(8329, "DS no parent object"); /* ERROR_DS_NO_PARENT_OBJECT */
			s_ErrorMessage.Add(8330, "DS parent is an alias"); /* ERROR_DS_PARENT_IS_AN_ALIAS */
			s_ErrorMessage.Add(8331, "DS can't mix master and reps"); /* ERROR_DS_CANT_MIX_MASTER_AND_REPS */
			s_ErrorMessage.Add(8332, "DS children exist"); /* ERROR_DS_CHILDREN_EXIST */
			s_ErrorMessage.Add(8333, "DS obj not found"); /* ERROR_DS_OBJ_NOT_FOUND */
			s_ErrorMessage.Add(8334, "DS aliased obj missing"); /* ERROR_DS_ALIASED_OBJ_MISSING */
			s_ErrorMessage.Add(8335, "DS bad name syntax"); /* ERROR_DS_BAD_NAME_SYNTAX */
			s_ErrorMessage.Add(8336, "DS alias points to alias"); /* ERROR_DS_ALIAS_POINTS_TO_ALIAS */
			s_ErrorMessage.Add(8337, "DS can't redef alias"); /* ERROR_DS_CANT_DEREF_ALIAS */
			s_ErrorMessage.Add(8338, "DS out of scope"); /* ERROR_DS_OUT_OF_SCOPE */
			s_ErrorMessage.Add(8339, "DS object being removed"); /* ERROR_DS_OBJECT_BEING_REMOVED */
			s_ErrorMessage.Add(8340, "DS can't delete dsa obj"); /* ERROR_DS_CANT_DELETE_DSA_OBJ */
			s_ErrorMessage.Add(8341, "DS generic error"); /* ERROR_DS_GENERIC_ERROR */
			s_ErrorMessage.Add(8342, "DS dsa must be int master"); /* ERROR_DS_DSA_MUST_BE_INT_MASTER */
			s_ErrorMessage.Add(8343, "DS class not dsa"); /* ERROR_DS_CLASS_NOT_DSA */
			s_ErrorMessage.Add(8344, "DS insuff access rights"); /* ERROR_DS_INSUFF_ACCESS_RIGHTS */
			s_ErrorMessage.Add(8345, "DS illegal superior"); /* ERROR_DS_ILLEGAL_SUPERIOR */
			s_ErrorMessage.Add(8346, "DS attribute owned by sam"); /* ERROR_DS_ATTRIBUTE_OWNED_BY_SAM */
			s_ErrorMessage.Add(8347, "DS name too many parts"); /* ERROR_DS_NAME_TOO_MANY_PARTS */
			s_ErrorMessage.Add(8348, "DS name too long"); /* ERROR_DS_NAME_TOO_LONG */
			s_ErrorMessage.Add(8349, "DS name value too long"); /* ERROR_DS_NAME_VALUE_TOO_LONG */
			s_ErrorMessage.Add(8350, "DS name unparseable"); /* ERROR_DS_NAME_UNPARSEABLE */
			s_ErrorMessage.Add(8351, "DS name type unknown"); /* ERROR_DS_NAME_TYPE_UNKNOWN */
			s_ErrorMessage.Add(8352, "DS not an object"); /* ERROR_DS_NOT_AN_OBJECT */
			s_ErrorMessage.Add(8353, "DS sec desc too short"); /* ERROR_DS_SEC_DESC_TOO_SHORT */
			s_ErrorMessage.Add(8354, "DS sec desc invalid"); /* ERROR_DS_SEC_DESC_INVALID */
			s_ErrorMessage.Add(8355, "DS no deleted name"); /* ERROR_DS_NO_DELETED_NAME */
			s_ErrorMessage.Add(8356, "DS subref must have parent"); /* ERROR_DS_SUBREF_MUST_HAVE_PARENT */
			s_ErrorMessage.Add(8357, "DS ncname must be nc"); /* ERROR_DS_NCNAME_MUST_BE_NC */
			s_ErrorMessage.Add(8358, "DS can't add system only"); /* ERROR_DS_CANT_ADD_SYSTEM_ONLY */
			s_ErrorMessage.Add(8359, "DS class must be concrete"); /* ERROR_DS_CLASS_MUST_BE_CONCRETE */
			s_ErrorMessage.Add(8360, "DS invalid dmd"); /* ERROR_DS_INVALID_DMD */
			s_ErrorMessage.Add(8361, "DS obj GUID exists"); /* ERROR_DS_OBJ_GUID_EXISTS */
			s_ErrorMessage.Add(8362, "DS not on backlink"); /* ERROR_DS_NOT_ON_BACKLINK */
			s_ErrorMessage.Add(8363, "DS no crossref for nc"); /* ERROR_DS_NO_CROSSREF_FOR_NC */
			s_ErrorMessage.Add(8364, "DS shutting down"); /* ERROR_DS_SHUTTING_DOWN */
			s_ErrorMessage.Add(8365, "DS unknown operation"); /* ERROR_DS_UNKNOWN_OPERATION */
			s_ErrorMessage.Add(8366, "DS invalid role owner"); /* ERROR_DS_INVALID_ROLE_OWNER */
			s_ErrorMessage.Add(8367, "DS couldn't contact fsmo"); /* ERROR_DS_COULDNT_CONTACT_FSMO */
			s_ErrorMessage.Add(8368, "DS cross nc dn rename"); /* ERROR_DS_CROSS_NC_DN_RENAME */
			s_ErrorMessage.Add(8369, "DS can't mod system only"); /* ERROR_DS_CANT_MOD_SYSTEM_ONLY */
			s_ErrorMessage.Add(8370, "DS replicator only"); /* ERROR_DS_REPLICATOR_ONLY */
			s_ErrorMessage.Add(8371, "DS obj class not defined"); /* ERROR_DS_OBJ_CLASS_NOT_DEFINED */
			s_ErrorMessage.Add(8372, "DS obj class not subclass"); /* ERROR_DS_OBJ_CLASS_NOT_SUBCLASS */
			s_ErrorMessage.Add(8373, "DS name reference invalid"); /* ERROR_DS_NAME_REFERENCE_INVALID */
			s_ErrorMessage.Add(8374, "DS cross ref exists"); /* ERROR_DS_CROSS_REF_EXISTS */
			s_ErrorMessage.Add(8375, "DS can't del master crossref"); /* ERROR_DS_CANT_DEL_MASTER_CROSSREF */
			s_ErrorMessage.Add(8376, "DS subtree notify not nc head"); /* ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD */
			s_ErrorMessage.Add(8377, "DS notify filter too complex"); /* ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX */
			s_ErrorMessage.Add(8378, "DS dup rdn"); /* ERROR_DS_DUP_RDN */
			s_ErrorMessage.Add(8379, "DS dup oid"); /* ERROR_DS_DUP_OID */
			s_ErrorMessage.Add(8380, "DS dup mapi ID"); /* ERROR_DS_DUP_MAPI_ID */
			s_ErrorMessage.Add(8381, "DS dup schema ID GUID"); /* ERROR_DS_DUP_SCHEMA_ID_GUID */
			s_ErrorMessage.Add(8382, "DS dup LDAP display name"); /* ERROR_DS_DUP_LDAP_DISPLAY_NAME */
			s_ErrorMessage.Add(8383, "DS semantic att test"); /* ERROR_DS_SEMANTIC_ATT_TEST */
			s_ErrorMessage.Add(8384, "DS syntax mismatch"); /* ERROR_DS_SYNTAX_MISMATCH */
			s_ErrorMessage.Add(8385, "DS exists in must have"); /* ERROR_DS_EXISTS_IN_MUST_HAVE */
			s_ErrorMessage.Add(8386, "DS exists in may have"); /* ERROR_DS_EXISTS_IN_MAY_HAVE */
			s_ErrorMessage.Add(8387, "DS nonexistent may have"); /* ERROR_DS_NONEXISTENT_MAY_HAVE */
			s_ErrorMessage.Add(8388, "DS nonexistent must have"); /* ERROR_DS_NONEXISTENT_MUST_HAVE */
			s_ErrorMessage.Add(8389, "DS aux cls test fail"); /* ERROR_DS_AUX_CLS_TEST_FAIL */
			s_ErrorMessage.Add(8390, "DS nonexistent poss sup"); /* ERROR_DS_NONEXISTENT_POSS_SUP */
			s_ErrorMessage.Add(8391, "DS sub cls test fail"); /* ERROR_DS_SUB_CLS_TEST_FAIL */
			s_ErrorMessage.Add(8392, "DS bad rdn att ID syntax"); /* ERROR_DS_BAD_RDN_ATT_ID_SYNTAX */
			s_ErrorMessage.Add(8393, "DS exists in aux cls"); /* ERROR_DS_EXISTS_IN_AUX_CLS */
			s_ErrorMessage.Add(8394, "DS exists in sub cls"); /* ERROR_DS_EXISTS_IN_SUB_CLS */
			s_ErrorMessage.Add(8395, "DS exists in poss sup"); /* ERROR_DS_EXISTS_IN_POSS_SUP */
			s_ErrorMessage.Add(8396, "DS recalcschema failed"); /* ERROR_DS_RECALCSCHEMA_FAILED */
			s_ErrorMessage.Add(8397, "DS tree delete not finished"); /* ERROR_DS_TREE_DELETE_NOT_FINISHED */
			s_ErrorMessage.Add(8398, "DS can't delete"); /* ERROR_DS_CANT_DELETE */
			s_ErrorMessage.Add(8399, "DS att schema req ID"); /* ERROR_DS_ATT_SCHEMA_REQ_ID */
			s_ErrorMessage.Add(8400, "DS bad att schema syntax"); /* ERROR_DS_BAD_ATT_SCHEMA_SYNTAX */
			s_ErrorMessage.Add(8401, "DS can't cache att"); /* ERROR_DS_CANT_CACHE_ATT */
			s_ErrorMessage.Add(8402, "DS can't cache class"); /* ERROR_DS_CANT_CACHE_CLASS */
			s_ErrorMessage.Add(8403, "DS can't remove att cache"); /* ERROR_DS_CANT_REMOVE_ATT_CACHE */
			s_ErrorMessage.Add(8404, "DS can't remove class cache"); /* ERROR_DS_CANT_REMOVE_CLASS_CACHE */
			s_ErrorMessage.Add(8405, "DS can't retrieve DN"); /* ERROR_DS_CANT_RETRIEVE_DN */
			s_ErrorMessage.Add(8406, "DS missing supref"); /* ERROR_DS_MISSING_SUPREF */
			s_ErrorMessage.Add(8407, "DS can't retrieve instance"); /* ERROR_DS_CANT_RETRIEVE_INSTANCE */
			s_ErrorMessage.Add(8408, "DS code inconsistency"); /* ERROR_DS_CODE_INCONSISTENCY */
			s_ErrorMessage.Add(8409, "DS database error"); /* ERROR_DS_DATABASE_ERROR */
			s_ErrorMessage.Add(8410, "DS governsid missing"); /* ERROR_DS_GOVERNSID_MISSING */
			s_ErrorMessage.Add(8411, "DS missing expected att"); /* ERROR_DS_MISSING_EXPECTED_ATT */
			s_ErrorMessage.Add(8412, "DS ncname missing cr ref"); /* ERROR_DS_NCNAME_MISSING_CR_REF */
			s_ErrorMessage.Add(8413, "DS security checking error"); /* ERROR_DS_SECURITY_CHECKING_ERROR */
			s_ErrorMessage.Add(8414, "DS schema not loaded"); /* ERROR_DS_SCHEMA_NOT_LOADED */
			s_ErrorMessage.Add(8415, "DS schema alloc failed"); /* ERROR_DS_SCHEMA_ALLOC_FAILED */
			s_ErrorMessage.Add(8416, "DS att schema req syntax"); /* ERROR_DS_ATT_SCHEMA_REQ_SYNTAX */
			s_ErrorMessage.Add(8417, "DS gcverify error"); /* ERROR_DS_GCVERIFY_ERROR */
			s_ErrorMessage.Add(8418, "DS dra schema mismatch"); /* ERROR_DS_DRA_SCHEMA_MISMATCH */
			s_ErrorMessage.Add(8419, "DS can't find dsa obj"); /* ERROR_DS_CANT_FIND_DSA_OBJ */
			s_ErrorMessage.Add(8420, "DS can't find expected nc"); /* ERROR_DS_CANT_FIND_EXPECTED_NC */
			s_ErrorMessage.Add(8421, "DS can't find nc in cache"); /* ERROR_DS_CANT_FIND_NC_IN_CACHE */
			s_ErrorMessage.Add(8422, "DS can't retrieve child"); /* ERROR_DS_CANT_RETRIEVE_CHILD */
			s_ErrorMessage.Add(8423, "DS security illegal modify"); /* ERROR_DS_SECURITY_ILLEGAL_MODIFY */
			s_ErrorMessage.Add(8424, "DS can't replace hidden rec"); /* ERROR_DS_CANT_REPLACE_HIDDEN_REC */
			s_ErrorMessage.Add(8425, "DS bad hierarchy file"); /* ERROR_DS_BAD_HIERARCHY_FILE */
			s_ErrorMessage.Add(8426, "DS build hierarchy table failed"); /* ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED */
			s_ErrorMessage.Add(8427, "DS config param missing"); /* ERROR_DS_CONFIG_PARAM_MISSING */
			s_ErrorMessage.Add(8428, "DS counting ab indices failed"); /* ERROR_DS_COUNTING_AB_INDICES_FAILED */
			s_ErrorMessage.Add(8429, "DS hierarchy table malloc failed"); /* ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED */
			s_ErrorMessage.Add(8430, "DS internal failure"); /* ERROR_DS_INTERNAL_FAILURE */
			s_ErrorMessage.Add(8431, "DS unknown error"); /* ERROR_DS_UNKNOWN_ERROR */
			s_ErrorMessage.Add(8432, "DS root requires class top"); /* ERROR_DS_ROOT_REQUIRES_CLASS_TOP */
			s_ErrorMessage.Add(8433, "DS refusing fmso roles"); /* ERROR_DS_REFUSING_FSMO_ROLES */
			s_ErrorMessage.Add(8434, "DS missing fmso settings"); /* ERROR_DS_MISSING_FSMO_SETTINGS */
			s_ErrorMessage.Add(8435, "DS unable to surrender roles"); /* ERROR_DS_UNABLE_TO_SURRENDER_ROLES */
		}

		public static void InitializeErrorMessages13()
		{
			s_ErrorMessage.Add(8436, "DS dra generic"); /* ERROR_DS_DRA_GENERIC */
			s_ErrorMessage.Add(8437, "DS dra invalid parameter"); /* ERROR_DS_DRA_INVALID_PARAMETER */
			s_ErrorMessage.Add(8438, "DS dra busy"); /* ERROR_DS_DRA_BUSY */
			s_ErrorMessage.Add(8439, "DS dra bad dn"); /* ERROR_DS_DRA_BAD_DN */
			s_ErrorMessage.Add(8440, "DS dra bad nc"); /* ERROR_DS_DRA_BAD_NC */
			s_ErrorMessage.Add(8441, "DS dra dn exists"); /* ERROR_DS_DRA_DN_EXISTS */
			s_ErrorMessage.Add(8442, "DS dra internal error"); /* ERROR_DS_DRA_INTERNAL_ERROR */
			s_ErrorMessage.Add(8443, "DS dra inconsistent dit"); /* ERROR_DS_DRA_INCONSISTENT_DIT */
			s_ErrorMessage.Add(8444, "DS dra connection failed"); /* ERROR_DS_DRA_CONNECTION_FAILED */
			s_ErrorMessage.Add(8445, "DS dra bad instance type"); /* ERROR_DS_DRA_BAD_INSTANCE_TYPE */
			s_ErrorMessage.Add(8446, "DS dra out of mem"); /* ERROR_DS_DRA_OUT_OF_MEM */
			s_ErrorMessage.Add(8447, "DS dra mail problem"); /* ERROR_DS_DRA_MAIL_PROBLEM */
			s_ErrorMessage.Add(8448, "DS dra ref already exists"); /* ERROR_DS_DRA_REF_ALREADY_EXISTS */
			s_ErrorMessage.Add(8449, "DS dra ref not found"); /* ERROR_DS_DRA_REF_NOT_FOUND */
			s_ErrorMessage.Add(8450, "DS dra obj is rep source"); /* ERROR_DS_DRA_OBJ_IS_REP_SOURCE */
			s_ErrorMessage.Add(8451, "DS dra db error"); /* ERROR_DS_DRA_DB_ERROR */
			s_ErrorMessage.Add(8452, "DS dra no replica"); /* ERROR_DS_DRA_NO_REPLICA */
			s_ErrorMessage.Add(8453, "DS dra access denied"); /* ERROR_DS_DRA_ACCESS_DENIED */
			s_ErrorMessage.Add(8454, "DS dra not supported"); /* ERROR_DS_DRA_NOT_SUPPORTED */
			s_ErrorMessage.Add(8455, "DS dra RPC cancelled"); /* ERROR_DS_DRA_RPC_CANCELLED */
			s_ErrorMessage.Add(8456, "DS dra source disabled"); /* ERROR_DS_DRA_SOURCE_DISABLED */
			s_ErrorMessage.Add(8457, "DS dra sink disabled"); /* ERROR_DS_DRA_SINK_DISABLED */
			s_ErrorMessage.Add(8458, "DS dra name collision"); /* ERROR_DS_DRA_NAME_COLLISION */
			s_ErrorMessage.Add(8459, "DS dra source reinstalled"); /* ERROR_DS_DRA_SOURCE_REINSTALLED */
			s_ErrorMessage.Add(8460, "DS dra missing parent"); /* ERROR_DS_DRA_MISSING_PARENT */
			s_ErrorMessage.Add(8461, "DS dra preempted"); /* ERROR_DS_DRA_PREEMPTED */
			s_ErrorMessage.Add(8462, "DS dra abandon sync"); /* ERROR_DS_DRA_ABANDON_SYNC */
			s_ErrorMessage.Add(8463, "DS dra shutdown"); /* ERROR_DS_DRA_SHUTDOWN */
			s_ErrorMessage.Add(8464, "DS dra incompatible partial set"); /* ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET */
			s_ErrorMessage.Add(8465, "DS dra source is partial replica"); /* ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA */
			s_ErrorMessage.Add(8466, "DS dra extn connection failed"); /* ERROR_DS_DRA_EXTN_CONNECTION_FAILED */
			s_ErrorMessage.Add(8467, "DS install schema mismatch"); /* ERROR_DS_INSTALL_SCHEMA_MISMATCH */
			s_ErrorMessage.Add(8468, "DS dup link ID"); /* ERROR_DS_DUP_LINK_ID */
			s_ErrorMessage.Add(8469, "DS name error resolving"); /* ERROR_DS_NAME_ERROR_RESOLVING */
			s_ErrorMessage.Add(8470, "DS name error not found"); /* ERROR_DS_NAME_ERROR_NOT_FOUND */
			s_ErrorMessage.Add(8471, "DS name error not unique"); /* ERROR_DS_NAME_ERROR_NOT_UNIQUE */
			s_ErrorMessage.Add(8472, "DS name error no mapping"); /* ERROR_DS_NAME_ERROR_NO_MAPPING */
			s_ErrorMessage.Add(8473, "DS name error domain only"); /* ERROR_DS_NAME_ERROR_DOMAIN_ONLY */
			s_ErrorMessage.Add(8474, "DS name error no syntactical mapping"); /* ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING */
			s_ErrorMessage.Add(8475, "DS constructed att mod"); /* ERROR_DS_CONSTRUCTED_ATT_MOD */
			s_ErrorMessage.Add(8476, "DS wrong om obj class"); /* ERROR_DS_WRONG_OM_OBJ_CLASS */
			s_ErrorMessage.Add(8477, "DS dra repl pending"); /* ERROR_DS_DRA_REPL_PENDING */
			s_ErrorMessage.Add(8478, "DS ds required"); /* ERROR_DS_DS_REQUIRED */
			s_ErrorMessage.Add(8479, "DS invalid LDAP display name"); /* ERROR_DS_INVALID_LDAP_DISPLAY_NAME */
			s_ErrorMessage.Add(8480, "DS non base search"); /* ERROR_DS_NON_BASE_SEARCH */
			s_ErrorMessage.Add(8481, "DS can't retrieve atts"); /* ERROR_DS_CANT_RETRIEVE_ATTS */
			s_ErrorMessage.Add(8482, "DS backlink without link"); /* ERROR_DS_BACKLINK_WITHOUT_LINK */
			s_ErrorMessage.Add(8483, "DS epoch mismatch"); /* ERROR_DS_EPOCH_MISMATCH */
			s_ErrorMessage.Add(8484, "DS src name mismatch"); /* ERROR_DS_SRC_NAME_MISMATCH */
			s_ErrorMessage.Add(8485, "DS src and dst nc identical"); /* ERROR_DS_SRC_AND_DST_NC_IDENTICAL */
			s_ErrorMessage.Add(8486, "DS dst nc mismatch"); /* ERROR_DS_DST_NC_MISMATCH */
			s_ErrorMessage.Add(8487, "DS not authoritive for dst nc"); /* ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC */
			s_ErrorMessage.Add(8488, "DS src GUID mismatch"); /* ERROR_DS_SRC_GUID_MISMATCH */
			s_ErrorMessage.Add(8489, "DS can't move deleted object"); /* ERROR_DS_CANT_MOVE_DELETED_OBJECT */
			s_ErrorMessage.Add(8490, "DS pdc operation in progress"); /* ERROR_DS_PDC_OPERATION_IN_PROGRESS */
			s_ErrorMessage.Add(8491, "DS cross domain cleanup reqd"); /* ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD */
			s_ErrorMessage.Add(8492, "DS illegal xdom move operation"); /* ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION */
			s_ErrorMessage.Add(8493, "DS can't with acct group membershps"); /* ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS */
			s_ErrorMessage.Add(8494, "DS nc must have nc parent"); /* ERROR_DS_NC_MUST_HAVE_NC_PARENT */
			s_ErrorMessage.Add(8496, "DS dst domain not native"); /* ERROR_DS_DST_DOMAIN_NOT_NATIVE */
			s_ErrorMessage.Add(8497, "DS missing infrastructure container"); /* ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER */
			s_ErrorMessage.Add(8498, "DS can't move account group"); /* ERROR_DS_CANT_MOVE_ACCOUNT_GROUP */
			s_ErrorMessage.Add(8499, "DS can't move resource group"); /* ERROR_DS_CANT_MOVE_RESOURCE_GROUP */
			s_ErrorMessage.Add(8500, "DS invalid search flag"); /* ERROR_DS_INVALID_SEARCH_FLAG */
			s_ErrorMessage.Add(8501, "DS no tree delete above nc"); /* ERROR_DS_NO_TREE_DELETE_ABOVE_NC */
			s_ErrorMessage.Add(8502, "DS couldn't lock tree for delete"); /* ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE */
			s_ErrorMessage.Add(8503, "DS couldn't identify objects for tree delete"); /* ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE */
			s_ErrorMessage.Add(8504, "DS sam init failure"); /* ERROR_DS_SAM_INIT_FAILURE */
			s_ErrorMessage.Add(8505, "DS sensitive group violation"); /* ERROR_DS_SENSITIVE_GROUP_VIOLATION */
			s_ErrorMessage.Add(8506, "DS can't mod primarygroupid"); /* ERROR_DS_CANT_MOD_PRIMARYGROUPID */
			s_ErrorMessage.Add(8507, "DS illegal base schema mod"); /* ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD */
			s_ErrorMessage.Add(8508, "DS nonsafe schema change"); /* ERROR_DS_NONSAFE_SCHEMA_CHANGE */
			s_ErrorMessage.Add(8509, "DS schema update disallowed"); /* ERROR_DS_SCHEMA_UPDATE_DISALLOWED */
			s_ErrorMessage.Add(8510, "DS can't create under schema"); /* ERROR_DS_CANT_CREATE_UNDER_SCHEMA */
			s_ErrorMessage.Add(8511, "DS install no src sch version"); /* ERROR_DS_INSTALL_NO_SRC_SCH_VERSION */
			s_ErrorMessage.Add(8512, "DS install no sch version in inifile"); /* ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE */
			s_ErrorMessage.Add(8513, "DS invalid group type"); /* ERROR_DS_INVALID_GROUP_TYPE */
			s_ErrorMessage.Add(8514, "DS no nest globalgroup in mixeddomain"); /* ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN */
			s_ErrorMessage.Add(8515, "DS no nest localgroup in mixeddomain"); /* ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN */
			s_ErrorMessage.Add(8516, "DS global can't have local member"); /* ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER */
			s_ErrorMessage.Add(8517, "DS global can't have universal member"); /* ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER */
			s_ErrorMessage.Add(8518, "DS universal can't have local member"); /* ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER */
			s_ErrorMessage.Add(8519, "DS global can't have crossdomain member"); /* ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER */
			s_ErrorMessage.Add(8520, "DS local can't have crossdomain local member"); /* ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER */
			s_ErrorMessage.Add(8521, "DS have primary members"); /* ERROR_DS_HAVE_PRIMARY_MEMBERS */
			s_ErrorMessage.Add(8522, "DS string sd conversion failed"); /* ERROR_DS_STRING_SD_CONVERSION_FAILED */
			s_ErrorMessage.Add(8523, "DS naming master gc"); /* ERROR_DS_NAMING_MASTER_GC */
			s_ErrorMessage.Add(8524, "DS lookup failure"); /* ERROR_DS_LOOKUP_FAILURE */
			s_ErrorMessage.Add(8525, "DS couldn't update spns"); /* ERROR_DS_COULDNT_UPDATE_SPNS */
			s_ErrorMessage.Add(8526, "DS can't retrieve sd"); /* ERROR_DS_CANT_RETRIEVE_SD */
			s_ErrorMessage.Add(8527, "DS key not unique"); /* ERROR_DS_KEY_NOT_UNIQUE */
			s_ErrorMessage.Add(8528, "DS wrong linked att syntax"); /* ERROR_DS_WRONG_LINKED_ATT_SYNTAX */
			s_ErrorMessage.Add(8529, "DS sam need bootkey password"); /* ERROR_DS_SAM_NEED_BOOTKEY_PASSWORD */
			s_ErrorMessage.Add(8530, "DS sam need bootkey floppy"); /* ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY */
			s_ErrorMessage.Add(8531, "DS can't start"); /* ERROR_DS_CANT_START */
			s_ErrorMessage.Add(8532, "DS init failure"); /* ERROR_DS_INIT_FAILURE */
			s_ErrorMessage.Add(8533, "DS no pkt privacy on connection"); /* ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION */
			s_ErrorMessage.Add(8534, "DS source domain in forest"); /* ERROR_DS_SOURCE_DOMAIN_IN_FOREST */
			s_ErrorMessage.Add(8535, "DS destination domain not in forest"); /* ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST */
			s_ErrorMessage.Add(8536, "DS destination auditing not enabled"); /* ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED */
			s_ErrorMessage.Add(8537, "DS can't find dc for src domain"); /* ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN */
			s_ErrorMessage.Add(8538, "DS src obj not group or user"); /* ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER */
			s_ErrorMessage.Add(8539, "DS src sid exists in forest"); /* ERROR_DS_SRC_SID_EXISTS_IN_FOREST */
			s_ErrorMessage.Add(8540, "DS src and dst object class mismatch"); /* ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH */
			s_ErrorMessage.Add(8541, "Sam init failure"); /* ERROR_SAM_INIT_FAILURE */
			s_ErrorMessage.Add(8542, "DS dra schema info ship"); /* ERROR_DS_DRA_SCHEMA_INFO_SHIP */
			s_ErrorMessage.Add(8543, "DS dra schema conflict"); /* ERROR_DS_DRA_SCHEMA_CONFLICT */
			s_ErrorMessage.Add(8544, "DS dra earlier schema conflict"); /* ERROR_DS_DRA_EARLIER_SCHEMA_CONLICT */
			s_ErrorMessage.Add(8545, "DS dra obj nc mismatch"); /* ERROR_DS_DRA_OBJ_NC_MISMATCH */
			s_ErrorMessage.Add(8546, "DS nc still has dsas"); /* ERROR_DS_NC_STILL_HAS_DSAS */
			s_ErrorMessage.Add(8547, "DS gc required"); /* ERROR_DS_GC_REQUIRED */
			s_ErrorMessage.Add(8548, "DS local member of local only"); /* ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY */
			s_ErrorMessage.Add(8549, "DS no fpo in universal groups"); /* ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS */
			s_ErrorMessage.Add(8550, "DS can't add to gc"); /* ERROR_DS_CANT_ADD_TO_GC */
			s_ErrorMessage.Add(8551, "DS no checkpoint with pdc"); /* ERROR_DS_NO_CHECKPOINT_WITH_PDC */
			s_ErrorMessage.Add(8552, "DS source auditing not enabled"); /* ERROR_DS_SOURCE_AUDITING_NOT_ENABLED */
			s_ErrorMessage.Add(8553, "DS can't create in nondomain nc"); /* ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC */
			s_ErrorMessage.Add(8554, "DS invalid name for spn"); /* ERROR_DS_INVALID_NAME_FOR_SPN */
			s_ErrorMessage.Add(8555, "DS filter uses constructed attrs"); /* ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS */
			s_ErrorMessage.Add(8556, "DS unicodepwd not in quotes"); /* ERROR_DS_UNICODEPWD_NOT_IN_QUOTES */
			s_ErrorMessage.Add(8557, "DS machine account quota exceeded"); /* ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED */
			s_ErrorMessage.Add(8558, "DS must be run on dst dc"); /* ERROR_DS_MUST_BE_RUN_ON_DST_DC */
			s_ErrorMessage.Add(8559, "DS src dc must be sp4 or greater"); /* ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER */
			s_ErrorMessage.Add(8560, "DS can't tree delete critical obj"); /* ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ */
			s_ErrorMessage.Add(8561, "DS init failure console"); /* ERROR_DS_INIT_FAILURE_CONSOLE */
			s_ErrorMessage.Add(8562, "DS sam init failure console"); /* ERROR_DS_SAM_INIT_FAILURE_CONSOLE */
			s_ErrorMessage.Add(8563, "DS forest version too high"); /* ERROR_DS_FOREST_VERSION_TOO_HIGH */
			s_ErrorMessage.Add(8564, "DS domain version too high"); /* ERROR_DS_DOMAIN_VERSION_TOO_HIGH */
			s_ErrorMessage.Add(8565, "DS forest version too low"); /* ERROR_DS_FOREST_VERSION_TOO_LOW */
			s_ErrorMessage.Add(8566, "DS domain version too low"); /* ERROR_DS_DOMAIN_VERSION_TOO_LOW */
			s_ErrorMessage.Add(8567, "DS incompatible version"); /* ERROR_DS_INCOMPATIBLE_VERSION */
			s_ErrorMessage.Add(8568, "DS low dsa version"); /* ERROR_DS_LOW_DSA_VERSION */
			s_ErrorMessage.Add(8569, "DS no behaviour version in mixeddomain"); /* ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN */
			s_ErrorMessage.Add(8570, "DS not supported sort order"); /* ERROR_DS_NOT_SUPPORTED_SORT_ORDER */
			s_ErrorMessage.Add(8571, "DS name not unique"); /* ERROR_DS_NAME_NOT_UNIQUE */
			s_ErrorMessage.Add(8572, "DS machine account created prent4"); /* ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 */
			s_ErrorMessage.Add(8573, "DS out of version store"); /* ERROR_DS_OUT_OF_VERSION_STORE */
			s_ErrorMessage.Add(8574, "DS incompatible controls used"); /* ERROR_DS_INCOMPATIBLE_CONTROLS_USED */
			s_ErrorMessage.Add(8575, "DS no ref domain"); /* ERROR_DS_NO_REF_DOMAIN */
			s_ErrorMessage.Add(8576, "DS reserved link ID"); /* ERROR_DS_RESERVED_LINK_ID */
			s_ErrorMessage.Add(8577, "DS link ID not available"); /* ERROR_DS_LINK_ID_NOT_AVAILABLE */
			s_ErrorMessage.Add(8578, "DS ag can't have universal member"); /* ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER */
			s_ErrorMessage.Add(8579, "DS modifydn disallowed by instance type"); /* ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE */
			s_ErrorMessage.Add(8580, "DS no object move in schema nc"); /* ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC */
			s_ErrorMessage.Add(8581, "DS modifydn disallowed by flag"); /* ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG */
			s_ErrorMessage.Add(8582, "DS modifydn wrong grandparent"); /* ERROR_DS_MODIFYDN_WRONG_GRANDPARENT */
			s_ErrorMessage.Add(8583, "DS name error trust referral"); /* ERROR_DS_NAME_ERROR_TRUST_REFERRAL */
			s_ErrorMessage.Add(8584, "Not supported on standard server"); /* ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER */
			s_ErrorMessage.Add(8585, "DS can't access remote part of ad"); /* ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD */
			s_ErrorMessage.Add(8586, "DS cr impossible to validate"); /* ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE */
			s_ErrorMessage.Add(8587, "DS thread limit exceeded"); /* ERROR_DS_THREAD_LIMIT_EXCEEDED */
			s_ErrorMessage.Add(8588, "DS not closest"); /* ERROR_DS_NOT_CLOSEST */
			s_ErrorMessage.Add(8589, "DS can't derive spn without server ref"); /* ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF */
			s_ErrorMessage.Add(8590, "DS single user mode failed"); /* ERROR_DS_SINGLE_USER_MODE_FAILED */
			s_ErrorMessage.Add(8591, "DS ntdscript syntax error"); /* ERROR_DS_NTDSCRIPT_SYNTAX_ERROR */
			s_ErrorMessage.Add(8592, "DS ntdscript process error"); /* ERROR_DS_NTDSCRIPT_PROCESS_ERROR */
			s_ErrorMessage.Add(8593, "DS different repl epochs"); /* ERROR_DS_DIFFERENT_REPL_EPOCHS */
			s_ErrorMessage.Add(8594, "DS drs extensions changed"); /* ERROR_DS_DRS_EXTENSIONS_CHANGED */
			s_ErrorMessage.Add(8595, "DS replica set change not allowed on disabled cr"); /* ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR */
			s_ErrorMessage.Add(8596, "DS no msds intid"); /* ERROR_DS_NO_MSDS_INTID */
			s_ErrorMessage.Add(8597, "DS dup msds intid"); /* ERROR_DS_DUP_MSDS_INTID */
			s_ErrorMessage.Add(8598, "DS exists in rdnattid"); /* ERROR_DS_EXISTS_IN_RDNATTID */
			s_ErrorMessage.Add(8599, "DS authorisation failed"); /* ERROR_DS_AUTHORIZATION_FAILED */
			s_ErrorMessage.Add(8600, "DS invalid script"); /* ERROR_DS_INVALID_SCRIPT */
			s_ErrorMessage.Add(8601, "DS remote crossref op failed"); /* ERROR_DS_REMOTE_CROSSREF_OP_FAILED */
			s_ErrorMessage.Add(9001, "DNS error rcode format error"); /* DNS_ERROR_RCODE_FORMAT_ERROR */
			s_ErrorMessage.Add(9002, "DNS error rcode server failure"); /* DNS_ERROR_RCODE_SERVER_FAILURE */
			s_ErrorMessage.Add(9003, "DNS error rcode name error"); /* DNS_ERROR_RCODE_NAME_ERROR */
			s_ErrorMessage.Add(9004, "DNS error rcode not implemented"); /* DNS_ERROR_RCODE_NOT_IMPLEMENTED */
			s_ErrorMessage.Add(9005, "DNS error rcode refused"); /* DNS_ERROR_RCODE_REFUSED */
			s_ErrorMessage.Add(9006, "DNS error rcode yxdomain"); /* DNS_ERROR_RCODE_YXDOMAIN */
			s_ErrorMessage.Add(9007, "DNS error rcode yxrrset"); /* DNS_ERROR_RCODE_YXRRSET */
			s_ErrorMessage.Add(9008, "DNS error rcode nxrrset"); /* DNS_ERROR_RCODE_NXRRSET */
		}

		public static void InitializeErrorMessages14()
		{
			s_ErrorMessage.Add(9009, "DNS error rcode notauth"); /* DNS_ERROR_RCODE_NOTAUTH */
			s_ErrorMessage.Add(9010, "DNS error rcode notzone"); /* DNS_ERROR_RCODE_NOTZONE */
			s_ErrorMessage.Add(9016, "DNS error rcode badsig"); /* DNS_ERROR_RCODE_BADSIG */
			s_ErrorMessage.Add(9017, "DNS error rcode badkey"); /* DNS_ERROR_RCODE_BADKEY */
			s_ErrorMessage.Add(9018, "DNS error rcode badtime"); /* DNS_ERROR_RCODE_BADTIME */
			s_ErrorMessage.Add(9501, "DNS info no records"); /* DNS_INFO_NO_RECORDS */
			s_ErrorMessage.Add(9502, "DNS error bad packet"); /* DNS_ERROR_BAD_PACKET */
			s_ErrorMessage.Add(9503, "DNS error no packet"); /* DNS_ERROR_NO_PACKET */
			s_ErrorMessage.Add(9504, "DNS error rcode"); /* DNS_ERROR_RCODE */
			s_ErrorMessage.Add(9505, "DNS error unsecure packet"); /* DNS_ERROR_UNSECURE_PACKET */
			s_ErrorMessage.Add(9551, "DNS error invalid type"); /* DNS_ERROR_INVALID_TYPE */
			s_ErrorMessage.Add(9552, "DNS error invalid IP address"); /* DNS_ERROR_INVALID_IP_ADDRESS */
			s_ErrorMessage.Add(9553, "DNS error invalid property"); /* DNS_ERROR_INVALID_PROPERTY */
			s_ErrorMessage.Add(9554, "DNS error try again later"); /* DNS_ERROR_TRY_AGAIN_LATER */
			s_ErrorMessage.Add(9555, "DNS error not unique"); /* DNS_ERROR_NOT_UNIQUE */
			s_ErrorMessage.Add(9556, "DNS error non RFC name"); /* DNS_ERROR_NON_RFC_NAME */
			s_ErrorMessage.Add(9557, "DNS status FQDN"); /* DNS_STATUS_FQDN */
			s_ErrorMessage.Add(9558, "DNS status dotted name"); /* DNS_STATUS_DOTTED_NAME */
			s_ErrorMessage.Add(9559, "DNS status single part name"); /* DNS_STATUS_SINGLE_PART_NAME */
			s_ErrorMessage.Add(9560, "DNS error invalid name char"); /* DNS_ERROR_INVALID_NAME_CHAR */
			s_ErrorMessage.Add(9561, "DNS error numeric name"); /* DNS_ERROR_NUMERIC_NAME */
			s_ErrorMessage.Add(9562, "DNS error not allowed on root server"); /* DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER */
			s_ErrorMessage.Add(9601, "DNS error zone does not exist"); /* DNS_ERROR_ZONE_DOES_NOT_EXIST */
			s_ErrorMessage.Add(9602, "DNS error not zone info"); /* DNS_ERROR_NO_ZONE_INFO */
			s_ErrorMessage.Add(9603, "DNS error invalid zone operation"); /* DNS_ERROR_INVALID_ZONE_OPERATION */
			s_ErrorMessage.Add(9604, "DNS error zone configuration error"); /* DNS_ERROR_ZONE_CONFIGURATION_ERROR */
			s_ErrorMessage.Add(9605, "DNS error zone has not SOA record"); /* DNS_ERROR_ZONE_HAS_NO_SOA_RECORD */
			s_ErrorMessage.Add(9606, "DNS error zone has no NS records"); /* DNS_ERROR_ZONE_HAS_NO_NS_RECORDS */
			s_ErrorMessage.Add(9607, "DNS error zone locked"); /* DNS_ERROR_ZONE_LOCKED */
			s_ErrorMessage.Add(9608, "DNS error zone creation failed"); /* DNS_ERROR_ZONE_CREATION_FAILED */
			s_ErrorMessage.Add(9609, "DNS error zone already exists"); /* DNS_ERROR_ZONE_ALREADY_EXISTS */
			s_ErrorMessage.Add(9610, "DNS error autozone already exists"); /* DNS_ERROR_AUTOZONE_ALREADY_EXISTS */
			s_ErrorMessage.Add(9611, "DNS error invalid zone type"); /* DNS_ERROR_INVALID_ZONE_TYPE */
			s_ErrorMessage.Add(9612, "DNS error secondary requires master IP"); /* DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP */
			s_ErrorMessage.Add(9613, "DNS error zone not secondary"); /* DNS_ERROR_ZONE_NOT_SECONDARY */
			s_ErrorMessage.Add(9614, "DNS error need secondary addresses"); /* DNS_ERROR_NEED_SECONDARY_ADDRESSES */
			s_ErrorMessage.Add(9615, "DNS error wins init failed"); /* DNS_ERROR_WINS_INIT_FAILED */
			s_ErrorMessage.Add(9616, "DNS error need wins servers"); /* DNS_ERROR_NEED_WINS_SERVERS */
			s_ErrorMessage.Add(9617, "DNS error nbstat init failed"); /* DNS_ERROR_NBSTAT_INIT_FAILED */
			s_ErrorMessage.Add(9618, "DNS error SOA delete invalid"); /* DNS_ERROR_SOA_DELETE_INVALID */
			s_ErrorMessage.Add(9619, "DNS error forwarder already exists"); /* DNS_ERROR_FORWARDER_ALREADY_EXISTS */
			s_ErrorMessage.Add(9620, "DNS error zone requires master IP"); /* DNS_ERROR_ZONE_REQUIRES_MASTER_IP */
			s_ErrorMessage.Add(9621, "DNS error zone is shutdown"); /* DNS_ERROR_ZONE_IS_SHUTDOWN */
			s_ErrorMessage.Add(9651, "DNS error primary requires datafile"); /* DNS_ERROR_PRIMARY_REQUIRES_DATAFILE */
			s_ErrorMessage.Add(9652, "DNS error invalid datafile name"); /* DNS_ERROR_INVALID_DATAFILE_NAME */
			s_ErrorMessage.Add(9653, "DNS error datafile open failure"); /* DNS_ERROR_DATAFILE_OPEN_FAILURE */
			s_ErrorMessage.Add(9654, "DNS error file writeback failed"); /* DNS_ERROR_FILE_WRITEBACK_FAILED */
			s_ErrorMessage.Add(9655, "DNS error datafile parsing"); /* DNS_ERROR_DATAFILE_PARSING */
			s_ErrorMessage.Add(9701, "DNS error record does not exist"); /* DNS_ERROR_RECORD_DOES_NOT_EXIST */
			s_ErrorMessage.Add(9702, "DNS error record format"); /* DNS_ERROR_RECORD_FORMAT */
			s_ErrorMessage.Add(9703, "DNS error node creation failed"); /* DNS_ERROR_NODE_CREATION_FAILED */
			s_ErrorMessage.Add(9704, "DNS error unknown record type"); /* DNS_ERROR_UNKNOWN_RECORD_TYPE */
			s_ErrorMessage.Add(9705, "DNS error record timed out"); /* DNS_ERROR_RECORD_TIMED_OUT */
			s_ErrorMessage.Add(9706, "DNS error name not in zone"); /* DNS_ERROR_NAME_NOT_IN_ZONE */
			s_ErrorMessage.Add(9707, "DNS error CNAME loop"); /* DNS_ERROR_CNAME_LOOP */
			s_ErrorMessage.Add(9708, "DNS error node is CNAME"); /* DNS_ERROR_NODE_IS_CNAME */
			s_ErrorMessage.Add(9709, "DNS error CNAME collision"); /* DNS_ERROR_CNAME_COLLISION */
			s_ErrorMessage.Add(9710, "DNS error record only at zone root"); /* DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT */
			s_ErrorMessage.Add(9711, "DNS error record already exists"); /* DNS_ERROR_RECORD_ALREADY_EXISTS */
			s_ErrorMessage.Add(9712, "DNS error secondary data"); /* DNS_ERROR_SECONDARY_DATA */
			s_ErrorMessage.Add(9713, "DNS error no create cache data"); /* DNS_ERROR_NO_CREATE_CACHE_DATA */
			s_ErrorMessage.Add(9714, "DNS error name does not exist"); /* DNS_ERROR_NAME_DOES_NOT_EXIST */
			s_ErrorMessage.Add(9715, "DNS warning PTR create failed"); /* DNS_WARNING_PTR_CREATE_FAILED */
			s_ErrorMessage.Add(9716, "DNS warning domain undeleted"); /* DNS_WARNING_DOMAIN_UNDELETED */
			s_ErrorMessage.Add(9717, "DNS error ds unavailable"); /* DNS_ERROR_DS_UNAVAILABLE */
			s_ErrorMessage.Add(9718, "DNS error ds zone already exists"); /* DNS_ERROR_DS_ZONE_ALREADY_EXISTS */
			s_ErrorMessage.Add(9719, "DNS error no bootfile if ds zone"); /* DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE */
			s_ErrorMessage.Add(9751, "DNS info AXFR complete"); /* DNS_INFO_AXFR_COMPLETE */
			s_ErrorMessage.Add(9752, "DNS error AXFR"); /* DNS_ERROR_AXFR */
			s_ErrorMessage.Add(9753, "DNS info added local wins"); /* DNS_INFO_ADDED_LOCAL_WINS */
			s_ErrorMessage.Add(9801, "DNS status continue needed"); /* DNS_STATUS_CONTINUE_NEEDED */
			s_ErrorMessage.Add(9851, "DNS error no TCPIP"); /* DNS_ERROR_NO_TCPIP */
			s_ErrorMessage.Add(9852, "DNS error no DNS servers"); /* DNS_ERROR_NO_DNS_SERVERS */
			s_ErrorMessage.Add(9901, "DNS error dp does not exist"); /* DNS_ERROR_DP_DOES_NOT_EXIST */
			s_ErrorMessage.Add(9902, "DNS error dp already exists"); /* DNS_ERROR_DP_ALREADY_EXISTS */
			s_ErrorMessage.Add(9903, "DNS error dp not enlisted"); /* DNS_ERROR_DP_NOT_ENLISTED */
			s_ErrorMessage.Add(9904, "DNS error dp already enlisted"); /* DNS_ERROR_DP_ALREADY_ENLISTED */
			s_ErrorMessage.Add(10110, "E_NO_MORE"); /* WSA_E_NO_MORE */
			s_ErrorMessage.Add(10111, "E_CANCELLED"); /* WSA_E_CANCELLED */
			s_ErrorMessage.Add(11005, "QOS receivers"); /* WSA_QOS_RECEIVERS */
			s_ErrorMessage.Add(11006, "QOS senders"); /* WSA_QOS_SENDERS */
			s_ErrorMessage.Add(11007, "QOS no senders"); /* WSA_QOS_NO_SENDERS */
			s_ErrorMessage.Add(11008, "QOS no receivers"); /* WSA_QOS_NO_RECEIVERS */
			s_ErrorMessage.Add(11009, "QOS request confirmed"); /* WSA_QOS_REQUEST_CONFIRMED */
			s_ErrorMessage.Add(11010, "QOS admission failure"); /* WSA_QOS_ADMISSION_FAILURE */
			s_ErrorMessage.Add(11011, "QOS policy failure"); /* WSA_QOS_POLICY_FAILURE */
			s_ErrorMessage.Add(11012, "QOS bad style"); /* WSA_QOS_BAD_STYLE */
			s_ErrorMessage.Add(11013, "QOS bad object"); /* WSA_QOS_BAD_OBJECT */
			s_ErrorMessage.Add(11014, "QOS traffic ctrl error"); /* WSA_QOS_TRAFFIC_CTRL_ERROR */
			s_ErrorMessage.Add(11015, "QOS generic error"); /* WSA_QOS_GENERIC_ERROR */
			s_ErrorMessage.Add(11016, "QOS eservicetype"); /* WSA_QOS_ESERVICETYPE */
			s_ErrorMessage.Add(11017, "QOS eflowspec"); /* WSA_QOS_EFLOWSPEC */
			s_ErrorMessage.Add(11018, "QOS eprovspecbuf"); /* WSA_QOS_EPROVSPECBUF */
			s_ErrorMessage.Add(11019, "QOS efilterstyle"); /* WSA_QOS_EFILTERSTYLE */
			s_ErrorMessage.Add(11020, "QOS efiltertype"); /* WSA_QOS_EFILTERTYPE */
			s_ErrorMessage.Add(11021, "QOS efiltercount"); /* WSA_QOS_EFILTERCOUNT */
			s_ErrorMessage.Add(11022, "QOS eobjlength"); /* WSA_QOS_EOBJLENGTH */
			s_ErrorMessage.Add(11023, "QOS eflowcount"); /* WSA_QOS_EFLOWCOUNT */
			s_ErrorMessage.Add(11024, "QOS eunknownpsobj"); /* WSA_QOS_EUNKNOWNPSOBJ */
			s_ErrorMessage.Add(11025, "QOS epolicyobj"); /* WSA_QOS_EPOLICYOBJ */
			s_ErrorMessage.Add(11026, "QOS eflowdesc"); /* WSA_QOS_EFLOWDESC */
			s_ErrorMessage.Add(11027, "QOS epsflowspec"); /* WSA_QOS_EPSFLOWSPEC */
			s_ErrorMessage.Add(11028, "QOS epsfilterspec"); /* WSA_QOS_EPSFILTERSPEC */
			s_ErrorMessage.Add(11029, "QOS esdmodeobj"); /* WSA_QOS_ESDMODEOBJ */
			s_ErrorMessage.Add(11030, "QOS eshaperateobj"); /* WSA_QOS_ESHAPERATEOBJ */
			s_ErrorMessage.Add(11031, "QOS reserved petype"); /* WSA_QOS_RESERVED_PETYPE */
			s_ErrorMessage.Add(13000, "IPSEC qm policy exists"); /* ERROR_IPSEC_QM_POLICY_EXISTS */
			s_ErrorMessage.Add(13001, "IPSEC qm policy not found"); /* ERROR_IPSEC_QM_POLICY_NOT_FOUND */
			s_ErrorMessage.Add(13002, "IPSEC qm policy in use"); /* ERROR_IPSEC_QM_POLICY_IN_USE */
			s_ErrorMessage.Add(13003, "IPSEC mm policy exists"); /* ERROR_IPSEC_MM_POLICY_EXISTS */
			s_ErrorMessage.Add(13004, "IPSEC mm policy not found"); /* ERROR_IPSEC_MM_POLICY_NOT_FOUND */
			s_ErrorMessage.Add(13005, "IPSEC mm policy in use"); /* ERROR_IPSEC_MM_POLICY_IN_USE */
			s_ErrorMessage.Add(13006, "IPSEC mm filter exists"); /* ERROR_IPSEC_MM_FILTER_EXISTS */
			s_ErrorMessage.Add(13007, "IPSEC mm filter not found"); /* ERROR_IPSEC_MM_FILTER_NOT_FOUND */
			s_ErrorMessage.Add(13008, "IPSEC transport filter exists"); /* ERROR_IPSEC_TRANSPORT_FILTER_EXISTS */
			s_ErrorMessage.Add(13009, "IPSEC transport filter not found"); /* ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND */
			s_ErrorMessage.Add(13010, "IPSEC mm auth exists"); /* ERROR_IPSEC_MM_AUTH_EXISTS */
			s_ErrorMessage.Add(13011, "IPSEC mm auth not found"); /* ERROR_IPSEC_MM_AUTH_NOT_FOUND */
			s_ErrorMessage.Add(13012, "IPSEC mm auth in use"); /* ERROR_IPSEC_MM_AUTH_IN_USE */
			s_ErrorMessage.Add(13013, "IPSEC default mm policy not found"); /* ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND */
			s_ErrorMessage.Add(13014, "IPSEC default mm auth not found"); /* ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND */
			s_ErrorMessage.Add(13015, "IPSEC default qm policy not found"); /* ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND */
			s_ErrorMessage.Add(13016, "IPSEC tunnel filter exists"); /* ERROR_IPSEC_TUNNEL_FILTER_EXISTS */
			s_ErrorMessage.Add(13017, "IPSEC tunnel filter not found"); /* ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND */
			s_ErrorMessage.Add(13018, "IPSEC mm filter pending deletion"); /* ERROR_IPSEC_MM_FILTER_PENDING_DELETION */
			s_ErrorMessage.Add(13019, "IPSEC transport filter pending deletion"); /* ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION */
			s_ErrorMessage.Add(13020, "IPSEC tunnel filter pending deletion"); /* ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION */
			s_ErrorMessage.Add(13021, "IPSEC mm policy pending deletion"); /* ERROR_IPSEC_MM_POLICY_PENDING_DELETION */
			s_ErrorMessage.Add(13022, "IPSEC mm auth pending deletion"); /* ERROR_IPSEC_MM_AUTH_PENDING_DELETION */
			s_ErrorMessage.Add(13023, "IPSEC qm policy pending deletion"); /* ERROR_IPSEC_QM_POLICY_PENDING_DELETION */
			s_ErrorMessage.Add(13801, "IPSEC IKE auth fail"); /* ERROR_IPSEC_IKE_AUTH_FAIL */
			s_ErrorMessage.Add(13802, "IPSEC IKE attrib fail"); /* ERROR_IPSEC_IKE_ATTRIB_FAIL */
			s_ErrorMessage.Add(13803, "IPSEC IKE negotiation pending"); /* ERROR_IPSEC_IKE_NEGOTIATION_PENDING */
			s_ErrorMessage.Add(13804, "IPSEC IKE general processing error"); /* ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR */
			s_ErrorMessage.Add(13805, "IPSEC IKE timed out"); /* ERROR_IPSEC_IKE_TIMED_OUT */
			s_ErrorMessage.Add(13806, "IPSEC IKE no cert"); /* ERROR_IPSEC_IKE_NO_CERT */
			s_ErrorMessage.Add(13807, "IPSEC IKE sa deleted"); /* ERROR_IPSEC_IKE_SA_DELETED */
			s_ErrorMessage.Add(13808, "IPSEC IKE sa reaped"); /* ERROR_IPSEC_IKE_SA_REAPED */
			s_ErrorMessage.Add(13809, "IPSEC IKE mm acquire drop"); /* ERROR_IPSEC_IKE_MM_ACQUIRE_DROP */
			s_ErrorMessage.Add(13810, "IPSEC IKE qm acquire drop"); /* ERROR_IPSEC_IKE_QM_ACQUIRE_DROP */
			s_ErrorMessage.Add(13811, "IPSEC IKE queue drop mm"); /* ERROR_IPSEC_IKE_QUEUE_DROP_MM */
			s_ErrorMessage.Add(13812, "IPSEC IKE queue drop no mm"); /* ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM */
			s_ErrorMessage.Add(13813, "IPSEC IKE drop no response"); /* ERROR_IPSEC_IKE_DROP_NO_RESPONSE */
			s_ErrorMessage.Add(13814, "IPSEC IKE mm delay drop"); /* ERROR_IPSEC_IKE_MM_DELAY_DROP */
			s_ErrorMessage.Add(13815, "IPSEC IKE qm delay drop"); /* ERROR_IPSEC_IKE_QM_DELAY_DROP */
			s_ErrorMessage.Add(13816, "IPSEC IKE error"); /* ERROR_IPSEC_IKE_ERROR */
			s_ErrorMessage.Add(13817, "IPSEC IKE crl failed"); /* ERROR_IPSEC_IKE_CRL_FAILED */
			s_ErrorMessage.Add(13818, "IPSEC IKE invalid key usage"); /* ERROR_IPSEC_IKE_INVALID_KEY_USAGE */
			s_ErrorMessage.Add(13819, "IPSEC IKE invalid cert type"); /* ERROR_IPSEC_IKE_INVALID_CERT_TYPE */
			s_ErrorMessage.Add(13820, "IPSEC IKE no private key"); /* ERROR_IPSEC_IKE_NO_PRIVATE_KEY */
			s_ErrorMessage.Add(13822, "IPSEC IKE dh fail"); /* ERROR_IPSEC_IKE_DH_FAIL */
			s_ErrorMessage.Add(13824, "IPSEC IKE invalid header"); /* ERROR_IPSEC_IKE_INVALID_HEADER */
			s_ErrorMessage.Add(13825, "IPSEC IKE no policy"); /* ERROR_IPSEC_IKE_NO_POLICY */
		}

		public static void InitializeErrorMessages15()
		{
			s_ErrorMessage.Add(13826, "IPSEC IKE invalid signature"); /* ERROR_IPSEC_IKE_INVALID_SIGNATURE */
			s_ErrorMessage.Add(13827, "IPSEC IKE kerberos error"); /* ERROR_IPSEC_IKE_KERBEROS_ERROR */
			s_ErrorMessage.Add(13828, "IPSEC IKE no public key"); /* ERROR_IPSEC_IKE_NO_PUBLIC_KEY */
			s_ErrorMessage.Add(13829, "IPSEC IKE process err"); /* ERROR_IPSEC_IKE_PROCESS_ERR */
			s_ErrorMessage.Add(13830, "IPSEC IKE process err sa"); /* ERROR_IPSEC_IKE_PROCESS_ERR_SA */
			s_ErrorMessage.Add(13831, "IPSEC IKE process err prop"); /* ERROR_IPSEC_IKE_PROCESS_ERR_PROP */
			s_ErrorMessage.Add(13832, "IPSEC IKE process err trans"); /* ERROR_IPSEC_IKE_PROCESS_ERR_TRANS */
			s_ErrorMessage.Add(13833, "IPSEC IKE process err ke"); /* ERROR_IPSEC_IKE_PROCESS_ERR_KE */
			s_ErrorMessage.Add(13834, "IPSEC IKE process err ID"); /* ERROR_IPSEC_IKE_PROCESS_ERR_ID */
			s_ErrorMessage.Add(13835, "IPSEC IKE process err cert"); /* ERROR_IPSEC_IKE_PROCESS_ERR_CERT */
			s_ErrorMessage.Add(13836, "IPSEC IKE process err cert req"); /* ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ */
			s_ErrorMessage.Add(13837, "IPSEC IKE process err hash"); /* ERROR_IPSEC_IKE_PROCESS_ERR_HASH */
			s_ErrorMessage.Add(13838, "IPSEC IKE process err sig"); /* ERROR_IPSEC_IKE_PROCESS_ERR_SIG */
			s_ErrorMessage.Add(13839, "IPSEC IKE process err nonce"); /* ERROR_IPSEC_IKE_PROCESS_ERR_NONCE */
			s_ErrorMessage.Add(13840, "IPSEC IKE process err notify"); /* ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY */
			s_ErrorMessage.Add(13841, "IPSEC IKE process err delete"); /* ERROR_IPSEC_IKE_PROCESS_ERR_DELETE */
			s_ErrorMessage.Add(13842, "IPSEC IKE process err vendor"); /* ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR */
			s_ErrorMessage.Add(13843, "IPSEC IKE invalid payload"); /* ERROR_IPSEC_IKE_INVALID_PAYLOAD */
			s_ErrorMessage.Add(13844, "IPSEC IKE load soft sa"); /* ERROR_IPSEC_IKE_LOAD_SOFT_SA */
			s_ErrorMessage.Add(13845, "IPSEC IKE soft sa torn down"); /* ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN */
			s_ErrorMessage.Add(13846, "IPSEC IKE invalid cookie"); /* ERROR_IPSEC_IKE_INVALID_COOKIE */
			s_ErrorMessage.Add(13847, "IPSEC IKE no peer cert"); /* ERROR_IPSEC_IKE_NO_PEER_CERT */
			s_ErrorMessage.Add(13848, "IPSEC IKE peer CRL failed"); /* ERROR_IPSEC_IKE_PEER_CRL_FAILED */
			s_ErrorMessage.Add(13849, "IPSEC IKE policy change"); /* ERROR_IPSEC_IKE_POLICY_CHANGE */
			s_ErrorMessage.Add(13850, "IPSEC IKE no mm policy"); /* ERROR_IPSEC_IKE_NO_MM_POLICY */
			s_ErrorMessage.Add(13851, "IPSEC IKE notcbpriv"); /* ERROR_IPSEC_IKE_NOTCBPRIV */
			s_ErrorMessage.Add(13852, "IPSEC IKE secloadfail"); /* ERROR_IPSEC_IKE_SECLOADFAIL */
			s_ErrorMessage.Add(13853, "IPSEC IKE failsspinit"); /* ERROR_IPSEC_IKE_FAILSSPINIT */
			s_ErrorMessage.Add(13854, "IPSEC IKE failqueryssp"); /* ERROR_IPSEC_IKE_FAILQUERYSSP */
			s_ErrorMessage.Add(13855, "IPSEC IKE srvacqfail"); /* ERROR_IPSEC_IKE_SRVACQFAIL */
			s_ErrorMessage.Add(13856, "IPSEC IKE srvquerycred"); /* ERROR_IPSEC_IKE_SRVQUERYCRED */
			s_ErrorMessage.Add(13857, "IPSEC IKE getspifail"); /* ERROR_IPSEC_IKE_GETSPIFAIL */
			s_ErrorMessage.Add(13858, "IPSEC IKE invalid filter"); /* ERROR_IPSEC_IKE_INVALID_FILTER */
			s_ErrorMessage.Add(13859, "IPSEC IKE out of memory"); /* ERROR_IPSEC_IKE_OUT_OF_MEMORY */
			s_ErrorMessage.Add(13860, "IPSEC IKE add update key failed"); /* ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED */
			s_ErrorMessage.Add(13861, "IPSEC IKE invalid policy"); /* ERROR_IPSEC_IKE_INVALID_POLICY */
			s_ErrorMessage.Add(13862, "IPSEC IKE unknown doi"); /* ERROR_IPSEC_IKE_UNKNOWN_DOI */
			s_ErrorMessage.Add(13863, "IPSEC IKE invalid situation"); /* ERROR_IPSEC_IKE_INVALID_SITUATION */
			s_ErrorMessage.Add(13864, "IPSEC IKE dh failure"); /* ERROR_IPSEC_IKE_DH_FAILURE */
			s_ErrorMessage.Add(13865, "IPSEC IKE invalid group"); /* ERROR_IPSEC_IKE_INVALID_GROUP */
			s_ErrorMessage.Add(13866, "IPSEC IKE encrypt"); /* ERROR_IPSEC_IKE_ENCRYPT */
			s_ErrorMessage.Add(13867, "IPSEC IKE decrypt"); /* ERROR_IPSEC_IKE_DECRYPT */
			s_ErrorMessage.Add(13868, "IPSEC IKE policy match"); /* ERROR_IPSEC_IKE_POLICY_MATCH */
			s_ErrorMessage.Add(13869, "IPSEC IKE unsupported ID"); /* ERROR_IPSEC_IKE_UNSUPPORTED_ID */
			s_ErrorMessage.Add(13870, "IPSEC IKE invalid hash"); /* ERROR_IPSEC_IKE_INVALID_HASH */
			s_ErrorMessage.Add(13871, "IPSEC IKE invalid hash alg"); /* ERROR_IPSEC_IKE_INVALID_HASH_ALG */
			s_ErrorMessage.Add(13872, "IPSEC IKE invalid hash size"); /* ERROR_IPSEC_IKE_INVALID_HASH_SIZE */
			s_ErrorMessage.Add(13873, "IPSEC IKE invalid encrypt alg"); /* ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG */
			s_ErrorMessage.Add(13874, "IPSEC IKE invalid auth alg"); /* ERROR_IPSEC_IKE_INVALID_AUTH_ALG */
			s_ErrorMessage.Add(13875, "IPSEC IKE invalid sig"); /* ERROR_IPSEC_IKE_INVALID_SIG */
			s_ErrorMessage.Add(13876, "IPSEC IKE load failed"); /* ERROR_IPSEC_IKE_LOAD_FAILED */
			s_ErrorMessage.Add(13877, "IPSEC IKE rpc delete"); /* ERROR_IPSEC_IKE_RPC_DELETE */
			s_ErrorMessage.Add(13878, "IPSEC IKE benign reinit"); /* ERROR_IPSEC_IKE_BENIGN_REINIT */
			s_ErrorMessage.Add(13879, "IPSEC IKE invalid responder lifetime notify"); /* ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY */
			s_ErrorMessage.Add(13881, "IPSEC IKE invalid cert keylen"); /* ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN */
			s_ErrorMessage.Add(13882, "IPSEC IKE mm limit"); /* ERROR_IPSEC_IKE_MM_LIMIT */
			s_ErrorMessage.Add(13883, "IPSEC IKE negotiation disabled"); /* ERROR_IPSEC_IKE_NEGOTIATION_DISABLED */
			s_ErrorMessage.Add(13884, "IPSEC IKE neg status end"); /* ERROR_IPSEC_IKE_NEG_STATUS_END */
			s_ErrorMessage.Add(100001, "Device not configured"); /* WSAENXIO */
		}
#endif // UNITY
	}
}