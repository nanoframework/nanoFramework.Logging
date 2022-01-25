using System;

namespace nanoFramework.Logging.Syslog
{
	/// <summary>
	/// Available facility types (RFC 3164).
	/// </summary>
	public enum Facility
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		Kern = 0,
		User = 1,
		Mail = 2,
		Daemon = 3,
		Auth = 4,
		Syslog = 5,
		LPR = 6,
		News = 7,
		UUCP = 8,
		Cron = 9,
		AuthPriv = 10,
		FTP = 11,
		NTP = 12,
		Audit = 13,
		Audit2 = 14,
		Cron2 = 15,
		Local0 = 16,
		Local1 = 17,
		Local2 = 18,
		Local3 = 19,
		Local4 = 20,
		Local5 = 21,
		Local6 = 22,
		Local7 = 23
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
