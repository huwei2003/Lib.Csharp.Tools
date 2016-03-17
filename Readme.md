Lib.Csharp.Tools

FtpHelper case
====== begin ======
#region config
		//<appSettings>
		//<add key="FtpUser" value="ftpuser"/>
		//<add key="FtpPassword" value="pwd2015ftpweb123456"/>
		//<add key="FtpUrl" value="192.168.3.103"/>
		//<add key="SourceFolder" value="C:\HQ\publish\testftpfile"/>
		//<add key="SourceFolderPrex" value="C:\HQ\publish\"/>
		//<add key="DestFolder" value="testftpfile"/>
		//</appSettings>
		
		var uid = System.Configuration.ConfigurationManager.AppSettings["FtpUser"];
		var pwd = System.Configuration.ConfigurationManager.AppSettings["FtpPassword"];
		var ftpUrl = System.Configuration.ConfigurationManager.AppSettings["FtpUrl"];
		var sourceFolder = System.Configuration.ConfigurationManager.AppSettings["SourceFolder"];
		var destFolder = System.Configuration.ConfigurationManager.AppSettings["DestFolder"];
		var sourceFolderPrex = System.Configuration.ConfigurationManager.AppSettings["SourceFolderPrex"];
		#endregion
		
		FtpHelper.CreateDirectory(destFolder, ftpUrl, uid, pwd);
		FtpHelper.UploadFolder(sourceFolderPrex,sourceFolder, destFolder, ftpUrl, uid, pwd);
====== end =====